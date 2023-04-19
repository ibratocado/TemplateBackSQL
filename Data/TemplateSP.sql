CREATE OR ALTER PROCEDURE accountVerify (@count VARCHAR(30), @pount VARCHAR(30)) 
	AS
		SELECT accounts.id,rols.id,rols.name FROM accounts 
		JOIN rols
		ON accounts.roleId = rols.id
		WHERE accounts.acount = @count AND accounts.pount = @pount;
		RETURN;
