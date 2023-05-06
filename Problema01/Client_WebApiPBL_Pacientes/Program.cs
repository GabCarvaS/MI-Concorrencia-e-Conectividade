using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http.Headers;
/*
 * Autor: Gabriel Araújo Carvalho dos Santos 
 * Componente Curricular: TEC 502 - MI Concorrência e conectividade
 * Turma: T03
 * Concluido: 10 / 09 / 2021
 * Descricao: Este programa realiza as solicitações http para uma web api.
 * 
 * Declaro que este código foi elaborado por mim de forma individual e 
 * não contém nenhum trecho de código de outro colega ou de outro autor, 
 * tais como provindos de livros e apostilas, e páginas ou documentos 
 * eletrônicos da Internet. 
 * Qualquer trecho de código de outra autoria que não a minha está destacado 
 * com uma citação para o autor e a fonte do código, e estou ciente que estes 
 * trechos não serão considerados para fins de avaliação.
 */
namespace Client_WebApiPBL_Pacientes
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {           
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
