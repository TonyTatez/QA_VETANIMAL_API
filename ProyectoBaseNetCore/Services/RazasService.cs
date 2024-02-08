using Org.BouncyCastle.Math;
using ProyectoBaseNetCore;
using VET_ANIMAL_API.Models;
using Microsoft.EntityFrameworkCore;
using ProyectoBaseNetCore.Entities;
using NPOI.POIFS.Crypt.Dsig;

namespace VET_ANIMAL_API.Services
{
    public class RazasService
    {
        private static string _usuario;
        private static string _ip;
        private readonly ApplicationDbContext context;

        public RazasService(string usuario, string ip, ApplicationDbContext context)
        {
            _ip = ip;
            _usuario = usuario;
            this.context = context;
        }
        public Task<List<RazasViewModel>> GetRazas() => context.Razas
                    .AsNoTracking()
                    .Where(x => x.Activo)
                    .Select(x => new RazasViewModel
                    {
                        descripcion = x.Descripcion,
                        idRaza = x.IdRaza
                    }).ToListAsync();

        public Task<RazasViewModel> GetIdPais(long IdRaza) => context.Razas
            .AsNoTracking()
            .Where(x => x.Activo && x.IdRaza == IdRaza)
            .Select(x => new RazasViewModel
            {
                idRaza = x.IdRaza,
                descripcion = x.Descripcion
            }).FirstOrDefaultAsync();

        public async Task<bool> SaveRaza(RazasViewModel Raza)
        {
            var RazaEncontrada = await context.Razas.FirstOrDefaultAsync(x => x.Activo && (x.IdRaza == Raza.idRaza || x.Descripcion == Raza.descripcion));
            if (RazaEncontrada == null)
            {
                Razas NewRaza = new Razas();
                NewRaza.IdRaza = Raza.idRaza;
                NewRaza.Descripcion = Raza.descripcion;
                NewRaza.Activo = true;
                NewRaza.FechaRegistro = DateTime.UtcNow;
                NewRaza.UsuarioRegistro = _usuario;
                NewRaza.IpRegistro = _ip;
               
                await context.Razas.AddAsync(NewRaza);
                await context.SaveChangesAsync();
            }
            else
            {

                RazaEncontrada.IdRaza = Raza.idRaza;
                RazaEncontrada.Descripcion = Raza.descripcion;
                RazaEncontrada.FechaModificacion = DateTime.UtcNow;
                RazaEncontrada.UsuarioModificacion = _usuario;
                RazaEncontrada.IpModificacion = _ip;
                await context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> DeleteRaza(long IdRaza)
        {
            var RazaEncontrada = await context.Razas.FirstOrDefaultAsync(x => x.Activo && x.IdRaza == IdRaza);
            if (RazaEncontrada == null)
                return false;
            RazaEncontrada.Activo = false;
            RazaEncontrada.FechaEliminacion = DateTime.UtcNow;
            RazaEncontrada.UsuarioEliminacion = _usuario;
            RazaEncontrada.IpEliminacion = _ip;
            await context.SaveChangesAsync();
            return true;
        }
    }
}