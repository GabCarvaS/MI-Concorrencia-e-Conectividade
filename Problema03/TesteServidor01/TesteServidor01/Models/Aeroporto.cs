using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteServidor01.Models
{
    //Vertex
    public class Aeroporto
    {
        public string name;
        public int distance;
        public bool visited;
        Aeroporto parent;
        public string empresa;
        List<Rota> edges;
        List<Aeroporto> neighbors;
        

        public Aeroporto(string nome)
        {
            name = nome;
            edges = new List<Rota>();
            neighbors = new List<Aeroporto>();
            parent = null;
            visited = false;
            empresa = "Companhia 01";
        }

        /**
     *
     * @return a distancia até o vertice buscado
     */
        public int getDistance()
        {
            return distance;
        }

        /**
         *
         * @return se o vértice ja foi visitado
         */
        public bool isVisited()
        {
            return visited;
        }

        /**
         * 
         * @return
         */
        public Aeroporto getParent()
        {
            return parent;
        }


        /**
         * Mètodo que retorna a lista de aresta do vértice.
         * 
         * @return lista de arestas do vértice
         */

        public List<Rota> getEdges()
        {
            return edges;
        }

        /**
         *
         * @return a lista de vértices vizinhos
         */
        public List<Aeroporto> getNeighbors()
        {
            return neighbors;
        }

        /**
         *
         * @param distance
         */
        public void setDistance(int distance)
        {
            this.distance = distance;
        }

        /**
         *
         * @param visited
         */
        public void setVisited(bool visited)
        {
            this.visited = visited;
        }

        /**
         *
         * @param parent
         */
        public void setParent(Aeroporto parent)
        {
            this.parent = parent;
        }

        /**
         *
         * @param edges
         */
        public void setEdges(List<Rota> edges)
        {
            this.edges = edges;
        }

        /**
         *
         * @param neighbors
         */
        public void setNeighbors(List<Aeroporto> neighbors)
        {
            this.neighbors = neighbors;
        }

        /**
         * Método para alterar o status visitado
         */
        public void visit()
        {
            this.setVisited(true);
        }

        /**
         *
         * @return o status visitado
         */
        public bool check_visit()
        {
            return this.isVisited();
        }

        /**
         * Método que retorna o nome do vértice.
         * 
         * @return nome do vértice
         */
        public String getName()
        {
            return name;
        }

        /**
         * Método que modifica o nome do vértice.
         * 
         * @param name novo nome do vértice
         */
        public void setName(String name)
        {
            this.name = name;
        }
                    

        /**
         * Insere um vpertice na lista de vizinhos
         * @param v vétice a ser inserido na lista de vizinhos
         */
        public void insertNeighbors(Aeroporto v)
        {
            neighbors.Add(v);
        }

        /**
         * Método para adicionar uma aresta à lista de arestas do vértice.
         * 
         * @param new_edge nova aresta do vértice
         */
        public void insertEdge(Rota new_rota)
        {
            edges.Add(new_rota);
        }


        /**
     * Método para atestar a existência de determinada aresta do vértice.
     * 
     * @param weight peso da aresta procurada
     * @param arrival_vertex vértice de destino da arest procurada
     * @return 'true' se a aresta já existir e 'falso' caso contrário
     */
        public bool searchExistenceEdge(int weight, String arrival_vertex)
        {
            foreach (Rota each_edge in edges)
            {

                if ((each_edge.getWeight() == weight) &&
                    (each_edge.getArrival_vertex().getName() == arrival_vertex))
                {
                    // caso já exista uma aresta cujo peso e vértice de destino sejam
                    // correspondentes aos pesquisados
                    return true;
                }
            }
            // caso ainda não exista uma aresta cujos atributos sejam igauis aos
            // valores passados como argumento
            return false;
        }

        /**
        * Método para procurar determinada aresta do vértice
        * 
        * @param weight peso da aresta procurada
        * @param arrival_vertex vértice de destino da arest procurada
        * @return a aresta, caso exista, caso contrário retorna 'null'
        */
        public Rota searchEdge(int weight, String arrival_vertex)
        {
            foreach (Rota each_edge in edges)
            {

                if (each_edge.getWeight() == weight &&
                    each_edge.getArrival_vertex().getName() == arrival_vertex)
                {
                    // caso já exista uma aresta cujo peso e vértice de destino sejam
                    // correspondentes aos pesquisados
                    return each_edge;
                }
            }
            // caso ainda não exista uma aresta cujos atributos sejam igauis aos
            // valores passados como argumento
            return null;
        }

        /**
         * Método para remover uma aresta da lista de arestas do vértice.
         * 
         * @param chosen_edge aresta escolhida a ser removida
         */
        public void deleteEdge(Rota chosen_edge)
        {
            foreach (Rota current_edge in edges)
            {
                if (current_edge.getArrival_vertex() == chosen_edge.getArrival_vertex() &&
                    current_edge.getWeight() == chosen_edge.getWeight())
                {
                    edges.Remove(current_edge);
                }
            }
        }    

        public int compareTo(Aeroporto v)
        {
            if (this.getDistance() < v.getDistance())
                return -1;
            else if (this.getDistance() == v.getDistance())
                return 0;
            else
                return 1;
        }              
       

    }
}
