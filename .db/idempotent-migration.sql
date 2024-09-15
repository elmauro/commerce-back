IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240914032531_InitialCreate'
)
BEGIN
    CREATE TABLE [Categories] (
        [CategoryId] uniqueidentifier NOT NULL,
        [CategoryName] nvarchar(255) NOT NULL,
        [CreatedBy] nvarchar(max) NOT NULL,
        [LastUpdatedBy] nvarchar(max) NOT NULL,
        [CreatedAt] datetimeoffset NOT NULL DEFAULT (SYSDATETIMEOFFSET()),
        [LastUpdatedAt] datetimeoffset NOT NULL DEFAULT (SYSDATETIMEOFFSET()),
        CONSTRAINT [PK_Categories] PRIMARY KEY ([CategoryId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240914032531_InitialCreate'
)
BEGIN
    CREATE TABLE [Customers] (
        [CustomerId] uniqueidentifier NOT NULL,
        [FirstName] nvarchar(255) NOT NULL,
        [LastName] nvarchar(255) NOT NULL,
        [Email] nvarchar(255) NOT NULL,
        [Phone] nvarchar(20) NOT NULL,
        [CreatedBy] nvarchar(max) NOT NULL,
        [LastUpdatedBy] nvarchar(max) NOT NULL,
        [CreatedAt] datetimeoffset NOT NULL DEFAULT (SYSDATETIMEOFFSET()),
        [LastUpdatedAt] datetimeoffset NOT NULL DEFAULT (SYSDATETIMEOFFSET()),
        CONSTRAINT [PK_Customers] PRIMARY KEY ([CustomerId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240914032531_InitialCreate'
)
BEGIN
    CREATE TABLE [Products] (
        [ProductId] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Code] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Price] decimal(18,2) NOT NULL,
        [Stock] int NOT NULL,
        [CreatedBy] nvarchar(max) NOT NULL,
        [LastUpdatedBy] nvarchar(max) NOT NULL,
        [CreatedAt] datetimeoffset NOT NULL DEFAULT (SYSDATETIMEOFFSET()),
        [LastUpdatedAt] datetimeoffset NOT NULL DEFAULT (SYSDATETIMEOFFSET()),
        CONSTRAINT [PK_Products] PRIMARY KEY ([ProductId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240914032531_InitialCreate'
)
BEGIN
    CREATE TABLE [Orders] (
        [OrderId] uniqueidentifier NOT NULL,
        [OrderDate] datetime2 NOT NULL,
        [TotalAmount] decimal(10,2) NOT NULL,
        [CustomerId] uniqueidentifier NOT NULL,
        [CreatedBy] nvarchar(max) NOT NULL,
        [LastUpdatedBy] nvarchar(max) NOT NULL,
        [CreatedAt] datetimeoffset NOT NULL DEFAULT (SYSDATETIMEOFFSET()),
        [LastUpdatedAt] datetimeoffset NOT NULL DEFAULT (SYSDATETIMEOFFSET()),
        CONSTRAINT [PK_Orders] PRIMARY KEY ([OrderId]),
        CONSTRAINT [FK_Orders_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([CustomerId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240914032531_InitialCreate'
)
BEGIN
    CREATE TABLE [ProductCategory] (
        [ProductId] uniqueidentifier NOT NULL,
        [CategoryId] uniqueidentifier NOT NULL,
        [CreatedBy] nvarchar(max) NOT NULL,
        [LastUpdatedBy] nvarchar(max) NOT NULL,
        [CreatedAt] datetimeoffset NOT NULL DEFAULT (SYSDATETIMEOFFSET()),
        [LastUpdatedAt] datetimeoffset NOT NULL DEFAULT (SYSDATETIMEOFFSET()),
        CONSTRAINT [PK_ProductCategory] PRIMARY KEY ([ProductId], [CategoryId]),
        CONSTRAINT [FK_ProductCategory_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([CategoryId]) ON DELETE CASCADE,
        CONSTRAINT [FK_ProductCategory_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240914032531_InitialCreate'
)
BEGIN
    CREATE TABLE [OrderProducts] (
        [OrderId] uniqueidentifier NOT NULL,
        [ProductId] uniqueidentifier NOT NULL,
        [Quantity] int NOT NULL,
        [CreatedBy] nvarchar(max) NOT NULL,
        [LastUpdatedBy] nvarchar(max) NOT NULL,
        [CreatedAt] datetimeoffset NOT NULL DEFAULT (SYSDATETIMEOFFSET()),
        [LastUpdatedAt] datetimeoffset NOT NULL DEFAULT (SYSDATETIMEOFFSET()),
        CONSTRAINT [PK_OrderProducts] PRIMARY KEY ([OrderId], [ProductId]),
        CONSTRAINT [FK_OrderProducts_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([OrderId]) ON DELETE CASCADE,
        CONSTRAINT [FK_OrderProducts_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240914032531_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'CategoryName', N'CreatedAt', N'CreatedBy', N'LastUpdatedAt', N'LastUpdatedBy') AND [object_id] = OBJECT_ID(N'[Categories]'))
        SET IDENTITY_INSERT [Categories] ON;
    EXEC(N'INSERT INTO [Categories] ([CategoryId], [CategoryName], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
    VALUES (''22222222-2222-2222-2222-222222222221'', N''Electronics'', ''2024-09-14T03:25:31.0089650+00:00'', N'''', ''2024-09-14T03:25:31.0089652+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222222'', N''Books'', ''2024-09-14T03:25:31.0089654+00:00'', N'''', ''2024-09-14T03:25:31.0089655+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222223'', N''Clothing'', ''2024-09-14T03:25:31.0089657+00:00'', N'''', ''2024-09-14T03:25:31.0089658+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222224'', N''Home & Kitchen'', ''2024-09-14T03:25:31.0089660+00:00'', N'''', ''2024-09-14T03:25:31.0089661+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222225'', N''Sports'', ''2024-09-14T03:25:31.0089663+00:00'', N'''', ''2024-09-14T03:25:31.0089663+00:00'', N'''')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'CategoryName', N'CreatedAt', N'CreatedBy', N'LastUpdatedAt', N'LastUpdatedBy') AND [object_id] = OBJECT_ID(N'[Categories]'))
        SET IDENTITY_INSERT [Categories] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240914032531_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CustomerId', N'CreatedAt', N'CreatedBy', N'Email', N'FirstName', N'LastName', N'LastUpdatedAt', N'LastUpdatedBy', N'Phone') AND [object_id] = OBJECT_ID(N'[Customers]'))
        SET IDENTITY_INSERT [Customers] ON;
    EXEC(N'INSERT INTO [Customers] ([CustomerId], [CreatedAt], [CreatedBy], [Email], [FirstName], [LastName], [LastUpdatedAt], [LastUpdatedBy], [Phone])
    VALUES (''00000000-0000-0000-0000-000000000001'', ''2024-09-14T03:25:31.0094833+00:00'', N'''', N''john.doe@example.com'', N''John'', N''Doe'', ''2024-09-14T03:25:31.0094834+00:00'', N'''', N''123-456-7890''),
    (''00000000-0000-0000-0000-000000000002'', ''2024-09-14T03:25:31.0094838+00:00'', N'''', N''jane.smith@example.com'', N''Jane'', N''Smith'', ''2024-09-14T03:25:31.0094838+00:00'', N'''', N''234-567-8901''),
    (''00000000-0000-0000-0000-000000000003'', ''2024-09-14T03:25:31.0094847+00:00'', N'''', N''michael.johnson@example.com'', N''Michael'', N''Johnson'', ''2024-09-14T03:25:31.0094848+00:00'', N'''', N''345-678-9012''),
    (''00000000-0000-0000-0000-000000000004'', ''2024-09-14T03:25:31.0094851+00:00'', N'''', N''emily.williams@example.com'', N''Emily'', N''Williams'', ''2024-09-14T03:25:31.0094851+00:00'', N'''', N''456-789-0123''),
    (''00000000-0000-0000-0000-000000000005'', ''2024-09-14T03:25:31.0094853+00:00'', N'''', N''chris.brown@example.com'', N''Chris'', N''Brown'', ''2024-09-14T03:25:31.0094854+00:00'', N'''', N''567-890-1234'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CustomerId', N'CreatedAt', N'CreatedBy', N'Email', N'FirstName', N'LastName', N'LastUpdatedAt', N'LastUpdatedBy', N'Phone') AND [object_id] = OBJECT_ID(N'[Customers]'))
        SET IDENTITY_INSERT [Customers] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240914032531_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'Code', N'CreatedAt', N'CreatedBy', N'Description', N'LastUpdatedAt', N'LastUpdatedBy', N'Price', N'Stock', N'Title') AND [object_id] = OBJECT_ID(N'[Products]'))
        SET IDENTITY_INSERT [Products] ON;
    EXEC(N'INSERT INTO [Products] ([ProductId], [Code], [CreatedAt], [CreatedBy], [Description], [LastUpdatedAt], [LastUpdatedBy], [Price], [Stock], [Title])
    VALUES (''11111111-1111-1111-1111-111111111111'', N''P001'', ''2024-09-14T03:25:31.0124029+00:00'', N'''', N''A powerful laptop'', ''2024-09-14T03:25:31.0124030+00:00'', N'''', 1000.0, 50, N''Laptop''),
    (''11111111-1111-1111-1111-111111111112'', N''P002'', ''2024-09-14T03:25:31.0124034+00:00'', N'''', N''A modern smartphone'', ''2024-09-14T03:25:31.0124035+00:00'', N'''', 800.0, 30, N''Smartphone''),
    (''11111111-1111-1111-1111-111111111113'', N''P003'', ''2024-09-14T03:25:31.0124038+00:00'', N'''', N''A versatile tablet'', ''2024-09-14T03:25:31.0124039+00:00'', N'''', 500.0, 20, N''Tablet''),
    (''11111111-1111-1111-1111-111111111114'', N''P004'', ''2024-09-14T03:25:31.0124041+00:00'', N'''', N''Noise-canceling headphones'', ''2024-09-14T03:25:31.0124042+00:00'', N'''', 200.0, 100, N''Headphones''),
    (''11111111-1111-1111-1111-111111111115'', N''P005'', ''2024-09-14T03:25:31.0124045+00:00'', N'''', N''A stylish smartwatch'', ''2024-09-14T03:25:31.0124045+00:00'', N'''', 250.0, 60, N''Smartwatch''),
    (''11111111-1111-1111-1111-111111111116'', N''P006'', ''2024-09-14T03:25:31.0124048+00:00'', N'''', N''A 24-inch monitor'', ''2024-09-14T03:25:31.0124049+00:00'', N'''', 300.0, 40, N''Monitor''),
    (''11111111-1111-1111-1111-111111111117'', N''P007'', ''2024-09-14T03:25:31.0124052+00:00'', N'''', N''Mechanical keyboard'', ''2024-09-14T03:25:31.0124052+00:00'', N'''', 100.0, 70, N''Keyboard''),
    (''11111111-1111-1111-1111-111111111118'', N''P008'', ''2024-09-14T03:25:31.0124055+00:00'', N'''', N''Wireless mouse'', ''2024-09-14T03:25:31.0124055+00:00'', N'''', 50.0, 80, N''Mouse''),
    (''11111111-1111-1111-1111-111111111119'', N''P009'', ''2024-09-14T03:25:31.0124061+00:00'', N'''', N''Ergonomic office chair'', ''2024-09-14T03:25:31.0124062+00:00'', N'''', 150.0, 25, N''Chair''),
    (''11111111-1111-1111-1111-111111111120'', N''P010'', ''2024-09-14T03:25:31.0124065+00:00'', N'''', N''Adjustable standing desk'', ''2024-09-14T03:25:31.0124065+00:00'', N'''', 500.0, 20, N''Desk''),
    (''11111111-1111-1111-1111-111111111121'', N''P011'', ''2024-09-14T03:25:31.0124068+00:00'', N'''', N''Wireless printer'', ''2024-09-14T03:25:31.0124069+00:00'', N'''', 200.0, 15, N''Printer''),
    (''11111111-1111-1111-1111-111111111122'', N''P012'', ''2024-09-14T03:25:31.0124071+00:00'', N'''', N''Document scanner'', ''2024-09-14T03:25:31.0124072+00:00'', N'''', 150.0, 30, N''Scanner''),
    (''11111111-1111-1111-1111-111111111123'', N''P013'', ''2024-09-14T03:25:31.0124074+00:00'', N'''', N''HD webcam'', ''2024-09-14T03:25:31.0124075+00:00'', N'''', 80.0, 50, N''Webcam''),
    (''11111111-1111-1111-1111-111111111124'', N''P014'', ''2024-09-14T03:25:31.0124077+00:00'', N'''', N''Wireless router'', ''2024-09-14T03:25:31.0124078+00:00'', N'''', 120.0, 45, N''Router''),
    (''11111111-1111-1111-1111-111111111125'', N''P015'', ''2024-09-14T03:25:31.0124080+00:00'', N'''', N''1TB external hard drive'', ''2024-09-14T03:25:31.0124081+00:00'', N'''', 90.0, 100, N''External Hard Drive'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'Code', N'CreatedAt', N'CreatedBy', N'Description', N'LastUpdatedAt', N'LastUpdatedBy', N'Price', N'Stock', N'Title') AND [object_id] = OBJECT_ID(N'[Products]'))
        SET IDENTITY_INSERT [Products] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240914032531_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'OrderId', N'CreatedAt', N'CreatedBy', N'CustomerId', N'LastUpdatedAt', N'LastUpdatedBy', N'OrderDate', N'TotalAmount') AND [object_id] = OBJECT_ID(N'[Orders]'))
        SET IDENTITY_INSERT [Orders] ON;
    EXEC(N'INSERT INTO [Orders] ([OrderId], [CreatedAt], [CreatedBy], [CustomerId], [LastUpdatedAt], [LastUpdatedBy], [OrderDate], [TotalAmount])
    VALUES (''33333333-3333-3333-3333-333333333331'', ''2024-09-14T03:25:31.0100786+00:00'', N'''', ''00000000-0000-0000-0000-000000000001'', ''2024-09-14T03:25:31.0100787+00:00'', N'''', ''2024-09-14T03:25:31.0100782Z'', 500.0),
    (''33333333-3333-3333-3333-333333333332'', ''2024-09-14T03:25:31.0100790+00:00'', N'''', ''00000000-0000-0000-0000-000000000002'', ''2024-09-14T03:25:31.0100791+00:00'', N'''', ''2024-09-14T03:25:31.0100790Z'', 1000.0),
    (''33333333-3333-3333-3333-333333333333'', ''2024-09-14T03:25:31.0100794+00:00'', N'''', ''00000000-0000-0000-0000-000000000003'', ''2024-09-14T03:25:31.0100794+00:00'', N'''', ''2024-09-14T03:25:31.0100793Z'', 750.0),
    (''33333333-3333-3333-3333-333333333334'', ''2024-09-14T03:25:31.0100797+00:00'', N'''', ''00000000-0000-0000-0000-000000000004'', ''2024-09-14T03:25:31.0100797+00:00'', N'''', ''2024-09-14T03:25:31.0100796Z'', 900.0),
    (''33333333-3333-3333-3333-333333333335'', ''2024-09-14T03:25:31.0100800+00:00'', N'''', ''00000000-0000-0000-0000-000000000005'', ''2024-09-14T03:25:31.0100801+00:00'', N'''', ''2024-09-14T03:25:31.0100800Z'', 1200.0)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'OrderId', N'CreatedAt', N'CreatedBy', N'CustomerId', N'LastUpdatedAt', N'LastUpdatedBy', N'OrderDate', N'TotalAmount') AND [object_id] = OBJECT_ID(N'[Orders]'))
        SET IDENTITY_INSERT [Orders] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240914032531_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'ProductId', N'CreatedAt', N'CreatedBy', N'LastUpdatedAt', N'LastUpdatedBy') AND [object_id] = OBJECT_ID(N'[ProductCategory]'))
        SET IDENTITY_INSERT [ProductCategory] ON;
    EXEC(N'INSERT INTO [ProductCategory] ([CategoryId], [ProductId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
    VALUES (''22222222-2222-2222-2222-222222222221'', ''11111111-1111-1111-1111-111111111111'', ''2024-09-14T03:25:31.0119970+00:00'', N'''', ''2024-09-14T03:25:31.0119971+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222222'', ''11111111-1111-1111-1111-111111111111'', ''2024-09-14T03:25:31.0119972+00:00'', N'''', ''2024-09-14T03:25:31.0119973+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222223'', ''11111111-1111-1111-1111-111111111111'', ''2024-09-14T03:25:31.0120016+00:00'', N'''', ''2024-09-14T03:25:31.0120017+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222221'', ''11111111-1111-1111-1111-111111111112'', ''2024-09-14T03:25:31.0119974+00:00'', N'''', ''2024-09-14T03:25:31.0119974+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222223'', ''11111111-1111-1111-1111-111111111112'', ''2024-09-14T03:25:31.0119975+00:00'', N'''', ''2024-09-14T03:25:31.0119976+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222224'', ''11111111-1111-1111-1111-111111111112'', ''2024-09-14T03:25:31.0120018+00:00'', N'''', ''2024-09-14T03:25:31.0120018+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222222'', ''11111111-1111-1111-1111-111111111113'', ''2024-09-14T03:25:31.0119977+00:00'', N'''', ''2024-09-14T03:25:31.0119978+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222224'', ''11111111-1111-1111-1111-111111111113'', ''2024-09-14T03:25:31.0119979+00:00'', N'''', ''2024-09-14T03:25:31.0119979+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222225'', ''11111111-1111-1111-1111-111111111113'', ''2024-09-14T03:25:31.0120019+00:00'', N'''', ''2024-09-14T03:25:31.0120020+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222221'', ''11111111-1111-1111-1111-111111111114'', ''2024-09-14T03:25:31.0120021+00:00'', N'''', ''2024-09-14T03:25:31.0120021+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222223'', ''11111111-1111-1111-1111-111111111114'', ''2024-09-14T03:25:31.0119980+00:00'', N'''', ''2024-09-14T03:25:31.0119981+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222225'', ''11111111-1111-1111-1111-111111111114'', ''2024-09-14T03:25:31.0119982+00:00'', N'''', ''2024-09-14T03:25:31.0119982+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222221'', ''11111111-1111-1111-1111-111111111115'', ''2024-09-14T03:25:31.0119983+00:00'', N'''', ''2024-09-14T03:25:31.0119983+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222222'', ''11111111-1111-1111-1111-111111111115'', ''2024-09-14T03:25:31.0120022+00:00'', N'''', ''2024-09-14T03:25:31.0120022+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222224'', ''11111111-1111-1111-1111-111111111115'', ''2024-09-14T03:25:31.0119985+00:00'', N'''', ''2024-09-14T03:25:31.0119985+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222222'', ''11111111-1111-1111-1111-111111111116'', ''2024-09-14T03:25:31.0119986+00:00'', N'''', ''2024-09-14T03:25:31.0119987+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222225'', ''11111111-1111-1111-1111-111111111116'', ''2024-09-14T03:25:31.0119987+00:00'', N'''', ''2024-09-14T03:25:31.0119988+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222221'', ''11111111-1111-1111-1111-111111111117'', ''2024-09-14T03:25:31.0119991+00:00'', N'''', ''2024-09-14T03:25:31.0119991+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222223'', ''11111111-1111-1111-1111-111111111117'', ''2024-09-14T03:25:31.0119989+00:00'', N'''', ''2024-09-14T03:25:31.0119989+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222222'', ''11111111-1111-1111-1111-111111111118'', ''2024-09-14T03:25:31.0119993+00:00'', N'''', ''2024-09-14T03:25:31.0119994+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222225'', ''11111111-1111-1111-1111-111111111118'', ''2024-09-14T03:25:31.0119992+00:00'', N'''', ''2024-09-14T03:25:31.0119993+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222221'', ''11111111-1111-1111-1111-111111111119'', ''2024-09-14T03:25:31.0119996+00:00'', N'''', ''2024-09-14T03:25:31.0119996+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222224'', ''11111111-1111-1111-1111-111111111119'', ''2024-09-14T03:25:31.0119995+00:00'', N'''', ''2024-09-14T03:25:31.0119995+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222222'', ''11111111-1111-1111-1111-111111111120'', ''2024-09-14T03:25:31.0119997+00:00'', N'''', ''2024-09-14T03:25:31.0119998+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222223'', ''11111111-1111-1111-1111-111111111120'', ''2024-09-14T03:25:31.0119999+00:00'', N'''', ''2024-09-14T03:25:31.0119999+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222224'', ''11111111-1111-1111-1111-111111111121'', ''2024-09-14T03:25:31.0120001+00:00'', N'''', ''2024-09-14T03:25:31.0120002+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222225'', ''11111111-1111-1111-1111-111111111121'', ''2024-09-14T03:25:31.0120000+00:00'', N'''', ''2024-09-14T03:25:31.0120001+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222221'', ''11111111-1111-1111-1111-111111111122'', ''2024-09-14T03:25:31.0120003+00:00'', N'''', ''2024-09-14T03:25:31.0120004+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222225'', ''11111111-1111-1111-1111-111111111122'', ''2024-09-14T03:25:31.0120005+00:00'', N'''', ''2024-09-14T03:25:31.0120005+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222222'', ''11111111-1111-1111-1111-111111111123'', ''2024-09-14T03:25:31.0120006+00:00'', N'''', ''2024-09-14T03:25:31.0120006+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222224'', ''11111111-1111-1111-1111-111111111123'', ''2024-09-14T03:25:31.0120007+00:00'', N'''', ''2024-09-14T03:25:31.0120008+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222223'', ''11111111-1111-1111-1111-111111111124'', ''2024-09-14T03:25:31.0120009+00:00'', N'''', ''2024-09-14T03:25:31.0120009+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222225'', ''11111111-1111-1111-1111-111111111124'', ''2024-09-14T03:25:31.0120012+00:00'', N'''', ''2024-09-14T03:25:31.0120013+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222221'', ''11111111-1111-1111-1111-111111111125'', ''2024-09-14T03:25:31.0120014+00:00'', N'''', ''2024-09-14T03:25:31.0120014+00:00'', N''''),
    (''22222222-2222-2222-2222-222222222223'', ''11111111-1111-1111-1111-111111111125'', ''2024-09-14T03:25:31.0120015+00:00'', N'''', ''2024-09-14T03:25:31.0120016+00:00'', N'''')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'ProductId', N'CreatedAt', N'CreatedBy', N'LastUpdatedAt', N'LastUpdatedBy') AND [object_id] = OBJECT_ID(N'[ProductCategory]'))
        SET IDENTITY_INSERT [ProductCategory] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240914032531_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'OrderId', N'ProductId', N'CreatedAt', N'CreatedBy', N'LastUpdatedAt', N'LastUpdatedBy', N'Quantity') AND [object_id] = OBJECT_ID(N'[OrderProducts]'))
        SET IDENTITY_INSERT [OrderProducts] ON;
    EXEC(N'INSERT INTO [OrderProducts] ([OrderId], [ProductId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy], [Quantity])
    VALUES (''33333333-3333-3333-3333-333333333331'', ''11111111-1111-1111-1111-111111111111'', ''2024-09-14T03:25:31.0110278+00:00'', N'''', ''2024-09-14T03:25:31.0110279+00:00'', N'''', 2),
    (''33333333-3333-3333-3333-333333333331'', ''11111111-1111-1111-1111-111111111112'', ''2024-09-14T03:25:31.0110336+00:00'', N'''', ''2024-09-14T03:25:31.0110337+00:00'', N'''', 1),
    (''33333333-3333-3333-3333-333333333331'', ''11111111-1111-1111-1111-111111111113'', ''2024-09-14T03:25:31.0110338+00:00'', N'''', ''2024-09-14T03:25:31.0110339+00:00'', N'''', 3),
    (''33333333-3333-3333-3333-333333333332'', ''11111111-1111-1111-1111-111111111114'', ''2024-09-14T03:25:31.0110340+00:00'', N'''', ''2024-09-14T03:25:31.0110340+00:00'', N'''', 1),
    (''33333333-3333-3333-3333-333333333332'', ''11111111-1111-1111-1111-111111111115'', ''2024-09-14T03:25:31.0110341+00:00'', N'''', ''2024-09-14T03:25:31.0110342+00:00'', N'''', 2),
    (''33333333-3333-3333-3333-333333333332'', ''11111111-1111-1111-1111-111111111116'', ''2024-09-14T03:25:31.0110343+00:00'', N'''', ''2024-09-14T03:25:31.0110343+00:00'', N'''', 3),
    (''33333333-3333-3333-3333-333333333333'', ''11111111-1111-1111-1111-111111111117'', ''2024-09-14T03:25:31.0110344+00:00'', N'''', ''2024-09-14T03:25:31.0110345+00:00'', N'''', 1),
    (''33333333-3333-3333-3333-333333333333'', ''11111111-1111-1111-1111-111111111118'', ''2024-09-14T03:25:31.0110346+00:00'', N'''', ''2024-09-14T03:25:31.0110346+00:00'', N'''', 2),
    (''33333333-3333-3333-3333-333333333334'', ''11111111-1111-1111-1111-111111111119'', ''2024-09-14T03:25:31.0110347+00:00'', N'''', ''2024-09-14T03:25:31.0110348+00:00'', N'''', 3),
    (''33333333-3333-3333-3333-333333333334'', ''11111111-1111-1111-1111-111111111120'', ''2024-09-14T03:25:31.0110349+00:00'', N'''', ''2024-09-14T03:25:31.0110349+00:00'', N'''', 1),
    (''33333333-3333-3333-3333-333333333334'', ''11111111-1111-1111-1111-111111111121'', ''2024-09-14T03:25:31.0110350+00:00'', N'''', ''2024-09-14T03:25:31.0110350+00:00'', N'''', 2),
    (''33333333-3333-3333-3333-333333333335'', ''11111111-1111-1111-1111-111111111122'', ''2024-09-14T03:25:31.0110352+00:00'', N'''', ''2024-09-14T03:25:31.0110352+00:00'', N'''', 3),
    (''33333333-3333-3333-3333-333333333335'', ''11111111-1111-1111-1111-111111111123'', ''2024-09-14T03:25:31.0110353+00:00'', N'''', ''2024-09-14T03:25:31.0110354+00:00'', N'''', 1),
    (''33333333-3333-3333-3333-333333333335'', ''11111111-1111-1111-1111-111111111124'', ''2024-09-14T03:25:31.0110355+00:00'', N'''', ''2024-09-14T03:25:31.0110355+00:00'', N'''', 2),
    (''33333333-3333-3333-3333-333333333335'', ''11111111-1111-1111-1111-111111111125'', ''2024-09-14T03:25:31.0110356+00:00'', N'''', ''2024-09-14T03:25:31.0110357+00:00'', N'''', 1)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'OrderId', N'ProductId', N'CreatedAt', N'CreatedBy', N'LastUpdatedAt', N'LastUpdatedBy', N'Quantity') AND [object_id] = OBJECT_ID(N'[OrderProducts]'))
        SET IDENTITY_INSERT [OrderProducts] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240914032531_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Categories_CreatedAt] ON [Categories] ([CreatedAt]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240914032531_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Customers_CreatedAt] ON [Customers] ([CreatedAt]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240914032531_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Customers_Email] ON [Customers] ([Email]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240914032531_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_OrderProducts_CreatedAt] ON [OrderProducts] ([CreatedAt]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240914032531_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_OrderProducts_ProductId] ON [OrderProducts] ([ProductId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240914032531_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Orders_CreatedAt] ON [Orders] ([CreatedAt]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240914032531_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Orders_CustomerId] ON [Orders] ([CustomerId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240914032531_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ProductCategory_CategoryId] ON [ProductCategory] ([CategoryId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240914032531_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ProductCategory_CreatedAt] ON [ProductCategory] ([CreatedAt]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240914032531_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Products_CreatedAt] ON [Products] ([CreatedAt]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240914032531_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240914032531_InitialCreate', N'8.0.8');
END;
GO

COMMIT;
GO

