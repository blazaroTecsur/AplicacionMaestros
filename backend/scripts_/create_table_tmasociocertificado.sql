-- ── Tabla relacion socio-certificacion ─────────────────────────────
CREATE TABLE IF NOT EXISTS tmasociocertificado (
    id_socio            BIGINT      NOT NULL,
    id_certificacion    BIGINT      NOT NULL,

    usuario_reg         VARCHAR(100)    NULL,
    fecha_reg           DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    usuario_act         VARCHAR(100)    NULL,
    fecha_act           DATETIME        NULL ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (id_socio, id_certificacion),

    INDEX idx_tmasociocertificado_id_certificacion (id_certificacion),

    CONSTRAINT fk_tmasociocertificado_tmasocio
        FOREIGN KEY (id_socio)
        REFERENCES tmasocio (id_socio)
        ON DELETE CASCADE,

    CONSTRAINT fk_tmasociocertificado_tmacertificacion
        FOREIGN KEY (id_certificacion)
        REFERENCES tmacertificacion (id_certificacion)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
