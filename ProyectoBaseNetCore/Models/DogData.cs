using Microsoft.ML.Data;

namespace VET_ANIMAL_API.Models
{
    public class DogData
    {
        [LoadColumn(0)] public int ID { get; set; }
        [LoadColumn(1)] public DateTime Fecha { get; set; }
        [LoadColumn(2)] public string Raza { get; set; }
        [LoadColumn(3)] public float SangradoCutaneo { get; set; }
        [LoadColumn(4)] public float ProblemasRespiratorios { get; set; }
        [LoadColumn(5)] public float PerdidaDePeso { get; set; }
        [LoadColumn(6)] public float Linfadenopatia { get; set; }
        [LoadColumn(7)] public float Letargia { get; set; }
        [LoadColumn(8)] public float Ictericia { get; set; }
        [LoadColumn(9)] public float HemorragiasMucosas { get; set; }
        [LoadColumn(10)] public float Hemoglobinuria { get; set; }
        [LoadColumn(11)] public float Fiebre { get; set; }
        [LoadColumn(12)] public float Esplenomegalia { get; set; }
        [LoadColumn(13)] public float Epistaxis { get; set; }
        [LoadColumn(14)] public float DolorArticular { get; set; }
        [LoadColumn(15)] public float CojeraIntermitente { get; set; }
        [LoadColumn(16)] public float AnorexiaDepresion { get; set; }
        [LoadColumn(17)] public string Resultado { get; set; }

       
    }
}
