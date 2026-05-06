-- ── Tabla maestro de socios ─────────────────────────────
CREATE TABLE tmasocio (
    IdSocio INT NOT NULL AUTO_INCREMENT,
    IdSocioExternal INT NOT NULL,

    CodigoSocio VARCHAR(50) NOT NULL,
    TipoEmpleado VARCHAR(50) NOT NULL,
    NroReferencia VARCHAR(50) NULL,
    NombreCompleto VARCHAR(255) NOT NULL,
    Supervisor VARCHAR(100) NULL,
    CodTrabajo VARCHAR(20) NULL,
    TipoPago VARCHAR(20) NULL,
    
    Activo TINYINT(1) NOT NULL DEFAULT 1,

    Email VARCHAR(150) NULL,
    DireccLocaliz VARCHAR(255) NULL,
    DireccMensaje VARCHAR(255) NULL,
    Almacen VARCHAR(100) NULL,
    Departamento VARCHAR(100) NULL,
    Usuario VARCHAR(150) NULL,

    UsuarioReg VARCHAR(30) NULL,
    FechaReg DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UsuarioAct VARCHAR(30) NULL,
    FechaAct DATETIME NULL ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (IdSocio),

    UNIQUE KEY UQ_tmasocio_IdSocioExternal (IdSocioExternal),
    UNIQUE KEY UQ_tmasocio_CodigoSocio (CodigoSocio),

    INDEX IX_tmasocio_TipoEmpleado (TipoEmpleado),
    INDEX IX_tmasocio_Activo (Activo),
    INDEX IX_tmasocio_Departamento (Departamento),
    INDEX IX_tmasocio_Supervisor (Supervisor)
);