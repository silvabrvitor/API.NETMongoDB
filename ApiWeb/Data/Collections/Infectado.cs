using System;
using MongoDB.Bson;
using MongoDB.Driver.GeoJsonObjectModel;

namespace ApiWeb.Data.Collections
{
    public class Infectado
    {
        
        public Infectado(string id, DateTime dataNascimento, string sexo, double latitude, double longitude)
        {
            this.Id = id;
            this.DataNascimento = dataNascimento;
            this.Sexo = sexo;
            this.Localizacao = new GeoJson2DGeographicCoordinates(longitude, latitude);
        }
        
        public string Id { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public GeoJson2DGeographicCoordinates Localizacao { get; set; }
        
    }
}