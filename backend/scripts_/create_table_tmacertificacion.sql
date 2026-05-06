-- ── Tabla maestro de certificaciones ─────────────────────────────
CREATE TABLE IF NOT EXISTS tmacertificacion (
    id_certificacion    BIGINT          NOT NULL AUTO_INCREMENT,
    codigo              VARCHAR(20)     NOT NULL,
    descripcion         VARCHAR(255)    NOT NULL,

    usuario_reg         VARCHAR(100)    NULL,
    fecha_reg           DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    usuario_act         VARCHAR(100)    NULL,
    fecha_act           DATETIME        NULL ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (id_certificacion),

    UNIQUE INDEX idx_tmacertificacion_codigo      (codigo),
    INDEX        idx_tmacertificacion_descripcion (descripcion(100))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
