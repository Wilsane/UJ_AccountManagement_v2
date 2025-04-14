

Use UJ_AccountManagement;


CREATE TABLE Customers (
    AccountId INT PRIMARY KEY,
    AccountHolder NVARCHAR(255) NULL
);

CREATE TABLE Transactions (
    TransactionId INT IDENTITY(1,1) PRIMARY KEY,
    AccountId INT NOT NULL,
    TransactionType NVARCHAR(50) NOT NULL,
    ReferenceId BIGINT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    TransactionDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (AccountId) REFERENCES Customers(AccountId)
);
CREATE TABLE Account (
    AccountId INT IDENTITY PRIMARY KEY,
    AccountHolder VARCHAR(100) NOT NULL,
    Phone1 VARCHAR(20) NOT NULL,
    Phone2 VARCHAR(20) NOT NULL,
    Email VARCHAR(100),
    AccountLimit DECIMAL(10, 2),
    Balance DECIMAL(10, 2) NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
);





GO
CREATE TRIGGER trg_SetReferenceId
ON Transactions
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE t
    SET t.ReferenceId = 1000 + (i.TransactionId - 1) * 10
    FROM Transactions t
    INNER JOIN inserted i ON t.TransactionId = i.TransactionId;
END;
GO



INSERT INTO Customers VALUES(100001, 'Liam Johnson'),
                             (100002, 'Emma Williams'),
                             (100003, 'Noah Smith'),
                             (100004, 'Olivia Brown'),
                             (100005, 'Elijah Davis');


INSERT INTO Transactions (AccountId, TransactionType, Amount)
VALUES 
    (100001, 'Deposit', 500.00),
    (100002, 'Withdrawal', 200.00),
    (100003, 'Transfer', 750.50),
    (100004, 'Deposit', 1200.75),
    (100005, 'Withdrawal', 450.25);

INSERT INTO Transactions (AccountId, TransactionType, Amount)
VALUES (100005, 'Deposit', 450.25);



SELECT*FROM Customers;
SELECT*FROM Transactions;
SELECT*FROM Account;

TRUNCATE table Transactions;
TRUNCATE TABLE Customers;
TRUNCATE TABLE Account;

DROP TABLE Transactions;
DROP TABLE Customers;
DROP TABLE Account;


--Transaction, Customer Inner Join
SELECT t.transactionId, c.AccountHolder as 'Account Holder',
        t.TransactionType, t.ReferenceId, t.Amount, t.TransactionDate
FROM Transactions t
INNER JOIN
Customers c ON t.AccountId=c.AccountId;



--Testing pagination
INSERT INTO Transactions (AccountId, TransactionType, Amount)
VALUES
    (100001, 'Deposit', 500.00), (100002, 'Withdrawal', 200.00), (100003, 'Transfer', 750.50),
    (100004, 'Deposit', 1200.75), (100005, 'Withdrawal', 450.25), (100001, 'Transfer', 300.10),
    (100002, 'Deposit', 620.00), (100003, 'Withdrawal', 150.50), (100004, 'Transfer', 875.25),
    (100005, 'Deposit', 980.00), (100001, 'Withdrawal', 240.40), (100002, 'Transfer', 415.15),
    (100003, 'Deposit', 720.00), (100004, 'Withdrawal', 320.20), (100005, 'Transfer', 110.00),
    (100001, 'Deposit', 1500.00), (100002, 'Withdrawal', 500.00), (100003, 'Transfer', 680.80),
    (100004, 'Deposit', 300.00), (100005, 'Withdrawal', 220.22), (100001, 'Transfer', 710.10),
    (100002, 'Deposit', 415.15), (100003, 'Withdrawal', 360.60), (100004, 'Transfer', 595.95),
    (100005, 'Deposit', 805.05), (100001, 'Withdrawal', 230.30), (100002, 'Transfer', 920.00),
    (100003, 'Deposit', 1000.00), (100004, 'Withdrawal', 380.80), (100005, 'Transfer', 510.00),
    (100001, 'Deposit', 990.90), (100002, 'Withdrawal', 210.10), (100003, 'Transfer', 130.30),
    (100004, 'Deposit', 340.40), (100005, 'Withdrawal', 460.60), (100001, 'Transfer', 570.70),
    (100002, 'Deposit', 680.80), (100003, 'Withdrawal', 790.90), (100004, 'Transfer', 800.00),
    (100005, 'Deposit', 910.10), (100001, 'Withdrawal', 101.01), (100002, 'Transfer', 202.02),
    (100003, 'Deposit', 303.03), (100004, 'Withdrawal', 404.04), (100005, 'Transfer', 505.05),
    (100001, 'Deposit', 606.06), (100002, 'Withdrawal', 707.07), (100003, 'Transfer', 808.08),
    (100004, 'Deposit', 909.09), (100005, 'Withdrawal', 100.10), (100001, 'Transfer', 200.20),
    (100002, 'Deposit', 300.30), (100003, 'Withdrawal', 400.40), (100004, 'Transfer', 500.50),
    (100005, 'Deposit', 600.60), (100001, 'Withdrawal', 700.70), (100002, 'Transfer', 800.80),
    (100003, 'Deposit', 900.90), (100004, 'Withdrawal', 110.11), (100005, 'Transfer', 220.22),
    (100001, 'Deposit', 330.33), (100002, 'Withdrawal', 440.44), (100003, 'Transfer', 550.55),
    (100004, 'Deposit', 660.66), (100005, 'Withdrawal', 770.77), (100001, 'Transfer', 880.88),
    (100002, 'Deposit', 990.99), (100003, 'Withdrawal', 111.11), (100004, 'Transfer', 222.22),
    (100005, 'Deposit', 333.33), (100001, 'Withdrawal', 444.44), (100002, 'Transfer', 555.55),
    (100003, 'Deposit', 666.66), (100004, 'Withdrawal', 777.77), (100005, 'Transfer', 888.88),
    (100001, 'Deposit', 999.99), (100002, 'Withdrawal', 123.45), (100003, 'Transfer', 234.56),
    (100004, 'Deposit', 345.67), (100005, 'Withdrawal', 456.78), (100001, 'Transfer', 567.89),
    (100002, 'Deposit', 678.90), (100003, 'Withdrawal', 789.01), (100004, 'Transfer', 890.12),
    (100005, 'Deposit', 901.23), (100001, 'Withdrawal', 101.10), (100002, 'Transfer', 202.20),
    (100003, 'Deposit', 303.30), (100004, 'Withdrawal', 404.40), (100005, 'Transfer', 505.50),
    (100001, 'Deposit', 606.60), (100002, 'Withdrawal', 707.70), (100003, 'Transfer', 808.80),
    (100004, 'Deposit', 909.90), (100005, 'Withdrawal', 1001.00), (100001, 'Transfer', 1111.10),
    (100002, 'Deposit', 1212.12), (100003, 'Withdrawal', 1313.13), (100004, 'Transfer', 1414.14),
    (100005, 'Deposit', 1515.15);






INSERT INTO Account (AccountHolder, Phone1, Phone2, Email, AccountLimit, Balance, CreatedDate) 
VALUES
('Thabo Mokoena', '+27111234567', '+27831234567', 'thabo.mokoena@example.com', 15000.00, 3200.75, '2024-12-01'),
('Lerato Dlamini', '+27111234568', '+27831234568', 'lerato.d@example.com', 20000.00, 11500.00, '2024-12-05'),
('Sipho Nkosi', '+27111234569', '+27831234569', 'sipho.nkosi@example.com', 18000.00, 7800.25, '2025-01-10'),
('Nomsa Khumalo', '+27111234570', '+27831234570', 'nomsa.khumalo@example.com', 22000.00, 15000.50, '2025-02-01'),
('Andile Zulu', '+27111234571', '+27831234571', 'andile.zulu@example.com', 17500.00, 9400.00, '2025-02-15'),
('Boitumelo Maseko', '+27111234572', '+27831234572', 'boitumelo.m@example.com', 16000.00, 500.00, '2025-02-20'),
('Karabo Mahlangu', '+27111234573', '+27831234573', 'karabo.m@example.com', 10000.00, 10000.00, '2025-03-01'),
('Mpho Molefe', '+27111234574', '+27831234574', 'mpho.molefe@example.com', 12000.00, 7800.00, '2025-03-05'),
('Zanele Ndlovu', '+27111234575', '+27831234575', 'zanele.ndlovu@example.com', 14000.00, 2200.00, '2025-03-10'),
('Kabelo Sehoana', '+27111234576', '+27831234576', 'kabelo.s@example.com', 19000.00, 10500.00, '2025-03-15'),
('Tshepo Radebe', '+27111234577', '+27831234577', 'tshepo.radebe@example.com', 13000.00, 3000.00, '2025-03-20'),
('Naledi Phiri', '+27111234578', '+27831234578', 'naledi.phiri@example.com', 25000.00, 24000.00, '2025-03-22'),
('Sibusiso Sithole', '+27111234579', '+27831234579', 'sibusiso.s@example.com', 11000.00, 5500.00, '2025-03-25'),
('Refilwe Modise', '+27111234580', '+27831234580', 'refilwe.modise@example.com', 21000.00, 21000.00, '2025-03-27'),
('Nthabiseng Ramaila', '+27111234581', '+27831234581', 'nthabiseng.r@example.com', 17000.00, 2500.00, '2025-03-30'),
('Themba Dube', '+27111234582', '+27831234582', 'themba.dube@example.com', 15000.00, 15000.00, '2025-04-01'),
('Puleng Mokoetla', '+27111234583', '+27831234583', 'puleng.mokoetla@example.com', 9000.00, 8900.00, '2025-04-05'),
('Lindiwe Xaba', '+27111234584', '+27831234584', 'lindiwe.x@example.com', 8000.00, 100.00, '2025-04-07'),
('Jabulani Gumede', '+27111234585', '+27831234585', 'jabulani.g@example.com', 13000.00, 7000.00, '2025-04-10'),
('Precious Mhlongo', '+27111234586', '+27831234586', 'precious.m@example.com', 16000.00, 3600.00, '2025-04-13');
