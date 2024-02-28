using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoBaseNetCore.Entities;
using ProyectoBaseNetCore;
using VET_ANIMAL_API.Models;
using VET_ANIMAL_API.Services;
using ProyectoBaseNetCore.Services;


namespace VET_ANIMAL_API.Controllers
{
    [ApiController]
    [Route("api/Reportes/")]

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReportesController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IAuthorizationService authorizationService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected ReportesService _service;
        private readonly ApplicationDbContext _context;

        public ReportesController(ApplicationDbContext context,
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
            this._service = new ReportesService(_context, configuration, userName, ip);
        }


        [HttpGet("ListarDiagnostico")]
        public async Task<IActionResult> GetDiagnostico() => Ok(await _service.GetDiagnostico());


        [HttpGet("ReportesHistorico")]
        public async Task<IActionResult> GetHistoriasClinicasAsync(string FechaInicio, string FechaFin, int enfermedades, string razas, string sexo) => Ok(await _service.GetHistoriasClinicasAsync(DateTime.Parse(FechaInicio), DateTime.Parse(FechaFin), enfermedades, razas, sexo));

        [HttpGet("ContarCasosPorEnfermedad")]
        public async Task<IActionResult> ContarCasosPorEnfermedad(int? año = null, int? mes = null) =>Ok(await _service.ContarCasosPorEnfermedadAsync(año, mes));









    }
}
