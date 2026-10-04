using API.Clients;
using DTOs;
using System.ComponentModel;
using System.Globalization;
using WindowsForms.Helpers;

namespace WindowsForms
{
    public partial class FacturaModalForm : Form
    {
        private readonly int _turnoId;
        private TurnoDTO? _turno;
        private FacturaDTO? _facturaExistente;
        private string _especialidadNombre = "Consulta General";
        private bool _modoSoloLectura = false;

        private readonly BindingList<DetalleItemRow> _detalles = new();

        public FacturaModalForm(int turnoId)
        {
            _turnoId = turnoId;
            InitializeComponent();
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            // Poblar ComboBox de Métodos de Pago
            var metodos = new List<MetodoPagoOption>
            {
                new() { Value = "Efectivo", Display = "Efectivo" },
                new() { Value = "TarjetaDebito", Display = "Tarjeta de Débito" },
                new() { Value = "TarjetaCredito", Display = "Tarjeta de Crédito" },
                new() { Value = "TransferenciaBancaria", Display = "Transferencia Bancaria" }
            };
            metodoPagoComboBox.DataSource = metodos;
            metodoPagoComboBox.DisplayMember = nameof(MetodoPagoOption.Display);
            metodoPagoComboBox.ValueMember = nameof(MetodoPagoOption.Value);
            metodoPagoComboBox.SelectedIndex = 0;

            ConfigurarGrillaDetalles();

            this.Load += FacturaModalForm_Load;
            agregarItemButton.Click += AgregarItemButton_Click;
            confirmarButton.Click += ConfirmarButton_Click;
            anularButton.Click += AnularButton_Click;
            cancelarButton.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            nuevoConceptoTextBox.KeyDown += NuevoItemInput_KeyDown;
            this.KeyPreview = true;
            this.KeyDown += FacturaModalForm_KeyDown;
        }

        private void ConfigurarGrillaDetalles()
        {
            detallesDataGridView.AutoGenerateColumns = false;
            detallesDataGridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
            detallesDataGridView.MultiSelect = false;
            detallesDataGridView.RowHeadersVisible = false;
            detallesDataGridView.AllowUserToAddRows = false;
            detallesDataGridView.AllowUserToDeleteRows = false;
            detallesDataGridView.EnableHeadersVisualStyles = false;
            detallesDataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = detallesDataGridView.ColumnHeadersDefaultCellStyle.BackColor;
            detallesDataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = detallesDataGridView.ColumnHeadersDefaultCellStyle.ForeColor;

            detallesDataGridView.Columns.Clear();

            detallesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "conceptoColumn",
                HeaderText = "Concepto / Descripción",
                DataPropertyName = nameof(DetalleItemRow.Concepto),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = false
            });

            detallesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "cantidadColumn",
                HeaderText = "Cantidad",
                DataPropertyName = nameof(DetalleItemRow.Cantidad),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter },
                ReadOnly = false
            });

            detallesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "precioUnitarioColumn",
                HeaderText = "Precio Unitario ($)",
                DataPropertyName = nameof(DetalleItemRow.PrecioUnitario),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Format = "N2"
                },
                ReadOnly = false
            });

            detallesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "subtotalColumn",
                HeaderText = "Subtotal ($)",
                DataPropertyName = nameof(DetalleItemRow.Subtotal),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Format = "N2",
                    BackColor = Color.FromArgb(245, 245, 245),
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                },
                ReadOnly = true
            });

            detallesDataGridView.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "quitarColumn",
                HeaderText = "",
                Text = "Quitar",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            });

            detallesDataGridView.DataSource = _detalles;

            detallesDataGridView.CellContentClick += DetallesDataGridView_CellContentClick;
            detallesDataGridView.CellValidating += DetallesDataGridView_CellValidating;
            detallesDataGridView.CellEndEdit += DetallesDataGridView_CellEndEdit;
            detallesDataGridView.DataError += (s, e) =>
            {
                e.ThrowException = false;
                MessageBox.Show("Ingrese un valor numérico válido para Cantidad o Precio Unitario.", "Valor Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            };
        }

        private async void FacturaModalForm_Load(object? sender, EventArgs e)
        {
            bool ok = await UiErrorHandler.ExecuteAsync(async () =>
            {
                _turno = await TurnoApiClient.GetAsync(_turnoId);
                if (_turno == null)
                {
                    MessageBox.Show("No se encontró el turno seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }

                var profesionales = await ProfesionalApiClient.GetAllAsync();
                var especialidades = await EspecialidadApiClient.GetAllAsync();
                var pacientes = await PacienteApiClient.GetAllAsync();

                var prof = profesionales.FirstOrDefault(p => p.Id == _turno.ProfesionalId);
                var esp = prof != null ? especialidades.FirstOrDefault(es => es.Id == prof.EspecialidadId) : null;
                var pac = _turno.PacienteId.HasValue ? pacientes.FirstOrDefault(pa => pa.Id == _turno.PacienteId.Value) : null;

                _especialidadNombre = esp?.Nombre ?? "Consulta General";

                turnoIdValueLabel.Text = $"#{_turno.Id}";
                fechaHoraValueLabel.Text = $"{_turno.FechaHoraInicio:dd/MM/yyyy} ({_turno.FechaHoraInicio:HH:mm} - {_turno.FechaHoraFin:HH:mm})";
                estadoTurnoValueLabel.Text = _turno.EstadoTurno;
                pacienteValueLabel.Text = pac != null ? $"{pac.Apellido}, {pac.Nombre} (DNI: {pac.NroDocumento})" : "— Sin Paciente —";
                obraSocialValueLabel.Text = pac != null && !string.IsNullOrWhiteSpace(pac.ObraSocial) ? pac.ObraSocial : "Particular / Sin Obra Social";
                profesionalValueLabel.Text = prof != null ? $"{prof.Apellido}, {prof.Nombre} ({_especialidadNombre})" : $"ID {_turno.ProfesionalId}";

                if (_turno.FacturaId.HasValue)
                {
                    _facturaExistente = await FacturaApiClient.GetAsync(_turno.FacturaId.Value);
                    ConfigurarModoConsulta();
                }
                else
                {
                    ConfigurarModoEmision();
                }
            }, this, "Error al cargar datos de facturación");

            if (!ok && _turno == null)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void ConfigurarModoEmision()
        {
            _modoSoloLectura = false;
            this.Text = $"Emitir Factura - Turno N° {_turno!.Id}";

            bannerLabel.BackColor = Color.FromArgb(235, 243, 255);
            bannerLabel.ForeColor = Color.FromArgb(0, 102, 204);
            bannerLabel.Text = "Emisión Maestro/Detalle: ingrese el Precio Unitario (> $0) en la grilla y sume otros conceptos si corresponde.";

            nroFacturaValueLabel.Text = "Nueva (Al emitir)";
            fechaEmisionValueLabel.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            estadoFacturaValueLabel.Text = "Pagada";
            estadoFacturaValueLabel.ForeColor = Color.FromArgb(40, 120, 60);

            metodoPagoComboBox.Enabled = true;
            agregarItemPanel.Visible = true;
            confirmarButton.Visible = true;
            anularButton.Visible = false;
            cancelarButton.Text = "Cancelar";

            detallesDataGridView.Columns["conceptoColumn"].ReadOnly = false;
            detallesDataGridView.Columns["cantidadColumn"].ReadOnly = false;
            detallesDataGridView.Columns["precioUnitarioColumn"].ReadOnly = false;
            detallesDataGridView.Columns["quitarColumn"].Visible = true;

            _detalles.Clear();
            _detalles.Add(new DetalleItemRow
            {
                Concepto = $"Consulta Médica - {_especialidadNombre}",
                Cantidad = 1,
                PrecioUnitario = 0.00m
            });

            ActualizarTotal();

            // Posicionar el foco en la celda Precio Unitario de la primera fila precargada
            BeginInvoke(new Action(() =>
            {
                if (detallesDataGridView.Rows.Count > 0)
                {
                    var precioCell = detallesDataGridView.Rows[0].Cells["precioUnitarioColumn"];
                    detallesDataGridView.CurrentCell = precioCell;
                    detallesDataGridView.BeginEdit(true);
                }
            }));
        }

        private void ConfigurarModoConsulta()
        {
            _modoSoloLectura = true;
            var f = _facturaExistente!;
            this.Text = $"Detalle de Factura N° {f.Id} - Turno N° {_turno!.Id}";

            bool esCancelada = string.Equals(f.EstadoFactura, "Cancelada", StringComparison.OrdinalIgnoreCase);

            if (esCancelada)
            {
                bannerLabel.BackColor = Color.FromArgb(255, 235, 235);
                bannerLabel.ForeColor = Color.DarkRed;
                bannerLabel.Text = $"Factura N° {f.Id} ANULADA (Cancelada).";
                estadoFacturaValueLabel.ForeColor = Color.DarkRed;
            }
            else
            {
                bannerLabel.BackColor = Color.FromArgb(235, 247, 238);
                bannerLabel.ForeColor = Color.FromArgb(40, 120, 60);
                bannerLabel.Text = $"Comprobante emitido: Factura N° {f.Id} registrada el {f.FechaEmision:dd/MM/yyyy HH:mm}.";
                estadoFacturaValueLabel.ForeColor = Color.FromArgb(40, 120, 60);
            }

            nroFacturaValueLabel.Text = $"#{f.Id:D5}";
            fechaEmisionValueLabel.Text = f.FechaEmision.ToString("dd/MM/yyyy HH:mm");
            estadoFacturaValueLabel.Text = f.EstadoFactura;

            metodoPagoComboBox.SelectedValue = f.MetodoPago;
            metodoPagoComboBox.Enabled = false;

            agregarItemPanel.Visible = false;
            confirmarButton.Visible = false;
            anularButton.Visible = !esCancelada;
            cancelarButton.Text = "Cerrar";

            detallesDataGridView.Columns["conceptoColumn"].ReadOnly = true;
            detallesDataGridView.Columns["cantidadColumn"].ReadOnly = true;
            detallesDataGridView.Columns["precioUnitarioColumn"].ReadOnly = true;
            detallesDataGridView.Columns["quitarColumn"].Visible = false;

            _detalles.Clear();
            foreach (var det in f.Detalles)
            {
                _detalles.Add(new DetalleItemRow
                {
                    Concepto = det.Concepto,
                    Cantidad = det.Cantidad,
                    PrecioUnitario = det.PrecioUnitario
                });
            }

            ActualizarTotal();
        }

        // ==========================================
        // GESTIÓN DE RENGLONES (DETALLE)
        // ==========================================

        private void AgregarItemButton_Click(object? sender, EventArgs e)
        {
            if (_modoSoloLectura) return;

            var concepto = nuevoConceptoTextBox.Text.Trim();
            var cantidad = (int)nuevaCantidadNumericUpDown.Value;
            var precio = nuevoPrecioNumericUpDown.Value;

            if (string.IsNullOrWhiteSpace(concepto))
            {
                MessageBox.Show("Ingrese el concepto o descripción del ítem a agregar.", "Validación de Detalle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nuevoConceptoTextBox.Focus();
                return;
            }

            if (concepto.Length > 200)
            {
                MessageBox.Show("El concepto no puede superar los 200 caracteres.", "Validación de Detalle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nuevoConceptoTextBox.Focus();
                return;
            }

            if (cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor que cero.", "Validación de Detalle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nuevaCantidadNumericUpDown.Focus();
                return;
            }

            if (precio <= 0)
            {
                MessageBox.Show("El precio unitario del nuevo ítem debe ser mayor que cero ($0,00).", "Validación de Detalle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nuevoPrecioNumericUpDown.Focus();
                return;
            }

            _detalles.Add(new DetalleItemRow
            {
                Concepto = concepto,
                Cantidad = cantidad,
                PrecioUnitario = decimal.Round(precio, 2)
            });

            nuevoConceptoTextBox.Clear();
            nuevaCantidadNumericUpDown.Value = 1;
            nuevoPrecioNumericUpDown.Value = 0;
            nuevoConceptoTextBox.Focus();

            ActualizarTotal();
        }

        private void DetallesDataGridView_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (_modoSoloLectura || e.RowIndex < 0) return;

            if (detallesDataGridView.Columns[e.ColumnIndex].Name == "quitarColumn")
            {
                if (_detalles.Count <= 1)
                {
                    MessageBox.Show("La factura debe conservar al menos un renglón en el detalle. Puede editar su concepto o importe directamente en la fila.", "Detalle Requerido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                detallesDataGridView.EndEdit();
                _detalles.RemoveAt(e.RowIndex);
                ActualizarTotal();
            }
        }

        private void DetallesDataGridView_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
        {
            if (_modoSoloLectura || e.RowIndex < 0) return;

            var colName = detallesDataGridView.Columns[e.ColumnIndex].Name;
            var rawValue = e.FormattedValue?.ToString()?.Trim() ?? string.Empty;

            if (colName == "conceptoColumn")
            {
                if (string.IsNullOrWhiteSpace(rawValue))
                {
                    e.Cancel = true;
                    MessageBox.Show("El concepto del renglón no puede quedar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (colName == "cantidadColumn")
            {
                if (!int.TryParse(rawValue, out int cant) || cant <= 0)
                {
                    e.Cancel = true;
                    MessageBox.Show("La cantidad debe ser un número entero mayor que cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (colName == "precioUnitarioColumn")
            {
                if (!TryParseDecimalFlexible(rawValue, out decimal precio) || precio < 0)
                {
                    e.Cancel = true;
                    MessageBox.Show("Ingrese un precio unitario numérico válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void DetallesDataGridView_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            if (_modoSoloLectura || e.RowIndex < 0) return;

            detallesDataGridView.Refresh();
            ActualizarTotal();
        }

        private static bool TryParseDecimalFlexible(string input, out decimal result)
        {
            var cleaned = input.Replace("$", "").Trim();
            if (decimal.TryParse(cleaned, NumberStyles.Any, CultureInfo.CurrentCulture, out result))
                return true;
            if (decimal.TryParse(cleaned, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                return true;
            return false;
        }

        private void ActualizarTotal()
        {
            decimal total = _detalles.Sum(d => d.Subtotal);
            totalLabel.Text = $"TOTAL: $ {total:N2}";
        }

        // ==========================================
        // CONFIRMACIÓN Y ANULACIÓN DE FACTURA
        // ==========================================

        private async void ConfirmarButton_Click(object? sender, EventArgs e)
        {
            if (_modoSoloLectura || _turno == null) return;

            detallesDataGridView.EndEdit();

            if (_detalles.Count == 0)
            {
                MessageBox.Show("La factura debe tener al menos un renglón de detalle.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            for (int i = 0; i < _detalles.Count; i++)
            {
                var item = _detalles[i];
                if (string.IsNullOrWhiteSpace(item.Concepto))
                {
                    MessageBox.Show($"El renglón #{i + 1} tiene el concepto vacío.", "Validación de Detalle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (item.Cantidad <= 0)
                {
                    MessageBox.Show($"La cantidad del renglón '{item.Concepto}' debe ser mayor que cero.", "Validación de Detalle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (item.PrecioUnitario <= 0)
                {
                    MessageBox.Show(
                        $"Debe ingresar un Precio Unitario mayor a $0,00 para el renglón '{item.Concepto}'.",
                        "Precio Unitario Requerido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    if (detallesDataGridView.Rows.Count > i)
                    {
                        detallesDataGridView.CurrentCell = detallesDataGridView.Rows[i].Cells["precioUnitarioColumn"];
                        detallesDataGridView.BeginEdit(true);
                    }
                    return;
                }
            }

            var metodoPago = metodoPagoComboBox.SelectedValue?.ToString() ?? "Efectivo";
            var metodoDisplay = (metodoPagoComboBox.SelectedItem as MetodoPagoOption)?.Display ?? metodoPago;
            var montoTotal = _detalles.Sum(d => d.Subtotal);

            var confirm = MessageBox.Show(
                $"¿Confirmar la emisión de la factura para el Turno N° {_turno.Id}?\n\n" +
                $"• Renglones de detalle: {_detalles.Count}\n" +
                $"• Método de Pago: {metodoDisplay}\n" +
                $"• Monto Total: $ {montoTotal:N2}",
                "Confirmar Emisión de Factura",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            await UiErrorHandler.ExecuteAsync(async () =>
            {
                var dto = new FacturaCreateDTO
                {
                    TurnoId = _turno.Id,
                    MetodoPago = metodoPago,
                    Detalles = _detalles.Select(d => new DetalleFacturaCreateDTO
                    {
                        Concepto = d.Concepto.Trim(),
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario
                    }).ToList()
                };

                var creada = await FacturaApiClient.CreateAsync(dto);

                MessageBox.Show(
                    $"Factura N° {creada.Id:D5} emitida exitosamente por un total de $ {creada.MontoTotal:N2}.",
                    "Facturación Exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }, this, "Error al emitir la factura");
        }

        private async void AnularButton_Click(object? sender, EventArgs e)
        {
            if (_facturaExistente == null) return;

            var confirm = MessageBox.Show(
                $"¿Está seguro de que desea ANULAR la Factura N° {_facturaExistente.Id:D5}?\n\n" +
                $"• La factura pasará a estado 'Cancelada'.\n" +
                $"• El Turno N° {_facturaExistente.TurnoId} volverá a quedar Pendiente de Facturar para que pueda emitir un nuevo comprobante.",
                "Confirmar Anulación de Factura",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            await UiErrorHandler.ExecuteAsync(async () =>
            {
                await FacturaApiClient.AnularAsync(_facturaExistente.Id);

                MessageBox.Show(
                    $"La Factura N° {_facturaExistente.Id:D5} fue anulada. El turno N° {_facturaExistente.TurnoId} se encuentra nuevamente disponible para facturar.",
                    "Factura Anulada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }, this, "Error al anular la factura");
        }

        private void NuevoItemInput_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                AgregarItemButton_Click(sender, e);
            }
        }

        private void FacturaModalForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && !detallesDataGridView.IsCurrentCellInEditMode)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        // ==========================================
        // CLASES AUXILIARES
        // ==========================================

        private class MetodoPagoOption
        {
            public string Value { get; set; } = string.Empty;
            public string Display { get; set; } = string.Empty;
        }

        public class DetalleItemRow
        {
            public string Concepto { get; set; } = string.Empty;
            public int Cantidad { get; set; } = 1;
            public decimal PrecioUnitario { get; set; }
            public decimal Subtotal => Cantidad * PrecioUnitario;
        }
    }
}
