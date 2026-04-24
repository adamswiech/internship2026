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
IF SCHEMA_ID(N'ksef') IS NULL EXEC(N'CREATE SCHEMA [ksef];');

CREATE TABLE [ksef].[Address] (
    [Id] int NOT NULL IDENTITY,
    [CountryCode] nvarchar(max) NOT NULL,
    [Line1] nvarchar(max) NOT NULL,
    [Line2] nvarchar(max) NULL,
    [GLN] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY ([Id])
);

CREATE TABLE [ksef].[BankAccount] (
    [Id] int NOT NULL IDENTITY,
    [FullNumber] nvarchar(max) NOT NULL,
    [Swift] nvarchar(max) NOT NULL,
    [BankName] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [IsBankOwnAccount] int NOT NULL,
    CONSTRAINT [PK_BankAccount] PRIMARY KEY ([Id])
);

CREATE TABLE [ksef].[ContactInfo] (
    [Id] int NOT NULL IDENTITY,
    [Email] nvarchar(max) NOT NULL,
    [Phone] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_ContactInfo] PRIMARY KEY ([Id])
);

CREATE TABLE [ksef].[Carrier] (
    [Id] int NOT NULL IDENTITY,
    [CountryCode] nvarchar(max) NOT NULL,
    [TaxId] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [AddressId] int NULL,
    CONSTRAINT [PK_Carrier] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Carrier_Address_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [ksef].[Address] ([Id])
);

CREATE TABLE [ksef].[Charges] (
    [Id] int NOT NULL IDENTITY,
    [Reason] nvarchar(max) NOT NULL,
    [Amount] decimal(18,2) NOT NULL,
    [SettlementId] int NOT NULL,
    CONSTRAINT [PK_Charges] PRIMARY KEY ([Id])
);

CREATE TABLE [ksef].[Contract] (
    [Id] int NOT NULL IDENTITY,
    [ContractDate] datetime2 NOT NULL,
    [ContractNumber] nvarchar(max) NOT NULL,
    [TermsId] int NOT NULL,
    CONSTRAINT [PK_Contract] PRIMARY KEY ([Id])
);

CREATE TABLE [ksef].[Deductions] (
    [Id] int NOT NULL IDENTITY,
    [Reason] nvarchar(max) NOT NULL,
    [Amount] decimal(18,2) NOT NULL,
    [SettlementId] int NOT NULL,
    CONSTRAINT [PK_Deductions] PRIMARY KEY ([Id])
);

CREATE TABLE [ksef].[Invoice] (
    [Id] int NOT NULL IDENTITY,
    [InvoiceNumber] nvarchar(max) NOT NULL,
    [KsefNumber] nvarchar(max) NOT NULL,
    [IssueDate] datetime2 NOT NULL,
    [DeliveryDate] datetime2 NOT NULL,
    [IssuePlace] nvarchar(max) NOT NULL,
    [CurrencyCode] nvarchar(max) NOT NULL,
    [CurrencyRate] decimal(18,2) NOT NULL,
    [SellerId] int NOT NULL,
    [BuyerId] int NOT NULL,
    [FactorBankAccountId] int NULL,
    [SellerBankAccountId] int NULL,
    [FooterNote] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Invoice] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Invoice_BankAccount_FactorBankAccountId] FOREIGN KEY ([FactorBankAccountId]) REFERENCES [ksef].[BankAccount] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Invoice_BankAccount_SellerBankAccountId] FOREIGN KEY ([SellerBankAccountId]) REFERENCES [ksef].[BankAccount] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [ksef].[InvoiceLine] (
    [Id] int NOT NULL IDENTITY,
    [InvoiceId] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [PricePerPieceNetto] decimal(18,2) NOT NULL,
    [Quantity] int NOT NULL,
    [Unit] nvarchar(max) NOT NULL,
    [TaxRate] int NOT NULL,
    [PriceTotalNetto] nvarchar(max) NOT NULL,
    [TaxValue] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_InvoiceLine] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_InvoiceLine_Invoice_InvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [ksef].[Invoice] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [ksef].[Party] (
    [Id] int NOT NULL IDENTITY,
    [Role] nvarchar(max) NULL,
    [Eori] nvarchar(max) NOT NULL,
    [Nip] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [MainAddressId] int NULL,
    [CorrespondenceAddressID] int NULL,
    [ContactInfoId] int NULL,
    [CustomerNumber] nvarchar(max) NULL,
    [InvoiceId] int NULL,
    CONSTRAINT [PK_Party] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Party_Address_CorrespondenceAddressID] FOREIGN KEY ([CorrespondenceAddressID]) REFERENCES [ksef].[Address] ([Id]),
    CONSTRAINT [FK_Party_Address_MainAddressId] FOREIGN KEY ([MainAddressId]) REFERENCES [ksef].[Address] ([Id]),
    CONSTRAINT [FK_Party_ContactInfo_ContactInfoId] FOREIGN KEY ([ContactInfoId]) REFERENCES [ksef].[ContactInfo] ([Id]),
    CONSTRAINT [FK_Party_Invoice_InvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [ksef].[Invoice] ([Id])
);

CREATE TABLE [ksef].[PaymentInfo] (
    [Id] int NOT NULL IDENTITY,
    [InvoiceId] int NOT NULL,
    [IsPartial] bit NOT NULL,
    [PaymentDueDate] datetime2 NOT NULL,
    [PaymentTermsDescription] nvarchar(max) NOT NULL,
    [PaymentMethod] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_PaymentInfo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PaymentInfo_Invoice_InvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [ksef].[Invoice] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [ksef].[Settlement] (
    [Id] int NOT NULL IDENTITY,
    [InvoiceId] int NOT NULL,
    [TotalToPay] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Settlement] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Settlement_Invoice_InvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [ksef].[Invoice] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [ksef].[TaxSummary] (
    [Id] int NOT NULL IDENTITY,
    [InvoiceId] int NOT NULL,
    [TaxRate] nvarchar(max) NOT NULL,
    [Netto] decimal(18,2) NOT NULL,
    [TaxAmount] decimal(18,2) NOT NULL,
    [Brutto] decimal(18,2) NOT NULL,
    [PLNAmount] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_TaxSummary] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TaxSummary_Invoice_InvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [ksef].[Invoice] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [ksef].[Terms] (
    [Id] int NOT NULL IDENTITY,
    [InvoiceId] int NOT NULL,
    [DeliveryTerms] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Terms] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Terms_Invoice_InvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [ksef].[Invoice] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [ksef].[PartialPayment] (
    [Id] int NOT NULL IDENTITY,
    [Date] datetime2 NOT NULL,
    [Amount] decimal(18,2) NOT NULL,
    [Method] nvarchar(max) NOT NULL,
    [PaymentInfoId] int NOT NULL,
    CONSTRAINT [PK_PartialPayment] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PartialPayment_PaymentInfo_PaymentInfoId] FOREIGN KEY ([PaymentInfoId]) REFERENCES [ksef].[PaymentInfo] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [ksef].[OrderInfo] (
    [Id] int NOT NULL IDENTITY,
    [OrderDate] datetime2 NOT NULL,
    [OrderNumber] nvarchar(max) NOT NULL,
    [TermsId] int NOT NULL,
    CONSTRAINT [PK_OrderInfo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderInfo_Terms_TermsId] FOREIGN KEY ([TermsId]) REFERENCES [ksef].[Terms] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [ksef].[TransportInfo] (
    [Id] int NOT NULL IDENTITY,
    [TransportType] int NOT NULL,
    [CarrierId] int NOT NULL,
    [TransportOrderNumber] nvarchar(max) NOT NULL,
    [CargoDescription] int NOT NULL,
    [PackagingUnit] nvarchar(max) NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [ShipFromId] int NULL,
    [ShipViaID] int NULL,
    [ShipToID] int NULL,
    [TermsId] int NOT NULL,
    CONSTRAINT [PK_TransportInfo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TransportInfo_Address_ShipFromId] FOREIGN KEY ([ShipFromId]) REFERENCES [ksef].[Address] ([Id]),
    CONSTRAINT [FK_TransportInfo_Address_ShipToID] FOREIGN KEY ([ShipToID]) REFERENCES [ksef].[Address] ([Id]),
    CONSTRAINT [FK_TransportInfo_Address_ShipViaID] FOREIGN KEY ([ShipViaID]) REFERENCES [ksef].[Address] ([Id]),
    CONSTRAINT [FK_TransportInfo_Carrier_CarrierId] FOREIGN KEY ([CarrierId]) REFERENCES [ksef].[Carrier] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_TransportInfo_Terms_TermsId] FOREIGN KEY ([TermsId]) REFERENCES [ksef].[Terms] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Carrier_AddressId] ON [ksef].[Carrier] ([AddressId]);

CREATE INDEX [IX_Charges_SettlementId] ON [ksef].[Charges] ([SettlementId]);

CREATE UNIQUE INDEX [IX_Contract_TermsId] ON [ksef].[Contract] ([TermsId]);

CREATE INDEX [IX_Deductions_SettlementId] ON [ksef].[Deductions] ([SettlementId]);

CREATE INDEX [IX_Invoice_BuyerId] ON [ksef].[Invoice] ([BuyerId]);

CREATE INDEX [IX_Invoice_FactorBankAccountId] ON [ksef].[Invoice] ([FactorBankAccountId]);

CREATE INDEX [IX_Invoice_SellerBankAccountId] ON [ksef].[Invoice] ([SellerBankAccountId]);

CREATE INDEX [IX_Invoice_SellerId] ON [ksef].[Invoice] ([SellerId]);

CREATE INDEX [IX_InvoiceLine_InvoiceId] ON [ksef].[InvoiceLine] ([InvoiceId]);

CREATE UNIQUE INDEX [IX_OrderInfo_TermsId] ON [ksef].[OrderInfo] ([TermsId]);

CREATE INDEX [IX_PartialPayment_PaymentInfoId] ON [ksef].[PartialPayment] ([PaymentInfoId]);

CREATE INDEX [IX_Party_ContactInfoId] ON [ksef].[Party] ([ContactInfoId]);

CREATE INDEX [IX_Party_CorrespondenceAddressID] ON [ksef].[Party] ([CorrespondenceAddressID]);

CREATE INDEX [IX_Party_InvoiceId] ON [ksef].[Party] ([InvoiceId]);

CREATE INDEX [IX_Party_MainAddressId] ON [ksef].[Party] ([MainAddressId]);

CREATE UNIQUE INDEX [IX_PaymentInfo_InvoiceId] ON [ksef].[PaymentInfo] ([InvoiceId]);

CREATE UNIQUE INDEX [IX_Settlement_InvoiceId] ON [ksef].[Settlement] ([InvoiceId]);

CREATE INDEX [IX_TaxSummary_InvoiceId] ON [ksef].[TaxSummary] ([InvoiceId]);

CREATE UNIQUE INDEX [IX_Terms_InvoiceId] ON [ksef].[Terms] ([InvoiceId]);

CREATE INDEX [IX_TransportInfo_CarrierId] ON [ksef].[TransportInfo] ([CarrierId]);

CREATE INDEX [IX_TransportInfo_ShipFromId] ON [ksef].[TransportInfo] ([ShipFromId]);

CREATE INDEX [IX_TransportInfo_ShipToID] ON [ksef].[TransportInfo] ([ShipToID]);

CREATE INDEX [IX_TransportInfo_ShipViaID] ON [ksef].[TransportInfo] ([ShipViaID]);

CREATE UNIQUE INDEX [IX_TransportInfo_TermsId] ON [ksef].[TransportInfo] ([TermsId]);

ALTER TABLE [ksef].[Charges] ADD CONSTRAINT [FK_Charges_Settlement_SettlementId] FOREIGN KEY ([SettlementId]) REFERENCES [ksef].[Settlement] ([Id]) ON DELETE CASCADE;

ALTER TABLE [ksef].[Contract] ADD CONSTRAINT [FK_Contract_Terms_TermsId] FOREIGN KEY ([TermsId]) REFERENCES [ksef].[Terms] ([Id]) ON DELETE CASCADE;

ALTER TABLE [ksef].[Deductions] ADD CONSTRAINT [FK_Deductions_Settlement_SettlementId] FOREIGN KEY ([SettlementId]) REFERENCES [ksef].[Settlement] ([Id]) ON DELETE CASCADE;

ALTER TABLE [ksef].[Invoice] ADD CONSTRAINT [FK_Invoice_Party_BuyerId] FOREIGN KEY ([BuyerId]) REFERENCES [ksef].[Party] ([Id]) ON DELETE NO ACTION;

ALTER TABLE [ksef].[Invoice] ADD CONSTRAINT [FK_Invoice_Party_SellerId] FOREIGN KEY ([SellerId]) REFERENCES [ksef].[Party] ([Id]) ON DELETE NO ACTION;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260409084534_Nullable', N'10.0.5');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [ksef].[Charges] DROP CONSTRAINT [FK_Charges_Settlement_SettlementId];

ALTER TABLE [ksef].[Deductions] DROP CONSTRAINT [FK_Deductions_Settlement_SettlementId];

ALTER TABLE [ksef].[TransportInfo] DROP CONSTRAINT [FK_TransportInfo_Carrier_CarrierId];

DECLARE @var nvarchar(max);
SELECT @var = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[TransportInfo]') AND [c].[name] = N'TransportType');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [ksef].[TransportInfo] DROP CONSTRAINT ' + @var + ';');
ALTER TABLE [ksef].[TransportInfo] ALTER COLUMN [TransportType] int NULL;

DECLARE @var1 nvarchar(max);
SELECT @var1 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[TransportInfo]') AND [c].[name] = N'TransportOrderNumber');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[TransportInfo] DROP CONSTRAINT ' + @var1 + ';');
ALTER TABLE [ksef].[TransportInfo] ALTER COLUMN [TransportOrderNumber] nvarchar(max) NULL;

DECLARE @var2 nvarchar(max);
SELECT @var2 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[TransportInfo]') AND [c].[name] = N'PackagingUnit');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[TransportInfo] DROP CONSTRAINT ' + @var2 + ';');
ALTER TABLE [ksef].[TransportInfo] ALTER COLUMN [PackagingUnit] nvarchar(max) NULL;

DECLARE @var3 nvarchar(max);
SELECT @var3 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[TransportInfo]') AND [c].[name] = N'CarrierId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[TransportInfo] DROP CONSTRAINT ' + @var3 + ';');
ALTER TABLE [ksef].[TransportInfo] ALTER COLUMN [CarrierId] int NULL;

DECLARE @var4 nvarchar(max);
SELECT @var4 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[TransportInfo]') AND [c].[name] = N'CargoDescription');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[TransportInfo] DROP CONSTRAINT ' + @var4 + ';');
ALTER TABLE [ksef].[TransportInfo] ALTER COLUMN [CargoDescription] int NULL;

DECLARE @var5 nvarchar(max);
SELECT @var5 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[Terms]') AND [c].[name] = N'DeliveryTerms');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[Terms] DROP CONSTRAINT ' + @var5 + ';');
ALTER TABLE [ksef].[Terms] ALTER COLUMN [DeliveryTerms] nvarchar(max) NULL;

DECLARE @var6 nvarchar(max);
SELECT @var6 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[PaymentInfo]') AND [c].[name] = N'PaymentTermsDescription');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[PaymentInfo] DROP CONSTRAINT ' + @var6 + ';');
ALTER TABLE [ksef].[PaymentInfo] ALTER COLUMN [PaymentTermsDescription] nvarchar(max) NULL;

DECLARE @var7 nvarchar(max);
SELECT @var7 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[PaymentInfo]') AND [c].[name] = N'PaymentMethod');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[PaymentInfo] DROP CONSTRAINT ' + @var7 + ';');
ALTER TABLE [ksef].[PaymentInfo] ALTER COLUMN [PaymentMethod] nvarchar(max) NULL;

DECLARE @var8 nvarchar(max);
SELECT @var8 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[PaymentInfo]') AND [c].[name] = N'PaymentDueDate');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[PaymentInfo] DROP CONSTRAINT ' + @var8 + ';');
ALTER TABLE [ksef].[PaymentInfo] ALTER COLUMN [PaymentDueDate] datetime2 NULL;

DECLARE @var9 nvarchar(max);
SELECT @var9 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[Party]') AND [c].[name] = N'Nip');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[Party] DROP CONSTRAINT ' + @var9 + ';');
ALTER TABLE [ksef].[Party] ALTER COLUMN [Nip] nvarchar(max) NULL;

DECLARE @var10 nvarchar(max);
SELECT @var10 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[Party]') AND [c].[name] = N'Name');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[Party] DROP CONSTRAINT ' + @var10 + ';');
ALTER TABLE [ksef].[Party] ALTER COLUMN [Name] nvarchar(max) NULL;

DECLARE @var11 nvarchar(max);
SELECT @var11 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[Party]') AND [c].[name] = N'Eori');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[Party] DROP CONSTRAINT ' + @var11 + ';');
ALTER TABLE [ksef].[Party] ALTER COLUMN [Eori] nvarchar(max) NULL;

DECLARE @var12 nvarchar(max);
SELECT @var12 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[OrderInfo]') AND [c].[name] = N'OrderNumber');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[OrderInfo] DROP CONSTRAINT ' + @var12 + ';');
ALTER TABLE [ksef].[OrderInfo] ALTER COLUMN [OrderNumber] nvarchar(max) NULL;

DECLARE @var13 nvarchar(max);
SELECT @var13 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[OrderInfo]') AND [c].[name] = N'OrderDate');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[OrderInfo] DROP CONSTRAINT ' + @var13 + ';');
ALTER TABLE [ksef].[OrderInfo] ALTER COLUMN [OrderDate] datetime2 NULL;

DECLARE @var14 nvarchar(max);
SELECT @var14 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[Invoice]') AND [c].[name] = N'IssuePlace');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[Invoice] DROP CONSTRAINT ' + @var14 + ';');
ALTER TABLE [ksef].[Invoice] ALTER COLUMN [IssuePlace] nvarchar(max) NULL;

DECLARE @var15 nvarchar(max);
SELECT @var15 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[Invoice]') AND [c].[name] = N'FooterNote');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[Invoice] DROP CONSTRAINT ' + @var15 + ';');
ALTER TABLE [ksef].[Invoice] ALTER COLUMN [FooterNote] nvarchar(max) NULL;

DECLARE @var16 nvarchar(max);
SELECT @var16 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[Invoice]') AND [c].[name] = N'CurrencyRate');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[Invoice] DROP CONSTRAINT ' + @var16 + ';');
ALTER TABLE [ksef].[Invoice] ALTER COLUMN [CurrencyRate] decimal(18,2) NULL;

DECLARE @var17 nvarchar(max);
SELECT @var17 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[Invoice]') AND [c].[name] = N'CurrencyCode');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[Invoice] DROP CONSTRAINT ' + @var17 + ';');
ALTER TABLE [ksef].[Invoice] ALTER COLUMN [CurrencyCode] nvarchar(max) NULL;

DECLARE @var18 nvarchar(max);
SELECT @var18 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[Deductions]') AND [c].[name] = N'SettlementId');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[Deductions] DROP CONSTRAINT ' + @var18 + ';');
ALTER TABLE [ksef].[Deductions] ALTER COLUMN [SettlementId] int NULL;

DECLARE @var19 nvarchar(max);
SELECT @var19 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[Deductions]') AND [c].[name] = N'Reason');
IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[Deductions] DROP CONSTRAINT ' + @var19 + ';');
ALTER TABLE [ksef].[Deductions] ALTER COLUMN [Reason] nvarchar(max) NULL;

DECLARE @var20 nvarchar(max);
SELECT @var20 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[Deductions]') AND [c].[name] = N'Amount');
IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[Deductions] DROP CONSTRAINT ' + @var20 + ';');
ALTER TABLE [ksef].[Deductions] ALTER COLUMN [Amount] decimal(18,2) NULL;

DECLARE @var21 nvarchar(max);
SELECT @var21 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[Contract]') AND [c].[name] = N'ContractNumber');
IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[Contract] DROP CONSTRAINT ' + @var21 + ';');
ALTER TABLE [ksef].[Contract] ALTER COLUMN [ContractNumber] nvarchar(max) NULL;

DECLARE @var22 nvarchar(max);
SELECT @var22 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[Contract]') AND [c].[name] = N'ContractDate');
IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[Contract] DROP CONSTRAINT ' + @var22 + ';');
ALTER TABLE [ksef].[Contract] ALTER COLUMN [ContractDate] datetime2 NULL;

DECLARE @var23 nvarchar(max);
SELECT @var23 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[ContactInfo]') AND [c].[name] = N'Phone');
IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[ContactInfo] DROP CONSTRAINT ' + @var23 + ';');
ALTER TABLE [ksef].[ContactInfo] ALTER COLUMN [Phone] nvarchar(max) NULL;

DECLARE @var24 nvarchar(max);
SELECT @var24 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[ContactInfo]') AND [c].[name] = N'Email');
IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[ContactInfo] DROP CONSTRAINT ' + @var24 + ';');
ALTER TABLE [ksef].[ContactInfo] ALTER COLUMN [Email] nvarchar(max) NULL;

DECLARE @var25 nvarchar(max);
SELECT @var25 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[Charges]') AND [c].[name] = N'SettlementId');
IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[Charges] DROP CONSTRAINT ' + @var25 + ';');
ALTER TABLE [ksef].[Charges] ALTER COLUMN [SettlementId] int NULL;

DECLARE @var26 nvarchar(max);
SELECT @var26 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[Charges]') AND [c].[name] = N'Reason');
IF @var26 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[Charges] DROP CONSTRAINT ' + @var26 + ';');
ALTER TABLE [ksef].[Charges] ALTER COLUMN [Reason] nvarchar(max) NULL;

DECLARE @var27 nvarchar(max);
SELECT @var27 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[Charges]') AND [c].[name] = N'Amount');
IF @var27 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[Charges] DROP CONSTRAINT ' + @var27 + ';');
ALTER TABLE [ksef].[Charges] ALTER COLUMN [Amount] decimal(18,2) NULL;

DECLARE @var28 nvarchar(max);
SELECT @var28 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[BankAccount]') AND [c].[name] = N'Swift');
IF @var28 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[BankAccount] DROP CONSTRAINT ' + @var28 + ';');
ALTER TABLE [ksef].[BankAccount] ALTER COLUMN [Swift] nvarchar(max) NULL;

DECLARE @var29 nvarchar(max);
SELECT @var29 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[BankAccount]') AND [c].[name] = N'Description');
IF @var29 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[BankAccount] DROP CONSTRAINT ' + @var29 + ';');
ALTER TABLE [ksef].[BankAccount] ALTER COLUMN [Description] nvarchar(max) NULL;

DECLARE @var30 nvarchar(max);
SELECT @var30 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[BankAccount]') AND [c].[name] = N'BankName');
IF @var30 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[BankAccount] DROP CONSTRAINT ' + @var30 + ';');
ALTER TABLE [ksef].[BankAccount] ALTER COLUMN [BankName] nvarchar(max) NULL;

DECLARE @var31 nvarchar(max);
SELECT @var31 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ksef].[Address]') AND [c].[name] = N'GLN');
IF @var31 IS NOT NULL EXEC(N'ALTER TABLE [ksef].[Address] DROP CONSTRAINT ' + @var31 + ';');
ALTER TABLE [ksef].[Address] ALTER COLUMN [GLN] nvarchar(max) NULL;

ALTER TABLE [ksef].[Charges] ADD CONSTRAINT [FK_Charges_Settlement_SettlementId] FOREIGN KEY ([SettlementId]) REFERENCES [ksef].[Settlement] ([Id]);

ALTER TABLE [ksef].[Deductions] ADD CONSTRAINT [FK_Deductions_Settlement_SettlementId] FOREIGN KEY ([SettlementId]) REFERENCES [ksef].[Settlement] ([Id]);

ALTER TABLE [ksef].[TransportInfo] ADD CONSTRAINT [FK_TransportInfo_Carrier_CarrierId] FOREIGN KEY ([CarrierId]) REFERENCES [ksef].[Carrier] ([Id]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260409085103_InitSync', N'10.0.5');

COMMIT;
GO

