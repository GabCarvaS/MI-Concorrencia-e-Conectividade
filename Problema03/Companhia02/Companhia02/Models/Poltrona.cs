using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Companhia02.Models
{
    public class Poltrona
    {
        public bool disponivel;
        public int num_assento;

        public Poltrona()
        {
            this.disponivel = true;
        }

        public void setDisponibilidade(bool disponibilidade)
        {
            this.disponivel = disponibilidade;         
        }
        public void setNum_Assento(int localização)
        {
            this.num_assento = localização;
        }

        public bool isDisponivel()
        {
            return disponivel;
        }
        public int getAssento()
        {
            return num_assento;
        }
    }
}
