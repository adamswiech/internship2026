USE [master]
GO

-- =====================
-- CREATE DATABASE
-- =====================

IF DB_ID('KSeF') IS NULL
BEGIN
    CREATE DATABASE [KSeF]
END
GO

USE [KSeF]
GO

-- =====================
-- CREATE SCHEMA
-- =====================

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'ksef')
BEGIN
    EXEC('CREATE SCHEMA ksef')
END
GO

-- =====================
-- TRANSACTION
-- =====================

BEGIN TRAN;
SET XACT_ABORT ON;

-- =====================
-- CORE TABLES
-- =====================

CREATE TABLE [ksef].[Address] (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CountryCode NVARCHAR(10),
    Line1 NVARCHAR(255),
    Line2 NVARCHAR(255),
    GLN NVARCHAR(50)
);

CREATE TABLE [ksef].[ContactInfo] (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(255),
    Phone NVARCHAR(50)
);

CREATE TABLE [ksef].[BankAccount] (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullNumber NVARCHAR(100),
    Swift NVARCHAR(50),
    BankName NVARCHAR(255),
    Description NVARCHAR(255),
    IsBankOwnAccount BIT
);

-- =====================
-- PARTY
-- =====================

CREATE TABLE [ksef].[Party] (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Role NVARCHAR(50),
    Eori NVARCHAR(50),
    Nip NVARCHAR(50),
    Name NVARCHAR(255),

    MainAddressId INT NULL,
    ContactInfoId INT NULL,

    CONSTRAINT FK_Party_Address 
        FOREIGN KEY (MainAddressId) REFERENCES [ksef].[Address](Id),

    CONSTRAINT FK_Party_Contact 
        FOREIGN KEY (ContactInfoId) REFERENCES [ksef].[ContactInfo](Id)
);

-- =====================
-- INVOICE
-- =====================

CREATE TABLE [ksef].[Invoice] (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceNumber NVARCHAR(100),
    KsefNumber NVARCHAR(100) NOT NULL,
    IssueDate DATETIME2,
    DeliveryDate DATETIME2,
    IssuePlace NVARCHAR(255),

    SellerId INT NOT NULL,
    BuyerId INT NOT NULL,

    CONSTRAINT UQ_KsefNumber UNIQUE (KsefNumber),

    CONSTRAINT FK_Invoice_Seller 
        FOREIGN KEY (SellerId) REFERENCES [ksef].[Party](Id),

    CONSTRAINT FK_Invoice_Buyer 
        FOREIGN KEY (BuyerId) REFERENCES [ksef].[Party](Id)
);

-- =====================
-- INVOICE LINES
-- =====================

CREATE TABLE [ksef].[InvoiceLine] (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceId INT NOT NULL,

    Name NVARCHAR(255),
    PricePerPieceNetto DECIMAL(18,4),
    Quantity INT,
    Unit NVARCHAR(50),
    TaxRate INT,

    CONSTRAINT FK_Line_Invoice 
        FOREIGN KEY (InvoiceId) REFERENCES [ksef].[Invoice](Id)
);

-- =====================
-- TAX SUMMARY
-- =====================

CREATE TABLE [ksef].[TaxSummary] (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceId INT NOT NULL,

    TaxRate NVARCHAR(20),
    Netto DECIMAL(18,4),
    TaxAmount DECIMAL(18,4),
    Brutto DECIMAL(18,4),
    PLNAmount DECIMAL(18,4),

    CONSTRAINT FK_Tax_Invoice 
        FOREIGN KEY (InvoiceId) REFERENCES [ksef].[Invoice](Id)
);

-- =====================
-- PAYMENT
-- =====================

CREATE TABLE [ksef].[PaymentInfo] (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceId INT NOT NULL,

    IsPartial BIT,
    PaymentDueDate DATETIME2,
    PaymentTermsDescription NVARCHAR(255),

    CONSTRAINT FK_Payment_Invoice 
        FOREIGN KEY (InvoiceId) REFERENCES [ksef].[Invoice](Id)
);

-- =====================
-- SETTLEMENT
-- =====================

CREATE TABLE [ksef].[Settlement] (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceId INT NOT NULL,

    TotalToPay DECIMAL(18,4),

    CONSTRAINT FK_Settlement_Invoice 
        FOREIGN KEY (InvoiceId) REFERENCES [ksef].[Invoice](Id)
);

-- =====================
-- TERMS
-- =====================

CREATE TABLE [ksef].[Terms] (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceId INT NOT NULL,

    DeliveryTerms NVARCHAR(255),

    CONSTRAINT FK_Terms_Invoice 
        FOREIGN KEY (InvoiceId) REFERENCES [ksef].[Invoice](Id)
);

-- =====================
-- INDEXES (PERFORMANCE)
-- =====================

CREATE INDEX IX_Invoice_SellerId ON [ksef].[Invoice](SellerId);
CREATE INDEX IX_Invoice_BuyerId ON [ksef].[Invoice](BuyerId);

CREATE INDEX IX_InvoiceLine_InvoiceId ON [ksef].[InvoiceLine](InvoiceId);
CREATE INDEX IX_TaxSummary_InvoiceId ON [ksef].[TaxSummary](InvoiceId);
CREATE INDEX IX_PaymentInfo_InvoiceId ON [ksef].[PaymentInfo](InvoiceId);
CREATE INDEX IX_Settlement_InvoiceId ON [ksef].[Settlement](InvoiceId);
CREATE INDEX IX_Terms_InvoiceId ON [ksef].[Terms](InvoiceId);

-- =====================
-- FINISH
-- =====================

COMMIT TRAN;
GO