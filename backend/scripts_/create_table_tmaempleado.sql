-- ── Tabla maestro de empleados ─────────────────────────────
CREATE TABLE tmaempleado (
    IdEmpleado          INT AUTO_INCREMENT PRIMARY KEY,

    IdEmpleadoExternal  VARCHAR(20)  NOT NULL,
    Codigo              VARCHAR(20)  NOT NULL,
    NombreCompleto      VARCHAR(200) NOT NULL,

    -- principal
    Apellido            VARCHAR(150),
    Nombre              VARCHAR(150),
    Alias               VARCHAR(150),
    Cargo               VARCHAR(150),
    Departamento        VARCHAR(20),
    Estado              VARCHAR(20),
    Turno               VARCHAR(20),
    Categoria           VARCHAR(20),
    IdUsuario           VARCHAR(50),
    FrecuenciaPago      VARCHAR(20),
    TipoEmpleado        VARCHAR(50),
    GeneraNomina        VARCHAR(50),
    CuentaSueldo        VARCHAR(50),

    -- nameTax
    PrimerNombre        VARCHAR(100),
    SegundoNombre       VARCHAR(100),
    PrimerApellido      VARCHAR(100),
    SegundoApellido     VARCHAR(100),

    -- contacto
    Direccion1          VARCHAR(200),
    Direccion2          VARCHAR(200),
    Direccion3          VARCHAR(200),
    Direccion4          VARCHAR(200),
    Ciudad              VARCHAR(100),
    CodProvincia        VARCHAR(10),
    CP                  VARCHAR(20),
    Municipio           VARCHAR(100),
    Telefono            VARCHAR(50),
    TelComercial        VARCHAR(50),
    ExtensionTel        VARCHAR(20),
    CorreoElect         VARCHAR(200),
    Correo              VARCHAR(200),

    -- recursos humanos
    FechaContratacion   DATE,
    FechaRevision       DATE,
    FechaRescision      DATE,

    UsuarioReg VARCHAR(30) NOT NULL,
    FechaReg DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UsuarioAct VARCHAR(30) NULL,
    FechaAct DATETIME NULL ON UPDATE CURRENT_TIMESTAMP,

    UNIQUE KEY uk_empleado_external (IdEmpleadoExternal)
);

-- ── Datos de prueba ────────────────────────────────────────
INSERT INTO tmaempleado
    (IdEmpleadoExternal, Codigo, NombreCompleto,
     PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido,
     Apellido, Nombre, Cargo, Departamento, Estado,
     TipoEmpleado, FechaContratacion)
VALUES
    ('EMP-001', 'E001', 'GARCIA LOPEZ JUAN CARLOS',
     'JUAN', 'CARLOS', 'GARCIA', 'LOPEZ',
     'GARCIA LOPEZ', 'JUAN CARLOS', 'ANALISTA FINANCIERO', 'FINANZAS', 'ACTIVO',
     'PLANILLA', '2020-03-15'),

    ('EMP-002', 'E002', 'TORRES QUISPE MARIA ELENA',
     'MARIA', 'ELENA', 'TORRES', 'QUISPE',
     'TORRES QUISPE', 'MARIA ELENA', 'COORDINADORA LOGISTICA', 'LOGISTICA', 'ACTIVO',
     'PLANILLA', '2019-07-01');
