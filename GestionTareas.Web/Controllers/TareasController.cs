using GestionTareas.Entidades;
using GestionTareas.Negocio;
using Microsoft.AspNetCore.Mvc;

namespace GestionTareas.Web.Controllers
{
    public class TareasController : Controller
    {
        //instancia
        private readonly TareaServicio _tareaServicio;

        //constructor
        public TareasController(TareaServicio tareaServicio)
        {
            _tareaServicio = tareaServicio;
        }

        //vista index (lista de tareas)
        public IActionResult Index()
        {
            var tareas = _tareaServicio.ListarTareas();
            return View(tareas);
        }

        //Detalles de una tarea
        public IActionResult Details(int id)
        {
            var tarea = _tareaServicio.ObtenerTareaPorId(id);
            if (tarea == null)
            {
                return NotFound();
            }
            return View(tarea);
        }

        // crear-get
        public IActionResult Create()
        {
            return View(new Tarea());
        }
        //crear-post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _tareaServicio.CrearTarea(tarea);
                    return RedirectToAction(nameof(Index));
                }
                catch (ApplicationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error inesperado.");
                }
            }
            return View(tarea);
        }

        //editar-get
        public IActionResult Edit(int id)
        {
            var tarea = _tareaServicio.ObtenerTareaPorId(id);
            //validar
            if (tarea == null)
            {
                return NotFound();
            }
            return View(tarea);
        }

        //editar-post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Tarea tarea)
        {

            try
            {
                _tareaServicio.ActualizarTarea(tarea);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(tarea);
        }

        //eliminar get

        //eliminar-post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            //validar
            try
            {
                _tareaServicio.EliminarTarea(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View("Delete");
            }
        }


    }
}
