using Newtonsoft.Json;
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
using System.Net.Http.Headers;


namespace ClientePBL03
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string origem = "";
        string URI = "";
        string destino = "";

        private async void GetAllAeroportos()
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

                            //dgvDados é uma DataGridView
                            dgvDados.DataSource = JsonConvert.DeserializeObject<Aeroporto[]>(PacienteJsonString).ToList();
                            //Destaca as linhas

                        }
                        else
                        {
                            MessageBox.Show("Não foi possível obter o paciente : " + httpResponse.StatusCode);
                        }
                    }
                }
            }
            catch(Exception e)
            {

            }
            
        }

        private async void confirmarCompra()
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
                            MessageBox.Show("Compra realizada");

                        }
                        else
                        {
                            MessageBox.Show("Não foi possível obter o paciente : " + httpResponse.StatusCode);
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }

        }


        private void btnObterRota_Click(object sender, EventArgs e)
        {
            GetAllAeroportos();
        }

        private void confirmCompra_Click(object sender, EventArgs e)
        {
            origem = textOrigem.Text;
            destino = textDestino.Text;
            URI = $"{URI}/atualizarPoltrona/{origem}/{destino}";
            confirmarCompra();
        }

        private void btnEmpresa01_Click(object sender, EventArgs e)
        {
            URI = @"http://localhost:62916/api/aeroportos";
        }

        private void btnEmpresa02_Click(object sender, EventArgs e)
        {
            URI = @"http://localhost:37830/api/Aeroportos";
        }
    }
}
