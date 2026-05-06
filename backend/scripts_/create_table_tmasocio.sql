-- ── Tabla maestro de socios ─────────────────────────────
CREATE TABLE IF NOT EXISTS tmasocio (
    IdSocio                 BIGINT          NOT NULL AUTO_INCREMENT,
    IdSocioExternal         BIGINT          NOT NULL,

    CodigoSocio             VARCHAR(50)     NOT NULL,
    TipoEmpleado            VARCHAR(50)     NOT NULL,
    NroReferencia           VARCHAR(50)     NOT NULL DEFAULT '',
    NombreCompleto          VARCHAR(255)    NOT NULL,
    Supervisor              VARCHAR(100)    NULL,
    CodTrabajo              VARCHAR(20)     NULL,
    TipoPago                VARCHAR(20)     NULL,

    Activo                  TINYINT(1)      NOT NULL DEFAULT 1,

    Email                   VARCHAR(150)    NULL,
    DireccionLocaliz        VARCHAR(255)    NULL,
    DireccionMensaje        VARCHAR(255)    NULL,
    Almacen                 VARCHAR(100)    NULL,
    Departamento            VARCHAR(100)    NULL,
    Usuario                 VARCHAR(150)    NULL,

    UsuarioReg              VARCHAR(100)    NULL,
    FechaReg                DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UsuarioAct              VARCHAR(100)    NULL,
    FechaAct                DATETIME        NULL ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (IdSocio),

    UNIQUE INDEX idx_tmasocio_IdSocioExternal (IdSocioExternal),
    UNIQUE INDEX idx_tmasocio_CodigoSocio     (CodigoSocio),
    INDEX        idx_tmasocio_TipoEmpleado    (TipoEmpleado),
    INDEX        idx_tmasocio_Activo          (Activo),
    INDEX        idx_tmasocio_Departamento    (Departamento),
    INDEX        idx_tmasocio_Supervisor      (Supervisor)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
