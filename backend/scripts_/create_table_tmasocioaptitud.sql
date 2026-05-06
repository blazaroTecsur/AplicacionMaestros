-- ── Tabla relacion socio-aptitud ─────────────────────────────
CREATE TABLE IF NOT EXISTS tmasocioaptitud (
    IdSocio         BIGINT      NOT NULL,
    IdAptitud       BIGINT      NOT NULL,

    UsuarioReg      VARCHAR(100)    NULL,
    FechaReg        DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UsuarioAct      VARCHAR(100)    NULL,
    FechaAct        DATETIME        NULL ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (IdSocio, IdAptitud),

    INDEX idx_tmasocioaptitud_IdAptitud (IdAptitud),

    CONSTRAINT fk_tmasocioaptitud_tmasocio
        FOREIGN KEY (IdSocio)
        REFERENCES tmasocio (IdSocio)
        ON DELETE CASCADE,

    CONSTRAINT fk_tmasocioaptitud_tmaaptitud
        FOREIGN KEY (IdAptitud)
        REFERENCES tmaaptitud (IdAptitud)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
