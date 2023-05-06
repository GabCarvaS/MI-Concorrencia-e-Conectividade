using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Companhia02.Models;

namespace Companhia02.Controllers
{
    public class ObterRotas
    {
        string URI;
        string NPacientes;
        string nomePaciente;
        public List<Rota> listRotaAux;
        public List<Aeroporto> listAeroAux;
        public ObterRotas()
        {
            URI = "";
            NPacientes = "";
            nomePaciente = "";
            listRotaAux = new List<Rota>();
            listAeroAux = new List<Aeroporto>();
        }


        /*
         * Retorna todos os pacientes
         */
        private async void GetAllAeroportos(string URI)
        {
            try
            {
                //Cria um client HHTP
                using (var client = new HttpClient())
                {
                    //Faz o request Get de forma assíncrona
                    using (var httpResponse = await client.GetAsync(URI))
                    {
                        //Se o request foi bem sucedido
                        if (httpResponse.IsSuccessStatusCode)
                        {
                            //Coleta as informações e desserializa em Pacientes
                            var PacienteJsonString = await httpResponse.Content.ReadAsStringAsync();

                            listAeroAux = JsonConvert.DeserializeObject<Aeroporto[]>(PacienteJsonString).ToList();
                        }
                        else
                        {
                            listAeroAux = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }

        }



        // Atualiza as poltronas no outro servidor
        private async void atualizarPoltronas(string origem, string destino)
        {             
            URI = $@"http://localhost:62916/api/aeroportos/atualizarPoltrona/{origem}/{destino}";//txtURI.Text;
            try
            {
                //Cria um client HHTP
                using (var client = new HttpClient())
                {
                    //Faz o request Get de forma assíncrona
                    using (var httpResponse = await client.GetAsync(URI))
                    {

                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public IEnumerable<Aeroporto> GetAeroportos(string uri)
        {
            GetAllAeroportos(uri);
            return listAeroAux;
        }

        public void attPoltronas(string origem, string destino)
        {           
            atualizarPoltronas(origem, destino);
        }
    }
}
