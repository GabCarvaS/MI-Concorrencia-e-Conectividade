using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Companhia02.Models;
using Companhia02;
using Companhia02.Controllers;

namespace TesteServidor01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AeroportosController : ControllerBase
    {
        /*
         * Faz a escolha do método a ser executado 
         * Converte as mensagens em parâmetros e os tipos adequados
         * Cada requisição por padrão terá como alvo um método dentro desta classe que processa 
         * e retorna o resultado.
        */
        private readonly IAeroportoRepositorio _repositorio;
        ObterRotas or = new ObterRotas();
        string URI02 = @"http://localhost:37830/api/Aeroportos/";

        public AeroportosController(IAeroportoRepositorio repos)
        {
            _repositorio = repos;
        }

        /*
         * Método Get que responde pela rota especificada e retorna a lista com os aeroportos cadastrados
         */
        [HttpGet]
        public IEnumerable<Aeroporto> GetPaciente()
        {
            return _repositorio.GetAll();
        }

        /*
        * Método Get que responde pela rota especificada e retorna um aeroporto específico
        * O aeroporto é encontrado na lista pelo nome fornecido
        */
        [HttpGet("Aeroporto/{aero}")]
        public ActionResult<Aeroporto> GetAeroporto(string aero)
        {

            //Procura no repositório o aeroporto com o nome informado
            Aeroporto aeroporto = _repositorio.Get(aero);
            if (aeroporto == null)
            {
                return NotFound();
            }
            return aeroporto;
        }

        /*
        * Método Get que responde pela rota especificada e retorna as rotas que saem de um aeroporto específico
        * O aeroporto é encontrado na lista pelo nome fornecido
        */
        [HttpGet]
        [Route("getRotas/{aeroporto}")]
        public IEnumerable<Rota> GetRotas(string aeroporto)
        {
            return _repositorio.getRotas(aeroporto);
        }

        /*
        * Método Get que responde pela rota especificada e retorna uma lista de Aeroportos contendo o menor caminho que liga dois aeroportos
        * Caminho calcular através de uma implementação baseada no algorítmo de Dijkstra
        */
        [HttpGet]
        [Route("menorCaminho/{origem}/{destino}/")]
        public IEnumerable<Aeroporto> findShortestPath(string origem, string destino)
        {
            return _repositorio.shortestPath(origem, destino);
        }

        /*
        * Método Get que responde pela rota especificada e retorna a lista de poltronas de um voo especifico
        */
        [HttpGet]
        [Route("getPoltronas/{origem}/{destino}/")]
        public IEnumerable<Poltrona> GetPoltronas(string origem, string destino)
        {
            var aux = _repositorio.GetN(origem, destino);
            return aux;
        }

        /*
        * Método Get que responde pela rota especificada e realiza a reserva de uma poltrona em um voo
        */
        [HttpGet]
        [Route("atualizarPoltrona/{origem}/{destino}/")]
        public bool atualizarVaga(string origem, string destino)
        {
            //Variavel de controle
            var aux = false;

            //Procura o menor caminho entre as cidades 
            var IlistAero = _repositorio.shortestPath(origem, destino);

            // Se encontrar
            if (IlistAero != null)
            {
                // Transforma em uma lista
                var listAero = IlistAero.ToList();

                //Trafega entre os itens da lista
                for (int i = 0; i < (listAero.Count() - 1); i++)
                {
                    // Atualiza as vagas nos voos
                    aux = _repositorio.atualizarVaga(listAero[i].getName(), listAero[i + 1].getName());
                }
            }
            return aux;
        }

        /*
        * Método Get que responde pela rota especificada e realiza a coleta de informações da e,presa afiliada
        * Obtem a lista de aeroportos e voos que ela opera
        */
        [HttpGet]
        [Route("getAero/afiliada/")]
        public IEnumerable<Aeroporto> GetAerosFiliada()
        {
            var a = or.GetAeroportos(URI02);
            return a.ToList();
        }

    }
}
