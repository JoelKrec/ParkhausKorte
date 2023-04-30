-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server Version:               10.10.3-MariaDB - mariadb.org binary distribution
-- Server Betriebssystem:        Win64
-- HeidiSQL Version:             11.3.0.6295
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Exportiere Datenbank Struktur für parkhaus
CREATE DATABASE IF NOT EXISTS `parkhaus` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;
USE `parkhaus`;

-- Exportiere Struktur von Tabelle parkhaus.parkers
CREATE TABLE IF NOT EXISTS `parkers` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `numberPlate` varchar(50) DEFAULT NULL,
  `entryTime` datetime DEFAULT NULL,
  `exitTime` datetime DEFAULT NULL,
  `ticket` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Exportiere Daten aus Tabelle parkhaus.parkers: ~14 rows (ungefähr)
DELETE FROM `parkers`;
/*!40000 ALTER TABLE `parkers` DISABLE KEYS */;
INSERT INTO `parkers` (`Id`, `numberPlate`, `entryTime`, `exitTime`, `ticket`) VALUES
	(8, 'te-st5789', '2023-04-30 20:01:14', '0001-01-01 00:00:00', 0),
	(9, 'te-st8773', '2023-04-30 20:01:23', '0001-01-01 00:00:00', 0),
	(10, 'te-st7948', '2023-04-30 20:06:06', '0001-01-01 00:00:00', 0),
	(15, 'te-st9810', '2023-04-30 20:48:20', '0001-01-01 00:00:00', 0),
	(17, 'te-st2461', '2023-04-30 20:52:23', '0001-01-01 00:00:00', 0),
	(18, 'te-st7599', '2023-04-30 20:52:49', '0001-01-01 00:00:00', 0),
	(19, 'te-st3403', '2023-04-30 20:52:52', '0001-01-01 00:00:00', 1),
	(20, 'te-st7733', '2023-04-30 21:08:42', '0001-01-01 00:00:00', 0),
	(21, 'te-st4921', '2023-04-30 21:08:45', '0001-01-01 00:00:00', 0),
	(22, 'te-st5945', '2023-04-30 21:08:46', '0001-01-01 00:00:00', 0),
	(23, 'te-st31', '2023-04-30 21:08:49', '0001-01-01 00:00:00', 0);
/*!40000 ALTER TABLE `parkers` ENABLE KEYS */;

-- Exportiere Struktur von Tabelle parkhaus.parkinggarage
CREATE TABLE IF NOT EXISTS `parkinggarage` (
  `maxParkingSpaces` int(11) DEFAULT NULL,
  `reservedSeasonParkingSpaces` int(11) DEFAULT NULL,
  `parkingSpaceBuffer` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Exportiere Daten aus Tabelle parkhaus.parkinggarage: ~0 rows (ungefähr)
DELETE FROM `parkinggarage`;
/*!40000 ALTER TABLE `parkinggarage` DISABLE KEYS */;
INSERT INTO `parkinggarage` (`maxParkingSpaces`, `reservedSeasonParkingSpaces`, `parkingSpaceBuffer`) VALUES
	(180, 40, 4);
/*!40000 ALTER TABLE `parkinggarage` ENABLE KEYS */;

-- Exportiere Struktur von Tabelle parkhaus.parkingspots
CREATE TABLE IF NOT EXISTS `parkingspots` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `parkerId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Exportiere Daten aus Tabelle parkhaus.parkingspots: ~0 rows (ungefähr)
DELETE FROM `parkingspots`;
/*!40000 ALTER TABLE `parkingspots` DISABLE KEYS */;
/*!40000 ALTER TABLE `parkingspots` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
