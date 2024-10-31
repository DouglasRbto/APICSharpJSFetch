using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ControladorAPI: ControllerBase
{
    [HttpGet("dados")]
    public IActionResult GetDados()
    {
        var dados = new List<object>
        {
            new { Id = 1, Nome = "Nome 1", Valor = 1.0 },
            new { Id = 2, Nome = "Nome 2", Valor = 2.0 },
            new { Id = 3, Nome = "Nome 3", Valor = 3.0 },
            new { Id = 4, Nome = "Nome 4", Valor = 4.0 }
        };

        return Ok(dados);
    }
}