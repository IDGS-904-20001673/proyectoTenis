use master;
GO
DROP DATABASE IF EXISTS tenis;
GO
create database tenis;
GO
use tenis;
GO

-------------------------------------------------------------------------------------------------------------creacion de las tablas--------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE roles (
  idRole int NOT NULL IDENTITY(1,1) PRIMARY KEY ,
  nombre varchar(80) DEFAULT NULL,
  descripccion varchar(255) DEFAULT NULL
);

GO
CREATE TABLE domicilio(
	domicilioId int NOT NULL PRIMARY KEY IDENTITY(1,1),
	estado varchar(255) NOT NULL,
	municipio varchar(255) NOT NULL,
	codigoPostal int NOT NULL,
	colonia varchar(255) NOT NULL,
	calle varchar(255) NOT NULL,
	numeroExt int NOT NULL,
	numeroInt int DEFAULT NULL,
	referencia varchar(255) NOT NULL
);

GO
CREATE TABLE usuario (
  idUsuario int NOT NULL PRIMARY KEY IDENTITY(1,1),
  nombre varchar(50) NOT NULL,
  correo varchar(255) DEFAULT NULL,
  contrasenia varchar(255) NOT NULL,
  estatus tinyint DEFAULT 1,
  fecha_creacion datetime DEFAULT GETDATE(),
  idRole INT not null default 3,
  UNIQUE (correo),
  domicilioId int NOT NULL,
  CONSTRAINT fk_domiclio_id FOREIGN KEY (domicilioId) REFERENCES domicilio(domicilioId),
  CONSTRAINT fk_rol_id FOREIGN KEY (idRole) REFERENCES roles(idRole)
);
GO
CREATE TABLE provedores(
	proovedoresId int NOT NULL PRIMARY KEY IDENTITY(1,1),
	nombreProvedor varchar(255) NOT NULL,
	telefono varchar(255) NOT NULL,
	estatus int NOT NULL DEFAULT 1
);
GO


CREATE TABLE materiaPrima (
  materiaPrimaId int NOT NULL PRIMARY KEY IDENTITY(1,1),
  proovedoresId int NOT NULL,
  nombreMateriaPrima varchar(255)NOT NULL,
  cantidadTotal float NOT NULL,
  costo float NOT NULL,
  image_name varchar(255) DEFAULT NULL,
  CONSTRAINT fk_proovedores_proovedoresId FOREIGN KEY (proovedoresId) REFERENCES provedores(proovedoresId)
);
GO

CREATE TABLE compraMateriaPrima(
	compraMateriaPrimaID int NOT NULL PRIMARY KEY IDENTITY(1,1),
	materiaPrimaId int NOT NULL,
	cantidadCompra int NOT NULL,
	pagoTotal float NOT NULL,
	fecha datetime DEFAULT GETDATE(),
	CONSTRAINT fk_materiaPrima_materiaPrimaId FOREIGN KEY (materiaPrimaId) REFERENCES materiaPrima(materiaPrimaId)
);
GO

CREATE TABLE compraMateriaPrimaPuntos(
    compraMateriaPrimaPuntoID int NOT NULL PRIMARY KEY IDENTITY(10000, 1),
    materiaPrimaId int NOT NULL,
    cantidadCompra int NOT NULL,
    punto float not null,
    pagoTotal float NOT NULL,
    fecha datetime DEFAULT GETDATE(),
    CONSTRAINT fk_materiaPrimaPunto_materiaPrimaId FOREIGN KEY (materiaPrimaId) REFERENCES materiaPrima(materiaPrimaId)
);

GO

CREATE TABLE productos (
  idProducto int NOT NULL PRIMARY KEY IDENTITY(1,1),
  nombre varchar(255) NOT NULL,
  precio float DEFAULT NULL,
  descripccion varchar(255) DEFAULT NULL,
  image_name varchar(255) DEFAULT NULL,
  estatus int DEFAULT NULL
);
GO

CREATE TABLE ProductoDetalle (
    ProductoDetalleID int NOT NULL PRIMARY KEY IDENTITY(1, 1),
    idProducto int NOT NULL,
    punto float,
    cantidad_STOCK int,
    CONSTRAINT fk_idProducto_detalle FOREIGN KEY (idProducto) REFERENCES productos(idProducto)
);

CREATE TABLE detalleMateriaProducto (
  idDetalleMateriaProducto int NOT NULL PRIMARY KEY IDENTITY(1,1),
  ProductoDetalleID int NOT NULL,
  materiaPrimaId int NOT NULL,
  cantidadUsoMateria float NOT NULL,
  CONSTRAINT fk_detalleMateria_detalleProducto FOREIGN KEY (ProductoDetalleID) REFERENCES ProductoDetalle(ProductoDetalleID),
  CONSTRAINT fk_detalleMateriaProducto_materiaPrimaId FOREIGN KEY (materiaPrimaId) REFERENCES materiaPrima(materiaPrimaId)
);
GO

---------------------------------------------------------------------------------------------inserciones------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
INSERT INTO roles (nombre, descripccion) VALUES
('Admin', 'administrador'),
('Empleado', 'Empleado de la empresa'),
('Cliente', 'Cliente de la empresa'),
('Inactivo', 'Usuario Inactivo');

GO
INSERT INTO domicilio (estado,municipio,codigoPostal,colonia,calle,numeroExt,referencia)
VALUES
('guanajuato','leon','37570','Granjeno Plus','Granja eva','114','casa dos pisos porton negro'),
('guanajuato','leon','37570','Granjeno Plus','Granja Norma','232','casa roja'),
('guanajuato','leon','37570','Granjeno Plus','Granja Martha','452','casa verde con reja negra');

GO
INSERT INTO usuario (nombre, correo, contrasenia, estatus, idRole, domicilioId) 
VALUES
('angel', 'angeltovar308@gmail.com', 'sha256$5DGfv5cgFrKbMZz3$52389e87feb6e1a17cad14d8fe8fcef25bbecb0564c6a7ec4752e99f55328d79',1,1,1),
('Jose', 'angro1212@gmail.com', 'sha256$6j2avdMEX7UgH3HT$a9ea793320d38ead008540397c27054079389b76e12ab9b99da9568af83b5e53',1,2,2),
('pedro', 'LeoGuapo@gmail.com', 'sha256$ERUhmMIzpUKtOSw4$eddbb4db44c30094412ebfebdfe42db31de476ae19d75d41193bf281325048a0',1,3,3);




-------------------------------------------------------------------------------------Store procedures-------------------------------------------------------------------------------------------------------------------------------------------------------------------
GO
CREATE PROCEDURE sp_consultar_usuario(
    @email varchar(255),
    @pass varchar(255)
	)
AS   
BEGIN
select * from usuario inner join roles on
usuario.idRole = roles.idRole inner join domicilio on
usuario.domicilioId = domicilio.domicilioId
where usuario.correo like @email and usuario.contrasenia like @pass;
END;
GO
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE sp_registrar_usuario(
    @estado varchar(255),
    @municipio varchar(255),
    @codigoPostal int,
    @colonia varchar(255),
    @calle varchar(255),
    @numeroExt int,
    @numeroInt int,
    @referencia varchar(255),

    @nombre varchar(255),
    @correo varchar(255),
    @contrasenia varchar(255),
    @idRole varchar(255)
	)
AS   
BEGIN
DECLARE @idDomicilio INT;

insert into domicilio(estado, municipio, codigoPostal, colonia, calle, numeroExt, numeroInt, referencia) values(
@estado,@municipio,@codigoPostal,@colonia, @calle,@numeroExt,@numeroInt,@referencia
);

SET @idDomicilio = SCOPE_IDENTITY();


insert into usuario(nombre,correo, contrasenia, idRole, domicilioId) values(
@nombre, @correo, @contrasenia, @idRole, @idDomicilio
);

END;
GO 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE sp_insertar_proveedor(
    @nombreP varchar(255),
    @telefono varchar(255)
	)
AS   
BEGIN
insert into provedores(nombreProvedor,telefono ) values(
@nombreP, @telefono
);
END;
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE sp_verificar_correo_existente
    @correo VARCHAR(100)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM usuario WHERE correo = @correo)
        SELECT 1 AS correo_existente;
    ELSE
        SELECT 0 AS correo_existente;
END
GO
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE sp_baja_proveedor(
    @id int
	)
AS
BEGIN

	declare @esta int;

    SELECT @esta = CASE WHEN estatus = 0 
	THEN 1 
	ELSE 0 
	END FROM provedores WHERE proovedoresId = @id;

    UPDATE provedores
    SET estatus = @esta
    WHERE proovedoresId = @id;
END;
GO
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE sp_mostar_proveedores_inactivos
AS
BEGIN
select * from provedores where estatus = 0;

END;
GO
----------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE sp_mostar_proveedores_activos
AS
BEGIN
select * from provedores where estatus = 1;

END;
GO
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE sp_Nueva_materiaPrima(
    @proovedoresId int,
    @nombreMateriaPrima varchar(255),
    @costo float,
    @image_name varchar(255)
)
AS   
BEGIN

        INSERT INTO materiaPrima (proovedoresId, nombreMateriaPrima, cantidadTotal, costo, image_name)
        VALUES (@proovedoresId, @nombreMateriaPrima, 0, @costo, @image_name);
END;
GO
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE sp_mostrar_materiaPrima
AS   
BEGIN

select * from materiaPrima;
END;
GO

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE sp_mostrar_materiaPrimaNormal
AS   
BEGIN
SELECT * FROM materiaPrima where nombreMateriaPrima Not like '%del%'
END;
GO

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE sp_mostrar_materiaPrimaPuntos
AS   
BEGIN
SELECT * FROM materiaPrima where nombreMateriaPrima like '%del%';
END;
GO
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE sp_comprar_materiaPrima(
    @materiaPrimaId int,
    @cantidadCompra int,
    @pagoTotal float
	)
AS   
BEGIN

insert into compraMateriaPrima(materiaPrimaId, cantidadCompra, pagoTotal) values(
@materiaPrimaId, @cantidadCompra, @pagoTotal
);

 UPDATE materiaPrima
    SET cantidadTotal = cantidadTotal + @cantidadCompra
    WHERE materiaPrimaId = @materiaPrimaId;

END;
GO
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE sp_mostrar_comprasMateriaPrima
AS   
BEGIN
SELECT 
    COALESCE(compraMateriaPrima.compraMateriaPrimaId  , compraMateriaPrimaPuntos.compraMateriaPrimaPuntoId) AS compraMateriaPrimaId,
    COALESCE(compraMateriaPrima.materiaPrimaId, compraMateriaPrimaPuntos.materiaPrimaId) AS materiaPrimaId,
    COALESCE(compraMateriaPrima.cantidadCompra, compraMateriaPrimaPuntos.cantidadCompra) AS cantidadCompra,
    COALESCE(compraMateriaPrima.pagoTotal, compraMateriaPrimaPuntos.pagoTotal) AS pagoTotal,
    COALESCE(compraMateriaPrima.fecha, compraMateriaPrimaPuntos.fecha) AS fecha
FROM materiaPrima
LEFT JOIN compraMateriaPrima ON materiaPrima.materiaPrimaId = compraMateriaPrima.materiaPrimaId
LEFT JOIN compraMateriaPrimaPuntos ON materiaPrima.materiaPrimaId = compraMateriaPrimaPuntos.materiaPrimaId;
END;
GO
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE sp_productoNuevo(
    @nombre varchar(255),
	@precio float,
    @descripccion varchar(255),
    @image_name varchar(255)
)
AS   
BEGIN

        INSERT INTO productos(nombre, precio, descripccion, image_name, estatus )
        VALUES (@nombre, @precio, @descripccion, @image_name, 1);
END;
GO
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE sp_mostarProductosActivos
AS   
BEGIN
select * from productos where estatus = 1;
END;
GO

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE sp_mostarProductosInactivos
AS   
BEGIN
select * from productos where estatus = 0;
END;
GO
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE sp_atualizar_usuario(
  @idUsuario int,
  @estado varchar(255) = NULL,
  @municipio varchar(255) = NULL,
  @codigoPostal int = NULL,
  @colonia varchar(255) = NULL,
  @calle varchar(255) = NULL,
  @numeroExt int = NULL,
  @numeroInt int = NULL,
  @referencia varchar(255) = NULL
)
AS   
BEGIN

    DECLARE @iDomcilio int;

    SELECT @iDomcilio = domicilioId FROM usuario WHERE idUsuario = @idUsuario;

    IF @iDomcilio IS NOT NULL
    BEGIN
        UPDATE domicilio
        SET
            estado = CASE WHEN @estado IS NULL or @estado = '' THEN estado ELSE @estado END,
            municipio = CASE WHEN @municipio IS NULL or @municipio = '' THEN municipio ELSE @municipio END,
            codigoPostal = CASE WHEN @codigoPostal IS NULL or @codigoPostal = 0  THEN codigoPostal ELSE @codigoPostal END,
            colonia = CASE WHEN @colonia IS NULL or @colonia = '' THEN colonia ELSE @colonia END,
            calle = CASE WHEN @calle IS NULL or @calle = '' THEN calle ELSE @calle END,
            numeroExt = CASE WHEN @numeroExt IS NULL or @numeroExt = 0 THEN numeroExt ELSE @numeroExt END,
            numeroInt = CASE WHEN @numeroInt IS NULL or @numeroInt = 0 THEN numeroInt ELSE @numeroInt END,
            referencia = CASE WHEN @referencia IS NULL or @referencia = '' THEN referencia ELSE @referencia END
        WHERE domicilioId = @iDomcilio;
    END;

END;
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE sp_comprar_materiaPrimaPunto(
    @materiaPrimaId int,
    @cantidadCompra int,
    @pagoTotal float,
	@punto int 
	)
AS   
BEGIN

insert into compraMateriaPrimaPuntos(materiaPrimaId, cantidadCompra, pagoTotal, punto) values(
@materiaPrimaId, @cantidadCompra, @pagoTotal, @punto
);

 UPDATE materiaPrima
    SET cantidadTotal = cantidadTotal + @cantidadCompra
    WHERE materiaPrimaId = @materiaPrimaId;

END;
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------
create PROCEDURE sp_estatus_producto(
    @idProducto int
	)
AS
BEGIN

	declare @esta int;

    SELECT @esta = CASE WHEN estatus = 0 
	THEN 1 
	ELSE 0 
	END FROM productos WHERE idProducto = @idProducto;


	UPDATE productos
    SET estatus = @esta
    WHERE idProducto = @idProducto;

END;
GO
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE sp_ProductoDetalle(
    @idProducto int,
    @punto float
	)
AS   
BEGIN

insert into ProductoDetalle(idProducto, punto, cantidad_STOCK) values
(@idProducto, @punto, 0)

END;
GO
------------------------------------------------------------------------------------------------------------------------------------------

create PROCEDURE sp_mostrar_puntosProducto(
    @idProducto int
	)
AS
BEGIN
    SELECT * from ProductoDetalle where idProducto = @idProducto;

END;
GO
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE sp_DetalleMateriaProducto(
    @idProductoDetalle int,
    @materiaPrimaId int,
    @cantidadUsoMateria float
	)
AS   
BEGIN

insert into detalleMateriaProducto(ProductoDetalleID, materiaPrimaId, cantidadUsoMateria ) values
(@idProductoDetalle, @materiaPrimaId, @cantidadUsoMateria)

END;
GO
------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE sp_MostrarDetalleMateriaProducto(
    @idProductoDetalle int
	)
AS   
BEGIN

select * from detalleMateriaProducto where ProductoDetalleID=@idProductoDetalle;

END;
GO
------------------------------------------------------------------------Ejecucion de los SP-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
EXEC sp_registrar_usuario
    @estado='Guanajuato',
    @municipio='Leon',
    @codigoPostal=37190,
    @colonia='La florida',
    @calle='san placido',
    @numeroExt=145,
    @numeroInt='',
    @referencia='Porton blanco',
    @nombre='Leonel Robledo',
    @correo='robledo@gmail.com',
    @contrasenia='leonel2002',
    @idRole=1;
GO

EXEC sp_insertar_proveedor
    @nombreP='cyprus',
    @telefono='477123873'
GO
EXEC sp_insertar_proveedor
    @nombreP='toefl',
    @telefono='4771276873'
GO
EXEC sp_nueva_materiaPrima
    @proovedoresId = 1,
    @nombreMateriaPrima = 'PVC',
    @costo = 517.20,
	@image_name = 'C:\Users\roble_p4xiojp\OneDrive\Escritorio\proyecto\base de datos'
GO
EXEC sp_nueva_materiaPrima
    @proovedoresId = 1,
    @nombreMateriaPrima = 'Suela del 5',
    @costo = 52.20,
	@image_name = 'C:\Users\roble_p4xiojp\OneDrive\Escritorio\proyecto\base de datos'
GO
exec sp_comprar_materiaPrima @materiaPrimaId=1, @cantidadCompra=25, @pagoTotal=23.362;
GO
exec sp_comprar_materiaPrima @materiaPrimaId=1, @cantidadCompra=51, @pagoTotal=233.62;
GO
exec sp_comprar_materiaPrimaPunto @materiaPrimaId=2, @cantidadCompra=21, @pagoTotal=372.62, @punto=5;
GO
EXEC sp_productoNuevo
    @nombre='Tenis Dama',
    @precio=500,
    @descripccion='zapato bonito para dama',
    @image_name='https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSVr0o0FVrzqLrAQa-ivchzoLiZjksjphqopA&usqp=CAU';
GO

EXEC sp_productoNuevo
    @nombre='Tenis Bato',
    @precio=500,
    @descripccion='zapato bonito para bato',
    @image_name='https://http2.mlstatic.com/D_NQ_NP_605573-MLM44430900575_122020-O.webp';
GO

EXEC sp_ProductoDetalle @idProducto = 1, @punto=3;
GO
exec sp_DetalleMateriaProducto @idProductoDetalle=1, @materiaPrimaId=1, @cantidadUsoMateria=0.05;
GO
--------------------------------------------------------------------------------------selects------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
select * from usuario; 
select * from domicilio; 
select * from provedores; 
select * from materiaPrima;
select * from compraMateriaPrima;
select * from productos;
select * from ProductoDetalle;
select * from detalleMateriaProducto;

  UPDATE materiaPrima
    SET cantidadTotal = cantidadTotal-0.05
    WHERE materiaPrimaId = 1;


	EXEC sp_atualizar_usuario
    @idUsuario=1,
    @estado='sinaloa',
    @municipio='Mazatlan',
    @codigoPostal=null,
    @colonia='',
    @calle='',
    @numeroExt=null,
    @numeroInt=null,
    @referencia='';

