using Microsoft.AspNetCore.Mvc;
using moutarde_back.Infrastructure.Data;

namespace moutarde_back.Features.Temporary;

[ApiController]
[Route("[controller]")]
public class DbTestController(MoutardeDbContext dbContext) : ControllerBase
{
    [HttpGet(Name = "GetDbTest")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var canConnect = await dbContext.Database.CanConnectAsync();
            if (canConnect) return Ok(new { success = true, message = "Database connection successful", name = dbContext.Database.ProviderName });
            return StatusCode(500, new { success = false, message = "Database connection failed"});
        }
        catch (Exception e)
        {
            return StatusCode(500, new { success = false, message = "Database connection failed", error = e.Message });
        }
    }
}