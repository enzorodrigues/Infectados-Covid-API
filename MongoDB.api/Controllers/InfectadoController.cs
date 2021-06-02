using Microsoft.AspNetCore.Mvc;
using MongoDB.api.Data.Collections;
using MongoDB.api.Model;
using MongoDB.Driver;

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

        /// <summary>
        /// Este serviço permite inserir uma pessoa infectada pelo vírus COVID-19
        /// de acordo com seu sexo e localização.
        /// </summary>
        /// <param name="InfectadoViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insere")]
        public ActionResult SalvarInfectado([FromBody] InfectadoViewModel dto)
        {
                var infectado = new Infectado(dto.cpf, dto.Idade, dto.Sexo, dto.Latitude, dto.Longitude);

                _infectadosCollection.InsertOne(infectado);
            
                return StatusCode(201, "Infectado inserido com sucesso");
        }

        /// <summary>
        /// Este serviço lista os dados registrados de todos os infectados.
        /// </summary>
        [HttpGet]
        [Route("lista")]
        public ActionResult ObterInfectados()
        {
            var infectados = _infectadosCollection.Find(Builders<Infectado>.Filter.Empty).ToList();
            
            return Ok(infectados);
        }

        /// <summary>
        /// Este serviço busca um infectado resgitrado, pelo CPF, e lista seus dados.
        /// </summary>
        [HttpGet("{CPF}")]
        public ActionResult ObterInfectado(long CPF)
        {
            if(!FoundInfect(CPF)){
                return NotFound("Infectado não encontrado na base de dados");
            }

            var infectados = _infectadosCollection.Find(Builders<Infectado>.Filter.Where(_ => _.cpf == CPF)).ToList();
            
            return Ok(infectados);
        }

        /// <summary>
        /// Este serviço atualiza os dados de um infectado regitrado, pelo CPF.
        /// </summary>
        [HttpPut]
        [Route("atualiza")]
        public ActionResult AtualizarInfectado([FromBody] InfectadoViewModel dto)
        {
            if(!FoundInfect(dto.cpf)){
                return NotFound("Infectado não encontrado na base de dados");
            }

            var infectado = new Infectado(dto.cpf, dto.Idade, dto.Sexo, dto.Latitude, dto.Longitude);
            _infectadosCollection.ReplaceOne(Builders<Infectado>.Filter.Where(_ => _.cpf == dto.cpf), infectado);
            
            return Ok("Atualizado com sucesso!");
        }

        /// <summary>
        /// Este serviço deleta os dados de um CPF registrado como infectado.
        /// </summary>
        [HttpDelete("{CPF}")]
        public ActionResult DeletarInfectado(long CPF)
        {   
            if(!FoundInfect(CPF)){
                return NotFound("Infectado não encontrado na base de dados");
            }

            _infectadosCollection.DeleteOne(Builders<Infectado>.Filter.Where(_ => _.cpf == CPF));
            
            return Ok("Deletado com sucesso!");
        }

        private bool FoundInfect(long CPF){
            var infectados = _infectadosCollection.Find(Builders<Infectado>.Filter.Where(_ => _.cpf == CPF)).ToList();
            if(infectados == null){
                return false;
            }
            return true;
        }
    }
}