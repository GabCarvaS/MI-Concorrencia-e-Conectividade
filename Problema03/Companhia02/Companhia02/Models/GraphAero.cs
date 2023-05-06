using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Companhia02;
using Companhia02.Models;

namespace Companhia02.Models
{
    public class GraphAero
    {
        private List<Aeroporto> graph;

        public GraphAero()
        {
            this.graph = new List<Aeroporto>();
        }

        
        /**
     * Adiconar um novo vértice à lista de vértices.
     * 
     * @param new_vertex novo vértice que se deseja adiconar
     */
        public void addVertex(Aeroporto new_vertex)
        {
            if (graph != null)
            {
                // caso já exista um vértice com o mesmo nome cadastrado na lista
                var a = graph.Find(p => p.name == new_vertex.getName());
                if (a != null)
                {
                    return;
                }
            }
 
            // caso não exista nenhum vértice com o mesmo nome do novo vértice 
            // que se deseja adicionar, adiciona-se o novo vértice à lista
            graph.Add(new_vertex);
        }

        /**
         * Adicionar uma aresta
         * @param new_edge
         */
        public void addEdge(String name_vertex, Rota new_edge)
        {
            //Procura o vértice inicial da aresta no grafo
            foreach (Aeroporto v in graph)
            {
                if (v.name == name_vertex)
                {
                    //Testa se ela ja existe
                    if (!v.searchExistenceEdge(new_edge.cost, new_edge.getArrival_vertex().getName()))
                    {
                        //Adiciona o vértice de chegada a lista de vizinhos do vértice de saida
                        v.getNeighbors().Add(new_edge.getArrival_vertex());
                        //Adiciona a aresta na lista de aresta do vértice
                        v.insertEdge(new_edge);
                    }
                }
            }
        }

        /**
         * Excluir um vértice do grafo.
         * 
         * @param name_vertex 
         */
        public void deleteVertex(String name_vertex)
        {
            foreach (Aeroporto current_vertex in graph)
            {
                if (current_vertex.getName().Equals(name_vertex))
                {
                    graph.Remove(current_vertex);
                }
            }
        }

        /**
         * Excluir uma aresta do grafo.
         * 
         * @param chosen_edge aresta que se deseja excluir
         */
        public void deleteEdge(String name_out_vertex, Rota chosen_edge)
        {
            if ((searchExistenceVertex(name_out_vertex) &&
                searchExistenceEdge(name_out_vertex, chosen_edge.getArrival_vertex().getName(), chosen_edge.getWeight())) == false)
            {
                // caso o vértice e a arsta não existam
                return;
            }

            // analisa se uma já existe tal aresta para o vértice indicado
            foreach (Aeroporto current_vertex in graph)
            {
                if (current_vertex.searchExistenceEdge(chosen_edge.getWeight(), chosen_edge.getArrival_vertex().getName()) == true)
                {
                    current_vertex.deleteEdge(chosen_edge);

                }
            }
        }

        /**
         * Método para atestar a existência de determinado vértice no grafo.
         * 
         * @param name_vertex nome do vértice
         * @return 'true' caso o vértice exista no grafo, 'false' caso contrário
         */
        public bool searchExistenceVertex(String name_vertex)
        {
            foreach (Aeroporto current_vertex in graph)
            {
                if (current_vertex.getName() == name_vertex) return true;
            }
            return false;
        }

        /**
         * Método para atestar a existência de determinada aresta no grafo.
         * 
         * @param name_out_vertex nome do vértice de "saída" da aresta
         * @param arrival_vertex npme do vértice de "chegada" da aresta
         * @param weight peso da aresta
         * @return 'true' caso a aresta exista no grafo, 'false' caso não exista
         */
        public bool searchExistenceEdge(String name_out_vertex, String arrival_vertex, int weight)
        {
            foreach (Aeroporto current_vertex in graph)
            {
                if (current_vertex.getName().Equals(name_out_vertex))
                {
                    return current_vertex.searchExistenceEdge(weight, arrival_vertex);
                }
            }
            return false;
        }

        /**
         * Retorna se o vértice é terminal
         * @param name_vertex
         * @return
         */
        public bool vertexIsTerminal(String name_vertex)
        {
            foreach (Aeroporto actual_vertex in graph)
            {
                if (actual_vertex.getName().Equals(name_vertex))
                {
                    return true;
                }
            }
            return false;
        }

        /**
         * Retorna a lista de vértices do grafo
         * @return
         */
        public List<Aeroporto> getVertex_list()
        {
            return graph;
        }

    }
}
