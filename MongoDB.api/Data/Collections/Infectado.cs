using System;
using MongoDB.Driver.GeoJsonObjectModel;

namespace MongoDB.api.Data.Collections
{
    public class Infectado
    {
        public Infectado(long cpf, int idade, string sexo, double latitude, double longitude)
        {
            this.cpf = cpf;
            this.Idade = idade;
            this.Sexo = sexo;
            this.Localizacao = new GeoJson2DGeographicCoordinates(longitude, latitude);
        }
        
        public long cpf {get; private set;}
        public int Idade { get; set; }
        public string Sexo { get; set; }
        public GeoJson2DGeographicCoordinates Localizacao { get; set; }
    }
}