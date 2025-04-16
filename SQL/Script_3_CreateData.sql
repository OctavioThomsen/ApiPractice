IF OBJECT_ID('dbo.Invoices', 'U') IS NOT NULL AND OBJECT_ID('dbo.Clients', 'U') IS NOT NULL
BEGIN
    DELETE FROM Invoices;
    DBCC CHECKIDENT ('Invoices', RESEED, 0);

    DELETE FROM Clients;
    DBCC CHECKIDENT ('Clients', RESEED, 0);

    DECLARE @i INT = 1;

    WHILE @i <= 20
    BEGIN
        INSERT INTO Clients (
            Name,
            BusinessName,
            CUIT,
            IsActive,
            ActivationDate,
            Email,
            Phone,
            Address,
            City,
            Province
        )
        VALUES (
            CONCAT('Cliente ', @i),
            CONCAT('Empresa ', @i),
            CONCAT('20-100', RIGHT('000' + CAST(@i AS VARCHAR), 3), '-1'),
            CASE WHEN @i % 2 = 0 THEN 1 ELSE 0 END,
            DATEADD(DAY, -(@i * 10), GETDATE()),
            CONCAT('cliente', @i, '@mail.com'),
            CONCAT('11-4444-', 1000 + @i),
            CONCAT('Calle ', @i, ' nro ', @i * 10),
            CONCAT('Ciudad ', @i % 5),
            CONCAT('Provincia ', @i % 3)
        );

        SET @i += 1;
    END

    SET @i = 1;
    DECLARE @ClientId INT;
    DECLARE @InvoiceDate DATE;
    DECLARE @TotalAmount DECIMAL(10,2);
    DECLARE @IsPaid BIT;

    WHILE @i <= 40
    BEGIN
        SET @ClientId = CAST(RAND(CHECKSUM(NEWID())) * 20 + 1 AS INT);
        SET @InvoiceDate = DATEADD(DAY, -CAST(RAND(CHECKSUM(NEWID())) * 200 AS INT), GETDATE());
        SET @TotalAmount = CAST(RAND(CHECKSUM(NEWID())) * (9999 - 1000) + 1000 AS DECIMAL(10,2));
        SET @IsPaid = CASE WHEN @i % 3 = 0 THEN 1 ELSE 0 END;

        INSERT INTO Invoices (ClientId, Date, TotalAmount, IsPaid)
        VALUES (@ClientId, @InvoiceDate, @TotalAmount, @IsPaid);

        SET @i += 1;
    END
END
GO