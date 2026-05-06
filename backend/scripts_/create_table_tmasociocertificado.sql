-- ── Tabla relacion socio-certificacion ─────────────────────────────
CREATE TABLE IF NOT EXISTS tmasociocertificado (
    IdSocio             BIGINT      NOT NULL,
    IdCertificacion     BIGINT      NOT NULL,

    UsuarioReg          VARCHAR(100)    NULL,
    FechaReg            DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UsuarioAct          VARCHAR(100)    NULL,
    FechaAct            DATETIME        NULL ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (IdSocio, IdCertificacion),

    INDEX idx_tmasociocertificado_IdCertificacion (IdCertificacion),

    CONSTRAINT fk_tmasociocertificado_tmasocio
        FOREIGN KEY (IdSocio)
        REFERENCES tmasocio (IdSocio)
        ON DELETE CASCADE,

    CONSTRAINT fk_tmasociocertificado_tmacertificacion
        FOREIGN KEY (IdCertificacion)
        REFERENCES tmacertificacion (IdCertificacion)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
