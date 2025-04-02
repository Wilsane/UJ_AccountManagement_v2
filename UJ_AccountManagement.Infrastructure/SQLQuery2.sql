

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