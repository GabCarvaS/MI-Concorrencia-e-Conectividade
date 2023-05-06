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
 * Autor: Gabriel Ara�jo Carvalho dos Santos 
 * Componente Curricular: TEC 502 - MI Concorr�ncia e conectividade
 * Turma: T03
 * Concluido: 10 / 09 / 2021
 * Descricao: Este programa realiza as solicita��es http para uma web api.
 * 
 * Declaro que este c�digo foi elaborado por mim de forma individual e 
 * n�o cont�m nenhum trecho de c�digo de outro colega ou de outro autor, 
 * tais como provindos de livros e apostilas, e p�ginas ou documentos 
 * eletr�nicos da Internet. 
 * Qualquer trecho de c�digo de outra autoria que n�o a minha est� destacado 
 * com uma cita��o para o autor e a fonte do c�digo, e estou ciente que estes 
 * trechos n�o ser�o considerados para fins de avalia��o.
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
