using Microsoft.AspNetCore.Mvc;
using MongoDB.api.Data.Collections;
using MongoDB.api.Model;
using MongoDB.Driver;
using System;

namespace MongoDB.api.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class InfectadoController : ControllerBase
    {
        Data.MongoContext _mongoDB;
        IMongoCollection<Infectado> _infectadosCollection;

        public InfectadoController(Data.MongoContext mongoDB)
        {
            _mongoDB = mongoDB;
            _infectadosCollection = _mongoDB.DB.GetCollection<Infectado>(typeof(Infectado).Name.ToLower());
        }
        
        [HttpPost]
        public ActionResult SalvarInfectado([FromBody] InfectadoViewModel dto)
        {
            // int count= Convert.ToInt32(_infectadosCollection.Count(FilterDefinition<Infectado>.Empty));
            // count+=1;
            
            var infectado = new Infectado(dto.cpf, dto.Idade, dto.Sexo, dto.Latitude, dto.Longitude);

            _infectadosCollection.InsertOne(infectado);
            
            return StatusCode(201, "Infectado adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult ObterInfectados()
        {
            var infectados = _infectadosCollection.Find(Builders<Infectado>.Filter.Empty).ToList();
            
            return Ok(infectados);
        }

        [HttpGet("{CPF}")]
        public ActionResult ObterInfectado(long CPF)
        {
            var infectados = _infectadosCollection.Find(Builders<Infectado>.Filter.Where(_ => _.cpf == CPF)).ToList();
            
            return Ok(infectados);
        }

        [HttpPut]
        public ActionResult AtualizarInfectado([FromBody] InfectadoViewModel dto)
        {
             var infectado = new Infectado(dto.cpf, dto.Idade, dto.Sexo, dto.Latitude, dto.Longitude);
            _infectadosCollection.ReplaceOne(Builders<Infectado>.Filter.Where(_ => _.cpf == dto.cpf), infectado);
            
            return Ok("Atualizado com sucesso!");
        }

        [HttpDelete("{CPF}")]
        public ActionResult DeletarInfectado(long CPF)
        {
            _infectadosCollection.DeleteOne(Builders<Infectado>.Filter.Where(_ => _.cpf == CPF));
            
            return Ok("Deletado com sucesso!");
        }
    }
}