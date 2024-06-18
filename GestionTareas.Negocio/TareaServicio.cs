using GestionTareas.Abstractions;
using GestionTareas.Entidades;

namespace GestionTareas.Negocio
{
    public class TareaServicio
    {
        private readonly ITareasRepositorio _tareasRepositorio;

        public TareaServicio(ITareasRepositorio tareasRepositorio)
        {
            _tareasRepositorio = tareasRepositorio;
        }

        public List<Tarea> ListarTareas()
        {
            try
            {
                return _tareasRepositorio.ListarTareas();
            }
            catch (Exception ex)
            {
                // registrar el error y lanzar una excepción específica si es necesario
                throw new ApplicationException("Error al listar las tareas.", ex);
            }
        }

        public Tarea ObtenerTareaPorId(int id)
        {
            try
            {
                return _tareasRepositorio.ListarTareaPorId(id);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new ApplicationException($"Error al obtener la tarea con ID {id}.", ex);

            }
        }

        public void CrearTarea(Tarea tarea)
        {
            try
            {
                // Regla de negocio: no permitir tareas duplicadas
                var tareasExistentes = _tareasRepositorio.ListarTareas();
                if (tareasExistentes.Any(t => t.Titulo == tarea.Titulo))
                {
                    throw new ApplicationException("Ya existe una tarea con el mismo título.");
                }

                // Validación adicional antes de insertar
                if (string.IsNullOrWhiteSpace(tarea.Titulo))
                {
                    throw new ArgumentException("El título de la tarea no puede estar vacío.");
                }

                _tareasRepositorio.InsertarTarea(tarea);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new ApplicationException("Error al crear la tarea.", ex);
            }
        }



        public void ActualizarTarea(Tarea tarea)
        {
            try
            {
                // Validación antes de actualizar
                if (string.IsNullOrWhiteSpace(tarea.Titulo))
                {
                    throw new ArgumentException("El título de la tarea no puede estar vacío.");
                }

                _tareasRepositorio.ActualizarTarea(tarea);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new ApplicationException("Error al actualizar la tarea.", ex);
            }
        }

        public void EliminarTarea(int id)
        {
            try
            {
                // Validación de negocio antes de eliminar
                if (id <= 0)
                {
                    throw new ArgumentException("El ID de la tarea debe ser válido.");
                }

                _tareasRepositorio.EliminarTarea(id);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new ApplicationException($"Error al eliminar la tarea con ID {id}.", ex);
            }
        }
    }
}
