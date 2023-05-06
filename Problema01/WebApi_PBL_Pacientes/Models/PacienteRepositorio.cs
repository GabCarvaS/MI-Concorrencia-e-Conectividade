using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace WebApi_PBL_Pacientes.Models
{
    public class PacienteRepositorio : IPacienteRepositorio
    {
        //Lista de pacientes cadastrados no sistema
        private List<Paciente> pacientes = new List<Paciente>();
        //Id que indica a ordem de cadastro dos pacientes
        private int _nextId;        

        public PacienteRepositorio()
        {
            //Thread para atualizar as leituras dos termômetros e oxímetros
            Thread t1 = new Thread(Update);

            //Carregar os pacientes cadastrados de um arquivo Json
            ReadJson();

            //Atualiza a referencia do id para o ultimo pacente cadastrado
            _nextId = pacientes.Last().Id + 1;

            //Inicia a thread           
            t1.Start();
        }

        /*
         * Método para adicionar um paciente na lista
         * Recebe uma instacia de paciente 
         * Retorna o paciente adicionado
         */
        public Paciente Add(Paciente paciente)
        {
            //Testa se é uma instancia válida
            if (paciente == null)
            {
                throw new ArgumentNullException("paciente");
            }
            // Se o Id for igual a zero, significa que é um novo paciente 
            // Caso o contrário, ele veio do Json e ja tem um id válido  
            if(paciente.Id == 0)
            {
                paciente.Id=_nextId++;
            }               
            pacientes.Add(paciente);
            return paciente;
        }

        /*
         * Método para retornar um paciente específico
         * Recebe uma string com o nome e procura na lista         
         */
        public Paciente Get(string nome)
        {  
            //Retorna o paciente em que o nome seja igual ao nome informado
            return  pacientes.Find(p => p.Nome == nome);         
        }
        
        /*
         * Método para retornar a lista contendo todos os pacientes cadastrados
         */
        public IEnumerable<Paciente> GetAll()
        {
            return pacientes;
        }

        /*
         * Método para remover um paciente da lista de pacientes
         * Recebe uma string com o nome e procura na lista
        */
        public void Remove(string nome)
        {
            //Remove todas as ocorrências do nome informado na lista
            pacientes.RemoveAll(p => p.Nome == nome);
            int maior = 0;
            
            //Atualiza a referência do _nextId
            foreach(Paciente p in pacientes)
            {
                if(p.Id > maior)
                {
                    maior = p.Id;
                }
            }
            _nextId = maior + 1;
        }

        /*
         * Método para salvar um Json contendo todos os pacientes da lista de pacientes;
        */
        public bool SaveJson()
        {
            //O caminho para o local onde será salvo o Json
            var pathpacientes = @"D:\Documentos\Data\pacientes.json";
            
            //Serializando os objetos da lista para o formato Json
            var str = JsonConvert.SerializeObject(pacientes, Formatting.Indented);
            try
            {   
                //Escreve no arquivo
                using (StreamWriter stream = new StreamWriter(pathpacientes))
                {
                    stream.WriteLine(str);
                    return true;
                }                
                
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /*
         * Método para carregar os dados do arquivo Json
        */
        public bool ReadJson()
        {
            //O caminho para o local onde está salvo o Json
            var pathPacientes = @"D:\Documentos\Data\pacientes.json";
            try
            {
                var strJson = "";
                //Abre o arquivo para leitura
                using(StreamReader sr = new StreamReader(pathPacientes))
                {
                    //Lê o arquivo até o final
                    strJson = sr.ReadToEnd();
                    //Se o arquivo não estiver vazio
                    if(strJson.Length != 0)
                    {
                        //Serializa as informações em objetos do tipo Paciente e adiciona na lista
                        pacientes = JsonConvert.DeserializeObject<List<Paciente>>(strJson);
                    }
                }
            }
            catch(Exception e)
            {
                return false;
            }
            
            return true;
        }

        /*
         * Método para atualizar as leituras do oxímetro e do termômetro
         * Funciona em uma thread separada
         * Atualiza os dados a cada 7 segundos
        */
        public void Update()
        {           
            while(true)
            {   
                //Percorre a lista de pacientes
                foreach (Paciente p in pacientes)
                {
                    //Se não for uma valor nulo
                    if(p!=null)
                    {
                        //Realiza as leituras
                        p.taxaO2 = p.oximetro.analiseLeitura(p.taxaO2);
                        p.temperatura = p.termometro.analiseLeitura(p.temperatura);
                    }

                    //Atualiza os status
                    if (p.taxaO2 < 92 || p.temperatura < 35 || p.temperatura >= 38)
                    {
                        p.Status = "Grave";
                    }
                    else if (p.taxaO2 >= 92 || p.taxaO2 < 95 || p.temperatura > 37 || p.temperatura < 38)
                    {
                        p.Status = "Moderado";
                    }
                    else if (p.taxaO2 >= 95 || p.temperatura >= 35 || p.temperatura <= 37)
                    {
                        p.Status = "Normal";
                    }
                }
                //Repete após 7 segundos
                Thread.Sleep(7000);
            }
        }      
    }
}