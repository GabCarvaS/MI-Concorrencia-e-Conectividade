using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Web.Http;
using WebApi_PBL_Pacientes.Models;
/*
 * Autor: Gabriel Araújo Carvalho dos Santos 
 * Componente Curricular: TEC 502 - MI Concorrência e conectividade
 * Turma: T03
 * Concluido: 10 / 09 / 2021
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
namespace WebApi_PBL_Pacientes.Controllers
{
    public class PacientesController : ApiController
    {
        /*
         * Faz a escolha do método a ser executado 
         * Converte as mensagens em parâmetros e os tipos adequados
         * Cada requisição por padrão terá como alvo um método dentro desta classe que processa 
         * e retorna o resultado.
        */
        static readonly IPacienteRepositorio repositorio = new PacienteRepositorio();

        /*
         * Método Get que responde pela rota especificada e retorna a lista com os pacientes cadastrados
         */
        [HttpGet]
        [Route("api/pacientes/")]
        public IEnumerable<Paciente> GetAllProdutos()
        {
            return repositorio.GetAll();
        }

        /*
         * Método Get que responde pela rota especificada e retorna um paciente específico
         * O paciente é encontrado na lista pelo nome fornecido
         */
        [HttpGet]
        [Route("api/pacientes/{nome}")]
        public Paciente GetPaciente(string nome)
        {
            //Procura no repositório o paciente com o nome informado
            Paciente paciente = repositorio.Get(nome);
            if (paciente == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return paciente;
        }

        /*
         * Método Post que responde pela rota especificada para adicionar um novo paciente
         * Recebe e retorna o novo paciente cadastrado
         */
        [HttpPost]
        [Route("api/pacientes/")]
        public Paciente PostPacient(Paciente paciente)
        {
            //Adiciona o paciente na lista
            paciente = repositorio.Add(paciente);

            //Cria uma rota direta para o paciente cadastrado para acessar via URI
            //Exemplo: api/pacientes/{nome}
            var response = Request.CreateResponse<Paciente>(HttpStatusCode.Created, paciente);
            string uri = Url.Link("DefaultApi", new { id = paciente.Id });
            try 
            {
                response.Headers.Location = new Uri(uri);
            }
            catch(Exception e)
            {
                return paciente;
            }                       
            //Retorna o paciente cadastrado
            return paciente;
        }

        /*
         * Método Get para salvar a lista de pacientes 
         * Salva um arquivo do tipo Json
         */
        [HttpGet]
        [Route("api/pacientes/salvar")]
        public void SalvarPaciente()
        {
            var aux = repositorio.SaveJson();

            if (!aux)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

        }

        /*
         * Método Get para ler o arquivo Json e preencher a lista de pacientes com os pacientes salvos
         */
        [HttpGet]
        [Route("api/pacientes/ler")]
        public void CarregarJson()
        {
            var aux = repositorio.ReadJson();
            if (!aux)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

        }

        /*
         * Método Delete para remover um paciente pelo nome
         */
        [HttpDelete]
        [Route("api/pacientes/{nome}")]
        public void DeletePaciente(string nome)
        {
            //Verifica se ele está na lista de pacientes
            Paciente p = GetPaciente(nome);
            if (p == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            //Remove 
            repositorio.Remove(nome);
        }
       
    }

}
