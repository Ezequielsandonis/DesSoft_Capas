using GestionTareas.Abstractions;
using GestionTareas.Entidades;
using GestionTareas.Negocio;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace GestionTareas.Tests.Servicios
{
    public class TareaServicioTests
    {
        [Fact]
        public void ListarTareas_DeberiaRetornarListaDeTareas()
        {
            // Arrange
            var mockRepositorio = new Mock<ITareasRepositorio>();
            mockRepositorio.Setup(repo => repo.ListarTareas()).Returns(new List<Tarea> { new Tarea { TareaId = 1, Titulo = "Tarea 1" } });

            var servicio = new TareaServicio(mockRepositorio.Object);

            // Act
            var tareas = servicio.ListarTareas();

            // Assert
            Assert.NotNull(tareas);
            Assert.Single(tareas);
            Assert.Equal("Tarea 1", tareas[0].Titulo);
        }

        [Fact]
        public void ObtenerTareaPorId_DeberiaRetornarTareaCorrecta()
        {
            // Arrange
            var mockRepositorio = new Mock<ITareasRepositorio>();
            mockRepositorio.Setup(repo => repo.ListarTareaPorId(It.IsAny<int>())).Returns(new Tarea { TareaId = 1, Titulo = "Tarea 1" });

            var servicio = new TareaServicio(mockRepositorio.Object);

            // Act
            var tarea = servicio.ObtenerTareaPorId(1);

            // Assert
            Assert.NotNull(tarea);
            Assert.Equal(1, tarea.TareaId);
            Assert.Equal("Tarea 1", tarea.Titulo);
        }

        [Fact]
        public void CrearTarea_DeberiaInsertarNuevaTarea()
        {
            // Arrange
            var mockRepositorio = new Mock<ITareasRepositorio>();
            mockRepositorio.Setup(repo => repo.ListarTareas()).Returns(new List<Tarea>());

            var servicio = new TareaServicio(mockRepositorio.Object);
            var nuevaTarea = new Tarea { TareaId = 1, Titulo = "Nueva Tarea" };

            // Act
            servicio.CrearTarea(nuevaTarea);

            // Assert
            mockRepositorio.Verify(repo => repo.InsertarTarea(It.IsAny<Tarea>()), Times.Once);
        }

        [Fact]
        public void ActualizarTarea_DeberiaActualizarTareaExistente()
        {
            // Arrange
            var mockRepositorio = new Mock<ITareasRepositorio>();
            var tareaExistente = new Tarea { TareaId = 1, Titulo = "Tarea Existente" };

            var servicio = new TareaServicio(mockRepositorio.Object);

            // Act
            servicio.ActualizarTarea(tareaExistente);

            // Assert
            mockRepositorio.Verify(repo => repo.ActualizarTarea(It.IsAny<Tarea>()), Times.Once);
        }

        [Fact]
        public void EliminarTarea_DeberiaEliminarTareaPorId()
        {
            // Arrange
            var mockRepositorio = new Mock<ITareasRepositorio>();

            var servicio = new TareaServicio(mockRepositorio.Object);
            int tareaId = 1;

            // Act
            servicio.EliminarTarea(tareaId);

            // Assert
            mockRepositorio.Verify(repo => repo.EliminarTarea(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void ObtenerTareaPorId_DeberiaLanzarExcepcionSiRepositorioFalla()
        {
            // Arrange
            var mockRepositorio = new Mock<ITareasRepositorio>();
            mockRepositorio.Setup(repo => repo.ListarTareaPorId(It.IsAny<int>())).Throws(new Exception("Error del repositorio"));

            var servicio = new TareaServicio(mockRepositorio.Object);

            // Act & Assert
            var exception = Assert.Throws<ApplicationException>(() => servicio.ObtenerTareaPorId(1));
            Assert.Equal("Error al obtener la tarea con ID 1.", exception.Message);
        }
    }
}
