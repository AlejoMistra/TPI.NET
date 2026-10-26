using Application.Services;
using Data;
using Domain.Model;
using DTOs;
using Moq;
using Xunit;

namespace Application.Services.Tests
{
    public class PacienteServiceTests
    {
        [Fact]
        public async Task AddAsync_DatosValidos_RegistraPacienteYRetornaDTO()
        {
            // Arrange
            var dto = new PacienteDTO
            {
                Nombre = "Lucía",
                Apellido = "Méndez",
                TipoDocumento = "DNI",
                NroDocumento = "38123456",
                FechaNacimiento = new DateTime(1995, 3, 20),
                ObraSocial = "Swiss Medical",
                Telefono = "011-9988-7766",
                Email = "lucia@test.com"
            };

            var mockRepo = new Mock<IPacienteRepository>();
            mockRepo.Setup(r => r.DocumentoExistsAsync("38123456", null)).ReturnsAsync(false);
            mockRepo.Setup(r => r.EmailExistsAsync("lucia@test.com", null)).ReturnsAsync(false);
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Paciente>()))
                    .Callback<Paciente>(p => p.SetId(50))
                    .Returns(Task.CompletedTask);

            var service = new PacienteService(mockRepo.Object);

            // Act
            var result = await service.AddAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(50, result.Id);
            Assert.Equal("Lucía", result.Nombre);
            Assert.Equal("Méndez", result.Apellido);
            Assert.Equal("38123456", result.NroDocumento);
            Assert.Equal("Swiss Medical", result.ObraSocial);
        }

        [Fact]
        public async Task AddAsync_DocumentoDuplicado_LanzaArgumentException()
        {
            // Arrange
            var dto = new PacienteDTO
            {
                Nombre = "Lucía",
                Apellido = "Méndez",
                TipoDocumento = "DNI",
                NroDocumento = "38123456",
                FechaNacimiento = new DateTime(1995, 3, 20)
            };

            var mockRepo = new Mock<IPacienteRepository>();
            mockRepo.Setup(r => r.DocumentoExistsAsync("38123456", null)).ReturnsAsync(true);

            var service = new PacienteService(mockRepo.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.AddAsync(dto));
            Assert.Contains("Ya existe un paciente registrado con el número de documento ingresado", ex.Message);
        }

        [Fact]
        public async Task AddAsync_EmailDuplicado_LanzaArgumentException()
        {
            // Arrange
            var dto = new PacienteDTO
            {
                Nombre = "Lucía",
                Apellido = "Méndez",
                TipoDocumento = "DNI",
                NroDocumento = "38123456",
                FechaNacimiento = new DateTime(1995, 3, 20),
                Email = "duplicado@test.com"
            };

            var mockRepo = new Mock<IPacienteRepository>();
            mockRepo.Setup(r => r.DocumentoExistsAsync("38123456", null)).ReturnsAsync(false);
            mockRepo.Setup(r => r.EmailExistsAsync("duplicado@test.com", null)).ReturnsAsync(true);

            var service = new PacienteService(mockRepo.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.AddAsync(dto));
            Assert.Contains("Ya existe un paciente con el correo electrónico ingresado", ex.Message);
        }

        [Fact]
        public async Task DeleteAsync_ConTurnosAsociados_LanzaInvalidOperationException()
        {
            // Arrange
            var mockRepo = new Mock<IPacienteRepository>();
            mockRepo.Setup(r => r.HasTurnosAsync(10)).ReturnsAsync(true);

            var service = new PacienteService(mockRepo.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => service.DeleteAsync(10));
            Assert.Contains("No se puede eliminar el paciente porque tiene turnos asociados", ex.Message);
        }

        [Fact]
        public async Task DeleteAsync_SinTurnos_RetornaTrue()
        {
            // Arrange
            var mockRepo = new Mock<IPacienteRepository>();
            mockRepo.Setup(r => r.HasTurnosAsync(10)).ReturnsAsync(false);
            mockRepo.Setup(r => r.DeleteAsync(10)).ReturnsAsync(true);

            var service = new PacienteService(mockRepo.Object);

            // Act
            var result = await service.DeleteAsync(10);

            // Assert
            Assert.True(result);
        }
    }
}

