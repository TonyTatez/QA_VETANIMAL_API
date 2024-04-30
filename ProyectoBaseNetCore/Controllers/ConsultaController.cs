using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoBaseNetCore.DTOs;
using ProyectoBaseNetCore.Entities;
using ProyectoBaseNetCore.Services;
using VET_ANIMAL_API.DTOs;
using VET_ANIMAL_API.Models;

namespace ProyectoBaseNetCore.Controllers
{
    [ApiController]
    [Route("api/consulta/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ConsultaController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IAuthorizationService authorizationService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected ConsultaServices _service;
        private readonly ApplicationDbContext _context;

        public ConsultaController(ApplicationDbContext context,
            IConfiguration configuration,
            IAuthorizationService Authorization,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            this.authorizationService = Authorization;
            this.configuration = configuration;
            this.userManager = userManager;
            this._httpContextAccessor = httpContextAccessor;
            string userName = Task.Run(async () => await userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "email").Value)).Result.UserName;
            var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
            this._service = new ConsultaServices(_context, configuration, userName, ip);
        }

        [HttpPost("FichaHemoparasitosis")]
        public async Task<IActionResult> CreateFichaHemo(FichaHemoparasitosisDTO Ficha) => Ok(await _service.SaveFichaHemoAsync(Ficha));

        [HttpGet("historial")]
        public async Task<IActionResult> GetHistoriales(long CI) => Ok(await _service.GetAllHitorialAsync(CI));

        [HttpGet("ResultadosHemo")]
        public async Task<IActionResult> GetAllFichaHemoparasitosisAsync() => Ok(await _service.GetAllFichaHemoparasitosisAsync());

        [HttpPost("GuardadoPdf")]
        public async Task<IActionResult> GuardadoTest([FromForm] IFormFile Excel, [FromForm] string idFichaHemo, [FromForm] string resultado) =>Ok(await _service.GuardadoTest(Excel, idFichaHemo, resultado));
        
        [HttpGet("ObtenerPdf/{idFichaHemo}")]
        public async Task<IActionResult> ObtenerPDFPorIdFichaHemo(string idFichaHemo) => Ok(await _service.ObtenerPDFPorIdFichaHemo(idFichaHemo));

        [HttpGet("GetAllResultados")]
        public async Task<IActionResult> GetAllResultados() => Ok(await _service.GetAllResultados());

        [HttpDelete("historial")]
        public async Task<IActionResult> EliminaCliente(long IdCliente) => Ok(await _service.DeleteHistorial(IdCliente));

        /// <summary>
        /// Listar Fichas de control
        /// </summary>
        /// <remarks>
        /// Aqui se cargan una lista de todoas
        /// </remarks>
        [HttpGet("FichasControl")]
        public async Task<IActionResult> GetAllFichasControl() => Ok(await _service.GetAllfichasControlAsync());

        [HttpGet("FichasHemo")]
        public async Task<IActionResult> GetAllFichasHemo() => Ok(await _service.GetAllfichasHemoAsync());


        [HttpGet("FichasControlSospecha")]
        public async Task<IActionResult> GetFichasControlConSospechaHemoAsync() => Ok(await _service.GetFichasControlConSospechaHemoAsync());

        /// <summary>
        /// Crer ficha de control
        /// </summary>
        /// <remarks>
        /// Aqui se envia l curepo para crear la fichaq 
        /// </remarks>
        [HttpPost("GuardarFichaControl")]
        public async Task<IActionResult> SaveFichaControlAsync(FichaControlDTO Ficha) => Ok(await _service.SaveFichaControlAsync(Ficha));



        [HttpPut("FichaControl")]
        public async Task<IActionResult> EditFichaControl(FichaControlDTO Ficha) => Ok(await _service.EditFichaControlAsync(Ficha));

    }
}