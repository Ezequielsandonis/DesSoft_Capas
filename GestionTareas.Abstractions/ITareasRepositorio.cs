using GestionTareas.Entidades;
using System.Threading;

namespace GestionTareas.Abstractions
{
  
        //interface para abstraer codigo
        public interface ITareasRepositorio
        {
            public List<Tarea> ListarTareas();
            public Tarea ListarTareaPorId(int id);

            void InsertarTarea(Tarea tarea);
            void ActualizarTarea(Tarea tarea);
            void EliminarTarea(int id);
        }
    
}
