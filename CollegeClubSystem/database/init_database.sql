USE master
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'CollegeClubDB')
BEGIN
    CREATE DATABASE CollegeClubDB
END
GO

USE CollegeClubDB
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Roles')
BEGIN
    CREATE TABLE Roles (
        RoleId INT PRIMARY KEY IDENTITY(1,1),
        RoleName VARCHAR(50) NOT NULL UNIQUE,
        Description VARCHAR(200),
        CreateTime DATETIME DEFAULT GETDATE()
    )

    INSERT INTO Roles (RoleName, Description) VALUES
    ('SuperAdmin', '超级管理员'),
    ('Admin', '学校管理员'),
    ('ClubManager', '社团负责人'),
    ('Member', '社团成员'),
    ('Student', '普通学生')
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        UserId INT PRIMARY KEY IDENTITY(1,1),
        Username VARCHAR(50) NOT NULL UNIQUE,
        Password VARCHAR(255) NOT NULL,
        RealName VARCHAR(50) NOT NULL,
        Email VARCHAR(100) UNIQUE,
        Phone VARCHAR(20),
        RoleId INT DEFAULT 5,
        Status INT DEFAULT 1,
        CreateTime DATETIME DEFAULT GETDATE(),
        CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
    )

    INSERT INTO Users (Username, Password, RealName, Email, RoleId) VALUES
    ('admin', '$2a$11$EixZaYbB.rK4fl8x2q7Meu6Q6D2V5fF5Q5Q5Q5Q5Q5Q', '系统管理员', 'admin@college.edu', 1)
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Clubs')
BEGIN
    CREATE TABLE Clubs (
        ClubId INT PRIMARY KEY IDENTITY(1,1),
        Name VARCHAR(100) NOT NULL,
        Description TEXT,
        Logo VARCHAR(255),
        Category VARCHAR(50),
        FounderId INT NOT NULL,
        Status INT DEFAULT 0,
        CreateTime DATETIME DEFAULT GETDATE(),
        CONSTRAINT FK_Clubs_Users FOREIGN KEY (FounderId) REFERENCES Users(UserId)
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Members')
BEGIN
    CREATE TABLE Members (
        MemberId INT PRIMARY KEY IDENTITY(1,1),
        UserId INT NOT NULL,
        ClubId INT NOT NULL,
        Role VARCHAR(20) DEFAULT 'Member',
        Status INT DEFAULT 1,
        JoinTime DATETIME DEFAULT GETDATE(),
        CONSTRAINT FK_Members_Users FOREIGN KEY (UserId) REFERENCES Users(UserId),
        CONSTRAINT FK_Members_Clubs FOREIGN KEY (ClubId) REFERENCES Clubs(ClubId),
        CONSTRAINT UQ_Members_UserClub UNIQUE (UserId, ClubId)
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Activities')
BEGIN
    CREATE TABLE Activities (
        ActivityId INT PRIMARY KEY IDENTITY(1,1),
        ClubId INT NOT NULL,
        Title VARCHAR(200) NOT NULL,
        Description TEXT,
        StartTime DATETIME NOT NULL,
        EndTime DATETIME NOT NULL,
        Location VARCHAR(200),
        MaxParticipants INT DEFAULT 100,
        Status INT DEFAULT 0,
        CreateTime DATETIME DEFAULT GETDATE(),
        CONSTRAINT FK_Activities_Clubs FOREIGN KEY (ClubId) REFERENCES Clubs(ClubId)
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'FinanceRecords')
BEGIN
    CREATE TABLE FinanceRecords (
        RecordId INT PRIMARY KEY IDENTITY(1,1),
        ClubId INT NOT NULL,
        Type INT NOT NULL,
        Amount DECIMAL(10,2) NOT NULL,
        Description VARCHAR(500),
        Status INT DEFAULT 0,
        CreateTime DATETIME DEFAULT GETDATE(),
        CONSTRAINT FK_FinanceRecords_Clubs FOREIGN KEY (ClubId) REFERENCES Clubs(ClubId)
    )
END
GO

INSERT INTO Clubs (Name, Description, Category, FounderId, Status) VALUES
('科技创新协会', '致力于科技创新与技术交流', '学术科技', 1, 1),
('文艺社', '弘扬传统文化，培养艺术情操', '文化艺术', 1, 1),
('足球协会', '热爱足球运动，增进同学友谊', '体育竞技', 1, 1),
('志愿者协会', '奉献爱心，服务社会', '公益服务', 1, 1),
('创业俱乐部', '激发创业热情，培养创业能力', '创新创业', 1, 1)
GO

INSERT INTO Activities (ClubId, Title, Description, StartTime, EndTime, Location, MaxParticipants, Status) VALUES
(1, '技术分享会', '邀请行业专家分享最新技术趋势', '2026-05-20 14:00:00', '2026-05-20 17:00:00', '学术报告厅', 100, 1),
(2, '书法展览', '展示同学们的书法作品', '2026-05-25 09:00:00', '2026-05-27 17:00:00', '图书馆展厅', 200, 1),
(3, '足球友谊赛', '与兄弟院校进行足球比赛', '2026-05-22 15:00:00', '2026-05-22 17:00:00', '学校足球场', 22, 1),
(4, '社区服务', '前往社区开展志愿服务', '2026-05-28 09:00:00', '2026-05-28 12:00:00', '阳光社区', 30, 1),
(5, '创业讲座', '成功创业者分享经验', '2026-06-01 14:00:00', '2026-06-01 16:30:00', '创业孵化中心', 80, 1)
GO

PRINT '数据库初始化完成！'