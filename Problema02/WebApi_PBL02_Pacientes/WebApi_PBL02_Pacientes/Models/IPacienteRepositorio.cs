using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WebApi_PBL02_Pacientes.Models
{
    /* 
     * A utilização de um repositório ajuda a
     * a centralizar a lógica de acesso aos dados  
    */
    public interface IPacienteRepositorio
    {
       public IEnumerable<Paciente> GetAll();
       public Paciente Get(string nome);
       public Paciente Add(Paciente item);
       public void Remove(string nome);
       public void Update();
       public bool SaveJson();
       public bool ReadJson();
       public IEnumerable<Paciente> GetN(string n);
    }
}
