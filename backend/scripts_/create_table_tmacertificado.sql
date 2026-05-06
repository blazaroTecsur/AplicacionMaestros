-- ── Tabla maestro de certificados ─────────────────────────────
CREATE TABLE tmacertificado (
    IdCertificado INT NOT NULL AUTO_INCREMENT,
    Codigo VARCHAR(20) NOT NULL,
    Descripcion VARCHAR(255) NOT NULL,

    UsuarioReg VARCHAR(30) NULL,
    FechaReg DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UsuarioAct VARCHAR(30) NULL,
    FechaAct DATETIME NULL ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (IdCertificado),

    UNIQUE KEY UQ_tmacertificado_Codigo (Codigo),
    INDEX IX_tmacertificado_Descripcion (Descripcion)
);