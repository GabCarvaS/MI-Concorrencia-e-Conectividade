using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_PBL_Pacientes.Models
{
    public class Termometro
    {
        //Classe para gerar os valores referentes a temperatura
        //Usados como base os valores de referencia da temperatura axilar       
        public double analiseLeitura(double referenceNumber)
        {
            // Função que gera os numeros das leituras
            // Tendência de gerar números semelhantes durante as iterações
            Random numAleatorio = new Random();

            //Limita o intervalo para referenceNumber-1 e referenceNumber+1
            int rateMax = 1;
            int rateMin = 1;

            //O referenceNumber é usado para determinar o intervalo da próxima leitura
            int aux = Convert.ToInt32(referenceNumber);

            //Escolhe o valor que será considerado como o lido pelo termômetro.

            //Um número inteiro para a temperatura
            int readInt = numAleatorio.Next(aux - rateMin, aux + rateMax);

            //Um número decimal entre 0,0 e 1,0 para a temperatura
            double readDouble = numAleatorio.NextDouble();

            //Soma dos dois valores da temperatura 
            double readValue = readInt + readDouble;

            //Ignora os valores considerados fora do intervalo                
            if (readValue < 34 || readValue > 40)
            {
                // Deve retornar um valor válido
                while (readValue < 34 || readValue > 40)
                {
                    readInt = numAleatorio.Next(aux - rateMin, aux + rateMax);
                    readDouble = numAleatorio.NextDouble();
                    readValue = readInt + readDouble;
                }
            }
            //Reduz ate uma casa após a virgula
            readValue = Math.Round(readValue, 1);

            //Retorna o valo lido
            return readValue;
        }
    }
}