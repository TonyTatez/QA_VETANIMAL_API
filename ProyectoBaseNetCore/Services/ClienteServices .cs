using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math;
using ProyectoBaseNetCore.DTOs;
using ProyectoBaseNetCore.Entities;
using ProyectoBaseNetCore.Models;

namespace ProyectoBaseNetCore.Services
{
    public class ClienteService
    {
        private static string _usuario;
        private static string _ip;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration configuration;
        public ClienteService(ApplicationDbContext context, IConfiguration configuration, string ip, string usuario)
        {
            _context = context;
            this.configuration = configuration;
            _ip = ip;
            _usuario = usuario;
        }
        public async Task<List<ClienteDTO>> GetCliente() => await _context.Cliente
            .Where(x => x.Activo).Select(x => new ClienteDTO
            {
                identificacion = x.Identificacion,
                nombres = x.Nombres,
                idCliente = x.IdCliente,
                direccion = x.Direccion,
                telefono = x.Telefono,
                correo = x.Correo,
                codigo = x.Codigo,
            }).ToListAsync();
        public async Task<List<MascotaDTO>> GetMascotasCliente(string CI) => await _context.HistoriaClinica
         .Where(hc => hc.Mascota.Activo && hc.Mascota.Cliente.Identificacion.Equals(CI))
         .Select(hc => new MascotaDTO
         {
             IdMascota = hc.Mascota.IdMascota,
             CODMascota = hc.Mascota.Codigo,
             NombreMascota = hc.Mascota.NombreMascota,
             IdCliente = hc.Mascota.IdCliente,
             IdHistoriaClinica = hc.IdHistoriaClinica,
             Cliente = hc.Mascota.Cliente.Nombres,
             Raza = hc.Mascota.Raza,
             Peso = hc.Mascota.Peso,
             Sexo = hc.Mascota.Sexo,
             FechaNacimiento = hc.Mascota.FechaNacimiento,
         }).ToListAsync();
        public async Task<ClienteDTO> GetClientByCI(string CI) => await _context.Cliente
            .Where(x => x.Activo && x.Identificacion == CI).Select(x => new ClienteDTO
            {
                idCliente = x.IdCliente,
                identificacion = x.Identificacion,
                nombres = x.Nombres,
                codigo = x.Codigo,
                direccion = x.Direccion,
                telefono = x.Telefono,
                correo = x.Correo,
            }).FirstOrDefaultAsync();
        public async Task<ClienteDTO> GetClientByUser(string IdUser) => await _context.Cliente
            .Where(x => x.Activo && x.IdUser == IdUser).Select(x => new ClienteDTO
            {
                idCliente = x.IdCliente,
                identificacion = x.Identificacion,
                nombres = x.Nombres,
                codigo = x.Codigo,
                direccion = x.Direccion,
                telefono = x.Telefono,
                correo = x.Correo,
            }).FirstOrDefaultAsync();

        public async Task<bool> SaveCliente(GuardarClienteViewModel Cliente)
        {
            var ClienteEncontrada = await _context.Cliente.FirstOrDefaultAsync(x => x.Activo && (x.IdCliente == Cliente.idCliente || x.Nombres == Cliente.nombres));
            if (ClienteEncontrada == null)
            {
                Cliente NewCliente = new Cliente();
                NewCliente.Nombres = Cliente.nombres;
                NewCliente.Identificacion = Cliente.identificacion;
                NewCliente.Direccion = Cliente.direccion;
                NewCliente.Correo = Cliente.correo;
                NewCliente.Telefono = Cliente.telefono;
                NewCliente.Activo = true;
                NewCliente.FechaRegistro = DateTime.UtcNow;
                NewCliente.UsuarioRegistro = _usuario;
                NewCliente.IpRegistro = _ip;
                await _context.Cliente.AddAsync(NewCliente);
                await _context.SaveChangesAsync();
            }
            else
            {
                ClienteEncontrada.Nombres = Cliente.nombres;
                ClienteEncontrada.Identificacion = Cliente.identificacion;
                ClienteEncontrada.Direccion = Cliente.direccion;
                ClienteEncontrada.Correo = Cliente.correo;
                ClienteEncontrada.Telefono = Cliente.telefono;
                ClienteEncontrada.FechaModificacion = DateTime.UtcNow;
                ClienteEncontrada.UsuarioModificacion = _usuario;
                ClienteEncontrada.IpModificacion = _ip;
                await _context.SaveChangesAsync();
            }
            return true;
        }
        public async Task<bool> EditCliente(ClienteDTO Cliente)
        {
            // Buscar el cliente por ID
            var ClienteEncontrado = await _context.Cliente.FindAsync(Cliente.idCliente);

            // Verificar si se encontró el cliente
            if (ClienteEncontrado != null)
            {
                // Actualizar las propiedades del cliente encontrado
                ClienteEncontrado.Nombres = Cliente.nombres;
                ClienteEncontrado.Identificacion = Cliente.identificacion;
                ClienteEncontrado.Direccion = Cliente.direccion;
                ClienteEncontrado.Correo = Cliente.correo;
                ClienteEncontrado.Telefono = Cliente.telefono;
                ClienteEncontrado.FechaModificacion = DateTime.UtcNow;
                ClienteEncontrado.UsuarioModificacion = _usuario;
                ClienteEncontrado.IpModificacion = _ip;

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                // Indicar que la edición fue exitosa
                return true;
            }
            else
            {
                // El cliente no fue encontrado, puedes manejar esto según tus necesidades
                // En este caso, devuelvo false para indicar que la edición no fue exitosa
                return false;
            }
        }

        public async Task<(int NumeroClientes, int NumeroMascotas)> GetNumeroClientesYNumeroMascotas()
        {
            try
            {
                // Contar el número de clientes activos
                int numeroClientes = await _context.Cliente.CountAsync(x => x.Activo);

                // Contar el número de mascotas registradas
                int numeroMascotas = await _context.Mascota.CountAsync(x => x.Activo);

                return (numeroClientes, numeroMascotas);
            }
            catch (Exception ex)
            {
                // Manejar errores según tus necesidades
                throw;
            }
        }


        public async Task<bool> DeleteCliente(long IdCliente)
        {
            try
            {
                var ClienteEncontrada = await _context.Cliente.FindAsync(IdCliente);
                if (ClienteEncontrada != null)
                {
                    ClienteEncontrada.Activo = false;
                    ClienteEncontrada.FechaEliminacion = DateTime.UtcNow;
                    ClienteEncontrada.UsuarioEliminacion = _usuario;
                    ClienteEncontrada.IpEliminacion = _ip;
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}