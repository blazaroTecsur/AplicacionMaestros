-- ── Tabla maestro de empleados ─────────────────────────────
CREATE TABLE IF NOT EXISTS tmaempleado (
    IdEmpleado              BIGINT          NOT NULL AUTO_INCREMENT,

    IdEmpleadoExternal      VARCHAR(20)     NOT NULL,
    Codigo                  VARCHAR(20)     NOT NULL,
    NombreCompleto          VARCHAR(200)    NOT NULL,

    -- datos principales
    Apellido                VARCHAR(150)    NULL,
    Nombre                  VARCHAR(150)    NULL,
    Alias                   VARCHAR(150)    NULL,
    Cargo                   VARCHAR(150)    NULL,
    Dpto                    VARCHAR(100)    NULL,
    Estado                  VARCHAR(20)     NULL,
    Turno                   VARCHAR(20)     NULL,
    Categoria               VARCHAR(20)     NULL,
    IdUsuario               VARCHAR(50)     NULL,
    FrecPago                VARCHAR(20)     NULL,
    TipEmp                  VARCHAR(50)     NULL,
    GenNomina               VARCHAR(50)     NULL,
    CtaSueldo               VARCHAR(50)     NULL,

    -- nombre fiscal
    PrimerNombre            VARCHAR(100)    NULL,
    SegundoNombre           VARCHAR(100)    NULL,
    PrimerApellido          VARCHAR(100)    NULL,
    SegundoApellido         VARCHAR(100)    NULL,

    -- contacto
    Direccion1              VARCHAR(200)    NULL,
    Direccion2              VARCHAR(200)    NULL,
    Direccion3              VARCHAR(200)    NULL,
    Direccion4              VARCHAR(200)    NULL,
    Ciudad                  VARCHAR(100)    NULL,
    CodProvincia            VARCHAR(10)     NULL,
    Cp                      VARCHAR(20)     NULL,
    Municipio               VARCHAR(100)    NULL,
    Telefono                VARCHAR(50)     NULL,
    TelComercial            VARCHAR(50)     NULL,
    ExtensionTel            VARCHAR(20)     NULL,
    CorreoElect             VARCHAR(200)    NULL,
    Correo                  VARCHAR(200)    NULL,

    -- recursos humanos
    FechaContr              DATETIME        NULL,
    FechaRevis              DATETIME        NULL,
    FechaRescis             DATETIME        NULL,

    UsuarioReg              VARCHAR(100)    NULL,
    FechaReg                DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UsuarioAct              VARCHAR(100)    NULL,
    FechaAct                DATETIME        NULL ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (IdEmpleado),

    UNIQUE INDEX idx_tmaempleado_IdEmpleadoExternal (IdEmpleadoExternal)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ── Datos de prueba ────────────────────────────────────────
INSERT INTO tmaempleado
    (IdEmpleadoExternal, Codigo, NombreCompleto,
     PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido,
     Apellido, Nombre, Cargo, Dpto, Estado,
     TipEmp, FechaContr)
VALUES
    ('EMP-001', 'E001', 'GARCIA LOPEZ JUAN CARLOS',
     'JUAN', 'CARLOS', 'GARCIA', 'LOPEZ',
     'GARCIA LOPEZ', 'JUAN CARLOS', 'ANALISTA FINANCIERO', 'FINANZAS', 'ACTIVO',
     'PLANILLA', '2020-03-15'),

    ('EMP-002', 'E002', 'TORRES QUISPE MARIA ELENA',
     'MARIA', 'ELENA', 'TORRES', 'QUISPE',
     'TORRES QUISPE', 'MARIA ELENA', 'COORDINADORA LOGISTICA', 'LOGISTICA', 'ACTIVO',
     'PLANILLA', '2019-07-01');
