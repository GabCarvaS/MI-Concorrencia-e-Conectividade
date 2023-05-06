using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WebApi_PBL_Pacientes.Models
{
    /* A utilização de um repositório ajuda a
     * a centralizar a lógica de acesso aos dados  
    */
    interface IPacienteRepositorio
    {
        IEnumerable<Paciente> GetAll();
        Paciente Get(string nome);
        Paciente Add(Paciente item);
        void Remove(string nome);
        void Update();
        bool SaveJson();
        bool ReadJson();
       
    }
}
