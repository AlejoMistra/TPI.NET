using Domain.Model;
using Xunit;

namespace Application.Services.Tests
{
    public class DomainModelTests
    {
        // ─────────────────────────────────────────────────────────────────
        // Persona / TestPersona (subclase concreta para testear Persona)
        // ─────────────────────────────────────────────────────────────────
        private class TestPersona : Persona
        {
            public TestPersona(
                string nombre,
                string apellido,
                string tipoDocumento,
                string nroDocumento,
                string? telefono = null,
                string? email = null)
                : base(nombre, apellido, tipoDocumento, nroDocumento, telefono, email)
            {
            }
        }

        [Fact]
        public void Persona_ConDatosValidos_SeInstanciaCorrectamente()
        {
            var persona = new TestPersona("Juan", "Pérez", "DNI", "12345678", "011-1234-5678", "juan@test.com");

            Assert.Equal("Juan", persona.Nombre);
            Assert.Equal("Pérez", persona.Apellido);
            Assert.Equal("DNI", persona.TipoDocumento);
            Assert.Equal("12345678", persona.NroDocumento);
            Assert.Equal("011-1234-5678", persona.Telefono);
            Assert.Equal("juan@test.com", persona.Email);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Persona_NombreInvalido_LanzaArgumentException(string? nombreInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
                new TestPersona(nombreInvalido!, "Pérez", "DNI", "12345678"));
        }

        [Fact]
        public void Persona_NombreMayorA100Caracteres_LanzaArgumentException()
        {
            var largo = new string('A', 101);
            Assert.Throws<ArgumentException>(() =>
                new TestPersona(largo, "Pérez", "DNI", "12345678"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Persona_ApellidoInvalido_LanzaArgumentException(string? apellidoInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
                new TestPersona("Juan", apellidoInvalido!, "DNI", "12345678"));
        }

        [Fact]
        public void Persona_TipoDocumentoInvalido_LanzaArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new TestPersona("Juan", "Pérez", "TipoInexistente", "12345678"));
        }

        [Theory]
        [InlineData("123")]         // Muy corto para DNI
        [InlineData("123456789")]   // Muy largo para DNI
        [InlineData("1234567A")]   // No numérico
        public void Persona_DniFormatoInvalido_LanzaArgumentException(string dniInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
                new TestPersona("Juan", "Pérez", "DNI", dniInvalido));
        }

        [Fact]
        public void Persona_EmailFormatoInvalido_LanzaArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new TestPersona("Juan", "Pérez", "DNI", "12345678", email: "email-invalido-sin-arroba"));
        }

        [Fact]
        public void Persona_SetIdNegativo_LanzaArgumentException()
        {
            var persona = new TestPersona("Juan", "Pérez", "DNI", "12345678");
            Assert.Throws<ArgumentException>(() => persona.SetId(-1));
        }

        // ─────────────────────────────────────────────────────────────────
        // Paciente
        // ─────────────────────────────────────────────────────────────────

        [Fact]
        public void Paciente_ConDatosValidos_SeInstanciaCorrectamente()
        {
            var fechaNac = new DateTime(1990, 5, 15);
            var paciente = new Paciente("Ana", "Gómez", "DNI", "35123456", fechaNac, "OSDE", "11223344", "ana@test.com");

            Assert.Equal("Ana", paciente.Nombre);
            Assert.Equal("Gómez", paciente.Apellido);
            Assert.Equal("35123456", paciente.NroDocumento);
            Assert.Equal(fechaNac.Date, paciente.FechaNacimiento);
            Assert.Equal("OSDE", paciente.ObraSocial);
            Assert.NotNull(paciente.Turnos);
        }

        [Fact]
        public void Paciente_FechaNacimientoEnElFuturo_LanzaArgumentException()
        {
            var fechaFutura = DateTime.Today.AddDays(1);
            Assert.Throws<ArgumentException>(() =>
                new Paciente("Ana", "Gómez", "DNI", "35123456", fechaFutura));
        }

        [Fact]
        public void Paciente_FechaNacimientoAnteriorA1900_LanzaArgumentException()
        {
            var fechaAntigua = new DateTime(1899, 12, 31);
            Assert.Throws<ArgumentException>(() =>
                new Paciente("Ana", "Gómez", "DNI", "35123456", fechaAntigua));
        }

        [Fact]
        public void Paciente_ObraSocialMayorA100_LanzaArgumentException()
        {
            var largo = new string('O', 101);
            Assert.Throws<ArgumentException>(() =>
                new Paciente("Ana", "Gómez", "DNI", "35123456", new DateTime(1990, 1, 1), largo));
        }

        [Fact]
        public void Paciente_ObraSocialVacia_NormalizaAStringVacio()
        {
            var paciente = new Paciente("Ana", "Gómez", "DNI", "35123456", new DateTime(1990, 1, 1), null!);
            Assert.Equal(string.Empty, paciente.ObraSocial);
        }

        // ─────────────────────────────────────────────────────────────────
        // Profesional
        // ─────────────────────────────────────────────────────────────────

        [Fact]
        public void Profesional_ConDatosValidos_SeInstanciaCorrectamente()
        {
            var profesional = new Profesional(
                "Carlos", "Ruiz", "DNI", "25123456", "MN-12345", 2, "11445566", "carlos@test.com", Profesional.EstadoProfesional.Activo);

            Assert.Equal("Carlos", profesional.Nombre);
            Assert.Equal("MN-12345", profesional.Matricula);
            Assert.Equal(2, profesional.EspecialidadId);
            Assert.Equal(Profesional.EstadoProfesional.Activo, profesional.Estado);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Profesional_MatriculaInvalida_LanzaArgumentException(string? matriculaInvalida)
        {
            Assert.Throws<ArgumentException>(() =>
                new Profesional("Carlos", "Ruiz", "DNI", "25123456", matriculaInvalida!, 2));
        }

        [Fact]
        public void Profesional_EspecialidadIdMenorOIgualACero_LanzaArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Profesional("Carlos", "Ruiz", "DNI", "25123456", "MN-12345", 0));
        }

        [Fact]
        public void Profesional_SetEstadoInvalido_LanzaArgumentException()
        {
            var profesional = new Profesional("Carlos", "Ruiz", "DNI", "25123456", "MN-12345", 2);
            Assert.Throws<ArgumentException>(() => profesional.SetEstado((Profesional.EstadoProfesional)999));
        }

        // ─────────────────────────────────────────────────────────────────
        // Especialidad
        // ─────────────────────────────────────────────────────────────────

        [Fact]
        public void Especialidad_ConDatosValidos_SeInstanciaCorrectamente()
        {
            var esp = new Especialidad(1, "Cardiología", 25000m);

            Assert.Equal(1, esp.Id);
            Assert.Equal("Cardiología", esp.Nombre);
            Assert.Equal(25000m, esp.Precio);
        }

        [Fact]
        public void Especialidad_PrecioNegativo_LanzaArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Especialidad(1, "Cardiología", -100m));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Especialidad_NombreVacio_LanzaArgumentException(string? nombreInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
                new Especialidad(1, nombreInvalido!, 1000m));
        }

        [Fact]
        public void Especialidad_SetPrecioNegativo_LanzaArgumentException()
        {
            var esp = new Especialidad(1, "Cardiología", 1000m);
            Assert.Throws<ArgumentException>(() => esp.SetPrecio(-50m));
        }
    }
}

