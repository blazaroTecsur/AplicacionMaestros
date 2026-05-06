-- ── Tabla relacion socio-aptitud ─────────────────────────────
CREATE TABLE IF NOT EXISTS tmasocioaptitudes (
    id_socio        BIGINT      NOT NULL,
    id_aptitud      BIGINT      NOT NULL,

    usuario_reg     VARCHAR(100)    NULL,
    fecha_reg       DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    usuario_act     VARCHAR(100)    NULL,
    fecha_act       DATETIME        NULL ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (id_socio, id_aptitud),

    INDEX idx_tmasocioaptitudes_id_aptitud (id_aptitud),

    CONSTRAINT fk_tmasocioaptitudes_tmasocios
        FOREIGN KEY (id_socio)
        REFERENCES tmasocios (id_socio)
        ON DELETE CASCADE,

    CONSTRAINT fk_tmasocioaptitudes_tmaaptitudes
        FOREIGN KEY (id_aptitud)
        REFERENCES tmaaptitudes (id_aptitud)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
