using Org.BouncyCastle.Math;
using ProyectoBaseNetCore;
using VET_ANIMAL_API.Models;
using Microsoft.EntityFrameworkCore;
using ProyectoBaseNetCore.Entities;
using NPOI.POIFS.Crypt.Dsig;

namespace VET_ANIMAL_API.Services
{
    public class VeterinarioService
    {
        private static string _usuario;
        private static string _ip;
        private readonly ApplicationDbContext context;

        public VeterinarioService(string usuario, string ip, ApplicationDbContext context)
        {
            _ip = ip;
            _usuario = usuario;
            this.context = context;
        }
        public Task<List<VeterinarioViewModel>> GetVeterinarios() => context.Veterinario
                    .AsNoTracking()
                    .Where(x => x.Activo)
                    .Select(x => new VeterinarioViewModel
                    {
                        nombres = x.Nombres,
                        id = x.Id,
                        sede = x.Sede,
                        telefono = x.Telefono,
                        correo = x.Correo,
                        identificacion = x.Identificacion,
                        rol = x.Rol
                    }).ToListAsync();


        public async Task<bool> SaveVeterinario(VeterinarioViewModel Veterinario)
        {
            var VeterinarioEncontrada = await context.Veterinario.FirstOrDefaultAsync(x => x.Activo && (x.Id == Veterinario.id || x.Nombres == Veterinario.nombres));
            if (VeterinarioEncontrada == null)
            {
                Veterinario NewVeterinario = new Veterinario();
                NewVeterinario.Id = Veterinario.id;
                NewVeterinario.Nombres = Veterinario.nombres;
                NewVeterinario.Sede = Veterinario.sede;
                NewVeterinario.Telefono = Veterinario.telefono;
                NewVeterinario.Correo = Veterinario.correo;
                NewVeterinario.Identificacion = Veterinario.identificacion;
                NewVeterinario.Rol = Veterinario.rol;
                NewVeterinario.Activo = true;
                NewVeterinario.FechaRegistro = DateTime.UtcNow;
                NewVeterinario.UsuarioRegistro = _usuario;
                NewVeterinario.IpRegistro = _ip;
               
                await context.Veterinario.AddAsync(NewVeterinario);
                await context.SaveChangesAsync();
            }
            else
            {

                VeterinarioEncontrada.Id = Veterinario.id;
                VeterinarioEncontrada.Nombres = Veterinario.nombres;
                VeterinarioEncontrada.Sede = Veterinario.sede;
                VeterinarioEncontrada.Telefono = Veterinario.telefono;
                VeterinarioEncontrada.Correo = Veterinario.correo;
                VeterinarioEncontrada.Identificacion = Veterinario.identificacion;
                VeterinarioEncontrada.Rol = Veterinario.rol;
                VeterinarioEncontrada.FechaModificacion = DateTime.UtcNow;
                VeterinarioEncontrada.UsuarioModificacion = _usuario;
                VeterinarioEncontrada.IpModificacion = _ip;
                await context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> EliminarVeterinario(long Id)
        {
            var VeterinarioEncontrada = await context.Veterinario.FirstOrDefaultAsync(x => x.Activo && x.Id == Id);
            if (VeterinarioEncontrada == null)
                return false;
            VeterinarioEncontrada.Activo = false;
            VeterinarioEncontrada.FechaEliminacion = DateTime.UtcNow;
            VeterinarioEncontrada.UsuarioEliminacion = _usuario;
            VeterinarioEncontrada.IpEliminacion = _ip;
            await context.SaveChangesAsync();
            return true;
        }
    }
}