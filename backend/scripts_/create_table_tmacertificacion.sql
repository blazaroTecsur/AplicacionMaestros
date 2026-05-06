-- ── Tabla maestro de certificaciones ─────────────────────────────
CREATE TABLE IF NOT EXISTS tmacertificacion (
    IdCertificacion     BIGINT          NOT NULL AUTO_INCREMENT,
    Codigo              VARCHAR(20)     NOT NULL,
    Descripcion         VARCHAR(255)    NOT NULL,

    UsuarioReg          VARCHAR(100)    NULL,
    FechaReg            DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UsuarioAct          VARCHAR(100)    NULL,
    FechaAct            DATETIME        NULL ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (IdCertificacion),

    UNIQUE INDEX idx_tmacertificacion_Codigo      (Codigo),
    INDEX        idx_tmacertificacion_Descripcion (Descripcion(100))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
