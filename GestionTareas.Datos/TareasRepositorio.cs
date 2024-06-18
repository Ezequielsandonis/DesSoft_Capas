using GestionTareas.Abstractions;
using GestionTareas.Entidades;

namespace GestionTareas.Datos
{
    public class TareasRepositorio : ITareasRepositorio
    {
        //Llamar a la conexion 
        private readonly AppDbContext _contexto;


        //constructor para iniciliazar la clase con la conexion 
        public TareasRepositorio(AppDbContext contexto)
        {
            _contexto = contexto;

        }

        void ITareasRepositorio.ActualizarTarea(Tarea tarea)
        {
            throw new NotImplementedException();
        }

        void ITareasRepositorio.EliminarTarea(int id)
        {
            throw new NotImplementedException();
        }

        void ITareasRepositorio.InsertarTarea(Tarea tarea)
        {
            throw new NotImplementedException();
        }

        Tarea ITareasRepositorio.ListarTareaPorId(int id)
        {
            throw new NotImplementedException();
        }

        List<Tarea> ITareasRepositorio.ListarTareas()
        {
            throw new NotImplementedException();
        }
    }
}
