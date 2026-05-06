-- ── Tabla maestro de articulos ─────────────────────────────
CREATE TABLE IF NOT EXISTS tmaarticulo (
    id_articulo             BIGINT          NOT NULL AUTO_INCREMENT,
    id_articulo_external    BIGINT          NOT NULL,

    codigo                  VARCHAR(15)     NOT NULL,
    descripcion             VARCHAR(500)    NOT NULL,
    unidad_medida           VARCHAR(10)     NULL,
    tipo                    VARCHAR(20)     NULL,
    origen                  VARCHAR(50)     NULL,
    codigo_producto         VARCHAR(50)     NULL,
    codigo_abc              VARCHAR(2)      NULL,

    seg_lote                TINYINT(1)      NOT NULL DEFAULT 0,

    estado_material         VARCHAR(20)     NOT NULL DEFAULT 'ACTIVO',

    usuario_reg             VARCHAR(100)    NULL,
    fecha_reg               DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    usuario_act             VARCHAR(100)    NULL,
    fecha_act               DATETIME        NULL ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (id_articulo),

    UNIQUE INDEX idx_tmaarticulo_id_articulo_external (id_articulo_external),
    UNIQUE INDEX idx_tmaarticulo_codigo               (codigo),
    INDEX        idx_tmaarticulo_tipo                 (tipo),
    INDEX        idx_tmaarticulo_origen               (origen),
    INDEX        idx_tmaarticulo_codigo_producto      (codigo_producto),
    INDEX        idx_tmaarticulo_estado_material      (estado_material)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
