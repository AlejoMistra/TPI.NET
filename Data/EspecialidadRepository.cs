using Domain.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Data;

public class EspecialidadRepository : IEspecialidadRepository
{
    private readonly string _connectionString;

    public EspecialidadRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Cadena de conexión 'DefaultConnection' no encontrada en la configuración.");
    }

    public async Task<IEnumerable<Especialidad>> GetAllAsync()
    {
        var result = new List<Especialidad>();
        const string query = "SELECT Id, Nombre, Precio FROM Especialidades ORDER BY Nombre";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(query, connection);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var id = reader.GetInt32(0);
            var nombre = reader.GetString(1);
            var precio = !reader.IsDBNull(2) ? reader.GetDecimal(2) : 0m;
            result.Add(new Especialidad(id, nombre, precio));
        }

        return result;
    }

    public async Task<Especialidad?> GetByIdAsync(int id)
    {
        const string query = "SELECT Id, Nombre, Precio FROM Especialidades WHERE Id = @Id";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(query, connection);
        command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            var nombre = reader.GetString(1);
            var precio = !reader.IsDBNull(2) ? reader.GetDecimal(2) : 0m;
            return new Especialidad(id, nombre, precio);
        }

        return null;
    }

    public async Task AddAsync(Especialidad especialidad)
    {
        const string query = @"
            INSERT INTO Especialidades (Nombre, Precio) 
            VALUES (@Nombre, @Precio);
            SELECT CAST(SCOPE_IDENTITY() AS int);";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(query, connection);
        command.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100).Value = especialidad.Nombre;
        command.Parameters.Add("@Precio", SqlDbType.Decimal).Value = especialidad.Precio;

        await connection.OpenAsync();
        var newId = (int)(await command.ExecuteScalarAsync())!;
        especialidad.SetId(newId);
    }

    public async Task<Especialidad?> UpdateAsync(Especialidad especialidad)
    {
        const string query = "UPDATE Especialidades SET Nombre = @Nombre, Precio = @Precio WHERE Id = @Id";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(query, connection);
        command.Parameters.Add("@Id", SqlDbType.Int).Value = especialidad.Id;
        command.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100).Value = especialidad.Nombre;
        command.Parameters.Add("@Precio", SqlDbType.Decimal).Value = especialidad.Precio;

        await connection.OpenAsync();
        var rowsAffected = await command.ExecuteNonQueryAsync();

        if (rowsAffected == 0)
            return null;

        return especialidad;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        const string query = "DELETE FROM Especialidades WHERE Id = @Id";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(query, connection);
        command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

        try
        {
            await connection.OpenAsync();
            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            // Error 547: Foreign Key restriction (hay registros que referencian esta Especialidad)
            return false;
        }
    }

    public async Task<bool> ExistsByNameAsync(string nombre, int? excludeId = null)
    {
        var query = excludeId.HasValue
            ? "SELECT COUNT(1) FROM Especialidades WHERE LOWER(Nombre) = LOWER(@Nombre) AND Id != @ExcludeId"
            : "SELECT COUNT(1) FROM Especialidades WHERE LOWER(Nombre) = LOWER(@Nombre)";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(query, connection);
        command.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100).Value = nombre.Trim();
        if (excludeId.HasValue)
        {
            command.Parameters.Add("@ExcludeId", SqlDbType.Int).Value = excludeId.Value;
        }

        await connection.OpenAsync();
        var count = (int)(await command.ExecuteScalarAsync() ?? 0);
        return count > 0;
    }
}