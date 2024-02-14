using Org.BouncyCastle.Math;
using ProyectoBaseNetCore;
using VET_ANIMAL_API.Models;
using Microsoft.EntityFrameworkCore;
using ProyectoBaseNetCore.Entities;
using NPOI.POIFS.Crypt.Dsig;

namespace VET_ANIMAL_API.Services
{
    public class ReportesService
    {
        private static string _usuario;
        private static string _ip;
        private readonly ApplicationDbContext context;

        public ReportesService(string usuario, string ip, ApplicationDbContext context)
        {
            _ip = ip;
            _usuario = usuario;
            this.context = context;
        }

        public Task<List<DiagnosticoViewModel>> GetDiagnostico() => context.Enfermedad
                   .AsNoTracking()
                   .Where(x => x.Activo)
                   .Select(x => new DiagnosticoViewModel
                   {
                       nombre = x.Nombre,
                       idEnfermedad = x.IdEnfermedad
                   }).ToListAsync();

    }

    



}
