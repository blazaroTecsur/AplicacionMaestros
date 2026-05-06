-- ── Tabla maestro de proveedores ─────────────────────────────
CREATE TABLE IF NOT EXISTS tmaproveedor (
    id_proveedor                BIGINT          NOT NULL AUTO_INCREMENT,
    id_proveedor_external       BIGINT          NOT NULL,
    nombre_proveedor            VARCHAR(255)    NOT NULL,
    tipo_persona                VARCHAR(2)      NOT NULL,

    direccion1                  VARCHAR(255)    NULL,
    direccion2                  VARCHAR(255)    NULL,
    direccion3                  VARCHAR(255)    NULL,
    direccion4                  VARCHAR(255)    NULL,

    comprador                   VARCHAR(150)    NULL,
    estado                      VARCHAR(10)     NOT NULL,

    contacto                    VARCHAR(250)    NULL,
    telefono_contacto           VARCHAR(20)     NULL,
    correo_externo_contacto     VARCHAR(100)    NULL,
    correo_interno_contacto     VARCHAR(100)    NULL,

    ruc                         VARCHAR(20)     NOT NULL,

    usuario_reg                 VARCHAR(100)    NULL,
    fecha_reg                   DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    usuario_act                 VARCHAR(100)    NULL,
    fecha_act                   DATETIME        NULL ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (id_proveedor),

    UNIQUE INDEX idx_tmaproveedor_id_proveedor_external (id_proveedor_external),
    UNIQUE INDEX idx_tmaproveedor_ruc                   (ruc),
    INDEX        idx_tmaproveedor_nombre_proveedor      (nombre_proveedor(100))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ── Datos de prueba ────────────────────────────────────────
INSERT INTO tmaproveedor
(
    id_proveedor_external,
    nombre_proveedor,
    tipo_persona,
    ruc,
    estado
)
VALUES
(
    1,
    'LUZ DEL SUR S.A.A.',
    '2',
    '20331898008',
    'A'
);
