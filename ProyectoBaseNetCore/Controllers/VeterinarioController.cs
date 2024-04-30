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
    [Route("api/Veterinario/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VeterinarioController : Controller
    {
        private readonly IConfiguration configuration;
        protected VeterinarioService _service;
        private readonly IAuthorizationService authorizationService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext context;

        public VeterinarioController(IConfiguration configuration, IAuthorizationService Authorization, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            this.authorizationService = Authorization;
            this.configuration = configuration;
            this.userManager = userManager;
            this._httpContextAccessor = httpContextAccessor;
            this.context = context;
            string userName = Task.Run(async () => await userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type.ToLower().Contains("email")).Value)).Result.UserName;
            var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
            this._service = new VeterinarioService(userName, ip, context);
        }
        [HttpGet("ListarVeterinarios")]
        public async Task<IActionResult> GetVeterinarios() => Ok(await _service.GetVeterinarios());

        [HttpPost("NuevoVeterinario")]
        public async Task<IActionResult> SaveVeterinario(VeterinarioViewModel Veterinario) => Ok(await _service.SaveVeterinario(Veterinario));

        [HttpPost("EliminarVeterinario")]
        public async Task<IActionResult> EliminarVeterinario(long Id) => Ok(await _service.EliminarVeterinario(Id));
    }
}
