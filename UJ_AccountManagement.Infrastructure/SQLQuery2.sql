

Use UJ_AccountManagement;


DROP TABLE Transactions;
DROP TABLE Customers;

TRUNCATE table Transactions;
TRUNCATE TABLE Customers;

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

INSERT INTO Customers VALUES(100001, 'Customer A'),
                            (100002, 'Customer B'),
                            (100003, 'Customer C'),
                            (100004, 'Customer D'),
                            (100005, 'Customer E');

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
SELECT t.transactionId, c.AccountHolder as 'Account Holder',
        t.TransactionType, t.ReferenceId, t.Amount, t.TransactionDate
FROM Transactions t
INNER JOIN
Customers c ON t.AccountId=c.AccountId;



--Testing pagination
INSERT INTO Transactions (AccountId, TransactionType, Amount)
VALUES
    (100001, 'Deposit', 500.00),
    (100002, 'Withdrawal', 200.00),
    (100003, 'Transfer', 750.50),
    (100004, 'Deposit', 1200.75),
    (100005, 'Withdrawal', 450.25),
    (100001, 'Transfer', 300.10),
    (100002, 'Deposit', 620.00),
    (100003, 'Withdrawal', 150.50),
    (100004, 'Transfer', 875.25),
    (100005, 'Deposit', 980.00),
    (100001, 'Withdrawal', 240.40),
    (100002, 'Transfer', 415.15),
    (100003, 'Deposit', 720.00),
    (100004, 'Withdrawal', 320.20),
    (100005, 'Transfer', 110.00),
    (100001, 'Deposit', 1500.00),
    (100002, 'Withdrawal', 500.00),
    (100003, 'Transfer', 680.80),
    (100004, 'Deposit', 300.00),
    (100005, 'Withdrawal', 220.22),
    (100001, 'Transfer', 710.10),
    (100002, 'Deposit', 415.15),
    (100003, 'Withdrawal', 360.60),
    (100004, 'Transfer', 595.95),
    (100005, 'Deposit', 805.05),
    (100001, 'Withdrawal', 230.30),
    (100002, 'Transfer', 920.00),
    (100003, 'Deposit', 1000.00),
    (100004, 'Withdrawal', 380.80),
    (100005, 'Transfer', 510.00),
    (100001, 'Deposit', 990.90),
    (100002, 'Withdrawal', 210.10),
    (100003, 'Transfer', 130.30),
    (100004, 'Deposit', 340.40),
    (100005, 'Withdrawal', 460.60),
    (100001, 'Transfer', 570.70),
    (100002, 'Deposit', 680.80),
    (100003, 'Withdrawal', 790.90),
    (100004, 'Transfer', 800.00),
    (100005, 'Deposit', 910.10),
    (100001, 'Withdrawal', 101.01),
    (100002, 'Transfer', 202.02),
    (100003, 'Deposit', 303.03),
    (100004, 'Withdrawal', 404.04),
    (100005, 'Transfer', 505.05),
    (100001, 'Deposit', 606.06),
    (100002, 'Withdrawal', 707.07),
    (100003, 'Transfer', 808.08),
    (100004, 'Deposit', 909.09),
    (100005, 'Withdrawal', 100.10),
    (100001, 'Transfer', 200.20),
    (100002, 'Deposit', 300.30),
    (100003, 'Withdrawal', 400.40),
    (100004, 'Transfer', 500.50),
    (100005, 'Deposit', 600.60),
    (100001, 'Withdrawal', 700.70),
    (100002, 'Transfer', 800.80),
    (100003, 'Deposit', 900.90),
    (100004, 'Withdrawal', 110.11),
    (100005, 'Transfer', 220.22),
    (100001, 'Deposit', 330.33),
    (100002, 'Withdrawal', 440.44),
    (100003, 'Transfer', 550.55),
    (100004, 'Deposit', 660.66),
    (100005, 'Withdrawal', 770.77),
    (100001, 'Transfer', 880.88),
    (100002, 'Deposit', 990.99),
    (100003, 'Withdrawal', 111.11),
    (100004, 'Transfer', 222.22),
    (100005, 'Deposit', 333.33),
    (100001, 'Withdrawal', 444.44),
    (100002, 'Transfer', 555.55),
    (100003, 'Deposit', 666.66),
    (100004, 'Withdrawal', 777.77),
    (100005, 'Transfer', 888.88),
    (100001, 'Deposit', 999.99),
    (100002, 'Withdrawal', 123.45),
    (100003, 'Transfer', 234.56),
    (100004, 'Deposit', 345.67),
    (100005, 'Withdrawal', 456.78),
    (100001, 'Transfer', 567.89),
    (100002, 'Deposit', 678.90),
    (100003, 'Withdrawal', 789.01),
    (100004, 'Transfer', 890.12),
    (100005, 'Deposit', 901.23),
    (100001, 'Withdrawal', 101.10),
    (100002, 'Transfer', 202.20),
    (100003, 'Deposit', 303.30),
    (100004, 'Withdrawal', 404.40),
    (100005, 'Transfer', 505.50),
    (100001, 'Deposit', 606.60),
    (100002, 'Withdrawal', 707.70),
    (100003, 'Transfer', 808.80),
    (100004, 'Deposit', 909.90),
    (100005, 'Withdrawal', 1001.00),
    (100001, 'Transfer', 1111.10),
    (100002, 'Deposit', 1212.12),
    (100003, 'Withdrawal', 1313.13),
    (100004, 'Transfer', 1414.14),
    (100005, 'Deposit', 1515.15);
