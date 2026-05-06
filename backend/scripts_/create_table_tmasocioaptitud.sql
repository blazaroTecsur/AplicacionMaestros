-- ── Tabla relacion socio-aptitud ─────────────────────────────
CREATE TABLE IF NOT EXISTS tmasocioaptitud (
    id_socio        BIGINT      NOT NULL,
    id_aptitud      BIGINT      NOT NULL,

    usuario_reg     VARCHAR(100)    NULL,
    fecha_reg       DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    usuario_act     VARCHAR(100)    NULL,
    fecha_act       DATETIME        NULL ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (id_socio, id_aptitud),

    INDEX idx_tmasocioaptitud_id_aptitud (id_aptitud),

    CONSTRAINT fk_tmasocioaptitud_tmasocio
        FOREIGN KEY (id_socio)
        REFERENCES tmasocio (id_socio)
        ON DELETE CASCADE,

    CONSTRAINT fk_tmasocioaptitud_tmaaptitud
        FOREIGN KEY (id_aptitud)
        REFERENCES tmaaptitud (id_aptitud)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
