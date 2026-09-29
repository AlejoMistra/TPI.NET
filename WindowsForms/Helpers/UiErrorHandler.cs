using API.Clients.Exceptions;

namespace WindowsForms.Helpers
{
    public static class UiErrorHandler
    {
        /// <summary>
        /// Ejecuta una acción asincrónica de forma segura, gestionando el cursor de espera,
        /// deshabilitando temporalmente el control para evitar doble clic y capturando
        /// excepciones de la API para mostrar mensajes amigables al usuario.
        /// </summary>
        /// <param name="action">La función asincrónica a ejecutar.</param>
        /// <param name="context">Control o Formulario que origina la acción (opcional).</param>
        /// <param name="errorTitle">Título por defecto para cuadros de diálogo de error inesperado.</param>
        /// <returns>True si la ejecución fue exitosa; False si ocurrió una excepción capturada.</returns>
        public static async Task<bool> ExecuteAsync(
            Func<Task> action,
            Control? context = null,
            string? errorTitle = "Error")
        {
            Cursor? originalCursor = null;
            bool wasEnabled = true;

            try
            {
                if (context != null)
                {
                    originalCursor = context.Cursor;
                    wasEnabled = context.Enabled;
                    context.Cursor = Cursors.WaitCursor;
                    context.Enabled = false;
                }

                await action();
                return true;
            }
            catch (ValidationApiException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    !string.IsNullOrWhiteSpace(ex.Title) ? ex.Title : "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            catch (ConflictApiException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    !string.IsNullOrWhiteSpace(ex.Title) ? ex.Title : "Conflicto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            catch (NotFoundApiException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    !string.IsNullOrWhiteSpace(ex.Title) ? ex.Title : "No encontrado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return false;
            }
            catch (UnauthorizedApiException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Sesión Expirada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            catch (NetworkApiException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error de Conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ocurrió un error inesperado al realizar la operación:\n\n{ex.Message}",
                    errorTitle ?? "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (context != null)
                {
                    context.Cursor = originalCursor ?? Cursors.Default;
                    context.Enabled = wasEnabled;
                }
            }
        }

        /// <summary>
        /// Ejecuta una función asincrónica que retorna un valor de forma segura.
        /// </summary>
        public static async Task<(bool success, T? result)> ExecuteAsync<T>(
            Func<Task<T>> action,
            Control? context = null,
            string? errorTitle = "Error")
        {
            T? result = default;
            bool ok = await ExecuteAsync(async () =>
            {
                result = await action();
            }, context, errorTitle);

            return (ok, result);
        }
    }
}
