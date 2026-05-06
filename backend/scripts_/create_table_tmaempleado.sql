-- ── Tabla maestro de empleados ─────────────────────────────
CREATE TABLE IF NOT EXISTS tmaempleado (
    id_empleado             BIGINT          NOT NULL AUTO_INCREMENT,

    id_empleado_external    VARCHAR(20)     NOT NULL,
    codigo                  VARCHAR(20)     NOT NULL,
    nombre_completo         VARCHAR(200)    NOT NULL,

    -- datos principales
    apellido                VARCHAR(150)    NULL,
    nombre                  VARCHAR(150)    NULL,
    alias                   VARCHAR(150)    NULL,
    cargo                   VARCHAR(150)    NULL,
    dpto                    VARCHAR(100)    NULL,
    estado                  VARCHAR(20)     NULL,
    turno                   VARCHAR(20)     NULL,
    categoria               VARCHAR(20)     NULL,
    id_usuario              VARCHAR(50)     NULL,
    frec_pago               VARCHAR(20)     NULL,
    tip_emp                 VARCHAR(50)     NULL,
    gen_nomina              VARCHAR(50)     NULL,
    cta_sueldo              VARCHAR(50)     NULL,

    -- nombre fiscal
    primer_nombre           VARCHAR(100)    NULL,
    segundo_nombre          VARCHAR(100)    NULL,
    primer_apellido         VARCHAR(100)    NULL,
    segundo_apellido        VARCHAR(100)    NULL,

    -- contacto
    direccion1              VARCHAR(200)    NULL,
    direccion2              VARCHAR(200)    NULL,
    direccion3              VARCHAR(200)    NULL,
    direccion4              VARCHAR(200)    NULL,
    ciudad                  VARCHAR(100)    NULL,
    cod_provincia           VARCHAR(10)     NULL,
    cp                      VARCHAR(20)     NULL,
    municipio               VARCHAR(100)    NULL,
    telefono                VARCHAR(50)     NULL,
    tel_comercial           VARCHAR(50)     NULL,
    extension_tel           VARCHAR(20)     NULL,
    correo_elect            VARCHAR(200)    NULL,
    correo                  VARCHAR(200)    NULL,

    -- recursos humanos
    fecha_contr             DATETIME        NULL,
    fecha_revis             DATETIME        NULL,
    fecha_rescis            DATETIME        NULL,

    usuario_reg             VARCHAR(100)    NULL,
    fecha_reg               DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    usuario_act             VARCHAR(100)    NULL,
    fecha_act               DATETIME        NULL ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (id_empleado),

    UNIQUE INDEX idx_tmaempleado_id_empleado_external (id_empleado_external)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ── Datos de prueba ────────────────────────────────────────
INSERT INTO tmaempleado
    (id_empleado_external, codigo, nombre_completo,
     primer_nombre, segundo_nombre, primer_apellido, segundo_apellido,
     apellido, nombre, cargo, dpto, estado,
     tip_emp, fecha_contr)
VALUES
    ('EMP-001', 'E001', 'GARCIA LOPEZ JUAN CARLOS',
     'JUAN', 'CARLOS', 'GARCIA', 'LOPEZ',
     'GARCIA LOPEZ', 'JUAN CARLOS', 'ANALISTA FINANCIERO', 'FINANZAS', 'ACTIVO',
     'PLANILLA', '2020-03-15'),

    ('EMP-002', 'E002', 'TORRES QUISPE MARIA ELENA',
     'MARIA', 'ELENA', 'TORRES', 'QUISPE',
     'TORRES QUISPE', 'MARIA ELENA', 'COORDINADORA LOGISTICA', 'LOGISTICA', 'ACTIVO',
     'PLANILLA', '2019-07-01');
