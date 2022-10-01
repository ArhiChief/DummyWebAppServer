using Microsoft.AspNetCore.Mvc;

namespace DummyWebAppServer.Controllers;

[ApiController]
[Route("[controller]")]
public class SimpleController : ControllerBase
{
    private const int _size = 1024 * 1024 * 10; // 10 MB
    private static readonly byte[] _mem = new byte[_size];

    static SimpleController()
    {
        var span = new Span<byte>(_mem);
        span.Fill(87); // ASCII or UTF-8 upper case W;
    }

    public SimpleController()
    {
    }

    [HttpGet("{size}")]
    [HttpGet("{size}/{delay}")]
    public async Task<IActionResult> Get(int size, int delay = 0)
    {
        if (size <= 0 || size > _size || delay < 0)
        {
            return BadRequest();
        }

        if (delay > 0)
        {
            await Task.Delay(delay).ConfigureAwait(false);
        }

        var stream = new MemoryStream(_mem, 0, size, false);
        return File(stream, "text/plain");
    }
}