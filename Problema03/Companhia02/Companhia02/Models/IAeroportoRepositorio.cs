using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Companhia02.Models
{
    public interface IAeroportoRepositorio
    {
        public Aeroporto AddAero(Aeroporto aeroporto);
        public Aeroporto Get(string nome);
        public IEnumerable<Aeroporto> GetAll();
        public IEnumerable<Poltrona> GetN(string origem, string destino);
        public IEnumerable<Rota> getRotas(string aero);
        public IEnumerable<Aeroporto> shortestPath(string origem, string destino);
        public bool importInfo();
        public bool exportInfo();
        public bool atualizarVaga(string aeroStart, string aeroEnd);
    }
}
