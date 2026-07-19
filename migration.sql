CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Users` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `Username` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `Email` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `PasswordHash` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Role` int NOT NULL,
    `CreatedAt` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    CONSTRAINT `PK_Users` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Courses` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `Title` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `Price` decimal(18,2) NOT NULL,
    `CreatedAt` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    `InstructorId` char(36) COLLATE ascii_general_ci NOT NULL,
    CONSTRAINT `PK_Courses` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Courses_Users_InstructorId` FOREIGN KEY (`InstructorId`) REFERENCES `Users` (`Id`) ON DELETE RESTRICT
) CHARACTER SET=utf8mb4;

CREATE TABLE `Enrollments` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `CourseId` char(36) COLLATE ascii_general_ci NOT NULL,
    `StudentId` char(36) COLLATE ascii_general_ci NOT NULL,
    `EnrolledAt` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    CONSTRAINT `PK_Enrollments` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Enrollments_Courses_CourseId` FOREIGN KEY (`CourseId`) REFERENCES `Courses` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Enrollments_Users_StudentId` FOREIGN KEY (`StudentId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_Courses_InstructorId` ON `Courses` (`InstructorId`);

CREATE INDEX `IX_Enrollments_CourseId` ON `Enrollments` (`CourseId`);

CREATE INDEX `IX_Enrollments_StudentId` ON `Enrollments` (`StudentId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20260716084620_InitialCreate', '8.0.0');

COMMIT;

START TRANSACTION;

ALTER TABLE `Users` ADD `Address` varchar(200) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20260717040608_AddAddress', '8.0.0');

COMMIT;

