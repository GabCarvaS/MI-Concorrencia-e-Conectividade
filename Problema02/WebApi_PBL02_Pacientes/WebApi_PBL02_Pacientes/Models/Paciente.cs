using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading;

namespace WebApi_PBL02_Pacientes.Models
{
    public class Paciente
    {
        //Modelo de domínio
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public int Idade { get; set; }
        public string Status { get; set; }
        public int taxaO2 { get; set; }
        public double temperatura { get; set; }            

        public Paciente()
        {
            /*
             * No construtor são gerados os primeiros dados
             * 
             * Estes simulam as primeiras leituras de oximetria e temperatura
            */
                    
            this.Id = 0;

            // Função que gera os numeros das leituras
            // Tendência de gerar números semelhantes durante as iterações
            Random numAleatorio = new Random();

            //Leitura da oximetria
            this.taxaO2 = numAleatorio.Next(85, 100);

            //Um número inteiro para a temperatura
            double tempInt = numAleatorio.Next(35, 39);

            //Um número decimal entre 0,0 e 1,0 para a temperatura
            double tempdec = numAleatorio.NextDouble();

            //Soma dos dois valores da temperatura 
            double temp = tempInt + tempdec;

            //Testa para saber se está dentro do intervalo aceito
            if (temp < 35 || temp > 39)
            {
                //Continua a gerar valores até que saia um válido
                while (temp < 35 || temp > 39)
                {
                    tempInt = numAleatorio.Next(35, 39);
                    tempdec = numAleatorio.NextDouble();
                    temp = tempInt + tempdec;
                }
            }
            //Reduz as casas decimais até um número após a virgula
            this.temperatura = Math.Round(temp, 1);

            //Realiza a análise dos valores gerados para determinar o status do paciente
            if (taxaO2 < 92 || temperatura < 35 || temperatura >= 38)
            {
                this.Status = "Grave";
            }
            else if (taxaO2 >= 92 || taxaO2 < 95 || temperatura > 37 || temperatura < 38)
            {
                this.Status = "Moderado";
            }
            else if (taxaO2 >= 95 || temperatura >= 35 || temperatura <= 37)
            {
                this.Status = "Normal";
            }
        }
    }
}