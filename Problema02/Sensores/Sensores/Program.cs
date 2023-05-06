using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;


namespace Sensores
{
    class Program
    {   
        //Lista de Pacientes
        public static List<Paciente> pacientes = new List<Paciente>();
        //String contendo a atualização das leituras
        public static string leituras;
        public static int cont1 = 0;
        public static int cont2 = 0;

        static void Main(string[] args)
        {
            Console.Title = "Simulação - Sensores - [V2.0]";            
            //portNum = 1883
            //ipAdress = 127.0.0.1:1883
            string hostName = "localhost";                       
           
            //Cliente MQTT
            var client1 = new MqttClient(hostName);
                        
            //Registra o cliente para receber mensagens
            client1.MqttMsgPublishReceived += Client1_MqttMsgPublishReceived;

            //Identificador único para o cliente
            string client1Id = Guid.NewGuid().ToString();
            client1.Connect(client1Id);
            
            //Inscreve no tópico como subscriber             
            client1.Subscribe(new string[] { "Sensores/nomePaciente" },new byte[] {MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE});
            Console.WriteLine("Conectado - Mosquitto");
        }       

        private static void Client1_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            //Quantidade de pacientes cadastrados
            int i = pacientes.Count;

            //Carrega as informações do Json
            ReadJson();
            
            //Quantidade de leituras atualizadas
            cont1 += 1;
            
            //Recebe a mensagem
            //A mensagem é o nome do paciente cuja litura será atualizada
            var message = System.Text.Encoding.Default.GetString(e.Message);
            
            //Procura o paciente
            foreach(Paciente p in pacientes)
            {
                //Procura o paciente na lista
                if(p.Nome == message)
                {
                    //Se econtrar, atualiza as leituras
                    Update(p);
                    //Junta as informações na string
                    leituras = $"{message}|{p.taxaO2}|{p.temperatura}";                                                         
                }
            }

            //Cria um novo cliente da mesma forma
            //Dessa vez para responder
            var client2 = new MqttClient("localhost");
            string client2Id = Guid.NewGuid().ToString();
            client2.Connect(client2Id);

            //Envia a string contendo as novas leituras                      
            client2.Publish("Sensores/leitura", Encoding.UTF8.GetBytes(leituras));
            if(cont1 == i)
            {
                //Quantidade de vezes que os sensores foram atualizados
                cont2 += 1;

                Console.WriteLine($"({cont2}) - {cont1} Leitura(s) atualizada(s)!");

                //Reinicia a contagem de sensores atualizados
                cont1 = 0;
            }            
        }

        public static void ReadJson()
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

            }                                      
        }
     
        /*
         * Atualiza os valores das leituras
         */
        public static void Update(Paciente p)
        {           
            //Se não for um valor nulo
            if (p != null)
            {
                //Realiza as leituras
                p.taxaO2 = p.oximetro.analiseLeitura(p.taxaO2);
                p.temperatura = p.termometro.analiseLeitura(p.temperatura);
            }                             
        }
        
    }
}

