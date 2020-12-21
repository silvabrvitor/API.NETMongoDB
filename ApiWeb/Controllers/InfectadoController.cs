using System;
using System.Timers;
using ApiWeb.Data.Collections;
using ApiWeb.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ApiWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectadoController : ControllerBase
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<Infectado> _infectadosCollection;

        public InfectadoController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _infectadosCollection = _mongoDB.DB.GetCollection<Infectado>(typeof(Infectado).Name.ToLower());
        }

        [HttpPost]
        public ActionResult SalvarInfectado([FromBody] InfectadoDto dto)
        {
            var infectado = new Infectado(dto.Id, dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);

            _infectadosCollection.InsertOne(infectado);
            
            return StatusCode(201, "Infectado adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult ObterInfectados()
        {
            //var infectados = _infectadosCollection.Find(new BsonDocument()).FirstOrDefault();
            var infectados = _infectadosCollection.Find(Builders<Infectado>.Filter.Empty).ToList();
            
            return Ok(infectados);
        }

        [HttpDelete]
        public ActionResult DeletaInfectados([FromBody] InfectadoDto dto)
        {
            
            var deleteFilter = Builders<Infectado>.Filter.Eq("_id", dto.Id);

            _infectadosCollection.DeleteOne(deleteFilter);
            
            
            return StatusCode(202, "Infectado deletado com sucesso");
        }

        [HttpPut]
        public ActionResult AtualizaInfectados([FromBody] InfectadoDto dto)
        {
            
            
            var updateFilterdt = Builders<Infectado>.Update.Set("dataNascimento", dto.DataNascimento);
            var updateFiltersexo = Builders<Infectado>.Update.Set("sexo", dto.Sexo);
            var updateFilterlat = Builders<Infectado>.Update.Set("latitude", dto.Latitude);
            var updateFilterlong = Builders<Infectado>.Update.Set("longitude", dto.Longitude);
                         
            var filter = Builders<Infectado>.Filter.Eq("_id", dto.Id);

            _infectadosCollection.UpdateOne(filter, updateFilterdt);
            _infectadosCollection.UpdateOne(filter, updateFiltersexo);
            _infectadosCollection.UpdateOne(filter, updateFilterlat);
            _infectadosCollection.UpdateOne(filter, updateFilterlong);
            
            return StatusCode(202, "Infectado Atualizado com sucesso");

            
        }

        
    }
}