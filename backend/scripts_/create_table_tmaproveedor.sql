-- ── Tabla maestro de proveedores ─────────────────────────────
CREATE TABLE tmaproveedor (
    IdProveedor INT NOT NULL AUTO_INCREMENT,
    IdProveedorExternal INT NOT NULL,
    NombreProveedor VARCHAR(255) NOT NULL,
    TipoPersona VARCHAR(2) NOT NULL,
    
    Direccion1 VARCHAR(255) NULL,
    Direccion2 VARCHAR(255) NULL,
    Direccion3 VARCHAR(255) NULL,
    Direccion4 VARCHAR(255) NULL,
    
    Comprador VARCHAR(150) NULL,
    Estado VARCHAR(10) NOT NULL,
    
    Contacto VARCHAR(250) NULL,
    TelefonoContacto VARCHAR(20) NULL,
    CorreoExternoContacto VARCHAR(100) NULL,
    CorreoInternoContacto VARCHAR(100) NULL,
    
    Ruc VARCHAR(20) NOT NULL,
    
    UsuarioReg VARCHAR(30) NULL,
    FechaReg DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    UsuarioAct VARCHAR(30) NULL,
    FechaAct DATETIME NULL ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (IdProveedor),
    
    UNIQUE KEY UQ_tmaproveedor_IdProveedorExternal (IdProveedorExternal),
    UNIQUE KEY UQ_tmaproveedor_Ruc (Ruc),
    
    INDEX IX_tmaproveedor_NombreProveedor (NombreProveedor)
);

-- ── Datos de prueba ────────────────────────────────────────
INSERT INTO tmaproveedor
(
    IdProveedorExternal,
    NombreProveedor,
    TipoPersona,
    Ruc,
    Estado
)
VALUES
(
    1,
    'LUZ DEL SUR S.A.A.',
    '2',
    '20331898008',
    'A'
);