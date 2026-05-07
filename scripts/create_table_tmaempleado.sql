-- ── Tabla maestro de empleados ─────────────────────────────
CREATE TABLE IF NOT EXISTS tmaempleado (
    IdEmpleado              BIGINT          NOT NULL AUTO_INCREMENT,

    IdEmpleadoExternal      VARCHAR(50)     NOT NULL,
    Codigo                  VARCHAR(50)     NOT NULL,
    NombreCompleto          VARCHAR(200)    NOT NULL,

    -- datos principales
    Apellido                VARCHAR(100)    NULL,
    Nombre                  VARCHAR(100)    NULL,
    Alias                   VARCHAR(100)    NULL,
    Cargo                   VARCHAR(100)    NULL,
    Dpto                    VARCHAR(100)    NULL,
    Estado                  VARCHAR(20)     NULL,
    Turno                   VARCHAR(50)     NULL,
    Categoria               VARCHAR(50)     NULL,
    IdUsuario               VARCHAR(100)    NULL,
    FrecPago                VARCHAR(50)     NULL,
    TipEmp                  VARCHAR(50)     NULL,
    GenNomina               VARCHAR(10)     NULL,
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
    CodProvincia            VARCHAR(50)     NULL,
    Cp                      VARCHAR(20)     NULL,
    Municipio               VARCHAR(100)    NULL,
    Telefono                VARCHAR(50)     NULL,
    TelComercial            VARCHAR(50)     NULL,
    ExtensionTel            VARCHAR(20)     NULL,
    CorreoElect             VARCHAR(150)    NULL,
    Correo                  VARCHAR(150)    NULL,

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
