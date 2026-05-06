-- ── Tabla maestro de aptitudes ─────────────────────────────
CREATE TABLE tmaaptitud (
    IdAptitud INT NOT NULL AUTO_INCREMENT,
    Codigo VARCHAR(20) NOT NULL,
    Descripcion VARCHAR(255) NOT NULL,

    UsuarioReg VARCHAR(30) NULL,
    FechaReg DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UsuarioAct VARCHAR(30) NULL,
    FechaAct DATETIME NULL ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (IdAptitud),
    
    UNIQUE KEY UQ_tmaaptitud_Codigo (Codigo),
    INDEX IX_tmaaptitud_Descripcion (Descripcion)
);
