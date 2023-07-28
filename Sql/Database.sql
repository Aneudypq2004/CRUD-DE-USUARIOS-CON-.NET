IF NOT EXISTS (SELECT name From sys.databases WHERE name = 'systemLogin')
BEGIN
 CREATE DATABASE systemLogin;
END

GO

USE systemLogin;

GO

CREATE TABLE users (
    id_user INT PRIMARY KEY IDENTITY(1,1),
    user_name VARCHAR(60) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    user_password VARCHAR(256),
    token VARCHAR(60),
    Confirmed BIT DEFAULT 0  
)
DROP TABLE users

SELECT * FROM users;

