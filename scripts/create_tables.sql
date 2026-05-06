-- =============================================================
-- Script de creación de tablas - AplicacionMaestros
-- Base de datos: MySQL
-- Convenciones:
--   Tablas  : prefijo tma + snake_case
--   Columnas: snake_case
--   PK      : BIGINT AUTO_INCREMENT
--   Índices : idx_tabla_columna
--   FK      : fk_tabla_referenciada
-- =============================================================

CREATE TABLE IF NOT EXISTS tmaproveedores (
    id_proveedor              BIGINT         NOT NULL AUTO_INCREMENT,
    id_proveedor_external     BIGINT         NOT NULL,
    tipo_persona              VARCHAR(50)    NOT NULL,
    ruc                       VARCHAR(20)    NOT NULL,
    nombre_proveedor          VARCHAR(200)   NOT NULL,
    direccion1                VARCHAR(200)   NOT NULL,
    direccion2                VARCHAR(200)   NOT NULL DEFAULT '',
    direccion3                VARCHAR(200)   NOT NULL DEFAULT '',
    direccion4                VARCHAR(200)   NOT NULL DEFAULT '',
    comprador                 VARCHAR(100)   NOT NULL,
    contacto                  VARCHAR(100)   NULL,
    telefono_contacto         VARCHAR(50)    NOT NULL DEFAULT '',
    correo_externo_contacto   VARCHAR(150)   NOT NULL DEFAULT '',
    correo_interno_contacto   VARCHAR(150)   NOT NULL DEFAULT '',
    estado                    VARCHAR(20)    NOT NULL,
    usuario_reg               VARCHAR(100)   NULL,
    fecha_reg                 DATETIME       NOT NULL DEFAULT CURRENT_TIMESTAMP,
    usuario_act               VARCHAR(100)   NULL,
    fecha_act                 DATETIME       NULL ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (id_proveedor),
    UNIQUE INDEX idx_tmaproveedores_id_proveedor_external (id_proveedor_external)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- -------------------------------------------------------------

CREATE TABLE IF NOT EXISTS tmaarticulos (
    id_articulo               BIGINT         NOT NULL AUTO_INCREMENT,
    id_articulo_external      BIGINT         NOT NULL,
    codigo                    VARCHAR(50)    NOT NULL,
    descripcion               VARCHAR(300)   NOT NULL,
    unidad_medida             VARCHAR(20)    NOT NULL,
    tipo                      VARCHAR(50)    NOT NULL,
    origen                    VARCHAR(50)    NOT NULL,
    codigo_producto           VARCHAR(50)    NOT NULL DEFAULT '',
    codigo_abc                VARCHAR(10)    NOT NULL DEFAULT '',
    seg_lote                  TINYINT(1)     NOT NULL DEFAULT 0,
    estado_material           VARCHAR(20)    NOT NULL,
    usuario_reg               VARCHAR(100)   NULL,
    fecha_reg                 DATETIME       NOT NULL DEFAULT CURRENT_TIMESTAMP,
    usuario_act               VARCHAR(100)   NULL,
    fecha_act                 DATETIME       NULL ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (id_articulo),
    UNIQUE INDEX idx_tmaarticulos_id_articulo_external (id_articulo_external)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- -------------------------------------------------------------

CREATE TABLE IF NOT EXISTS tmasocios (
    id_socio                  BIGINT         NOT NULL AUTO_INCREMENT,
    id_socio_external         BIGINT         NOT NULL,
    codigo_socio              VARCHAR(50)    NOT NULL,
    tipo_empleado             VARCHAR(50)    NOT NULL,
    nro_referencia            VARCHAR(50)    NOT NULL DEFAULT '',
    nombre_completo           VARCHAR(200)   NOT NULL,
    supervisor                VARCHAR(100)   NULL,
    cod_trabajo               VARCHAR(50)    NULL,
    tipo_pago                 VARCHAR(50)    NULL,
    email                     VARCHAR(150)   NULL,
    direccion_localiz         VARCHAR(200)   NULL,
    direccion_mensaje         VARCHAR(200)   NULL,
    almacen                   VARCHAR(50)    NULL,
    departamento              VARCHAR(100)   NULL,
    usuario                   VARCHAR(100)   NULL,
    activo                    TINYINT(1)     NOT NULL DEFAULT 1,
    usuario_reg               VARCHAR(100)   NULL,
    fecha_reg                 DATETIME       NOT NULL DEFAULT CURRENT_TIMESTAMP,
    usuario_act               VARCHAR(100)   NULL,
    fecha_act                 DATETIME       NULL ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (id_socio),
    UNIQUE INDEX idx_tmasocios_id_socio_external (id_socio_external)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- -------------------------------------------------------------

CREATE TABLE IF NOT EXISTS tmaaptitudes (
    id_aptitud                BIGINT         NOT NULL AUTO_INCREMENT,
    codigo                    VARCHAR(50)    NOT NULL,
    descripcion               VARCHAR(200)   NOT NULL,
    usuario_reg               VARCHAR(100)   NULL,
    fecha_reg                 DATETIME       NOT NULL DEFAULT CURRENT_TIMESTAMP,
    usuario_act               VARCHAR(100)   NULL,
    fecha_act                 DATETIME       NULL ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (id_aptitud),
    UNIQUE INDEX idx_tmaaptitudes_codigo (codigo)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- -------------------------------------------------------------

CREATE TABLE IF NOT EXISTS tmacertificaciones (
    id_certificacion          BIGINT         NOT NULL AUTO_INCREMENT,
    codigo                    VARCHAR(50)    NOT NULL,
    descripcion               VARCHAR(200)   NOT NULL,
    usuario_reg               VARCHAR(100)   NULL,
    fecha_reg                 DATETIME       NOT NULL DEFAULT CURRENT_TIMESTAMP,
    usuario_act               VARCHAR(100)   NULL,
    fecha_act                 DATETIME       NULL ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (id_certificacion),
    UNIQUE INDEX idx_tmacertificaciones_codigo (codigo)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- -------------------------------------------------------------

CREATE TABLE IF NOT EXISTS tmaalmacenes (
    id_almacen                BIGINT         NOT NULL AUTO_INCREMENT,
    id_almacen_external       VARCHAR(50)    NOT NULL,
    codigo_almacen            VARCHAR(50)    NOT NULL,
    nombre_almacen            VARCHAR(200)   NOT NULL,
    direccion1                VARCHAR(200)   NULL,
    direccion2                VARCHAR(200)   NULL,
    direccion3                VARCHAR(200)   NULL,
    direccion4                VARCHAR(200)   NULL,
    ciudad                    VARCHAR(100)   NULL,
    codigo_provincia          VARCHAR(50)    NULL,
    codigo_postal             VARCHAR(20)    NULL,
    contacto                  VARCHAR(100)   NULL,
    telefono                  VARCHAR(50)    NULL,
    fax                       VARCHAR(50)    NULL,
    ruc                       VARCHAR(20)    NULL,
    usuario_reg               VARCHAR(100)   NULL,
    fecha_reg                 DATETIME       NOT NULL DEFAULT CURRENT_TIMESTAMP,
    usuario_act               VARCHAR(100)   NULL,
    fecha_act                 DATETIME       NULL ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (id_almacen),
    UNIQUE INDEX idx_tmaalmacenes_id_almacen_external (id_almacen_external)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- -------------------------------------------------------------

CREATE TABLE IF NOT EXISTS tmaempleados (
    id_empleado               BIGINT         NOT NULL AUTO_INCREMENT,
    id_empleado_external      VARCHAR(50)    NOT NULL,
    codigo                    VARCHAR(50)    NOT NULL,
    nombre_completo           VARCHAR(200)   NOT NULL,
    apellido                  VARCHAR(100)   NULL,
    nombre                    VARCHAR(100)   NULL,
    alias                     VARCHAR(100)   NULL,
    cargo                     VARCHAR(100)   NULL,
    dpto                      VARCHAR(100)   NULL,
    estado                    VARCHAR(20)    NULL,
    turno                     VARCHAR(50)    NULL,
    categoria                 VARCHAR(50)    NULL,
    id_usuario                VARCHAR(100)   NULL,
    frec_pago                 VARCHAR(50)    NULL,
    tip_emp                   VARCHAR(50)    NULL,
    gen_nomina                VARCHAR(10)    NULL,
    cta_sueldo                VARCHAR(50)    NULL,
    primer_nombre             VARCHAR(100)   NULL,
    segundo_nombre            VARCHAR(100)   NULL,
    primer_apellido           VARCHAR(100)   NULL,
    segundo_apellido          VARCHAR(100)   NULL,
    direccion1                VARCHAR(200)   NULL,
    direccion2                VARCHAR(200)   NULL,
    direccion3                VARCHAR(200)   NULL,
    direccion4                VARCHAR(200)   NULL,
    ciudad                    VARCHAR(100)   NULL,
    cod_provincia             VARCHAR(50)    NULL,
    cp                        VARCHAR(20)    NULL,
    municipio                 VARCHAR(100)   NULL,
    telefono                  VARCHAR(50)    NULL,
    tel_comercial             VARCHAR(50)    NULL,
    extension_tel             VARCHAR(20)    NULL,
    correo_elect              VARCHAR(150)   NULL,
    correo                    VARCHAR(150)   NULL,
    fecha_contr               DATETIME       NULL,
    fecha_revis               DATETIME       NULL,
    fecha_rescis              DATETIME       NULL,
    usuario_reg               VARCHAR(100)   NULL,
    fecha_reg                 DATETIME       NOT NULL DEFAULT CURRENT_TIMESTAMP,
    usuario_act               VARCHAR(100)   NULL,
    fecha_act                 DATETIME       NULL ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (id_empleado),
    UNIQUE INDEX idx_tmaempleados_id_empleado_external (id_empleado_external)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- -------------------------------------------------------------

CREATE TABLE IF NOT EXISTS tmacodsunidad1 (
    id_cod_unidad1            BIGINT         NOT NULL AUTO_INCREMENT,
    codigo                    VARCHAR(50)    NOT NULL,
    descripcion               VARCHAR(200)   NOT NULL,
    usuario_reg               VARCHAR(100)   NULL,
    fecha_reg                 DATETIME       NOT NULL DEFAULT CURRENT_TIMESTAMP,
    usuario_act               VARCHAR(100)   NULL,
    fecha_act                 DATETIME       NULL ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (id_cod_unidad1),
    UNIQUE INDEX idx_tmacodsunidad1_codigo (codigo)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- -------------------------------------------------------------

CREATE TABLE IF NOT EXISTS tmacuentascontables (
    id_cuenta_contable        BIGINT         NOT NULL AUTO_INCREMENT,
    cuenta                    VARCHAR(50)    NOT NULL,
    descripcion               VARCHAR(300)   NOT NULL,
    tipo                      VARCHAR(50)    NOT NULL,
    usuario_reg               VARCHAR(100)   NULL,
    fecha_reg                 DATETIME       NOT NULL DEFAULT CURRENT_TIMESTAMP,
    usuario_act               VARCHAR(100)   NULL,
    fecha_act                 DATETIME       NULL ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (id_cuenta_contable),
    UNIQUE INDEX idx_tmacuentascontables_cuenta (cuenta)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
