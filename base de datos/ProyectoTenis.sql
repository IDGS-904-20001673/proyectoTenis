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
  id int NOT NULL PRIMARY KEY IDENTITY(1,1),
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
	materiaPrima varchar(255) NOT NULL,
	costo float NOT NULL,
	estatus int NOT NULL DEFAULT 1
);
GO

---------------------------------------------------------------------------------------------inserciones------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
GO
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
    @nombreP varchar(250),
    @materiPrima varchar(250),
	@costo float
	)
AS   
BEGIN
insert into provedores(nombreProvedor,materiaPrima,costo) values(
@nombreP, @materiPrima, @costo
);
END;
GO


--------------------------------------------------------------Ejecucion de los SP---------------------------------------------------------------------------------------------------------------------------------------------------------------
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


EXEC sp_insertar_proveedor
    @nombreP='Portal',
    @materiPrima='Calsetin',
    @costo=60;
GO
--------------------------------selects------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
select * from usuario; 
select * from domicilio; 
select * from provedores; 






