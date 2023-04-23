CREATE DATABASE Db_Template;
GO
USE Db_Template;
GO
CREATE TABLE rols(
	id INT PRIMARY KEY IDENTITY(1,1),
	name VARCHAR(10),
	description VARCHAR(15)
	);
GO
CREATE TABLE accounts(
	id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	acount VARCHAR(30),
	pount VARCHAR(30),
	roleId INT,
	FOREIGN KEY (roleId) REFERENCES rols(id) ON UPDATE CASCADE ON DELETE CASCADE
	);
GO
CREATE TABLE articulos(
	id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	name VARCHAR(30),
	price REAL
	);
GO
CREATE TABLE estudiantes
(
	clave INT NOT NULL,
	nombre VARCHAR (30) NOT NULL,
	escuela_procedencia VARCHAR (200) NOT NULL,
	CONSTRAINT PK_estudiantes PRIMARY KEY (clave)
)

SELECT * FROM estudiantes AS C1
WHERE (SELECT COUNT(*) FROM estudiantes AS C2 
		WHERE C1.escuela_procedencia = C2.escuela_procedencia) > 1;