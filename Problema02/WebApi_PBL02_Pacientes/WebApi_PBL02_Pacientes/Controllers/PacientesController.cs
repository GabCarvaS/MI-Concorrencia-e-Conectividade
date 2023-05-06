using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_PBL02_Pacientes.Models;
/*
 * Autor: Gabriel Araújo Carvalho dos Santos 
 * Componente Curricular: TEC 502 - MI Concorrência e conectividade
 * Turma: T03
 * Concluido: 02 / 11 / 2021
 * Descricao: Este programa realiza a implementação de uma web api para um sistema de 
 * gerenciamento e monitoração de pacientes.
 * 
 * Declaro que este código foi elaborado por mim de forma individual e 
 * não contém nenhum trecho de código de outro colega ou de outro autor, 
 * tais como provindos de livros e apostilas, e páginas ou documentos 
 * eletrônicos da Internet. 
 * Qualquer trecho de código de outra autoria que não a minha está destacado 
 * com uma citação para o autor e a fonte do código, e estou ciente que estes 
 * trechos não serão considerados para fins de avaliação.
 */
namespace WebApi_PBL02_Pacientes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class PacientesController : ControllerBase
    {
        /*
         * Faz a escolha do método a ser executado 
         * Converte as mensagens em parâmetros e os tipos adequados
         * Cada requisição por padrão terá como alvo um método dentro desta classe que processa 
         * e retorna o resultado.
        */
        private readonly IPacienteRepositorio _repositorio;

        public PacientesController(IPacienteRepositorio repos)
        {
            _repositorio = repos;
        }

        /*
         * Método Get que responde pela rota especificada e retorna a lista com os pacientes cadastrados
         */
        [HttpGet]
        public IEnumerable<Paciente> GetPaciente()
        {
             return _repositorio.GetAll();
        }

        /*
        * Método Get que responde pela rota especificada e retorna um paciente específico
        * O paciente é encontrado na lista pelo nome fornecido
        */
        [HttpGet("{nome}")]
        public ActionResult<Paciente> GetPaciente(string nome)
        {
            //Procura no repositório o paciente com o nome informado
            Paciente paciente = _repositorio.Get(nome);
            if (paciente == null)
            {
                return NotFound();
            }
            return paciente;
        }

        /*
         * Método Post que responde pela rota especificada para adicionar um novo paciente
         * Recebe e retorna o novo paciente cadastrado
         */
        [HttpPost]
        public ActionResult<Paciente> PostPaciente(Paciente paciente)
        {
            //Verifica se o paciente ja está cadastrado
            var jaCadastrado = _repositorio.Get(paciente.Nome);
            if(jaCadastrado != null )
            {
                return Conflict();
            }

            //Adiciona o paciente na lista
            var aux = _repositorio.Add(paciente);
            if (aux == null)
            {
                return BadRequest();
            }
            return Ok();
        }

        /*
         * Método Delete para remover um paciente pelo nome
         */
        [HttpDelete]
        [Route("{nome}")]
        public ActionResult<Paciente> DeletePaciente(string nome)
        {
            //Verifica se ele está na lista de pacientes
            var aux = _repositorio.Get(nome);
            if (aux == null)
            {
                return NotFound();
            }
            //Remove 
            _repositorio.Remove(nome);
            return Ok();
        }

        /*
        * Método Get que responde pela rota especificada e retorna a lista com os N pacientes escolhidos pelo usuário
        */
        [HttpGet]
        [Route("getN/{n}")]
        public IEnumerable<Paciente> GetNPacientes(string n)
        {
            return _repositorio.GetN(n);
        }
    }
}
