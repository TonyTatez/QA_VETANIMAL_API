using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoBaseNetCore.Entities;
using ProyectoBaseNetCore;
using VET_ANIMAL_API.Models;
using VET_ANIMAL_API.Services;

namespace VET_ANIMAL_API.Controllers
{
    [ApiController]
    [Route("api/Razas/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RazasController : Controller
    {
        private readonly IConfiguration configuration;
        protected RazasService _service;
        private readonly IAuthorizationService authorizationService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext context;

        public RazasController(IConfiguration configuration, IAuthorizationService Authorization, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            this.authorizationService = Authorization;
            this.configuration = configuration;
            this.userManager = userManager;
            this._httpContextAccessor = httpContextAccessor;
            this.context = context;
            string userName = Task.Run(async () => await userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type.ToLower().Contains("email")).Value)).Result.UserName;
            var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
            this._service = new RazasService(userName, ip, context);
        }
        [HttpGet("ListarRazas")]
        public async Task<IActionResult> GetRazas() => Ok(await _service.GetRazas());

        [HttpGet("CantidadXRazas")]
        public async Task<IActionResult> GetCantidadRazas() => Ok(await _service.GetCantidadRazas());

        [HttpPost("NuevaRaza")]
        public async Task<IActionResult> NuevaPais(RazasViewModel Pais) => Ok(await _service.SaveRaza(Pais));

        [HttpPost("EliminarRaza")]
        public async Task<IActionResult> EliminarPais(long IdRaza) => Ok(await _service.DeleteRaza(IdRaza));
    }
}
