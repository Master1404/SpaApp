-- scripts/seed-mock-data.sql

-- Убедитесь, что используете правильную базу данных
USE SpaDB; 
GO

-- Удалить всех пользователей из таблицы Users
DELETE FROM Users; 
GO

-- Вставить тестовых пользователей
INSERT INTO Users (UserName, Email, Password)
VALUES
  ('john_doe', 'john.doe@example.com', 'hashedpassword'),
  ('jane_smith', 'jane.smith@example.com', 'hashedpassword');
GO

