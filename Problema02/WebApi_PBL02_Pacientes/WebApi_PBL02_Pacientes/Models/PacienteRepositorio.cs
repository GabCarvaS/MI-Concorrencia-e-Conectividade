using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Text;

namespace WebApi_PBL02_Pacientes.Models
{
    public class PacienteRepositorio : IPacienteRepositorio
    {
        //Lista de pacientes cadastrados no sistema
        private List<Paciente> pacientes = new List<Paciente>();

        //Lista que contém os N pacientes mais grave
        private List<Paciente> Npacientes = new List<Paciente>();

        //Id que indica a ordem de cadastro dos pacientes
        private int _nextId;
      
        static string hostName = "localhost";

        //Cliente MQTT
        public static MqttClient client2 = new MqttClient(hostName);
       
        public PacienteRepositorio()
        {
            
            //Thread para atualizar as leituras dos termômetros e oxímetros
            Thread t1 = new(Update);

            client2.MqttMsgPublishReceived += Client2_MqttMsgPublishReceived;
            //Cada cliente mqtt deve ter um id único
            string client2Id = Guid.NewGuid().ToString();
            client2.Connect(client2Id);           

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
                return null;
            }
            
            // Se o Id for igual a zero, significa que é um novo paciente 
            // Caso o contrário, ele veio do Json e ja tem um id válido  
            if (paciente.Id == 0)
            {
                paciente.Id = _nextId++;
            }
            pacientes.Add(paciente);
            SaveJson();
            return paciente;
        }

        /*
         * Método para retornar um paciente específico
         * Recebe uma string com o nome e procura na lista         
         */
        public Paciente Get(string nome)
        {
            //Retorna o paciente em que o nome seja igual ao nome informado
            return pacientes.Find(p => p.Nome == nome);
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
            foreach (Paciente p in pacientes)
            {
                if (p.Id > maior)
                {
                    maior = p.Id;
                }
            }
            _nextId = maior + 1;
            SaveJson();
        }

        /*
         * Método para salvar um Json contendo todos os pacientes da lista de pacientes;
        */
        public bool SaveJson()
        {
            //O caminho para o local onde será salvo o Json            
            var pathpacientes = @"C:\Users\Gabri\OneDrive\Área de Trabalho\Data\pacientes.json";
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
            var pathPacientes = @"C:\Users\Gabri\OneDrive\Área de Trabalho\Data\pacientes.json";
            try
            {
                var strJson = "";
                //Abre o arquivo para leitura
                using (StreamReader sr = new StreamReader(pathPacientes))
                {
                    //Lê o arquivo até o final
                    strJson = sr.ReadToEnd();
                    //Se o arquivo não estiver vazio
                    if (strJson.Length != 0)
                    {
                        //Serializa as informações em objetos do tipo Paciente e adiciona na lista
                        pacientes = JsonConvert.DeserializeObject<List<Paciente>>(strJson);
                    }
                }
            }
            catch (Exception e)
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
            while (true)
            {
                //Percorre a lista de pacientes
                foreach (Paciente p in pacientes)
                {
                    //Se não for um valor nulo
                    if (p != null)
                    {
                        //Registra o cliente para publicar mensagens                        
                        client2.Publish("Sensores/nomePaciente", Encoding.UTF8.GetBytes(p.Nome));

                        //Registra o cliente para receber mensagens
                        client2.Subscribe(new string[] { "Sensores/leitura" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });                      
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

                SaveJson();
                //Repete após 7 segundos
                Thread.Sleep(7000);

            }
        }

        /*
         * Evento de recebimento de mensagem do broker mosquitto
         */
        private void Client2_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            //Recebe a mensagem
            var message = System.Text.Encoding.Default.GetString(e.Message);

            //Separa as informações
            string[] subs = message.Split("|");
            string stro2,strtemp,nome;
            int o2;
            double temp;

            nome = subs[0];
            stro2 = subs[1];
            strtemp = subs[2];

            //Realiza as conversões de valores
            o2 = Convert.ToInt32(stro2);
            temp = Convert.ToDouble(strtemp);
            
            foreach (Paciente p in pacientes)
            {
                if(p.Nome == nome)
                {   
                    //Atualiza os valores
                    p.taxaO2 = o2;
                    p.temperatura = temp;
                }
            }  
            //Salva
           SaveJson();
        }
       
        /*
         * Retornar os N pacientes mais graves
         */
        public IEnumerable<Paciente> GetN(string n)
        {
            int nInt = Convert.ToInt32(n);
            attListaNPacientes(nInt);
            return Npacientes;
        }

        /*
         * Atualiza a lista dos N pacientes mais graves
         */
        private void attListaNPacientes(int n)
        {
            //Garante que ela esteja vazia
            Npacientes.Clear();

            //Lista auxiliar ordenada pelo status
            List<Paciente> ordenadaStatus = new List<Paciente>();
            if (pacientes.Count != 0)
            {
                //Lista somente para os pacientes com o status grave
                List<Paciente> Grave = pacientes.FindAll(p => p.Status == "Grave");

                //Lista somente para os pacientes com o status moderado
                List<Paciente> Moderado = pacientes.FindAll(p => p.Status == "Moderado");

                //Lista somente para os pacientes com o status normal
                List<Paciente> Normal = pacientes.FindAll(p => p.Status == "Normal");

                //Concatena as listas
                //Adiciona primeiro os pacientes graves, depois os moderados e por ultimos os normais
                ordenadaStatus.AddRange(Grave);
                ordenadaStatus.AddRange(Moderado);
                ordenadaStatus.AddRange(Normal);
            }

            //Se o usuário escolher um N e este N for maior do que o total de pacientes cadastrados
            //O N é alterado para esse total
            if (n > ordenadaStatus.Count)
            {
                n = ordenadaStatus.Count;
            }
               
            for (int i = 0; i < n ;i++)
            {
                Npacientes.Add(ordenadaStatus[i]);            
            }                                       
        }
    }
}
