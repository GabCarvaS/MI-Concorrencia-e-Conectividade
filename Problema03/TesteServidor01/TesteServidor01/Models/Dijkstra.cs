using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteServidor01.Models
{
    public class Dijkstra
    {
		List<Aeroporto> shortest_Path;
		Aeroporto vertex_path;
		Aeroporto current;
		Aeroporto neighbor;
		List<Aeroporto> unvisited;

		public Dijkstra()
		{

			// Lista que guarda os vertices pertencentes ao menor caminho encontrado
			shortest_Path = new List<Aeroporto>();

			// Variavel que recebe os vertices pertencentes ao menor caminho
			vertex_path = new Aeroporto(null);

			// Variavel que guarda o vertice que esta sendo visitado
			current = new Aeroporto(null);

			// Variavel que marca o neighbor do vertice atualmente visitado
			neighbor = new Aeroporto(null);

			// Lista dos vertices que ainda nao foram visitados
			unvisited = new List<Aeroporto>();

		}

		public List<Aeroporto> findShortestPath(GraphAero graph, Aeroporto v1, Aeroporto v2)
		{

			// Adiciona a origem na lista do menor caminho
			shortest_Path.Add(v1);

			if (v1.getEdges() == null)
            {
				return null;
            }
			else
            {           
				unvisited.Add(v1);

				// Colocando a distancias iniciais				
				for(int i=0;i< graph.getVertex_list().Count();i++)
				{

					// O vertice inicial tem distancia igual a zero
					//if (!a.getName().Equals(v1.getName()))
					if (graph.getVertex_list()[i].getName().Equals(v1.getName()))
					{
						graph.getVertex_list()[i].setDistance(0);												
					}
					else
					{
						// Os demais vértices tem uma distancia muito grande inicialmente
						graph.getVertex_list()[i].setDistance(9999);
											
					}
					// Insere o vertice na lista de nao visitados
					unvisited.Add(graph.getVertex_list()[i]);
				}
				//Organiza a lista pelas distancias				
				unvisited = unvisited.OrderBy(u => u.getDistance()).ToList();

				//Continua ate acabar a lista de não visitados				
				while (unvisited.Any())				
				{
					//Como foi organizado anteriormente, o primeiro é o vértice que possui o menor peso			
					current = unvisited[0];

					//Procura a menor distãncia entre as arestas					
					for (int i = 0; i < current.getEdges().Count; i++)
					{						
						neighbor = current.getEdges()[i].getArrival_vertex();

						if (!neighbor.isVisited())
						{														
							// Comparando o peso das arestas 
							if (neighbor.getDistance() > (current.getDistance() + current.getEdges()[i].getWeight()))
							{
								neighbor.setDistance(current.getDistance() + current.getEdges()[i].getWeight());
								neighbor.setParent(current);

								//Se for o vértice final, altera a distância
								if (neighbor.getName() == v2.getName())
								{
									shortest_Path.Clear();
									vertex_path = neighbor;
									shortest_Path.Add(neighbor);

									while (vertex_path.getParent() != null)
									{
										shortest_Path.Add(vertex_path.getParent());
										vertex_path = vertex_path.getParent();
									}
									//Ordena a lista de menor caminho																	
									shortest_Path = shortest_Path.OrderBy(u => u.getDistance()).ToList();
								}
							}
						}
					}
					// Marca o vertice current como visitado e tira ele dos não visitados
					current.visit();
					unvisited.Remove(current);

					//Ordena os não visitados					
					unvisited = unvisited.OrderBy(u => u.getDistance()).ToList();
				}			
			}
			return shortest_Path;
		}		
	}
}
