-- ── Tabla maestro de socios ─────────────────────────────
CREATE TABLE IF NOT EXISTS tmasocios (
    id_socio                BIGINT          NOT NULL AUTO_INCREMENT,
    id_socio_external       BIGINT          NOT NULL,

    codigo_socio            VARCHAR(50)     NOT NULL,
    tipo_empleado           VARCHAR(50)     NOT NULL,
    nro_referencia          VARCHAR(50)     NOT NULL DEFAULT '',
    nombre_completo         VARCHAR(255)    NOT NULL,
    supervisor              VARCHAR(100)    NULL,
    cod_trabajo             VARCHAR(20)     NULL,
    tipo_pago               VARCHAR(20)     NULL,

    activo                  TINYINT(1)      NOT NULL DEFAULT 1,

    email                   VARCHAR(150)    NULL,
    direccion_localiz       VARCHAR(255)    NULL,
    direccion_mensaje       VARCHAR(255)    NULL,
    almacen                 VARCHAR(100)    NULL,
    departamento            VARCHAR(100)    NULL,
    usuario                 VARCHAR(150)    NULL,

    usuario_reg             VARCHAR(100)    NULL,
    fecha_reg               DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    usuario_act             VARCHAR(100)    NULL,
    fecha_act               DATETIME        NULL ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (id_socio),

    UNIQUE INDEX idx_tmasocios_id_socio_external (id_socio_external),
    UNIQUE INDEX idx_tmasocios_codigo_socio      (codigo_socio),
    INDEX        idx_tmasocios_tipo_empleado     (tipo_empleado),
    INDEX        idx_tmasocios_activo            (activo),
    INDEX        idx_tmasocios_departamento      (departamento),
    INDEX        idx_tmasocios_supervisor        (supervisor)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
