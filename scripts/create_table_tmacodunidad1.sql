-- ── Tabla maestro de codigos unidad 1 ─────────────────────────────
CREATE TABLE IF NOT EXISTS tmacodunidad1 (
    IdCodUnidad1    BIGINT          NOT NULL AUTO_INCREMENT,
    Codigo          VARCHAR(50)     NOT NULL,
    Descripcion     VARCHAR(200)    NOT NULL,

    UsuarioReg      VARCHAR(100)    NULL,
    FechaReg        DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UsuarioAct      VARCHAR(100)    NULL,
    FechaAct        DATETIME        NULL ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (IdCodUnidad1),

    UNIQUE INDEX idx_tmacodunidad1_Codigo (Codigo)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
