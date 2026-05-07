-- ── Tabla maestro de almacenes ─────────────────────────────
CREATE TABLE IF NOT EXISTS tmaalmacen (
    IdAlmacen           BIGINT          NOT NULL AUTO_INCREMENT,
    IdAlmacenExternal   VARCHAR(50)     NOT NULL,
    CodigoAlmacen       VARCHAR(50)     NOT NULL,
    NombreAlmacen       VARCHAR(200)    NOT NULL,

    Direccion1          VARCHAR(200)    NULL,
    Direccion2          VARCHAR(200)    NULL,
    Direccion3          VARCHAR(200)    NULL,
    Direccion4          VARCHAR(200)    NULL,
    Ciudad              VARCHAR(100)    NULL,
    CodigoProvincia     VARCHAR(50)     NULL,
    CodigoPostal        VARCHAR(20)     NULL,
    Contacto            VARCHAR(100)    NULL,
    Telefono            VARCHAR(50)     NULL,
    Fax                 VARCHAR(50)     NULL,
    Ruc                 VARCHAR(20)     NULL,

    UsuarioReg          VARCHAR(100)    NULL,
    FechaReg            DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UsuarioAct          VARCHAR(100)    NULL,
    FechaAct            DATETIME        NULL ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (IdAlmacen),

    UNIQUE INDEX idx_tmaalmacen_IdAlmacenExternal (IdAlmacenExternal)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
