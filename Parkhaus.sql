-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server-Version:               10.10.4-MariaDB - mariadb.org binary distribution
-- Server-Betriebssystem:        Win64
-- HeidiSQL Version:             12.5.0.6677
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

-- Exportiere Struktur von Tabelle parkhaus.parkers
DROP TABLE IF EXISTS `parkers`;
CREATE TABLE IF NOT EXISTS `parkers` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Datensatz-Id (Primärschlüssel)',
  `numberPlate` varchar(50) DEFAULT NULL COMMENT 'Nummernschild des Parkers',
  `entryTime` datetime DEFAULT NULL COMMENT 'Einfahrtszeit',
  `exitTime` datetime DEFAULT NULL COMMENT 'Ausfahrtszeit',
  `ticket` tinyint(4) DEFAULT NULL COMMENT 'Art des Tickets (0 = Kurz, 1 = Dauer)',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=180 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci COMMENT='Diese Tabelle speichert alle Parker, welche sich im Parkhaus befinden mit Nummernschild, Tickettyp, Ein- und Ausfahrtszeiten.\r\nKurzparker werden nach dem ausfahren gelöscht, Dauerparker bekommen eine Ausfahrtszeit, da sie weiterhin vom System gespeichert werden.\r\nAktuell nicht implementierte Aspekte: Wiederkehr eines existierenden Dauerparkers.';

-- Exportiere Daten aus Tabelle parkhaus.parkers: ~113 rows (ungefähr)
REPLACE INTO `parkers` (`Id`, `numberPlate`, `entryTime`, `exitTime`, `ticket`) VALUES
	(8, 'te-st5789', '2023-04-30 20:01:14', '2023-05-23 11:49:52', 0),
	(19, 'te-st3403', '2023-04-30 20:52:52', '2023-06-02 12:55:18', 1),
	(30, 'te-st1830', '2023-06-02 12:55:32', '2023-06-06 09:51:56', 1),
	(31, 'te-st1610', '2023-06-02 12:55:33', '2023-06-06 09:51:57', 1),
	(32, 'te-st5938', '2023-06-02 12:55:34', '2023-06-06 09:53:28', 1),
	(33, 'te-st1056', '2023-06-02 12:55:34', '2023-06-06 09:53:27', 1),
	(34, 'te-st5347', '2023-06-02 12:55:34', '2023-06-06 09:53:27', 1),
	(43, 'te-st9971', '2023-06-02 12:55:38', NULL, 0),
	(48, 'te-st9745', '2023-06-02 12:55:39', NULL, 0),
	(49, 'te-st4073', '2023-06-02 12:55:39', NULL, 0),
	(50, 'te-st3657', '2023-06-02 12:55:39', NULL, 0),
	(51, 'te-st5368', '2023-06-02 12:55:39', NULL, 0),
	(56, 'te-st3292', '2023-06-02 12:55:40', NULL, 0),
	(57, 'te-st4795', '2023-06-02 12:55:40', NULL, 0),
	(59, 'te-st5978', '2023-06-02 12:55:41', NULL, 0),
	(60, 'te-st9720', '2023-06-02 12:55:41', NULL, 0),
	(62, 'te-st2591', '2023-06-02 12:55:41', NULL, 0),
	(66, 'te-st9033', '2023-06-02 12:55:42', NULL, 0),
	(67, 'te-st6248', '2023-06-02 12:55:42', NULL, 0),
	(75, 'te-st2998', '2023-06-02 12:55:44', NULL, 0),
	(80, 'te-st1150', '2023-06-02 12:55:45', NULL, 0),
	(82, 'te-st2785', '2023-06-02 12:55:45', NULL, 0),
	(83, 'te-st2940', '2023-06-02 12:55:55', NULL, 0),
	(84, 'te-st8488', '2023-06-02 12:55:55', NULL, 0),
	(85, 'te-st568', '2023-06-02 12:55:56', '2023-06-06 11:55:33', 1),
	(86, 'te-st4978', '2023-06-02 12:55:57', '2023-06-06 11:55:33', 1),
	(87, 'te-st2930', '2023-06-02 12:55:58', '2023-06-06 11:55:34', 1),
	(88, 'te-st6357', '2023-06-02 12:55:58', '2023-06-06 11:55:34', 1),
	(89, 'te-st9851', '2023-06-02 12:55:59', NULL, 0),
	(91, 'te-st5660', '2023-06-02 12:56:00', NULL, 0),
	(93, 'te-st4988', '2023-06-02 12:56:04', '2023-06-06 11:55:34', 1),
	(95, 'te-st4559', '2023-06-06 11:54:43', NULL, 0),
	(96, 'te-st7392', '2023-06-06 11:54:44', NULL, 0),
	(97, 'te-st2370', '2023-06-06 11:54:45', NULL, 0),
	(99, 'te-st8319', '2023-06-06 11:54:45', NULL, 0),
	(100, 'te-st7507', '2023-06-06 11:54:45', NULL, 0),
	(105, 'te-st2282', '2023-06-06 11:54:47', '2023-06-06 11:55:35', 1),
	(106, 'te-st7', '2023-06-06 11:54:47', '2023-06-06 11:55:34', 1),
	(107, 'te-st442', '2023-06-06 11:54:47', '2023-06-06 11:55:35', 1),
	(108, 'te-st6211', '2023-06-06 11:54:47', '2023-06-06 11:55:35', 1),
	(109, 'te-st6588', '2023-06-06 11:54:48', '2023-06-06 12:35:12', 1),
	(110, 'te-st5552', '2023-06-06 11:54:48', '2023-06-06 11:55:35', 1),
	(111, 'te-st2224', '2023-06-06 11:54:48', '2023-06-06 11:55:41', 1),
	(112, 'te-st1675', '2023-06-06 11:54:48', '2023-06-06 12:20:18', 1),
	(113, 'te-st4435', '2023-06-06 11:54:48', '2023-06-06 12:30:34', 1),
	(114, 'te-st2489', '2023-06-06 11:54:48', '2023-06-06 12:34:15', 1),
	(115, 'te-st4521', '2023-06-06 11:54:49', '2023-06-06 12:35:58', 1),
	(116, 'te-st5637', '2023-06-06 11:54:49', '2023-06-06 12:37:05', 1),
	(117, 'te-st802', '2023-06-06 12:37:55', '2023-06-06 12:38:11', 1),
	(118, 'te-st4776', '2023-06-06 12:37:56', '2023-06-06 12:48:08', 1),
	(119, 'te-st9671', '2023-06-06 12:37:56', '2023-06-06 12:39:32', 1),
	(120, 'te-st4484', '2023-06-06 12:37:56', '2023-06-06 12:42:48', 1),
	(121, 'te-st9412', '2023-06-06 12:37:56', '2023-06-06 12:43:40', 1),
	(122, 'te-st94', '2023-06-06 12:37:56', '2023-06-06 12:46:53', 1),
	(123, 'te-st4053', '2023-06-06 12:37:57', '2023-06-06 14:13:58', 1),
	(124, 'te-st8592', '2023-06-06 12:37:57', '2023-06-06 12:50:07', 1),
	(125, 'te-st6835', '2023-06-06 12:37:57', '2023-06-06 14:08:45', 1),
	(126, 'te-st5963', '2023-06-06 12:37:57', '2023-06-06 14:08:53', 1),
	(127, 'te-st7346', '2023-06-06 12:37:57', '2023-06-06 14:08:58', 1),
	(128, 'te-st133', '2023-06-06 12:37:57', '2023-06-06 14:13:56', 1),
	(129, 'te-st9182', '2023-06-06 12:37:58', '2023-06-06 14:14:00', 1),
	(130, 'te-st7260', '2023-06-06 12:37:58', '2023-06-06 14:13:59', 1),
	(131, 'te-st8066', '2023-06-06 12:37:58', '2023-06-06 14:13:59', 1),
	(132, 'te-st813', '2023-06-06 12:37:58', '2023-06-06 14:14:00', 1),
	(133, 'te-st4952', '2023-06-06 12:37:58', '2023-06-06 14:14:00', 1),
	(134, 'te-st7685', '2023-06-06 12:37:58', '2023-06-06 14:14:00', 1),
	(135, 'te-st2923', '2023-06-06 12:37:59', '2023-06-06 14:25:02', 1),
	(136, 'te-st9041', '2023-06-06 12:37:59', '2023-06-06 14:14:00', 1),
	(137, 'te-st8043', '2023-06-06 12:37:59', '2023-06-06 14:14:01', 1),
	(138, 'te-st6035', '2023-06-06 12:37:59', '2023-06-06 14:14:01', 1),
	(139, 'te-st9105', '2023-06-06 12:37:59', '2023-06-06 14:14:01', 1),
	(140, 'te-st5184', '2023-06-06 12:37:59', '2023-06-06 14:25:01', 1),
	(141, 'te-st4114', '2023-06-06 12:38:00', '2023-06-06 14:25:06', 1),
	(142, 'te-st2886', '2023-06-06 12:38:00', '2023-06-06 14:25:02', 1),
	(143, 'te-st6844', '2023-06-06 12:38:00', '2023-06-06 14:25:02', 1),
	(144, 'te-st238', '2023-06-06 12:38:00', '2023-06-06 14:25:03', 1),
	(145, 'te-st7177', '2023-06-06 12:38:00', '2023-06-06 14:25:03', 1),
	(146, 'te-st5169', '2023-06-06 12:38:00', '2023-06-06 14:25:03', 1),
	(147, 'te-st4720', '2023-06-06 12:38:01', '2023-06-06 14:56:10', 1),
	(148, 'te-st5227', '2023-06-06 12:38:01', '2023-06-06 14:25:09', 1),
	(149, 'te-st5528', '2023-06-06 12:38:01', '2023-06-06 14:56:10', 1),
	(150, 'te-st4522', '2023-06-06 12:38:01', '2023-06-06 14:56:10', 1),
	(151, 'te-st4593', '2023-06-06 12:38:01', '2023-06-06 14:56:10', 1),
	(152, 'te-st323', '2023-06-06 12:38:01', '2023-06-06 14:56:10', 1),
	(153, 'te-st9554', '2023-06-06 12:38:02', '2023-06-06 14:56:11', 1),
	(154, 'te-st7239', '2023-06-06 12:38:02', '2023-06-06 14:56:11', 1),
	(155, 'te-st8040', '2023-06-06 12:38:02', '2023-06-06 14:56:11', 1),
	(156, 'te-st5544', '2023-06-06 12:38:02', '2023-06-06 14:56:11', 1),
	(157, 'te-st162', '2023-06-06 12:38:02', '2023-06-06 14:56:11', 1),
	(158, 'te-st4412', '2023-06-06 12:38:02', '2023-06-06 14:56:11', 1),
	(159, 'te-st6103', '2023-06-06 12:38:03', '2023-06-06 14:56:14', 1),
	(160, 'te-st5882', '2023-06-06 12:38:03', '2023-06-06 14:56:12', 1),
	(161, 'te-st6864', '2023-06-06 12:38:03', '2023-06-06 14:56:12', 1),
	(162, 'te-st3595', '2023-06-06 12:38:03', '2023-06-06 14:56:14', 1),
	(163, 'te-st6334', '2023-06-06 12:38:03', '2023-06-06 14:56:14', 1),
	(164, 'te-st7421', '2023-06-06 12:38:04', '2023-06-06 14:56:15', 1),
	(165, 'te-st2397', '2023-06-06 12:38:04', '2023-06-06 14:56:14', 1),
	(166, 'te-st8838', '2023-06-06 12:38:05', NULL, 0),
	(167, 'te-st2832', '2023-06-06 12:38:05', NULL, 0),
	(168, 'te-st3147', '2023-06-06 12:38:06', NULL, 0),
	(169, 'te-st40', '2023-06-06 12:38:06', NULL, 0),
	(170, 'te-st6263', '2023-06-06 12:38:07', '2023-06-06 14:56:15', 1),
	(171, 'te-st4271', '2023-06-06 15:00:13', '2023-06-06 15:00:18', 1),
	(172, 'te-st5839', '2023-06-06 15:00:14', '2023-06-06 15:00:20', 1),
	(173, 'te-st5647', '2023-06-06 15:00:14', '2023-06-06 15:00:19', 1),
	(174, 'te-st6230', '2023-06-06 15:00:15', '2023-06-06 15:00:22', 1),
	(175, 'te-st1302', '2023-06-06 15:00:15', '2023-06-06 15:00:22', 1),
	(176, 'te-st6184', '2023-06-06 15:00:15', '2023-06-06 15:00:21', 1),
	(177, 'te-st1559', '2023-06-06 15:00:15', '2023-06-06 15:00:21', 1),
	(178, 'te-st5404', '2023-06-06 15:00:15', '2023-06-06 15:00:20', 1),
	(179, 'te-st3208', '2023-06-06 15:00:15', '2023-06-06 15:00:20', 1);

-- Exportiere Struktur von Tabelle parkhaus.parkinggarage
DROP TABLE IF EXISTS `parkinggarage`;
CREATE TABLE IF NOT EXISTS `parkinggarage` (
  `maxParkingSpaces` int(11) DEFAULT NULL COMMENT 'Maximale Anzahl von Parkplätzen im gesamten Parkhaus (In der Realität wäre dieser Wert durch die physische Größe bedingt)',
  `reservedSeasonParkingSpaces` int(11) DEFAULT NULL COMMENT 'Für Dauerparker reservierte Parkplätze',
  `parkingSpaceBuffer` int(11) DEFAULT NULL COMMENT 'Immer beizubehaltender Puffer freier Parkplätze'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci COMMENT='In dieser Tabelle werden grundlegende persistente Daten über Das Parkhaus gespeichert.';

-- Exportiere Daten aus Tabelle parkhaus.parkinggarage: ~0 rows (ungefähr)
REPLACE INTO `parkinggarage` (`maxParkingSpaces`, `reservedSeasonParkingSpaces`, `parkingSpaceBuffer`) VALUES
	(180, 40, 4);

-- Exportiere Struktur von Tabelle parkhaus.parkingspots
DROP TABLE IF EXISTS `parkingspots`;
CREATE TABLE IF NOT EXISTS `parkingspots` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Datensatz-Id (Primärschlüssel)',
  `parkerId` int(11) DEFAULT NULL COMMENT 'Id des sich auf dem Parkplatz befindenden Parkers (Fremdschlüssel)',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci COMMENT='In dieser Tabelle wird die Belegung des Parkhauses gespeichert, um jeden Parker einem konkreten (in der Realität physischen) Parkplatz innerhalb des Parkhauses zuordnen zu können.';

-- Exportiere Daten aus Tabelle parkhaus.parkingspots: ~0 rows (ungefähr)

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
