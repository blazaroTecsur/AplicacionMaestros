-- ── Tabla maestro de articulos ─────────────────────────────
CREATE TABLE tmaarticulo (
    IdArticulo INT NOT NULL AUTO_INCREMENT,
    IdArticuloExternal INT NOT NULL,
    
    Codigo VARCHAR(15) NOT NULL,
    Descripcion VARCHAR(500) NOT NULL,
    UnidadMedida VARCHAR(10) NULL,
    Tipo VARCHAR(20) NULL,
    Origen VARCHAR(50) NULL,
    CodigoProducto VARCHAR(50) NULL,
    CodigoAbc VARCHAR(2) NULL,
    
    SegLote TINYINT(1) NOT NULL DEFAULT 0,
    
    EstadoMaterial ENUM('ACTIVO','INACTIVO') NOT NULL DEFAULT 'ACTIVO',
    
    UsuarioReg VARCHAR(30) NULL,
    FechaReg DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    UsuarioAct VARCHAR(30) NULL,
    FechaAct DATETIME NULL ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (IdArticulo),
    
    UNIQUE KEY UQ_tmaarticulo_External (IdArticuloExternal),
    UNIQUE KEY UQ_tmaarticulo_Codigo (Codigo),

    INDEX IX_tmaarticulo_Tipo (Tipo),
    INDEX IX_tmaarticulo_Origen (Origen),
    INDEX IX_tmaarticulo_CodigoProducto (CodigoProducto),
    INDEX IX_tmaarticulo_Estado (EstadoMaterial)
);