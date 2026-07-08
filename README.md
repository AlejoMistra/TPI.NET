# Propuesta de Trabajo Práctico Integrador

## Sistema de Gestión de Turnos Médicos

**Integrantes:**

- Alejo Mistraletti (52665)
- Lucas Alonso (52904)
- Marco Bernaus (52172)

**Asignatura:** Tecnologías de Desarrollo de Software IDE
**Comisión:** 3EK01
**Docentes:** Ezequiel Porta y Severino Guimpel

## Descripción

Sistema de gestión para consultorios médicos que permite administrar pacientes, profesionales, especialidades, turnos y su facturación. El sistema centraliza los datos médicos de cada paciente en una Historia Clínica, la cual agrupa todas sus consultas a lo largo del tiempo, mientras que un módulo administrativo paralelo gestiona el cobro y estado de cuenta de las prestaciones realizadas.

## Diagrama de Clases

```mermaid
classDiagram
    direction LR
    class Usuario {
        +IdUsuario
        +NombreUsuario
        +Contraseña
        +TipoUsuario
        +Estado
    }
    class Persona {
        +Nombre
        +Apellido
        +NroDocumento
        +Email
        +Telefono
    }
    class Paciente {
        +IdPaciente
        +FechaNacimiento
        +ObraSocial
    }
    class Turno {
        +IdTurno
        +FechaHoraInicio
        +FechaHoraFin
        +Motivo
        +EstadoTurno
        +Observacion
    }
    class Administrativo {
        +IdAdministrativo
        +Legajo
    }
    class HistoriaClinica {
        +IdHistoria
        +GrupoSanguineo
        +Alergias
        +Antecedentes
        +FechaCreacion
    }
    class Factura {
        +IdFactura
        +FechaEmision
        +MontoTotal
        +MetodoPago
        +EstadoFactura
    }
    class DetalleFactura {
        +IdDetalle
        +Concepto
        +Cantidad
        +PrecioUnitario
        +Subtotal
    }
    class ConsultaMedica {
        +IdConsultaMedica
        +Sintomas
        +NotasClinicas
        +Diagnostico
    }
    class Profesional {
        +IdProfesional
        +Matricula
    }
    class Especialidad {
        +IdEspecialidad
        +Nombre
    }
    <<abstract>> Persona
    Persona <|-- Administrativo
    Persona <|-- Paciente
    Persona <|-- Profesional
    Usuario "1" -- "0..1" Persona : credenciales de
    Administrativo "1" -- "*" Turno : gestiona
    Paciente "1" -- "*" Turno : solicita
    Profesional "*" -- "1" Especialidad : tiene
    Profesional "1" -- "*" Turno : atiende
    Paciente "1" *-- "1" HistoriaClinica : posee
    Turno "1" -- "0..1" ConsultaMedica : genera
    HistoriaClinica "1" -- "*" ConsultaMedica : contiene
    Turno "1" -- "0..1" Factura : origina
    Factura "1" *-- "*" DetalleFactura : contiene
```

## Roles

1. **Administrativo:** Gestiona la agenda de turnos (asignación, reprogramación y cancelación de turnos). Se encarga de emitir y gestionar la facturación de los turnos. Además gestiona los datos maestros del sistema (altas y modificaciones de Especialidades y Profesionales).

2. **Profesional:** Accede a su agenda de turnos asignados. Puede visualizar los antecedentes en la Historia Clínica del Paciente y registrar las Consultas Médicas realizadas completando diagnósticos y notas clínicas necesarias.

3. **Paciente:** Accede al sistema para autogestionar la solicitud de turnos, visualizar el cronograma de sus próximos turnos y consultar un registro básico de sus atenciones previas.

## CRUDs

1. **CRUD Pacientes** (Gestión de datos personales y obra social).

2. **CRUD Profesionales** (Gestión de datos personales, matrícula y asignación de especialidad).

3. **CRUD Especialidades** (Administración de datos maestros).

4. **CRUD Turnos** (Incluye búsqueda con filtros, por ejemplo: búsqueda por rango de fechas, profesional o estado del turno).

5. **CRUD Consultas Médicas** (Registro realizado por el médico sobre la atención de un Turno para vincularlo a la Historia Clínica).

6. **CRUD Facturación** (Implementación del patrón Maestro/Detalle mediante las entidades Factura y DetalleFactura).
