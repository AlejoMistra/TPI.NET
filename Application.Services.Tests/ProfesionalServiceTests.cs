using Application.Services;
using Data;
using Domain.Model;
using DTOs;
using Moq;
using Xunit;

namespace Application.Services.Tests
{
    public class ProfesionalServiceTests
    {
        [Fact]
        public async Task AddAsync_DatosValidos_RegistraProfesionalYRetornaDTO()
        {
            // Arrange
            var dto = new ProfesionalDTO
            {
                Nombre = "Mariana",
                Apellido = "Díaz",
                TipoDocumento = "DNI",
                NroDocumento = "30123456",
                Matricula = "MP-5555",
                EspecialidadId = 1,
                Telefono = "011-4455-6677",
                Email = "mariana.diaz@clinica.com",
                Estado = "Activo"
            };

            var mockProfRepo = new Mock<IProfesionalRepository>();
            mockProfRepo.Setup(r => r.MatriculaExistsAsync("MP-5555", null)).ReturnsAsync(false);
            mockProfRepo.Setup(r => r.DocumentoExistsAsync("30123456", null)).ReturnsAsync(false);
            mockProfRepo.Setup(r => r.EmailExistsAsync("mariana.diaz@clinica.com", null)).ReturnsAsync(false);
            mockProfRepo.Setup(r => r.AddAsync(It.IsAny<Profesional>()))
                        .Callback<Profesional>(p => p.SetId(101))
                        .Returns(Task.CompletedTask);

            var mockEspRepo = new Mock<IEspecialidadRepository>();
            mockEspRepo.Setup(r => r.GetByIdAsync(1))
                       .ReturnsAsync(new Especialidad(1, "Clínica Médica", 20000m));

            var service = new ProfesionalService(mockProfRepo.Object, mockEspRepo.Object);

            // Act
            var result = await service.AddAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(101, result.Id);
            Assert.Equal("Mariana", result.Nombre);
            Assert.Equal("Díaz", result.Apellido);
            Assert.Equal("MP-5555", result.Matricula);
            Assert.Equal(1, result.EspecialidadId);
            Assert.Equal("Activo", result.Estado);
        }

        [Fact]
        public async Task AddAsync_EspecialidadInexistente_LanzaArgumentException()
        {
            // Arrange
            var dto = new ProfesionalDTO
            {
                Nombre = "Mariana",
                Apellido = "Díaz",
                TipoDocumento = "DNI",
                NroDocumento = "30123456",
                Matricula = "MP-5555",
                EspecialidadId = 999
            };

            var mockProfRepo = new Mock<IProfesionalRepository>();
            var mockEspRepo = new Mock<IEspecialidadRepository>();
            mockEspRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Especialidad?)null);

            var service = new ProfesionalService(mockProfRepo.Object, mockEspRepo.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.AddAsync(dto));
            Assert.Contains("La especialidad con ID 999 no existe", ex.Message);
        }

        [Fact]
        public async Task AddAsync_MatriculaDuplicada_LanzaArgumentException()
        {
            // Arrange
            var dto = new ProfesionalDTO
            {
                Nombre = "Mariana",
                Apellido = "Díaz",
                TipoDocumento = "DNI",
                NroDocumento = "30123456",
                Matricula = "MP-1001",
                EspecialidadId = 1
            };

            var mockProfRepo = new Mock<IProfesionalRepository>();
            mockProfRepo.Setup(r => r.MatriculaExistsAsync("MP-1001", null)).ReturnsAsync(true);

            var mockEspRepo = new Mock<IEspecialidadRepository>();
            mockEspRepo.Setup(r => r.GetByIdAsync(1))
                       .ReturnsAsync(new Especialidad(1, "Clínica", 1000m));

            var service = new ProfesionalService(mockProfRepo.Object, mockEspRepo.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.AddAsync(dto));
            Assert.Contains("Ya existe un profesional registrado con la matrícula ingresada", ex.Message);
        }

        [Fact]
        public async Task AddAsync_DocumentoDuplicado_LanzaArgumentException()
        {
            // Arrange
            var dto = new ProfesionalDTO
            {
                Nombre = "Mariana",
                Apellido = "Díaz",
                TipoDocumento = "DNI",
                NroDocumento = "30123456",
                Matricula = "MP-5555",
                EspecialidadId = 1
            };

            var mockProfRepo = new Mock<IProfesionalRepository>();
            mockProfRepo.Setup(r => r.MatriculaExistsAsync("MP-5555", null)).ReturnsAsync(false);
            mockProfRepo.Setup(r => r.DocumentoExistsAsync("30123456", null)).ReturnsAsync(true);

            var mockEspRepo = new Mock<IEspecialidadRepository>();
            mockEspRepo.Setup(r => r.GetByIdAsync(1))
                       .ReturnsAsync(new Especialidad(1, "Clínica", 1000m));

            var service = new ProfesionalService(mockProfRepo.Object, mockEspRepo.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.AddAsync(dto));
            Assert.Contains("Ya existe un profesional registrado con el número de documento ingresado", ex.Message);
        }

        [Fact]
        public async Task AddAsync_EmailDuplicado_LanzaArgumentException()
        {
            // Arrange
            var dto = new ProfesionalDTO
            {
                Nombre = "Mariana",
                Apellido = "Díaz",
                TipoDocumento = "DNI",
                NroDocumento = "30123456",
                Matricula = "MP-5555",
                EspecialidadId = 1,
                Email = "repetido@clinica.com"
            };

            var mockProfRepo = new Mock<IProfesionalRepository>();
            mockProfRepo.Setup(r => r.MatriculaExistsAsync("MP-5555", null)).ReturnsAsync(false);
            mockProfRepo.Setup(r => r.DocumentoExistsAsync("30123456", null)).ReturnsAsync(false);
            mockProfRepo.Setup(r => r.EmailExistsAsync("repetido@clinica.com", null)).ReturnsAsync(true);

            var mockEspRepo = new Mock<IEspecialidadRepository>();
            mockEspRepo.Setup(r => r.GetByIdAsync(1))
                       .ReturnsAsync(new Especialidad(1, "Clínica", 1000m));

            var service = new ProfesionalService(mockProfRepo.Object, mockEspRepo.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.AddAsync(dto));
            Assert.Contains("Ya existe un profesional registrado con el correo electrónico ingresado", ex.Message);
        }

        [Fact]
        public async Task DeleteAsync_ConTurnosAsociados_LanzaInvalidOperationException()
        {
            // Arrange
            var mockProfRepo = new Mock<IProfesionalRepository>();
            mockProfRepo.Setup(r => r.HasTurnosAsync(2)).ReturnsAsync(true);

            var mockEspRepo = new Mock<IEspecialidadRepository>();

            var service = new ProfesionalService(mockProfRepo.Object, mockEspRepo.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => service.DeleteAsync(2));
            Assert.Contains("No se puede eliminar el profesional porque tiene turnos asociados", ex.Message);
        }

        [Fact]
        public async Task DeleteAsync_ConRegistrosClinicos_LanzaInvalidOperationException()
        {
            // Arrange
            var mockProfRepo = new Mock<IProfesionalRepository>();
            mockProfRepo.Setup(r => r.HasTurnosAsync(2)).ReturnsAsync(false);
            mockProfRepo.Setup(r => r.HasRegistrosClinicosAsync(2)).ReturnsAsync(true);

            var mockEspRepo = new Mock<IEspecialidadRepository>();

            var service = new ProfesionalService(mockProfRepo.Object, mockEspRepo.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => service.DeleteAsync(2));
            Assert.Contains("No se puede eliminar el profesional porque tiene registros clínicos asociados", ex.Message);
        }

        [Fact]
        public async Task DeleteAsync_SinDependencias_RetornaTrue()
        {
            // Arrange
            var mockProfRepo = new Mock<IProfesionalRepository>();
            mockProfRepo.Setup(r => r.HasTurnosAsync(2)).ReturnsAsync(false);
            mockProfRepo.Setup(r => r.HasRegistrosClinicosAsync(2)).ReturnsAsync(false);
            mockProfRepo.Setup(r => r.DeleteAsync(2)).ReturnsAsync(true);

            var mockEspRepo = new Mock<IEspecialidadRepository>();

            var service = new ProfesionalService(mockProfRepo.Object, mockEspRepo.Object);

            // Act
            var result = await service.DeleteAsync(2);

            // Assert
            Assert.True(result);
        }
    }
}

