using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Usuario
    {
        public enum Roles
        {
            Administrativo,
            Paciente,
            Profesional
        }

        public enum Estados //ver de usar Activo, y tome como valor un booleano
        {
            Activo,
            Inactivo
        }

        public int Id { get; private set; }

        public string Username { get; private set; } = string.Empty;

        public string Email { get; private set; } = string.Empty;

        public string PasswordHash { get; private set; } = string.Empty;

        public string Salt { get; private set; }


        public DateTime FechaCreacion {  get; private set; }= DateTime.Now;

        public Roles Rol { get; private set; }

        public bool Activo { get; private set; }

        public int PersonaId { get; private set; }
        public Persona Persona { get; private set; } = null!;

        public Usuario(int Id, string username, string email, string password, DateTime fechaCreacion, Roles rol, Estados estado)
        {
            SetId(Id);
            SetUsername(username);
            SetEmail(Email);
            SetPassword(password);
            SetFechaCreacion(fechaCreacion);
            SetRol(Rol);
            SetActivo(Activo);
        }

        private Usuario() { }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id debe ser mayor que 0.", nameof(id));
            Id = id;
        }

        public void SetUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("El nombre de usuario no puede ser nulo o vacío.", nameof(username));

            if (username.Length < 5)
                throw new ArgumentException("El nombre de usuario debe tener al menos 5 caracteres.", nameof(username));

            Username = username;
        }

        public void SetEmail(string email)
        {
            if (!EsEmailValido(email))
                throw new ArgumentException("El email no tiene un formato válido.", nameof(email));
            Email = email;
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La contraseña no puede ser nula o vacía.", nameof(password));

            if (password.Length < 6)
                throw new ArgumentException("La contraseña debe tener al menos 6 caracteres.", nameof(password));

            Salt = GenerateSalt();
            PasswordHash = HashPassword(password, Salt);
        }

        public void SetFechaCreacion(DateTime fechaCreacion)
        {
            if (fechaCreacion == default)
                throw new ArgumentException("La fecha de creación no puede ser nula.", nameof(fechaCreacion));
            FechaCreacion = fechaCreacion;
        }

        public void SetRol (Roles Rol)
        {
            this.Rol = Rol;
        }

        public void SetActivo(bool activo)
        {
            Activo = activo;
        }

        public bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            string hashedInput = HashPassword(password, Salt);
            return PasswordHash == hashedInput;
        }

        private static bool EsEmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private static string GenerateSalt()
        {
            byte[] saltBytes = new byte[32];
            RandomNumberGenerator.Fill(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        private static string HashPassword(string password, string salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 10000, HashAlgorithmName.SHA256);
            byte[] hashBytes = pbkdf2.GetBytes(32);
            return Convert.ToBase64String(hashBytes);
        }


    }
}
