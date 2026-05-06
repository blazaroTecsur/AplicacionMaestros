-- ── Tabla maestro de socio-aptitud ─────────────────────────────
CREATE TABLE tmasocio_aptitud (
    IdSocio INT NOT NULL,
    IdAptitud INT NOT NULL,

    UsuarioReg VARCHAR(30) NULL,
    FechaReg DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UsuarioAct VARCHAR(30) NULL,
    FechaAct DATETIME NULL ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (IdSocio, IdAptitud),

    CONSTRAINT FK_socioaptitud_socio
        FOREIGN KEY (IdSocio)
        REFERENCES tmasocio(IdSocio)
        ON DELETE CASCADE,

    CONSTRAINT FK_socioaptitud_aptitud
        FOREIGN KEY (IdAptitud)
        REFERENCES tmaaptitud(IdAptitud)
        ON DELETE CASCADE
);