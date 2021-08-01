-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               8.0.26 - MySQL Community Server - GPL
-- Server OS:                    Win64
-- HeidiSQL Version:             11.1.0.6116
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for notes
CREATE DATABASE IF NOT EXISTS `notes` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `notes`;

-- Dumping structure for table notes.folders
CREATE TABLE IF NOT EXISTS `folders` (
  `IdFolder` int NOT NULL AUTO_INCREMENT,
  `IdUser` int NOT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `InsertedAt` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  PRIMARY KEY (`IdFolder`),
  KEY `IX_Folders_IdUser` (`IdUser`),
  CONSTRAINT `FK_Folders_Users_IdUser` FOREIGN KEY (`IdUser`) REFERENCES `users` (`IdUser`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table notes.folders: ~0 rows (approximately)
/*!40000 ALTER TABLE `folders` DISABLE KEYS */;
INSERT INTO `folders` (`IdFolder`, `IdUser`, `Name`, `InsertedAt`) VALUES
	(8, 1, 'school', '2021-08-01 16:22:50.789911'),
	(9, 1, 'work', '2021-08-01 16:22:58.697338'),
	(10, 2, 'sports', '2021-08-01 16:25:42.942234');
/*!40000 ALTER TABLE `folders` ENABLE KEYS */;

-- Dumping structure for table notes.notebodies
CREATE TABLE IF NOT EXISTS `notebodies` (
  `IdNoteBody` int NOT NULL AUTO_INCREMENT,
  `IdNote` int NOT NULL,
  `Body` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`IdNoteBody`),
  KEY `IX_NoteBodies_IdNote` (`IdNote`),
  CONSTRAINT `FK_NoteBodies_Notes_IdNote` FOREIGN KEY (`IdNote`) REFERENCES `notes` (`IdNote`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table notes.notebodies: ~0 rows (approximately)
/*!40000 ALTER TABLE `notebodies` DISABLE KEYS */;
INSERT INTO `notebodies` (`IdNoteBody`, `IdNote`, `Body`) VALUES
	(7, 9, 'plank'),
	(8, 9, 'deadlift'),
	(9, 9, 'curls'),
	(10, 9, 'running'),
	(11, 10, 'work'),
	(12, 10, 'tennis'),
	(13, 11, 'study'),
	(14, 12, 'create db'),
	(15, 13, 'mock data'),
	(16, 13, 'create api'),
	(17, 14, 'aips'),
	(18, 14, 'pb2'),
	(19, 15, 'tennis'),
	(20, 15, 'football'),
	(24, 16, 'football'),
	(25, 16, 'basketball');
/*!40000 ALTER TABLE `notebodies` ENABLE KEYS */;

-- Dumping structure for table notes.notebodytypes
CREATE TABLE IF NOT EXISTS `notebodytypes` (
  `IdNoteBodyType` int NOT NULL AUTO_INCREMENT,
  `Code` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Title` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`IdNoteBodyType`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table notes.notebodytypes: ~0 rows (approximately)
/*!40000 ALTER TABLE `notebodytypes` DISABLE KEYS */;
INSERT INTO `notebodytypes` (`IdNoteBodyType`, `Code`, `Title`) VALUES
	(1, 'single', 'Single text body'),
	(2, 'multiple', 'Multiple text bodies');
/*!40000 ALTER TABLE `notebodytypes` ENABLE KEYS */;

-- Dumping structure for table notes.notes
CREATE TABLE IF NOT EXISTS `notes` (
  `IdNote` int NOT NULL AUTO_INCREMENT,
  `IdUser` int NOT NULL,
  `IdNoteVisibility` int NOT NULL,
  `IdNoteBodyType` int NOT NULL,
  `Title` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `InsertedAt` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  PRIMARY KEY (`IdNote`),
  KEY `IX_Notes_IdNoteBodyType` (`IdNoteBodyType`),
  KEY `IX_Notes_IdNoteVisibility` (`IdNoteVisibility`),
  KEY `IX_Notes_IdUser` (`IdUser`),
  CONSTRAINT `FK_Notes_NoteBodyTypes_IdNoteBodyType` FOREIGN KEY (`IdNoteBodyType`) REFERENCES `notebodytypes` (`IdNoteBodyType`) ON DELETE CASCADE,
  CONSTRAINT `FK_Notes_NoteVisibilites_IdNoteVisibility` FOREIGN KEY (`IdNoteVisibility`) REFERENCES `notevisibilites` (`IdVisibility`) ON DELETE CASCADE,
  CONSTRAINT `FK_Notes_Users_IdUser` FOREIGN KEY (`IdUser`) REFERENCES `users` (`IdUser`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table notes.notes: ~0 rows (approximately)
/*!40000 ALTER TABLE `notes` DISABLE KEYS */;
INSERT INTO `notes` (`IdNote`, `IdUser`, `IdNoteVisibility`, `IdNoteBodyType`, `Title`, `InsertedAt`) VALUES
	(9, 1, 1, 2, 'gym exercise', '2021-08-01 16:34:08.638342'),
	(10, 1, 2, 2, 'monday list', '2021-08-01 16:34:43.985834'),
	(11, 1, 2, 1, 'tuesday list', '2021-08-01 16:35:03.654421'),
	(12, 1, 2, 1, 'wendsday list', '2021-08-01 16:35:58.433626'),
	(13, 1, 2, 2, 'tuesday list', '2021-08-01 16:36:18.435758'),
	(14, 1, 1, 2, 'weekend', '2021-08-01 16:36:53.401890'),
	(15, 2, 1, 2, 'teambuilding list', '2021-08-01 16:37:41.350758'),
	(16, 2, 2, 2, 'possible team sports', '2021-08-01 16:39:50.958473');
/*!40000 ALTER TABLE `notes` ENABLE KEYS */;

-- Dumping structure for table notes.notesfolders
CREATE TABLE IF NOT EXISTS `notesfolders` (
  `IdNote` int NOT NULL,
  `IdFolder` int NOT NULL,
  PRIMARY KEY (`IdNote`,`IdFolder`),
  KEY `IX_NotesFolders_IdFolder` (`IdFolder`),
  CONSTRAINT `FK_NotesFolders_Folders_IdFolder` FOREIGN KEY (`IdFolder`) REFERENCES `folders` (`IdFolder`) ON DELETE CASCADE,
  CONSTRAINT `FK_NotesFolders_Notes_IdNote` FOREIGN KEY (`IdNote`) REFERENCES `notes` (`IdNote`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table notes.notesfolders: ~0 rows (approximately)
/*!40000 ALTER TABLE `notesfolders` DISABLE KEYS */;
INSERT INTO `notesfolders` (`IdNote`, `IdFolder`) VALUES
	(14, 8),
	(12, 9),
	(13, 9),
	(15, 10),
	(16, 10);
/*!40000 ALTER TABLE `notesfolders` ENABLE KEYS */;

-- Dumping structure for table notes.notevisibilites
CREATE TABLE IF NOT EXISTS `notevisibilites` (
  `IdVisibility` int NOT NULL AUTO_INCREMENT,
  `Code` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Title` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`IdVisibility`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table notes.notevisibilites: ~0 rows (approximately)
/*!40000 ALTER TABLE `notevisibilites` DISABLE KEYS */;
INSERT INTO `notevisibilites` (`IdVisibility`, `Code`, `Title`) VALUES
	(1, 'public', 'Public'),
	(2, 'private', 'Private');
/*!40000 ALTER TABLE `notevisibilites` ENABLE KEYS */;

-- Dumping structure for table notes.users
CREATE TABLE IF NOT EXISTS `users` (
  `IdUser` int NOT NULL AUTO_INCREMENT,
  `Username` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `FirstName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `LastName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Password` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`IdUser`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table notes.users: ~0 rows (approximately)
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` (`IdUser`, `Username`, `FirstName`, `LastName`, `Password`) VALUES
	(1, 'doppler', 'jure', 'mohar', 'doppler'),
	(2, 'celtra', 'celtra', 'celtra', 'celtra');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;

-- Dumping structure for table notes.__efmigrationshistory
CREATE TABLE IF NOT EXISTS `__efmigrationshistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table notes.__efmigrationshistory: ~1 rows (approximately)
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
	('20210801095334_InitDb', '5.0.8'),
	('20210801100754_AddFolderToNotes', '5.0.8'),
	('20210801101234_CreateNotesFoldersTable', '5.0.8'),
	('20210801101504_FixPFKNotesFolders', '5.0.8');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
