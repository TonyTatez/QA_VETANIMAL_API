using Azure.Messaging;
using Microsoft.EntityFrameworkCore;
using NPOI.OpenXmlFormats.Spreadsheet;
using ProyectoBaseNetCore.DTOs;
using ProyectoBaseNetCore.Entities;
using ProyectoBaseNetCore.Utilities;
using VET_ANIMAL_API.DTOs;
using VET_ANIMAL_API.Entities;
using VET_ANIMAL_API.Models;
using Microsoft.AspNetCore.Mvc;
namespace ProyectoBaseNetCore.Services
{
    public class ConsultaServices
    {
        private static string _usuario;
        private static string _ip;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration configuration;
        private readonly GeneratorCodeHelper COD;
        public ConsultaServices(ApplicationDbContext context, IConfiguration configuration, string ip, string usuario)
        {
            _context = context;
            this.configuration = configuration;
            _ip = ip;
            _usuario = usuario;
            COD = new GeneratorCodeHelper(context, configuration, ip, usuario);
        }
        public async Task<TratamientoDTO.HistoriaClinicDTO> GetAllHitorialAsync(long CI) => await _context.HistoriaClinica
            .Where(x => x.Activo && x.Mascota.IdMascota == CI).Select(x => new TratamientoDTO.HistoriaClinicDTO
            {
                IdHistoriaClinica = x.IdHistoriaClinica,
                CodigoHistorial = x.CodigoHistorial,
                NombreMascota = x.Mascota.NombreMascota,
                Cedula = x.Mascota.Cliente.Identificacion,
                Raza = x.Mascota.Raza,
                FechaNacimiento = x.Mascota.FechaNacimiento,
                Sexo = x.Mascota.Sexo,
                Cliente = x.Mascota.Cliente.Nombres,
                CODMascota = x.Mascota.Codigo,
                Peso = x.Mascota.Peso,
                IdMascota = x.Mascota.IdMascota,
                IdCliente = x.Mascota.Cliente.IdCliente,
                FichasSintoma = x.FichasSintoma.Select(f => new TratamientoDTO.FichaSintomaDTO
                {

                    IdFicha = f.IdFicha,
                    CodigoFicha = f.CodigoFicha,
                    Fecha = f.FechaRegistro,
                    FichaDetalles = f.FichaDetalles.Select(fd => new TratamientoDTO.FichaDetalleDTO
                    {
                        IdDetalle = fd.IdDetalle,
                        Sintoma = fd.Sintoma.Nombre,
                        Observacion = fd.Observacion,
                    }).ToList(),

                }).ToList(),

                FichasControl = x.FichaControl.Select(fc => new FichaControlDTO
                {
                    CodigoFichaControl = fc.CodigoFichaControl,
                    Fecha = fc.FechaRegistro,
                    IdFichaControl = fc.IdFichaControl,
                    Peso = fc.Peso,
                    Motivo = fc.MotivoConsulta.Nombre,
                    Observacion = fc.Observacion,
                }).ToList(),

                FichasHemoparasitosis = x.FichaHemoparasitosis.Select(fh => new FichaHemoparasitosisDTO

                {
                    CodigoFichaHemo = fh.CodigoFichaHemo,
                    Fecha = fh.FechaRegistro,
                    IdHistoriaClinica = fh.IdHistoriaClinica,
                    IdFichaHemo = fh.IdFichaHemo,
                    Enfermedad = fh.Enfermedad.Nombre,
                    Observaciones = fh.Observacion,
                    Tratamiento = fh.Tratamiento,
                }).ToList(),

            }).FirstOrDefaultAsync();


        public async Task<IActionResult> ObtenerPDFPorIdFichaHemo(string idFichaHemo)
        {
            try
            {
                // Convertir el idFichaHemo a tipo long
                long id = Convert.ToInt64(idFichaHemo);

                // Buscar el registro en la base de datos usando el id
                var tablaContenido = await _context.TablaContenido.FirstOrDefaultAsync(tc => tc.IdFichaHemo == id && tc.Activo);

                // Verificar si se encontró el registro
                if (tablaContenido != null)
                {// Retornar el valor del campo Resultado directamente
                    return new  JsonResult(tablaContenido.Resultado);
                }
                else
                {
                    // No se encontró ningún registro con el id proporcionado
                    return new NotFoundResult();
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones y retorno de false
                throw;
            }
        }
        public async Task<bool> GuardadoTest(IFormFile Excel, string idFichaHemo, string resultado)
        {
            try
            {
                // Convertir el archivo a bytes
                byte[] fileBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    Excel.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }

                // Crear una nueva instancia de TablaContenido
                var tablaContenido = new TablaContenido
                {
                    Content = fileBytes,
                    IdFichaHemo = Convert.ToInt64(idFichaHemo),
                    Resultado = resultado,
                    // Establecer campos adicionales directamente en TablaContenido
                    Activo = true,
                    FechaRegistro = DateTime.UtcNow,
                    UsuarioRegistro = _usuario,
                    IpRegistro = _ip
                };

                // Agregar la instancia a tu contexto de base de datos y guardar los cambios
                _context.TablaContenido.Add(tablaContenido);
                await _context.SaveChangesAsync();

                // El guardado fue exitoso
                return true;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                // Puedes registrar el error o devolverlo como excepción para manejarlo en el controlador
                return false;
            }
        }
        public async Task<List<FichaTEST>> GetAllResultados()
        {
            return await _context.FichaHemoparasitosis
                .Where(fh => fh.TablaContenido.Resultado != null)
                .Select(fh => new FichaTEST
                {
                    IdHistoriaClinica = fh.IdHistoriaClinica,
                    IdFichaHemo = fh.IdFichaHemo,
                    ResultadoAlgoritmo = fh.Enfermedad.Nombre,
                    IdContenido = fh.TablaContenido.Id,
                    ResultadoFrotis = fh.TablaContenido.Resultado,
                    ResultadoFinal =
                        (fh.Enfermedad.Nombre == "BABESIOSIS" && fh.TablaContenido.Resultado == "POSITIVO (Babesia spp.) ") ||
                        (fh.Enfermedad.Nombre == "ANAPLASMOSIS" && fh.TablaContenido.Resultado == "POSITIVO (Anaplasma spp.) ") ||
                        (fh.Enfermedad.Nombre == "EHRLICHIOSIS" && fh.TablaContenido.Resultado == "POSITIVO (Ehrlichia spp.) ") ||
                        (fh.Enfermedad.Nombre == "Enfermedad Desconocida" && fh.TablaContenido.Resultado == "NEGATIVO  ") ? "1" : "0"
                }).ToListAsync();
        }
        public async Task<List<FichaHemoparasitosisTEST>> GetAllFichaHemoparasitosisAsync()
        {
            return await _context.FichaHemoparasitosis
                .Select(fh => new FichaHemoparasitosisTEST
                {
                    CodigoFichaHemo = fh.CodigoFichaHemo,
                    Fecha = fh.FechaRegistro,
                    IdHistoriaClinica = fh.IdHistoriaClinica,
                    IdFichaHemo = fh.IdFichaHemo,
                    Enfermedad = fh.Enfermedad.Nombre,
                    Observaciones = fh.Observacion,
                    Tratamiento = fh.Tratamiento,
                    NombreMascota = fh.HistoriaClinica.Mascota.NombreMascota,
                    Cliente = fh.HistoriaClinica.Mascota.Cliente.Nombres,
                    Raza = fh.HistoriaClinica.Mascota.Raza,
                    Peso = fh.HistoriaClinica.Mascota.Peso,
                    IdContenido = fh.TablaContenido.Id,


                }).ToListAsync();
        }

        public async Task<ClienteDTO> GetIdCliente(string Ruc) => await _context.Cliente
            .Where(x => x.Activo && x.Identificacion == Ruc).Select(x => new ClienteDTO
            {
                idCliente = x.IdCliente,
                identificacion = x.Identificacion,
                nombres = x.Nombres,
                //Apellidos = x.Codigo,
                direccion = x.Direccion,
                telefono = x.Telefono,
            }).FirstOrDefaultAsync();

        public async Task<bool> SaveHistorial(long IdMascota)
        {
            var CurrentHistory = await _context.HistoriaClinica.FirstOrDefaultAsync(x => x.IdMascotas == IdMascota);
            if (CurrentHistory is not null) throw new Exception("Ya existe un historial para esta mascota!");
            var codigo = await COD.GetOrCreateCodeAsync("HTC");
            HistoriaClinica NewHistory = new HistoriaClinica();
            NewHistory.CodigoHistorial = codigo;
            NewHistory.IdMascotas = IdMascota;

            NewHistory.Activo = true;
            NewHistory.FechaRegistro = DateTime.UtcNow;
            NewHistory.UsuarioRegistro = _usuario;
            NewHistory.IpRegistro = _ip;
            await _context.HistoriaClinica.AddAsync(NewHistory);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteHistorial(long IdCliente)
        {
            try
            {
                var ClienteEncontrada = await _context.HistoriaClinica.FindAsync(IdCliente);
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

        public async Task<List<FichaControlDTO>> GetFichasControlConSospechaHemoAsync()
        {
            var fichasControl = await _context.FichaControl
                .Include(fc => fc.HistoriaClinica)
                    .ThenInclude(hc => hc.Mascota)
                .Include(fc => fc.MotivoConsulta)
                .Where(fc => fc.Observacion.Contains("SOSPECHA HEMO"))
                .Where(fc => !_context.FichaHemoparasitosis.Any(fh => fh.IdFichaControl == fc.IdFichaControl))
                .Select(fc => new FichaControlDTO
                {
                    CodigoFichaControl = fc.CodigoFichaControl,
                    Fecha = fc.FechaRegistro,
                    FechaNacimiento= fc.HistoriaClinica.Mascota.FechaNacimiento,
                    IdFichaControl = fc.IdFichaControl,
                    IdHistoriaClinica = fc.IdHistoriaClinica,
                    Peso = fc.Peso,
                    Cliente = fc.HistoriaClinica.Mascota.Cliente.Nombres,
                    Motivo = fc.MotivoConsulta.Nombre,
                    Observacion = fc.Observacion,
                    NombreMascota = fc.HistoriaClinica.Mascota.NombreMascota,
                    Raza = fc.HistoriaClinica.Mascota.Raza,
                    Sexo = fc.HistoriaClinica.Mascota.Sexo,
                    IdMascota = fc.HistoriaClinica.Mascota.IdMascota,
                })
                .ToListAsync();

            return fichasControl;
        }
        //public async Task<List<FichaControlDTO>> GetFichasControlConSospechaHemoAsync() =>
        //await _context.FichaControl
        //    .Where(x => x.Activo && x.Observacion.Contains("SOSPECHA HEMO"))
        //    .Select(x => new FichaControlDTO
        //    {
        //        IdFichaControl = x.IdFichaControl,
        //        Fecha = x.FechaRegistro,
        //        CodigoFichaControl = x.CodigoFichaControl,
        //        Peso = x.Peso,
        //        Motivo = x.MotivoConsulta.Nombre,
        //        Observacion = x.Observacion,
        //    })
        //    .ToListAsync();
        public async Task<List<FichaControlDTO>> GetAllfichasControlAsync() => await _context.FichaControl
           .Where(x => x.Activo).Select(x => new FichaControlDTO
           {
               IdFichaControl = x.IdFichaControl,
               Fecha = x.FechaRegistro,
               CodigoFichaControl = x.CodigoFichaControl,
               Peso = x.Peso,
               Motivo = x.MotivoConsulta.Nombre,
               Observacion = x.Observacion,
           }).ToListAsync();

        public async Task<List<FichaHemoparasitosisDTO>> GetAllfichasHemoAsync() => await _context.FichaHemoparasitosis
          .Where(x => x.Activo).Select(x => new FichaHemoparasitosisDTO
          {
              IdFichaHemo = x.IdFichaHemo,
              Fecha = x.FechaRegistro,
              IdHistoriaClinica = x.IdHistoriaClinica,
              CodigoFichaHemo = x.CodigoFichaHemo,
              Enfermedad = x.Enfermedad.Nombre,
              Observaciones = x.Observacion,
              Tratamiento = x.Tratamiento,
          }).ToListAsync();
        public async Task<bool> SaveFichaControlAsync(FichaControlDTO Ficha)
        {

            if (string.IsNullOrEmpty(Ficha.Motivo)) throw new Exception("Debe registrar un motivo consulta!");
            bool Exististorial = await _context.HistoriaClinica.Where(x => x.IdHistoriaClinica == Ficha.IdHistoriaClinica).AnyAsync();
            if (!Exististorial) throw new Exception("Historia clinica no encntrada!");
            var codigo = await COD.GetOrCreateCodeAsync("FC");
            long IdMotivo = await COD.GetOrCreateMotivoAsync(Ficha.Motivo);
            FichaControl NewFControl = new FichaControl
            {
                CodigoFichaControl = codigo,
                IdMotivo = IdMotivo,
                Peso = Ficha.Peso,
                Observacion = Ficha.Observacion,
                IdHistoriaClinica = Ficha.IdHistoriaClinica,
                Activo = true,
                FechaRegistro = DateTime.UtcNow,
                UsuarioRegistro = _usuario,
                IpRegistro = _ip,
            };

            await _context.FichaControl.AddAsync(NewFControl);
            await _context.SaveChangesAsync();

            return true;
        }




        public async Task<bool> SaveFichaHemoAsync(FichaHemoparasitosisDTO Ficha)
        {


            if (string.IsNullOrEmpty(Ficha.Enfermedad)) throw new Exception("Debe registrar una Enfermedad consulta!");
            bool Exististorial = await _context.HistoriaClinica.Where(x => x.IdHistoriaClinica == Ficha.IdHistoriaClinica).AnyAsync();
            if (!Exististorial) throw new Exception("Historia clinica no encntrada!");
            var codigo = await COD.GetOrCreateCodeAsync("FH");
            long IdEnfermedad = await COD.GetOrCreateEnfermedadAsync(Ficha.Enfermedad);

            FichaHemoparasitosis NewFControl = new FichaHemoparasitosis
            {
                CodigoFichaHemo = codigo,
                IdEnfermedad= IdEnfermedad,
                IdHistoriaClinica = Ficha.IdHistoriaClinica,
                IdFichaControl= Ficha.IdFichaControl,
                Observacion = Ficha.Observaciones,
                Tratamiento = Ficha.Tratamiento,
                Activo = true,
                FechaRegistro = DateTime.UtcNow,
                UsuarioRegistro = _usuario,
                IpRegistro = _ip,
            };

            await _context.FichaHemoparasitosis.AddAsync(NewFControl);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditFichaControlAsync(FichaControlDTO Ficha)
        {
            if (Ficha.Motivo == null) throw new Exception("Debe registrar un motivo consulta!");
            long IdMotivo = await COD.GetOrCreateMotivoAsync(Ficha.Motivo);
            var CurrentFicha = await _context.FichaControl.FindAsync(Ficha.IdFichaControl);
            CurrentFicha.IdMotivo = IdMotivo;
            CurrentFicha.Peso = Ficha.Peso;
            CurrentFicha.Observacion = Ficha.Observacion;
            CurrentFicha.Activo = true;
            CurrentFicha.FechaModificacion = DateTime.UtcNow;
            CurrentFicha.UsuarioModificacion = _usuario;
            CurrentFicha.IpModificacion = _ip;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}