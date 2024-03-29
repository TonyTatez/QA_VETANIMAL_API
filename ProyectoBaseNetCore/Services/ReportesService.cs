﻿using Org.BouncyCastle.Math;
using ProyectoBaseNetCore;
using VET_ANIMAL_API.Models;
using Microsoft.EntityFrameworkCore;
using ProyectoBaseNetCore.Entities;
using NPOI.POIFS.Crypt.Dsig;
using Microsoft.IdentityModel.Tokens;
using Microsoft.ML;
using static ProyectoBaseNetCore.DTOs.TratamientoDTO;
using ProyectoBaseNetCore.Utilities;
using VET_ANIMAL_API.DTOs;
using VET_ANIMAL_API.Entities;
using NPOI.OpenXmlFormats.Spreadsheet;
using ProyectoBaseNetCore.DTOs;

namespace VET_ANIMAL_API.Services
{
    public class ReportesService
    {
        private static string _usuario;
        private static string _ip;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration configuration;
        private readonly GeneratorCodeHelper COD;

        public ReportesService(ApplicationDbContext context, IConfiguration configuration, string ip, string usuario)
        {
            _context = context;
            this.configuration = configuration;
            _ip = ip;
            _usuario = usuario;
            COD = new GeneratorCodeHelper(context, configuration, ip, usuario);
        }

        public Task<List<DiagnosticoViewModel>> GetDiagnostico() => _context.Enfermedad
                   .AsNoTracking()
                   .Where(x => x.Activo)
                   .Select(x => new DiagnosticoViewModel
                   {
                       nombre = x.Nombre,
                       idEnfermedad = x.IdEnfermedad
                   }).ToListAsync();





        public async Task<List<ReportesHistoricoFiltros>> GetHistoriasClinicasAsync(DateTime fechaInicio, DateTime fechaFin, int idEnfermedad, string razas, string sexo)
        {
            fechaInicio = fechaInicio.ToUniversalTime();
            fechaFin = fechaFin.ToUniversalTime();

            // Obtener el nombre de la enfermedad correspondiente al ID
            var nombreEnfermedad = await _context.Enfermedad
                .Where(e => e.IdEnfermedad == idEnfermedad)
                .Select(e => e.Nombre)
                .FirstOrDefaultAsync();

            if (nombreEnfermedad == null)
            {
                // Manejar el caso en el que el ID de enfermedad no existe
                // Por ejemplo, lanzar una excepción o devolver una lista vacía
                return new List<ReportesHistoricoFiltros>();
            }

            var query = _context.FichaHemoparasitosis
                .Where(fc => fc.Activo &&
                    fc.FechaRegistro >= fechaInicio &&
                    fc.FechaRegistro <= fechaFin &&
                    fc.Enfermedad.Nombre == nombreEnfermedad); // Filtrar por nombre de enfermedad

            if (!string.IsNullOrEmpty(razas))
            {
                query = query.Where(fc => fc.HistoriaClinica.Mascota.Raza == razas);
            }

            if (!string.IsNullOrEmpty(sexo))
            {
                query = query.Where(fc => fc.HistoriaClinica.Mascota.Sexo == sexo);
            }

            var fichasControl = await query
                .Select(fc => new ReportesHistoricoFiltros
                {
                    Fecha = fc.FechaRegistro.ToUniversalTime(), // Convertir a UTC antes de proyectar
                    IdFichaControl = fc.IdFichaControl,
                    IdHistoriaClinica = fc.IdHistoriaClinica,
                    Cliente = fc.HistoriaClinica.Mascota.Cliente.Nombres,
                    Peso = fc.HistoriaClinica.Mascota.Peso,
                    Observacion = fc.Observacion,
                    NombreMascota = fc.HistoriaClinica.Mascota.NombreMascota,
                    Raza = fc.HistoriaClinica.Mascota.Raza,
                    Sexo = fc.HistoriaClinica.Mascota.Sexo,
                    IdMascota = fc.HistoriaClinica.Mascota.IdMascota,
                    Resultado = nombreEnfermedad, // Aquí se asigna el nombre de la enfermedad
                })
                .ToListAsync();

            return fichasControl;
        }
        public async Task<List<object>> ContarCasosPorEnfermedadAsync(int? año = null, int? mes = null)
        {
            var query = _context.FichaHemoparasitosis
                                .Where(fc => fc.Activo);

            // Aplicar filtros de año y/o mes si están presentes
            if (año.HasValue)
            {
                query = query.Where(fc => fc.FechaRegistro.Year == año);
            }

            if (mes.HasValue)
            {
                query = query.Where(fc => fc.FechaRegistro.Month == mes);
            }

            var casosPorEnfermedad = await query
                                            .GroupBy(fc => fc.Enfermedad.Nombre)
                                            .Select(g => new
                                            {
                                                Enfermedad = g.Key,
                                                Cantidad = g.Count()
                                            })
                                            .ToListAsync();

            return casosPorEnfermedad.Cast<object>().ToList();
        }

        public async Task<object> ObtenerDetallesYTotalesFichasHemoparasitosisAsync(int? año = null, int? mes = null)
        {
            var queryFichaHemoparasitosis = _context.FichaHemoparasitosis
                                                   .Where(fc => fc.Activo);

            var queryTablaContenido = _context.TablaContenido;

            // Aplicar filtros de año y/o mes si están presentes a ambas consultas
            if (año.HasValue)
            {
                queryFichaHemoparasitosis = queryFichaHemoparasitosis.Where(fc => fc.FechaRegistro.Year == año);
            }

            if (mes.HasValue)
            {
                queryFichaHemoparasitosis = queryFichaHemoparasitosis.Where(fc => fc.FechaRegistro.Month == mes);
            }

            var detallesFichas = await queryFichaHemoparasitosis
                                        .Select(fc => new
                                        {
                                            IdFichaControl = fc.IdFichaControl, 
                                            IdFichaHemo = fc.IdFichaHemo, 
                                            ResultadoAlgoritmo = fc.Enfermedad.Nombre,                                                                                     
                                            ResultadoFrotis = fc.TablaContenido.Resultado,
                                            IdHistoriaClinica = fc.IdHistoriaClinica, 
                                        })
                                        .ToListAsync();

            var totalDiagnosticosFichaHemoparasitosis = await queryFichaHemoparasitosis.CountAsync();
            var totalDiagnosticosTablaContenido = await queryTablaContenido.CountAsync();

            return new
            {
                DetallesFichas = detallesFichas,
                DiagnosticoAlgoritmo = totalDiagnosticosFichaHemoparasitosis,
                DiagnosticoFrotis = totalDiagnosticosTablaContenido
            };
        }

    }

}
