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

exec existAccount 'jaun','1234'

alter table rols alter column description varchar(50);