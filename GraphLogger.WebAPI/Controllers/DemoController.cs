using Microsoft.AspNetCore.Mvc;

namespace GraphLogger.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class DemoController : ControllerBase
{
    private readonly ILogger<DemoController> _logger;

    public DemoController(ILogger<DemoController> logger)
    {
        _logger = logger;
    }


    [HttpGet("logger/info")]
    public IActionResult LogInfo()
    {
        _logger.LogInformation("Information log invoked");
        return Ok();
    }

    [HttpGet("logger/error")]
    public IActionResult LogError()
    {
        _logger.LogError("Error log invoked");
        return Ok();
    }

    [HttpGet("logger/warning")]
    public IActionResult LogWarning()
    {
        _logger.LogWarning("Warning log invoked");
        return Ok();
    }

    [HttpGet("exaptation/manually")]
    public IActionResult ManuallyExaptation()
    {
        throw new Exception("Throw exception manually");
    }
}