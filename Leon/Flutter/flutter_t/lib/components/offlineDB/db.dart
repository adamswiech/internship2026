const String migrationScript = '''
PRAGMA foreign_keys = ON;

CREATE TABLE Address (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    CountryCode TEXT NOT NULL,
    Line1 TEXT NOT NULL,
    Line2 TEXT,
    GLN TEXT
);

CREATE TABLE BankAccount (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    FullNumber TEXT NOT NULL,
    Swift TEXT,
    BankName TEXT,
    Description TEXT,
    IsBankOwnAccount INTEGER NOT NULL
);

CREATE TABLE ContactInfo (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Email TEXT,
    Phone TEXT
);

CREATE TABLE Carrier (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    CountryCode TEXT NOT NULL,
    TaxId TEXT NOT NULL,
    Name TEXT,
    AddressId INTEGER,
    FOREIGN KEY (AddressId) REFERENCES Address(Id)
);

CREATE TABLE Charges (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Reason TEXT,
    Amount REAL,
    SettlementId INTEGER,
    FOREIGN KEY (SettlementId) REFERENCES Settlement(Id)
);

CREATE TABLE Contract (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    ContractDate TEXT,
    ContractNumber TEXT,
    TermsId INTEGER,
    FOREIGN KEY (TermsId) REFERENCES Terms(Id)
);

CREATE TABLE Deductions (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Reason TEXT,
    Amount REAL,
    SettlementId INTEGER,
    FOREIGN KEY (SettlementId) REFERENCES Settlement(Id)
);

CREATE TABLE Invoice (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    InvoiceNumber TEXT NOT NULL,
    KsefNumber TEXT NOT NULL,
    IssueDate TEXT,
    DeliveryDate TEXT,
    IssuePlace TEXT,
    CurrencyCode TEXT,
    CurrencyRate REAL,
    SellerId INTEGER,
    BuyerId INTEGER,
    FactorBankAccountId INTEGER,
    SellerBankAccountId INTEGER,
    FooterNote TEXT,
    FOREIGN KEY (FactorBankAccountId) REFERENCES BankAccount(Id),
    FOREIGN KEY (SellerBankAccountId) REFERENCES BankAccount(Id)
);

CREATE TABLE InvoiceLine (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    InvoiceId INTEGER NOT NULL,
    Name TEXT NOT NULL,
    PricePerPieceNetto REAL NOT NULL,
    Quantity INTEGER NOT NULL,
    Unit TEXT NOT NULL,
    TaxRate INTEGER NOT NULL,
    PriceTotalNetto TEXT NOT NULL,
    TaxValue REAL NOT NULL,
    FOREIGN KEY (InvoiceId) REFERENCES Invoice(Id) ON DELETE CASCADE
);

CREATE TABLE Party (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Role TEXT,
    Eori TEXT,
    Nip TEXT,
    Name TEXT,
    MainAddressId INTEGER,
    CorrespondenceAddressID INTEGER,
    ContactInfoId INTEGER,
    CustomerNumber TEXT,
    InvoiceId INTEGER,
    FOREIGN KEY (MainAddressId) REFERENCES Address(Id),
    FOREIGN KEY (CorrespondenceAddressID) REFERENCES Address(Id),
    FOREIGN KEY (ContactInfoId) REFERENCES ContactInfo(Id),
    FOREIGN KEY (InvoiceId) REFERENCES Invoice(Id)
);

CREATE TABLE PaymentInfo (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    InvoiceId INTEGER NOT NULL,
    IsPartial INTEGER NOT NULL,
    PaymentDueDate TEXT,
    PaymentTermsDescription TEXT,
    PaymentMethod TEXT,
    FOREIGN KEY (InvoiceId) REFERENCES Invoice(Id) ON DELETE CASCADE
);

CREATE TABLE Settlement (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    InvoiceId INTEGER NOT NULL,
    TotalToPay REAL NOT NULL,
    FOREIGN KEY (InvoiceId) REFERENCES Invoice(Id) ON DELETE CASCADE
);

CREATE TABLE TaxSummary (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    InvoiceId INTEGER NOT NULL,
    TaxRate TEXT NOT NULL,
    Netto REAL NOT NULL,
    TaxAmount REAL NOT NULL,
    Brutto REAL NOT NULL,
    PLNAmount REAL NOT NULL,
    FOREIGN KEY (InvoiceId) REFERENCES Invoice(Id) ON DELETE CASCADE
);

CREATE TABLE Terms (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    InvoiceId INTEGER NOT NULL,
    DeliveryTerms TEXT,
    FOREIGN KEY (InvoiceId) REFERENCES Invoice(Id) ON DELETE CASCADE
);

CREATE TABLE PartialPayment (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Date TEXT NOT NULL,
    Amount REAL NOT NULL,
    Method TEXT NOT NULL,
    PaymentInfoId INTEGER NOT NULL,
    FOREIGN KEY (PaymentInfoId) REFERENCES PaymentInfo(Id) ON DELETE CASCADE
);

CREATE TABLE OrderInfo (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    OrderDate TEXT,
    OrderNumber TEXT,
    TermsId INTEGER NOT NULL,
    FOREIGN KEY (TermsId) REFERENCES Terms(Id) ON DELETE CASCADE
);

CREATE TABLE TransportInfo (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    TransportType INTEGER,
    CarrierId INTEGER,
    TransportOrderNumber TEXT,
    CargoDescription INTEGER,
    PackagingUnit TEXT,
    StartDate TEXT NOT NULL,
    EndDate TEXT NOT NULL,
    ShipFromId INTEGER,
    ShipViaID INTEGER,
    ShipToID INTEGER,
    TermsId INTEGER NOT NULL,
    FOREIGN KEY (CarrierId) REFERENCES Carrier(Id),
    FOREIGN KEY (ShipFromId) REFERENCES Address(Id),
    FOREIGN KEY (ShipViaID) REFERENCES Address(Id),
    FOREIGN KEY (ShipToID) REFERENCES Address(Id),
    FOREIGN KEY (TermsId) REFERENCES Terms(Id)
);


''';
