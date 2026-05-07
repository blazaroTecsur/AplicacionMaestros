-- ── Tabla maestro de cuentas contables ─────────────────────────────
CREATE TABLE IF NOT EXISTS tmacuentacontable (
    IdCuentaContable    BIGINT          NOT NULL AUTO_INCREMENT,
    Cuenta              VARCHAR(50)     NOT NULL,
    Descripcion         VARCHAR(300)    NOT NULL,
    Tipo                VARCHAR(50)     NOT NULL,

    UsuarioReg          VARCHAR(100)    NULL,
    FechaReg            DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UsuarioAct          VARCHAR(100)    NULL,
    FechaAct            DATETIME        NULL ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (IdCuentaContable),

    UNIQUE INDEX idx_tmacuentacontable_Cuenta (Cuenta)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
