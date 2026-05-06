-- ── Tabla relacion socio-certificacion ─────────────────────────────
CREATE TABLE IF NOT EXISTS tmasociocertificados (
    id_socio            BIGINT      NOT NULL,
    id_certificacion    BIGINT      NOT NULL,

    usuario_reg         VARCHAR(100)    NULL,
    fecha_reg           DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    usuario_act         VARCHAR(100)    NULL,
    fecha_act           DATETIME        NULL ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (id_socio, id_certificacion),

    INDEX idx_tmasociocertificados_id_certificacion (id_certificacion),

    CONSTRAINT fk_tmasociocertificados_tmasocios
        FOREIGN KEY (id_socio)
        REFERENCES tmasocios (id_socio)
        ON DELETE CASCADE,

    CONSTRAINT fk_tmasociocertificados_tmacertificaciones
        FOREIGN KEY (id_certificacion)
        REFERENCES tmacertificaciones (id_certificacion)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
