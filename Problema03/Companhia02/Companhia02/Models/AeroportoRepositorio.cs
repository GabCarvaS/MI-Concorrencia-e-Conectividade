using System;
using System.Collections.Generic;
using System.IO;
using Companhia02.Controllers;
using Newtonsoft.Json;

namespace Companhia02.Models
{
    public class AeroportoRepositorio : IAeroportoRepositorio
    {
        private ObterRotas or;
        public GraphAero aeroportos;
        public List<Aeroporto> listAeroAux;
        public List<Rota> listRotaAux;
        public Dijkstra path;

        public AeroportoRepositorio()
        {
            or = new ObterRotas();
            listAeroAux = new List<Aeroporto>();
            aeroportos = new GraphAero();
            path = new Dijkstra();
            listRotaAux = new List<Rota>();
                       
            Aeroporto SSA = new Aeroporto("SSA");
            Aeroporto SP = new Aeroporto("SP");
            Aeroporto RJ = new Aeroporto("RJ");
            Aeroporto BH = new Aeroporto("BH");

            SSA.insertNeighbors(BH);                                           
                       
            SP.insertNeighbors(BH);            
            SP.insertEdge(new Rota(SP, BH, 102, "Companhia 02"));

            RJ.insertNeighbors(BH);           
            RJ.insertEdge(new Rota(RJ, BH, 554, "Companhia 02"));
           
            BH.insertNeighbors(SSA);           
            BH.insertEdge(new Rota(BH, SSA, 214, "Companhia 02"));
                     
            aeroportos.addVertex(BH);                      
            aeroportos.addVertex(RJ);
            aeroportos.addVertex(SP);
            aeroportos.addVertex(SSA);
            
            foreach(var a in aeroportos.getVertex_list())
            {
                foreach(var b in a.getEdges())
                {
                    for(int i = 1; i<11; i++)
                    {
                        b.addAssento(i);
                    }                   
                }
            }
        }

        public Aeroporto AddAero(Aeroporto aeroporto)
        {
            if (aeroporto == null)
            {
                return null;
            }

            aeroportos.addVertex(aeroporto);

            return aeroporto;
        }

        public Aeroporto Get(string nome)
        {
            var aux = aeroportos.getVertex_list();
            return aux.Find(p => p.name == nome);
        }

        public IEnumerable<Aeroporto> GetAll()
        {          
            return aeroportos.getVertex_list();
        }

        public IEnumerable<Poltrona> GetN(string origem, string destino)
        {
            var aux = aeroportos.getVertex_list();
            foreach (var a in aux)
            {
                if (a.getName() == origem)
                {
                    foreach (var b in a.getEdges())
                    {
                        if (b.getArrival_vertex().getName() == destino)
                        {
                            return b.getPoltronas();
                        }
                    }
                }
            }
            return null;

        }

        public IEnumerable<Rota> getRotas(string aero)
        {
            List<Rota> rotasAero = new List<Rota>();
            foreach (var a in aeroportos.getVertex_list())
            {
                if (a.getName() == aero)
                {
                    foreach (var rota in a.getEdges())
                    {
                        rotasAero.Add(rota);
                    }
                }
            }
            return rotasAero;
        }

        public IEnumerable<Aeroporto> shortestPath(string origem, string destino)
        {
            var a1 = aeroportos.getVertex_list().Find(p => p.name == origem);
            var a2 = aeroportos.getVertex_list().Find(p => p.name == destino);
            var aux = path.findShortestPath(aeroportos, a1, a2);
            return aux;
        }

        //FALTA TERMINAR
        public bool importInfo()
        {
            //O caminho para o local onde está salvo o Json contendo os aeroportos           
            var pathPacientes = @"C:\Users\Gabri\OneDrive\Área de Trabalho\EmpresaA\aeroportosA.json";
            try
            {
                var strJson = "";
                //Abre o arquivo para leitura
                using (StreamReader sr = new StreamReader(pathPacientes))
                {
                    //Lê o arquivo até o final
                    strJson = sr.ReadToEnd();
                    //Se o arquivo não estiver vazio
                    if (strJson.Length != 0)
                    {
                        //Serializa as informações em objetos do tipo Paciente e adiciona na lista
                        listAeroAux = JsonConvert.DeserializeObject<List<Aeroporto>>(strJson);
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
            //Adiciona os aeroportos na lista
            foreach (var a in listAeroAux)
            {
                aeroportos.addVertex(a);
            }

            //Adiciona as rotas
            foreach (var aux in aeroportos.getVertex_list())
            {
                var strJson = "";
                var pathAero = $@"C:\Users\Gabri\OneDrive\Área de Trabalho\EmpresaA\{aux.getName()}.json";
                using (StreamReader sr = new StreamReader(pathAero))
                {
                    //Lê o arquivo até o final
                    strJson = sr.ReadToEnd();
                    //Se o arquivo não estiver vazio
                    if (strJson.Length != 0)
                    {
                        //Serializa as informações em objetos do tipo Paciente e adiciona na lista
                        listRotaAux = JsonConvert.DeserializeObject<List<Rota>>(strJson);
                    }
                }
                aux.setEdges(listRotaAux);
            }
            return true;
        }

        public bool exportInfo()
        {
            //O caminho para o local onde será salvo o Json            
            var pathpacientes = @"C:\Users\Gabri\OneDrive\Área de Trabalho\Empresa1\aeroportosA.json";

            //Serializando os objetos da lista para o formato Json
            var str = JsonConvert.SerializeObject(aeroportos.getVertex_list(), Formatting.Indented);
            foreach (var a in aeroportos.getVertex_list())
            {
                //O caminho para o local onde será salvo o Json com as rotas por aeroporto
                var rotas = $@"C:\Users\Gabri\OneDrive\Área de Trabalho\EmpresaA\{a.getName()}.json";
                var str2 = JsonConvert.SerializeObject(a.getEdges(), Formatting.Indented);
                try
                {
                    //Escreve no arquivo
                    using (StreamWriter stream2 = new StreamWriter(rotas))
                    {
                        stream2.WriteLine(str2);
                    }

                }
                catch (Exception e)
                {
                    return false;
                }
            }
            try
            {
                //Escreve no arquivo
                using (StreamWriter stream = new StreamWriter(pathpacientes))
                {
                    stream.WriteLine(str);

                }

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool atualizarVaga(string aeroStart, string aeroEnd)
        {
            var start = aeroportos.getVertex_list().Find(p => p.getName() == aeroStart);
            var end = aeroportos.getVertex_list().Find(p => p.getName() == aeroEnd);
            if (start != null && end != null)
            {
                //Procura na lista de aeroportos o aeroporto de saida
                foreach (var a in aeroportos.getVertex_list())
                {
                    //Se encontrar
                    if (a.getName() == start.getName())
                    {
                        //Procura na lista de rotas desse aeroporto o aeroporto de chegada
                        foreach (var b in a.getEdges())
                        {
                            //Se encontrar
                            if (b.getArrival_vertex().getName() == end.getName())
                            {
                                //Se for da outra empresa
                                if (b.company() == "Companhia 01")
                                {
                                    or.attPoltronas(aeroStart, aeroEnd);
                                }
                                //Procura na lista de assentos dessa rota a poltrona vendida
                                foreach (var c in b.getPoltronas())
                                {

                                    if (c.isDisponivel() == true)
                                    {
                                        c.setDisponibilidade(false);
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
                return false;
            }
            return false;

        }
    }
}
