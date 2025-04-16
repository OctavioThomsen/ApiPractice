IF OBJECT_ID('Sp_GetActivatedClientsWithTotalAmount', 'P') IS NOT NULL
    DROP PROCEDURE Sp_GetActivatedClientsWithTotalAmount
GO

CREATE PROCEDURE Sp_GetActivatedClientsWithTotalAmount
	@ClientId INT = NULL
AS
BEGIN
	SELECT 
		c.Id, 
		c.Name, 
		c.CUIT, 
		c.Province,
		SUM(i.TotalAmount) AS TotalAmount,
		COUNT(i.Id) AS InvoicesCount
		FROM Clients c
	JOIN Invoices i ON c.Id = i.ClientId
	WHERE 
		c.IsActive = 1 AND
		(@ClientId IS NULL OR c.Id = @ClientId)
	GROUP BY 
		c.Id, 
		c.Name, 
		c.CUIT, 
		c.Province
	HAVING COUNT(i.Id) > 0
END
GO

-- Execute examples
-- EXEC Sp_GetActivatedClientsWithTotalAmount
-- EXEC Sp_GetActivatedClientsWithTotalAmount @ClientId = 0