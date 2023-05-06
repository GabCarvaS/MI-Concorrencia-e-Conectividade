using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading;

namespace Client_WebApiPBL_Pacientes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }        
        string URI = "";
        string NPacientes = "";
        string nomePaciente = "";

        /*
         * Retorna todos os pacientes
         */
        private async void GetAllPacientes()
        {
            
            //Pega a URI do texbox 
            URI = txtURI.Text;
            //Cria um client HHTP
            using(var client = new HttpClient())
            {
                //Faz o request Get de forma assíncrona
                using (var httpResponse = await client.GetAsync(URI))
                {
                    //Se o request foi bem sucedido
                    if (httpResponse.IsSuccessStatusCode)
                    {   
                        //Coleta as informações e desserializa em Pacientes
                        var PacienteJsonString = await httpResponse.Content.ReadAsStringAsync();

                        //dgvDados é uma DataGridView
                        dgvDados.DataSource = JsonConvert.DeserializeObject<Paciente[]>(PacienteJsonString).ToList();
                        //Destaca as linhas
                        DestacarLinha();                        
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível obter o paciente : " + httpResponse.StatusCode);
                    }                       
                }
            }
        }

        /*
         * Retorna um paciente pelo seu nome
         */
        private async void GetPacienteByName()
        {
            
            //Cria um client HTTP
            using (var client = new HttpClient())
            {
                //Cria uma instancia do que vai mostrar na tela
                BindingSource dados = new BindingSource();

                //Pega a URI do texbox e complementa a rota com o nome do paciente
                URI = txtURI.Text + "/" + nomePaciente;

                //Envia o request 
                HttpResponseMessage httpResponse = await client.GetAsync(URI);
                if (httpResponse.IsSuccessStatusCode)
                {
                    //Coleta as informações e desserializa em um Paciente para mostrar no DATA GRID VIEW
                    var PacienteJsonString = await httpResponse.Content.ReadAsStringAsync();
                    dados.DataSource = JsonConvert.DeserializeObject<Paciente>(PacienteJsonString);
                    dgvDados.DataSource = dados;                    
                }
                else
                {
                    timer3.Stop();
                    MessageBox.Show("Falha ao obter o paciente : " + httpResponse.StatusCode);
                }
            }
        }

        /*
         * Adiciona um novo paciente
         */
        private async void AddPaciente()
        {
            //Pega a URI do texbox 
            URI = txtURI.Text;

            //Cria um novo paciente
            Paciente p1 = new Paciente();

            //Coleta os dados
            string Prompt; 
            string Titulo = "Coleta de Informações";
                        
            Prompt = "Informe o nome ";
            string nome = Microsoft.VisualBasic.Interaction.InputBox(Prompt, Titulo, "", 600, 350);
            Prompt = "Informe o sexo\n\n M - Masculino  |  F - Feminino ";
            string sexo = Microsoft.VisualBasic.Interaction.InputBox(Prompt, Titulo, "", 600, 350);
            Prompt = "Informe a idade ";
            string idade = Microsoft.VisualBasic.Interaction.InputBox(Prompt, Titulo, "", 600, 350);
            p1.Nome = nome;
            p1.Sexo = sexo;

            //Converte a string idade para int
            p1.Idade = Convert.ToInt32(idade);

            //Cria um client HTTP
            //Serializa o objeto e envia o request para adicionar na lista
            using (var client = new HttpClient())
            {
                var serializedPaciente = JsonConvert.SerializeObject(p1);
                var content = new StringContent(serializedPaciente, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(URI, content);
            }
            MessageBox.Show($"Adicionado!\nNome: {p1.Nome} | Idade: {p1.Idade} | Sexo: {p1.Sexo}");

            //Volta para mostrar todos os pacientes cadastrados
            GetAllPacientes();
        }     

        /*
         * Remove um paciente
         */
        private async void DeletePacient(string name)
        {
            //Pega a URI do texbox e complementa a rota com o nome do paciente
            URI = txtURI.Text;
            string NomePaciente = name;

            //Cria um client HTTP
            using (var client = new HttpClient())
            {
                //Envia o request 
                HttpResponseMessage httpResponse = await client.DeleteAsync($"{URI}/{NomePaciente}");
                if(httpResponse.IsSuccessStatusCode)
                {
                    MessageBox.Show("Paciente removido");
                }
                else
                {
                    MessageBox.Show($"Erro ao remover paciente: {httpResponse.StatusCode}");
                }
            }
            //Volta para mostrar todos os pacientes cadastrados
            GetAllPacientes();
        }

        private async void GetNPacientes()
        {
            var a = true;
            //Pega a URI e quantidade dos texbox
            NPacientes = txtNPacientes.Text;
            URI = $"{txtURI.Text}/getN/{NPacientes}";
            //Cria um client HHTP
            using (var client = new HttpClient())
            {
                //Faz o request Get de forma assíncrona
                using (var httpResponse = await client.GetAsync(URI))
                {
                    //Se o request foi bem sucedido
                    if (httpResponse.StatusCode != System.Net.HttpStatusCode.NotFound)
                    {
                        //Coleta as informações e desserializa em Pacientes
                        var PacienteJsonString = await httpResponse.Content.ReadAsStringAsync();

                        //dgvDados é uma DataGridView
                        dgvDados.DataSource = JsonConvert.DeserializeObject<Paciente[]>(PacienteJsonString).ToList();
                        //Destaca as linhas
                        DestacarLinha();
                    }
                    else
                    {
                        timer2.Stop();
                        MessageBox.Show("Não foi possível obter o(s) paciente(s) : " + httpResponse.StatusCode);
                    }
                }
            }
        }

        /*
         * Muda a cor das linhas de acordo com status do paciente
         */
        private void DestacarLinha()
        {           
            foreach(DataGridViewRow dgvRow in dgvDados.Rows)
            {                                                                                                                                                          
                if(dgvRow.Cells[4].Value.ToString().Equals("Grave")) 
                {
                    dgvRow.DefaultCellStyle.BackColor = Color.Tomato; 
                }
                else if (dgvRow.Cells[4].Value.ToString().Equals("Moderado"))
                {
                    dgvRow.DefaultCellStyle.BackColor = Color.LightYellow; 
                }
            }
        }

        //--------Botões------------------------------------------------------------------------
        
        private string InputBox()
        {
            /* usando a função VB.Net para exibir um prompt  */
            string Prompt = "Informe o nome do paciente";
            string Titulo = "Client WebApi";
            string Resultado = Microsoft.VisualBasic.Interaction.InputBox(Prompt, Titulo, "", 600, 350);
           
            return Resultado;
        }
       
        private void btnPacientesPorNome_Click(object sender, EventArgs e)
        {
            //Para os outros timers
            timer1.Stop();
            timer2.Stop();

            //Inicia o timer para atualizar as informações do paciente escolhido
            timer3.Start();

            nomePaciente = InputBox();
            if(nomePaciente.Length != 0)
            {
                GetPacienteByName();
            }
        }

        private void btnObterPacientes_Click(object sender, EventArgs e)
        {
            //Para os outros timers
            timer2.Stop();
            timer3.Stop();

            //Inicia o timer para atualizar as informações dos pacientes 
            timer1.Start();
            
            GetAllPacientes();
        }

        private void btnIncluirPaciente_Click(object sender, EventArgs e)
        {
            AddPaciente();
        }               
        
        private void btnRemoverPaciente_Click(object sender, EventArgs e)
        {
            string nome = InputBox();
            if (nome.Length != 0)
            {
                DeletePacient(nome);
            }
        }

        private void btnNPacientes_Click(object sender, EventArgs e)
        {
            //Para os outros timers
            timer1.Stop();
            timer3.Stop();

            //Inicia o timer para atualizar as informações dos N pacientes 
            timer2.Start();

            if (txtNPacientes.Text.Length != 0)
            {               
                GetNPacientes();
            }
            
        }

        //--------Timers------------------------------------------------------------------------
       
         //Timers para realizar a atualização das informações no DataGridView 
         
        /*
         * Atualiza a tabela com todos os pacientes cadastrados
         */
        private void timer1_Tick(object sender, EventArgs e)
        {
            GetAllPacientes();
        }

        /*
         * Atualiza a tabela com os N pacientes escolhidos pelo usuário
         */
        private void timer2_Tick(object sender, EventArgs e)
        {            
            GetNPacientes();
        }

        /*
         * Atualiza  a tabela com as informações de um paciente escolhido pelo usuário
         */
        private void timer3_Tick(object sender, EventArgs e)
        {
            GetPacienteByName();
        }
    }
}
