using Exo.WebApi.Repository;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;


namespace Exo.WebApi.Contollers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class ProjetosController : ControllerBase
    {
        private readonly ProjetoRepository _ProjetoRepository;

        public ProjetosController(ProjetoRepository _ProjetoRepository)
        {
            _projetoRepository = projetoRepository;
        }
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_ProjetoRepository.Listar());
        }
    }
}