using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_PBL_Pacientes.Models
{
    public class Oximetro
    {
        //Classe para gerar os valores referentes a temperatura
        //Usados como base os valores de referencia da temperatura axilar  
        public int analiseLeitura(int referenceNumber)
        {
            // Função que gera os numeros das leituras
            // Tendência de gerar números semelhantes durante as iterações
            Random numAleatorio = new Random();

            //Limitadores para o intervalo
            int rateMax, rateMin;

            //Se o valor da oximetria for muito baixo, aumenta a probabilidade de sair um valor maior
            if (referenceNumber <= 90)
            {
                rateMax = 10;
                rateMin = 5;
            }
            //Se o valor da oximetria for muito alto, aumenta a probabilidade de sair um valor menor
            else if (referenceNumber >= 96)
            {
                rateMax = 0;
                rateMin = 10;
            }
            else
            {
                rateMax = 2;
                rateMin = 2;
            }

            //Escolhe o valor que será considerado como o lido pelo oximetro. Funciona dentro do intervalo
            int readValue = numAleatorio.Next(referenceNumber - rateMin, referenceNumber + rateMax);

            //Ignora os valores considerados fora do intervalo                
            if (readValue < 85 || readValue > 100)
            {
                // Deve retornar um valor válido
                while (readValue < 85 || readValue > 100)
                {
                    readValue = numAleatorio.Next(referenceNumber - rateMin, referenceNumber + rateMax);
                }
            }
            //Retorna o valor lido
            return readValue;
        }
    }
}