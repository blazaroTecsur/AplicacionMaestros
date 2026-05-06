-- ── Tabla maestro de aptitudes ─────────────────────────────
CREATE TABLE IF NOT EXISTS tmaaptitud (
    IdAptitud       BIGINT          NOT NULL AUTO_INCREMENT,
    Codigo          VARCHAR(20)     NOT NULL,
    Descripcion     VARCHAR(255)    NOT NULL,

    UsuarioReg      VARCHAR(100)    NULL,
    FechaReg        DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UsuarioAct      VARCHAR(100)    NULL,
    FechaAct        DATETIME        NULL ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (IdAptitud),

    UNIQUE INDEX idx_tmaaptitud_Codigo      (Codigo),
    INDEX        idx_tmaaptitud_Descripcion (Descripcion(100))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
