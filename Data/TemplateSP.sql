USE Db_Template;

GO

CREATE OR ALTER PROCEDURE accountVerify (@count VARCHAR(30), @pount VARCHAR(30)) 
	AS
		SELECT accounts.id,rols.id,rols.name FROM accounts 
		JOIN rols
		ON accounts.roleId = rols.id
		WHERE accounts.acount = @count AND accounts.pount = @pount;
		RETURN;

GO
--REGRESA UN BOOLEANO PARA VER SI EXISTE EL REGISTRO 
CREATE OR ALTER PROCEDURE existAccount (@count VARCHAR(30), @pount VARCHAR(30))
	AS
		BEGIN
		SELECT CAST(COUNT(1) AS BIT) FROM accounts 
		WHERE accounts.acount = @count AND accounts.pount = @pount;
		RETURN;
		END

GO

CREATE OR ALTER PROCEDURE exitsArticulo(@id	UNIQUEIDENTIFIER)
	AS 
		SELECT CAST(COUNT(1) AS BIT) FROM articulos
		WHERE articulos.id = @id;
		RETURN;
GO

CREATE OR ALTER PROCEDURE selectArticulosPagination(@page INT,@length INT)
	AS
		BEGIN
		DECLARE @skipReg INT = @page * @length;
		SELECT articulos.id, articulos.name, articulos.price FROM articulos
		ORDER BY articulos.name
		OFFSET @skipReg ROWS
		FETCH NEXT @length ROWS ONLY
		RETURN;
		END
GO

CREATE OR ALTER PROCEDURE countArticles
	AS 
		SELECT COUNT(*) FROM articulos;
		RETURN;

GO

CREATE OR ALTER PROCEDURE addArticulo(@id UNIQUEIDENTIFIER, @name VARCHAR(30), @price REAL)
	AS
		INSERT INTO articulos VALUES (@id,@name,@price);

GO

CREATE OR ALTER PROCEDURE updateArticulo(@id UNIQUEIDENTIFIER, @name VARCHAR(30), @price REAL)
	AS
		UPDATE articulos SET name = @name , price = @price
		WHERE articulos.id = @id;

GO

CREATE OR ALTER PROCEDURE deleteArticulo(@id UNIQUEIDENTIFIER)
	AS
		DELETE FROM articulos
			WHERE articulos.id = @id;