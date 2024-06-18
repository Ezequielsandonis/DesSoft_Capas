using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTareas.Datos
{
    public class AppDbContext
    {
        public string Conexion { get; }

        public AppDbContext(string valor)
        {
            Conexion = valor;
        }
    }
}
