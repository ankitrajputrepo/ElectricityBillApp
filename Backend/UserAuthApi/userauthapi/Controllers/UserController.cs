using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.Data;
namespace userauthapi;

[Route("api/[controller]")]
[ApiController]
public class ConsumersController : ControllerBase
{
    private readonly ConsumerContext _context;

    public ConsumersController(ConsumerContext context)
    {
        _context = context;
    }

    // GET: api/Consumers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Consumer>>> GetConsumers()
    {
        return await _context.Consumers.ToListAsync();
    }

    // GET: api/Consumers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Consumer>> GetConsumer(int id)
    {
        var consumer = await _context.Consumers.FindAsync(id);

        if (consumer == null)
        {
            return NotFound();
        }

        return consumer;
    }

    // POST: api/Consumers
    [HttpPost]
    public async Task<ActionResult<Consumer>> PostConsumer(Consumer consumer)
    {
        _context.Consumers.Add(consumer);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetConsumer), new { id = consumer.ConsumerId }, consumer);
    }

    // PUT: api/Consumers/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutConsumer(int id, Consumer consumer)
    {
        if (id != consumer.ConsumerId)
        {
            return BadRequest();
        }

        _context.Entry(consumer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ConsumerExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Consumers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteConsumer(int id)
    {
        var consumer = await _context.Consumers.FindAsync(id);
        if (consumer == null)
        {
            return NotFound();
        }

        _context.Consumers.Remove(consumer);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ConsumerExists(int id)
    {
        return _context.Consumers.Any(e => e.ConsumerId == id);
    }
    [HttpPost("login")]
    public async Task<ActionResult<ConsumerResponse>> Login([FromBody] LoginRequest request)
    {
        // Validate the request
        if (request == null || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest("Email and password are required.");
        }

        // Look for the consumer by email
        var consumer = await _context.Consumers
            .FirstOrDefaultAsync(c => c.ConsumerEmail == request.Email);

        // Check if the consumer exists and password matches
        if (consumer == null || consumer.ConsumerPassword != request.Password) // Use hashed password for actual implementation
        {
            return Unauthorized("Invalid email or password.");
        }

        // If authentication is successful, return a response
        var response = new ConsumerResponse
        {
            ConsumerId = consumer.ConsumerId,
            Email = consumer.ConsumerEmail,
            Role = consumer.ConsumerEmail == "admin@123" ? "admin" : "user"
        };

        return Ok(response);
    }


}

// Define classes for request and response


public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class ConsumerResponse
{
    public int ConsumerId { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}