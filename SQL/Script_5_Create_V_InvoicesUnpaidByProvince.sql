IF OBJECT_ID('V_InvoicesUnpaidByProvince', 'V') IS NOT NULL
    DROP VIEW V_InvoicesUnpaidByProvince
GO

CREATE VIEW V_InvoicesUnpaidByProvince
AS
	SELECT 
		c.Province,
		COUNT(i.Id) AS InvoicesCount,
		SUM(i.TotalAmount) AS TotalAmount
	FROM Invoices i
	JOIN Clients c ON i.ClientId = c.Id
	WHERE IsPaid = 0
	GROUP BY 
		c.Province
GO

-- Execute examples
-- SELECT * FROM V_InvoicesUnpaidByProvince