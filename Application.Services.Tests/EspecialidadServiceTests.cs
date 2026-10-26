using Application.Services;
using Data;
using Domain.Model;
using DTOs;
using Moq;
using Xunit;

namespace Application.Services.Tests
{
    public class EspecialidadServiceTests
    {
        [Fact]
        public async Task AddAsync_Valida_RetornaDTOConId()
        {
            // Arrange
            var dto = new EspecialidadDTO
            {
                Nombre = "Traumatología",
                Precio = 20000m
            };

            var mockRepo = new Mock<IEspecialidadRepository>();
            mockRepo.Setup(r => r.ExistsByNameAsync("Traumatología", null)).ReturnsAsync(false);
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Especialidad>()))
                    .Callback<Especialidad>(e => e.SetId(10))
                    .Returns(Task.CompletedTask);

            var mockProfRepo = new Mock<IProfesionalRepository>();

            var service = new EspecialidadService(mockRepo.Object, mockProfRepo.Object);

            // Act
            var result = await service.AddAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(10, result.Id);
            Assert.Equal("Traumatología", result.Nombre);
            Assert.Equal(20000m, result.Precio);
        }

        [Fact]
        public async Task AddAsync_NombreDuplicado_LanzaArgumentException()
        {
            // Arrange
            var dto = new EspecialidadDTO
            {
                Nombre = "Cardiología",
                Precio = 15000m
            };

            var mockRepo = new Mock<IEspecialidadRepository>();
            mockRepo.Setup(r => r.ExistsByNameAsync("Cardiología", null)).ReturnsAsync(true);

            var mockProfRepo = new Mock<IProfesionalRepository>();

            var service = new EspecialidadService(mockRepo.Object, mockProfRepo.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.AddAsync(dto));
            Assert.Contains("Ya existe una especialidad con el nombre", ex.Message);
        }

        [Fact]
        public async Task UpdateAsync_NombreDuplicadoEnOtraEspecialidad_LanzaArgumentException()
        {
            // Arrange
            var dto = new EspecialidadDTO
            {
                Id = 1,
                Nombre = "Pediatría",
                Precio = 18000m
            };

            var mockRepo = new Mock<IEspecialidadRepository>();
            mockRepo.Setup(r => r.ExistsByNameAsync("Pediatría", 1)).ReturnsAsync(true);

            var mockProfRepo = new Mock<IProfesionalRepository>();

            var service = new EspecialidadService(mockRepo.Object, mockProfRepo.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.UpdateAsync(dto));
            Assert.Contains("Ya existe otra especialidad con el nombre", ex.Message);
        }

        [Fact]
        public async Task DeleteAsync_ConProfesionalesAsociados_LanzaInvalidOperationException()
        {
            // Arrange
            var mockRepo = new Mock<IEspecialidadRepository>();
            var mockProfRepo = new Mock<IProfesionalRepository>();
            mockProfRepo.Setup(r => r.ExistsWithEspecialidadAsync(5)).ReturnsAsync(true);

            var service = new EspecialidadService(mockRepo.Object, mockProfRepo.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => service.DeleteAsync(5));
            Assert.Contains("No se puede eliminar la especialidad porque tiene profesionales asociados", ex.Message);
        }

        [Fact]
        public async Task DeleteAsync_SinProfesionales_RetornaTrue()
        {
            // Arrange
            var mockRepo = new Mock<IEspecialidadRepository>();
            mockRepo.Setup(r => r.DeleteAsync(5)).ReturnsAsync(true);

            var mockProfRepo = new Mock<IProfesionalRepository>();
            mockProfRepo.Setup(r => r.ExistsWithEspecialidadAsync(5)).ReturnsAsync(false);

            var service = new EspecialidadService(mockRepo.Object, mockProfRepo.Object);

            // Act
            var result = await service.DeleteAsync(5);

            // Assert
            Assert.True(result);
        }
    }
}

