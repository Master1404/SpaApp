-- Удаляем пользователя, если он существует
IF EXISTS (SELECT * FROM sys.database_principals WHERE name = 'admin')
BEGIN
    DROP USER admin;
    DROP LOGIN admin;
END

-- Создаем базу данных, если она еще не создана
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'SpaDB')
BEGIN
    CREATE DATABASE SpaDB;
END

-- Используем базу данных
USE NewSQLComments;

-- Создаем таблицу пользователей
CREATE TABLE Users (
    Id NVARCHAR(50) PRIMARY KEY DEFAULT NEWID(), -- Строка для ID, так как в C# используется string
    Username NVARCHAR(50) NOT NULL UNIQUE, -- Уникальное имя пользователя
    Email NVARCHAR(100) NOT NULL UNIQUE, -- Уникальный email
    Password NVARCHAR(255) NOT NULL -- Для хранения хешированного пароля
);