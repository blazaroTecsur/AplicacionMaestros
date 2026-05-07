-- ── Tabla maestro de articulos ─────────────────────────────
CREATE TABLE IF NOT EXISTS tmaarticulo (
    IdArticulo              BIGINT          NOT NULL AUTO_INCREMENT,
    IdArticuloExternal      BIGINT          NOT NULL,

    Codigo                  VARCHAR(15)     NOT NULL,
    Descripcion             VARCHAR(500)    NOT NULL,
    UnidadMedida            VARCHAR(10)     NULL,
    Tipo                    VARCHAR(20)     NULL,
    Origen                  VARCHAR(50)     NULL,
    CodigoProducto          VARCHAR(50)     NULL,
    CodigoAbc               VARCHAR(2)      NULL,

    SegLote                 TINYINT(1)      NOT NULL DEFAULT 0,

    EstadoMaterial          VARCHAR(20)     NOT NULL DEFAULT 'ACTIVO',

    UsuarioReg              VARCHAR(100)    NULL,
    FechaReg                DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UsuarioAct              VARCHAR(100)    NULL,
    FechaAct                DATETIME        NULL ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (IdArticulo),

    UNIQUE INDEX idx_tmaarticulo_IdArticuloExternal (IdArticuloExternal),
    UNIQUE INDEX idx_tmaarticulo_Codigo             (Codigo),
    INDEX        idx_tmaarticulo_Tipo               (Tipo),
    INDEX        idx_tmaarticulo_Origen             (Origen),
    INDEX        idx_tmaarticulo_CodigoProducto     (CodigoProducto),
    INDEX        idx_tmaarticulo_EstadoMaterial     (EstadoMaterial)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
