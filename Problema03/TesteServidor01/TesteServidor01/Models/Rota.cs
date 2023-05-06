using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteServidor01.Models
{
    //Edge
    public class Rota
    {
        Aeroporto source_vertex, arrival_vertex;
        public int weight;
        public string empresa;
        List<Poltrona> vagas;

        public Rota(Aeroporto start, Aeroporto end, int cost, string empresa)
        {
            this.vagas = new List<Poltrona>();
            this.source_vertex = start;
            this.arrival_vertex = end;
            this.weight = cost;
            this.empresa = empresa;
        }

        public IEnumerable<Poltrona> getPoltronas()
        {
            return vagas;
        }

        public int cost
        {
            get { return weight; }            
        }        

        public string company()
        {          
            return empresa;
        }

        public Aeroporto start
        {
            get { return source_vertex; }
        }

        public Aeroporto end
        {
            get { return arrival_vertex; }
        }

        public void setArrival_vertex(Aeroporto arrival_vertex)
        {
            this.arrival_vertex = arrival_vertex;
        }

        public void setSource(Aeroporto source)
        {
            this.source_vertex = source;
        }

        /**
         * Método que retorna o peso da aresta.
         * 
         * @return peso da aresta
         */
        public int getWeight()
        {
            return weight;
        }

        /**
         * Uma aresta é formada por um peso e por um vértice de destino, se a aresta
         * tem que ser mudada, então devem ser mudados esses dois atributos.
         * 
         * @param weight novo peso da aresta
         * @param arrival_vertex novo nó de destino da aresta
         */
        public void setWeight(int weight)
        {
            this.weight = weight;
        }

        /**
         * Método que retorna o vértice destino da aresta.
         * 
         * @return nome do vértice destino da aresta
         */
        public Aeroporto getArrival_vertex()
        {
            return arrival_vertex;
        }
         public bool addAssento(int i)
        {
            Poltrona p = new Poltrona();
            p.setNum_Assento(i);
            vagas.Add(p);
            return true;
        }

    }
}
