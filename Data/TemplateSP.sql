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

 

GO

CREATE OR ALTER PROCEDURE dbo.selectFullRols
AS 
	SELECT id,name FROM rols;


GO

CREATE OR ALTER PROCEDURE dbo.addUser(
@idAccount UNIQUEIDENTIFIER,
@idUser UNIQUEIDENTIFIER,
@roleId INT,
@curp VARCHAR(20),
@accountId UNIQUEIDENTIFIER,
@name VARCHAR(30),
@lastName VARCHAR(30),
@secondLastName VARCHAR(30),
@salary FLOAT,
@phone VARCHAR(14),
@acount VARCHAR(30),
@pount VARCHAR(30)
) AS
	BEGIN 
		INSERT INTO accounts (id,acount,pount,roleId) VALUES(
			@idAccount,
			@acount,
			@pount,
			@roleId
		);
		
		INSERT INTO users (id,curp,accountId,name,lastName,secondLastName,salary,phone) VALUES(
			@idUser,
			@curp,
			@accountId,
			@name,
			@lastName,
			@secondLastName,
			@salary,
			@phone
		);
	END
GO

CREATE OR ALTER PROCEDURE dbo.UpdateUser(
@idUser UNIQUEIDENTIFIER,
@curp VARCHAR(20),
@accountId UNIQUEIDENTIFIER,
@name VARCHAR(30),
@lastName VARCHAR(30),
@secondLastName VARCHAR(30),
@salary FLOAT,
@phone VARCHAR(14)
) AS
	BEGIN 
		
		UPDATE users SET 
			curp = @curp,
			name = @name,
			lastName = @lastName,
			secondLastName = @secondLastName,
			salary = @salary,
			phone = @phone
			WHERE id = @idUser;
	END
GO	

CREATE OR ALTER PROCEDURE dbo.DeleteUser(
@idUser UNIQUEIDENTIFIER
) AS 
	DELETE FROM users WHERE id = @idUser;
GO

CREATE OR ALTER PROCEDURE dbo.SelectFullUsers(@page INT,@length INT)
AS 
	BEGIN
		DECLARE @skipReg INT = @page * @length;

		SELECT users.id,
		rols.name,
		rols.id,
		accounts.acount,
		users.curp,
		users.lastName,
		users.secondLastName,
		users.name,
		users.salary,
		users.phone 
		FROM users 
		JOIN accounts 
		ON accounts.id = users.accountId
		JOIN rols
		ON accounts.roleId = rols.id 
		WHERE users.state = 1
		ORDER BY users.LastName
		OFFSET @skipReg ROWS
		FETCH NEXT @length ROWS ONLY
		RETURN 
	END;
GO

CREATE OR ALTER PROCEDURE dbo.SelectOneUser(@id UNIQUEIDENTIFIER)
AS 
	BEGIN

		SELECT users.id,
		rols.name,
		rols.id,
		accounts.acount,
		users.curp,
		users.lastName,
		users.secondLastName,
		users.name,
		users.salary,
		users.phone 
		FROM users 
		JOIN accounts 
		ON accounts.id = users.accountId
		JOIN rols
		ON accounts.roleId = rols.id 
		WHERE users.id = @id AND users.state = 1
		RETURN 
	END;
GO

CREATE OR ALTER PROCEDURE dbo.exitsUser(@id UNIQUEIDENTIFIER)
AS	
	SELECT CAST(COUNT(1) AS BIT) FROM users WHERE id = @id; 
GO

CREATE OR ALTER PROCEDURE dbo.exitsCurpUser(@curp Varchar(20))
AS	
	SELECT CAST(COUNT(1) AS BIT) FROM users WHERE @curp = curp; 
GO

CREATE OR ALTER PROCEDURE dbo.countUsers
AS 
	SELECT COUNT(*) FROM users WHERE state = 1;
GO

CREATE OR ALTER PROCEDURE dbo.DeleteLogcicUser(
@idUser UNIQUEIDENTIFIER
) AS 
	UPDATE users SET users.state = 0 WHERE id = @idUser;

exec dbo.countUsers