-- ── Tabla maestro de socio-certificado ─────────────────────────────
CREATE TABLE tmasocio_certificado (
    IdSocio INT NOT NULL,
    IdCertificado INT NOT NULL,

    UsuarioReg VARCHAR(30) NULL,
    FechaReg DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UsuarioAct VARCHAR(30) NULL,
    FechaAct DATETIME NULL ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (IdSocio, IdCertificado),

    CONSTRAINT FK_sociocertificado_socio
        FOREIGN KEY (IdSocio)
        REFERENCES tmasocio(IdSocio)
        ON DELETE CASCADE,

    CONSTRAINT FK_sociocertificado_certificado
        FOREIGN KEY (IdCertificado)
        REFERENCES tmacertificado(IdCertificado)
        ON DELETE CASCADE
);