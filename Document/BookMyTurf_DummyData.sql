CREATE DATABASE IF NOT EXISTS bookmyturf_db;
USE bookmyturf_db;
-- MySQL dump with DUMMY DATA for BookMyTurf Database
-- Generated: 2026-01-22
-- Author: Database Team

SET FOREIGN_KEY_CHECKS=0;
SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";

-- --------------------------------------------------------
-- Database: bookmyturf_db
-- --------------------------------------------------------

DROP DATABASE IF EXISTS `bookmyturf_db`;
CREATE DATABASE `bookmyturf_db` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;
USE `bookmyturf_db`;

-- --------------------------------------------------------
-- Table structure for table `user_type`
-- --------------------------------------------------------

DROP TABLE IF EXISTS `user_type`;
CREATE TABLE `user_type` (
  `UserTypeID` int NOT NULL AUTO_INCREMENT,
  `TypeName` varchar(20) NOT NULL,
  `Description` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`UserTypeID`),
  UNIQUE KEY `TypeName` (`TypeName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table `user_type`
INSERT INTO `user_type` (`UserTypeID`, `TypeName`, `Description`) VALUES
(1, 'Player', 'Regular users who book turfs for playing'),
(2, 'Turf Owner', 'Owners who manage and rent out their turf facilities'),
(3, 'Administrator', 'System administrators with full access');

-- --------------------------------------------------------
-- Table structure for table `user`
-- --------------------------------------------------------

DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `UserID` int NOT NULL AUTO_INCREMENT,
  `UserTypeID` int NOT NULL,
  `FirstName` varchar(50) NOT NULL,
  `LastName` varchar(50) NOT NULL,
  `EmailAddress` varchar(100) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `PermanentAddress` varchar(200) NOT NULL,
  `CityName` varchar(50) NOT NULL,
  `ContactPhoneNo` varchar(15) NOT NULL,
  `AccountStatus` enum('Active','Suspended') NOT NULL DEFAULT 'Active',
  `CreatedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`UserID`),
  UNIQUE KEY `EmailAddress` (`EmailAddress`),
  KEY `UserTypeID` (`UserTypeID`),
  CONSTRAINT `user_ibfk_1` FOREIGN KEY (`UserTypeID`) REFERENCES `user_type` (`UserTypeID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table `user` (Password: Welcome@123 - encrypted)
INSERT INTO `user` (`UserID`, `UserTypeID`, `FirstName`, `LastName`, `EmailAddress`, `Password`, `PermanentAddress`, `CityName`, `ContactPhoneNo`, `AccountStatus`, `CreatedDate`) VALUES
-- ADMINISTRATORS (3)
(1, 3, 'Rajesh', 'Kumar', 'admin@bookmyturf.com', '$2a$10$N9qo8uLOickgx2ZBn5fYC.q.OKGJrCBM5nQz5lDrQwGP2W7eP3bYy', 'Admin Office, MG Road', 'Mumbai', '9876543210', 'Active', '2025-12-01 10:00:00'),
(2, 3, 'Priya', 'Sharma', 'priya.admin@bookmyturf.com', '$2a$10$N9qo8uLOickgx2ZBn5fYC.q.OKGJrCBM5nQz5lDrQwGP2W7eP3bYy', 'Sector 15, Vashi', 'Mumbai', '9876543211', 'Active', '2025-12-05 11:30:00'),

-- TURF OWNERS (5)
(3, 2, 'Amit', 'Patel', 'amit.patel@gmail.com', '$2a$10$N9qo8uLOickgx2ZBn5fYC.q.OKGJrCBM5nQz5lDrQwGP2W7eP3bYy', 'Andheri West, Mumbai', 'Mumbai', '9123456780', 'Active', '2025-12-10 09:00:00'),
(4, 2, 'Suresh', 'Deshmukh', 'suresh.deshmukh@yahoo.com', '$2a$10$N9qo8uLOickgx2ZBn5fYC.q.OKGJrCBM5nQz5lDrQwGP2W7eP3bYy', 'Kothrud, Pune', 'Pune', '9123456781', 'Active', '2025-12-12 14:20:00'),
(5, 2, 'Vikram', 'Singh', 'vikram.turf@gmail.com', '$2a$10$N9qo8uLOickgx2ZBn5fYC.q.OKGJrCBM5nQz5lDrQwGP2W7eP3bYy', 'Civil Lines, Nagpur', 'Nagpur', '9123456782', 'Active', '2025-12-15 16:45:00'),
(6, 2, 'Deepak', 'Joshi', 'deepak.sports@gmail.com', '$2a$10$N9qo8uLOickgx2ZBn5fYC.q.OKGJrCBM5nQz5lDrQwGP2W7eP3bYy', 'Bandra East, Mumbai', 'Mumbai', '9123456783', 'Active', '2025-12-18 10:15:00'),
(7, 2, 'Ramesh', 'Kulkarni', 'ramesh.kulkarni@outlook.com', '$2a$10$N9qo8uLOickgx2ZBn5fYC.q.OKGJrCBM5nQz5lDrQwGP2W7eP3bYy', 'Hinjewadi, Pune', 'Pune', '9123456784', 'Active', '2025-12-20 13:00:00'),

-- PLAYERS (15)
(8, 1, 'Rohit', 'Verma', 'rohit.verma@gmail.com', '$2a$10$N9qo8uLOickgx2ZBn5fYC.q.OKGJrCBM5nQz5lDrQwGP2W7eP3bYy', 'Malad West, Mumbai', 'Mumbai', '9988776655', 'Active', '2026-01-02 08:30:00'),
(9, 1, 'Sneha', 'Rao', 'sneha.rao@gmail.com', '$2a$10$N9qo8uLOickgx2ZBn5fYC.q.OKGJrCBM5nQz5lDrQwGP2W7eP3bYy', 'Koregaon Park, Pune', 'Pune', '9988776656', 'Active', '2026-01-03 09:15:00'),
(10, 1, 'Arjun', 'Nair', 'arjun.nair@yahoo.com', '$2a$10$N9qo8uLOickgx2ZBn5fYC.q.OKGJrCBM5nQz5lDrQwGP2W7eP3bYy', 'Sitabuldi, Nagpur', 'Nagpur', '9988776657', 'Active', '2026-01-04 10:00:00'),
(11, 1, 'Pooja', 'Mehta', 'pooja.mehta@gmail.com', '$2a$10$N9qo8uLOickgx2ZBn5fYC.q.OKGJrCBM5nQz5lDrQwGP2W7eP3bYy', 'Powai, Mumbai', 'Mumbai', '9988776658', 'Active', '2026-01-05 11:20:00'),
(12, 1, 'Karthik', 'Reddy', 'karthik.reddy@gmail.com', '$2a$10$N9qo8uLOickgx2ZBn5fYC.q.OKGJrCBM5nQz5lDrQwGP2W7eP3bYy', 'Wakad, Pune', 'Pune', '9988776659', 'Active', '2026-01-06 12:45:00'),
(13, 1, 'Neha', 'Gupta', 'neha.gupta@outlook.com', '$2a$10$N9qo8uLOickgx2ZBn5fYC.q.OKGJrCBM5nQz5lDrQwGP2W7eP3bYy', 'Dharampeth, Nagpur', 'Nagpur', '9988776660', 'Active', '2026-01-07 14:10:00'),
(14, 1, 'Aditya', 'Shah', 'aditya.shah@gmail.com', '$2a$10$N9qo8uLOickgx2ZBn5fYC.q.OKGJrCBM5nQz5lDrQwGP2W7eP3bYy', 'Goregaon, Mumbai', 'Mumbai', '9988776661', 'Active', '2026-01-08 15:30:00'),
(15, 1, 'Riya', 'Iyer', 'riya.iyer@gmail.com', '$2a$10$N9qo8uLOickgx2ZBn5fYC.q.OKGJrCBM5nQz5lDrQwGP2W7eP3bYy', 'Viman Nagar, Pune', 'Pune', '9988776662', 'Active', '2026-01-09 16:00:00'),
(16, 1, 'Siddharth', 'Jain', 'sid.jain@yahoo.com', '$2a$10$N9qo8uLOickgx2ZBn5fYC.q.OKGJrCBM5nQz5lDrQwGP2W7eP3bYy', 'Sadar, Nagpur', 'Nagpur', '9988776663', 'Active', '2026-01-10 09:30:00'),
(17, 1, 'Ananya', 'Pillai', 'ananya.pillai@gmail.com', '$2a$10$N9qo8uLOickgx2ZBn5fYC.q.OKGJrCBM5nQz5lDrQwGP2W7eP3bYy', 'Juhu, Mumbai', 'Mumbai', '9988776664', 'Active', '2026-01-11 10:45:00'),
(18, 1, 'Varun', 'Chopra', 'varun.chopra@gmail.com', '$2a$10$N9qo8uLOickgx2ZBn5fYC.q.OKGJrCBM5nQz5lDrQwGP2W7eP3bYy', 'Aundh, Pune', 'Pune', '9988776665', 'Active', '2026-01-12 11:00:00'),
(19, 1, 'Divya', 'Menon', 'divya.menon@outlook.com', '$2a$10$N9qo8uLOickgx2ZBn5fYC.q.OKGJrCBM5nQz5lDrQwGP2W7eP3bYy', 'Ramdaspeth, Nagpur', 'Nagpur', '9988776666', 'Active', '2026-01-13 12:15:00'),
(20, 1, 'Rahul', 'Kapoor', 'rahul.kapoor@gmail.com', '$2a$10$N9qo8uLOickgx2ZBn5fYC.q.OKGJrCBM5nQz5lDrQwGP2W7eP3bYy', 'Borivali, Mumbai', 'Mumbai', '9988776667', 'Active', '2026-01-14 13:30:00'),
(21, 1, 'Priyanka', 'Shetty', 'priyanka.shetty@gmail.com', '$2a$10$N9qo8uLOickgx2ZBn5fYC.q.OKGJrCBM5nQz5lDrQwGP2W7eP3bYy', 'Kalyani Nagar, Pune', 'Pune', '9988776668', 'Active', '2026-01-15 14:45:00'),
(22, 1, 'Kunal', 'Thakur', 'kunal.thakur@yahoo.com', '$2a$10$N9qo8uLOickgx2ZBn5fYC.q.OKGJrCBM5nQz5lDrQwGP2W7eP3bYy', 'Wardhaman Nagar, Nagpur', 'Nagpur', '9988776669', 'Active', '2026-01-16 15:00:00');

-- --------------------------------------------------------
-- Table structure for table `admin`
-- --------------------------------------------------------

DROP TABLE IF EXISTS `admin`;
CREATE TABLE `admin` (
  `AdminID` int NOT NULL AUTO_INCREMENT,
  `UserID` int NOT NULL,
  `UserName` varchar(50) NOT NULL,
  PRIMARY KEY (`AdminID`),
  UNIQUE KEY `UserName` (`UserName`),
  KEY `UserID` (`UserID`),
  CONSTRAINT `admin_ibfk_1` FOREIGN KEY (`UserID`) REFERENCES `user` (`UserID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table `admin`
INSERT INTO `admin` (`AdminID`, `UserID`, `UserName`) VALUES
(1, 1, 'admin_rajesh'),
(2, 2, 'admin_priya');

-- --------------------------------------------------------
-- Table structure for table `amenities`
-- --------------------------------------------------------

DROP TABLE IF EXISTS `amenities`;
CREATE TABLE `amenities` (
  `AmenityID` int NOT NULL AUTO_INCREMENT,
  `AmenityName` varchar(50) NOT NULL,
  `Description` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`AmenityID`),
  UNIQUE KEY `AmenityName` (`AmenityName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table `amenities`
INSERT INTO `amenities` (`AmenityID`, `AmenityName`, `Description`) VALUES
(1, 'Parking', 'Free parking facility for vehicles'),
(2, 'Changing Rooms', 'Separate changing rooms for players'),
(3, 'Shower Facilities', 'Clean shower facilities with hot water'),
(4, 'Floodlights', 'High-quality floodlights for night matches'),
(5, 'Refreshments', 'Cafeteria/canteen with snacks and beverages'),
(6, 'First Aid', 'First aid kit and trained personnel available'),
(7, 'Seating Area', 'Spectator seating arrangements'),
(8, 'Equipment Rental', 'Sports equipment available on rent'),
(9, 'Washrooms', 'Clean washroom facilities'),
(10, 'Water Cooler', 'Drinking water facility'),
(11, 'CCTV Surveillance', 'Security cameras for safety'),
(12, 'Wifi', 'Free WiFi connectivity');

-- --------------------------------------------------------
-- Table structure for table `turf`
-- --------------------------------------------------------

DROP TABLE IF EXISTS `turf`;
CREATE TABLE `turf` (
  `TurfID` int NOT NULL AUTO_INCREMENT,
  `TurfOwnerID` int NOT NULL,
  `TurfName` varchar(100) NOT NULL,
  `Location` varchar(200) NOT NULL,
  `City` varchar(50) NOT NULL,
  `Description` text,
  `TurfStatus` enum('Active','Inactive') NOT NULL DEFAULT 'Active',
  `CreatedDate` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`TurfID`),
  KEY `TurfOwnerID` (`TurfOwnerID`),
  CONSTRAINT `turf_ibfk_1` FOREIGN KEY (`TurfOwnerID`) REFERENCES `user` (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table `turf`
INSERT INTO `turf` (`TurfID`, `TurfOwnerID`, `TurfName`, `Location`, `City`, `Description`, `TurfStatus`, `CreatedDate`) VALUES
(1, 3, 'Champions Sports Arena', 'Andheri West, Near Metro Station', 'Mumbai', 'Premium quality artificial turf with floodlights. Perfect for football and cricket. Well-maintained facility with parking and changing rooms.', 'Active', '2025-12-11 10:00:00'),
(2, 3, 'Victory Ground', 'Malad Link Road, Opposite Mall', 'Mumbai', 'Spacious turf suitable for corporate tournaments. Features include seating area, refreshments, and equipment rental.', 'Active', '2025-12-11 11:30:00'),
(3, 4, 'Pune Sports Hub', 'Kothrud, Near University', 'Pune', 'Modern turf facility with FIFA standard artificial grass. Ideal for professional training and matches.', 'Active', '2025-12-13 09:00:00'),
(4, 4, 'Dream Eleven Arena', 'Hinjewadi Phase 2, IT Park Road', 'Pune', 'Corporate-friendly turf with excellent amenities. Popular among IT professionals for evening matches.', 'Active', '2025-12-13 14:00:00'),
(5, 5, 'Orange City Sports', 'Civil Lines, Main Road', 'Nagpur', 'Well-lit turf with quality grass. Family-friendly environment with spectator seating.', 'Active', '2025-12-16 10:30:00'),
(6, 5, 'Tiger Turf Arena', 'Dharampeth, Near Garden', 'Nagpur', 'Compact turf perfect for 5-a-side and 7-a-side football. Clean facilities and friendly staff.', 'Active', '2025-12-16 15:00:00'),
(7, 6, 'Kings XI Ground', 'Bandra East, Reclamation Area', 'Mumbai', 'Premium turf with ocean view. High-end facility popular for celebrity matches and events.', 'Active', '2025-12-19 11:00:00'),
(8, 7, 'Tech Park Sports Arena', 'Hinjewadi Phase 1, Near Infosys', 'Pune', 'Conveniently located for tech professionals. Open 24/7 with excellent lighting.', 'Active', '2025-12-21 10:00:00');

-- --------------------------------------------------------
-- Table structure for table `turf_amenities`
-- --------------------------------------------------------

DROP TABLE IF EXISTS `turf_amenities`;
CREATE TABLE `turf_amenities` (
  `TurfID` int NOT NULL,
  `AmenityID` int NOT NULL,
  PRIMARY KEY (`TurfID`,`AmenityID`),
  KEY `AmenityID` (`AmenityID`),
  CONSTRAINT `turf_amenities_ibfk_1` FOREIGN KEY (`TurfID`) REFERENCES `turf` (`TurfID`) ON DELETE CASCADE,
  CONSTRAINT `turf_amenities_ibfk_2` FOREIGN KEY (`AmenityID`) REFERENCES `amenities` (`AmenityID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table `turf_amenities`
INSERT INTO `turf_amenities` (`TurfID`, `AmenityID`) VALUES
-- Champions Sports Arena (Mumbai) - Premium facility
(1, 1), (1, 2), (1, 3), (1, 4), (1, 5), (1, 6), (1, 7), (1, 9), (1, 10), (1, 11),
-- Victory Ground (Mumbai) - Good facility
(2, 1), (2, 2), (2, 4), (2, 5), (2, 7), (2, 8), (2, 9), (2, 10),
-- Pune Sports Hub - Professional
(3, 1), (3, 2), (3, 3), (3, 4), (3, 6), (3, 9), (3, 10), (3, 11), (3, 12),
-- Dream Eleven Arena (Pune) - Corporate
(4, 1), (4, 2), (4, 4), (4, 5), (4, 7), (4, 9), (4, 10), (4, 12),
-- Orange City Sports (Nagpur)
(5, 1), (5, 2), (5, 4), (5, 7), (5, 9), (5, 10),
-- Tiger Turf Arena (Nagpur)
(6, 1), (6, 2), (6, 4), (6, 9), (6, 10),
-- Kings XI Ground (Mumbai) - Premium
(7, 1), (7, 2), (7, 3), (7, 4), (7, 5), (7, 6), (7, 7), (7, 8), (7, 9), (7, 10), (7, 11), (7, 12),
-- Tech Park Sports Arena (Pune)
(8, 1), (8, 2), (8, 3), (8, 4), (8, 5), (8, 9), (8, 10), (8, 12);

-- --------------------------------------------------------
-- Table structure for table `turf_photos`
-- --------------------------------------------------------

DROP TABLE IF EXISTS `turf_photos`;
CREATE TABLE `turf_photos` (
  `PhotoID` int NOT NULL AUTO_INCREMENT,
  `TurfID` int NOT NULL,
  `PhotoURL` varchar(500) NOT NULL,
  `IsMain` tinyint(1) DEFAULT '0',
  `Caption` varchar(100) DEFAULT NULL,
  `PhotoType` varchar(50) DEFAULT NULL,
  `UploadDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`PhotoID`),
  KEY `TurfID` (`TurfID`),
  CONSTRAINT `turf_photos_ibfk_1` FOREIGN KEY (`TurfID`) REFERENCES `turf` (`TurfID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table `turf_photos`
INSERT INTO `turf_photos` (`PhotoID`, `TurfID`, `PhotoURL`, `IsMain`, `Caption`, `PhotoType`, `UploadDate`) VALUES
-- Champions Sports Arena photos
(1, 1, 'https://storage.bookmyturf.com/turfs/champions/main-field.jpg', 1, 'Main Playing Field', 'Field', '2025-12-11 10:30:00'),
(2, 1, 'https://storage.bookmyturf.com/turfs/champions/night-view.jpg', 0, 'Night Match View', 'Field', '2025-12-11 10:35:00'),
(3, 1, 'https://storage.bookmyturf.com/turfs/champions/changing-room.jpg', 0, 'Changing Rooms', 'Facilities', '2025-12-11 10:40:00'),
-- Victory Ground photos
(4, 2, 'https://storage.bookmyturf.com/turfs/victory/main.jpg', 1, 'Victory Ground Overview', 'Exterior', '2025-12-11 12:00:00'),
(5, 2, 'https://storage.bookmyturf.com/turfs/victory/field.jpg', 0, 'Playing Area', 'Field', '2025-12-11 12:10:00'),
-- Pune Sports Hub photos
(6, 3, 'https://storage.bookmyturf.com/turfs/pune-hub/main.jpg', 1, 'Professional Turf', 'Field', '2025-12-13 09:30:00'),
(7, 3, 'https://storage.bookmyturf.com/turfs/pune-hub/facilities.jpg', 0, 'Premium Facilities', 'Facilities', '2025-12-13 09:45:00'),
-- Dream Eleven Arena photos
(8, 4, 'https://storage.bookmyturf.com/turfs/dream11/main.jpg', 1, 'Arena View', 'Field', '2025-12-13 14:30:00'),
(9, 4, 'https://storage.bookmyturf.com/turfs/dream11/corporate.jpg', 0, 'Corporate Events Area', 'Facilities', '2025-12-13 14:45:00'),
-- Orange City Sports photos
(10, 5, 'https://storage.bookmyturf.com/turfs/orange/main.jpg', 1, 'Orange City Main Field', 'Field', '2025-12-16 11:00:00'),
(11, 5, 'https://storage.bookmyturf.com/turfs/orange/seating.jpg', 0, 'Spectator Seating', 'Facilities', '2025-12-16 11:15:00'),
-- Tiger Turf Arena photos
(12, 6, 'https://storage.bookmyturf.com/turfs/tiger/main.jpg', 1, 'Compact Turf', 'Field', '2025-12-16 15:30:00'),
-- Kings XI Ground photos
(13, 7, 'https://storage.bookmyturf.com/turfs/kings/main.jpg', 1, 'Premium Ground with Ocean View', 'Exterior', '2025-12-19 11:30:00'),
(14, 7, 'https://storage.bookmyturf.com/turfs/kings/field.jpg', 0, 'FIFA Quality Turf', 'Field', '2025-12-19 11:45:00'),
(15, 7, 'https://storage.bookmyturf.com/turfs/kings/vip.jpg', 0, 'VIP Area', 'Facilities', '2025-12-19 12:00:00'),
-- Tech Park Sports Arena photos
(16, 8, 'https://storage.bookmyturf.com/turfs/techpark/main.jpg', 1, 'Tech Park Arena', 'Field', '2025-12-21 10:30:00'),
(17, 8, 'https://storage.bookmyturf.com/turfs/techpark/night.jpg', 0, '24/7 Facility', 'Field', '2025-12-21 10:45:00');

-- --------------------------------------------------------
-- Table structure for table `slot_master`
-- --------------------------------------------------------

DROP TABLE IF EXISTS `slot_master`;
CREATE TABLE `slot_master` (
  `SlotID` int NOT NULL AUTO_INCREMENT,
  `TurfID` int NOT NULL,
  `SlotDate` date NOT NULL,
  `StartTime` time NOT NULL,
  `EndTime` time NOT NULL,
  `SlotPrice` decimal(10,2) NOT NULL,
  `IsAvailable` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`SlotID`),
  KEY `TurfID` (`TurfID`),
  CONSTRAINT `slot_master_ibfk_1` FOREIGN KEY (`TurfID`) REFERENCES `turf` (`TurfID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table `slot_master` (Multiple dates and times for each turf)
INSERT INTO `slot_master` (`SlotID`, `TurfID`, `SlotDate`, `StartTime`, `EndTime`, `SlotPrice`, `IsAvailable`) VALUES
-- Champions Sports Arena (Turf 1) - Premium pricing
(1, 1, '2026-01-25', '06:00:00', '08:00:00', 800.00, 1),
(2, 1, '2026-01-25', '08:00:00', '10:00:00', 800.00, 1),
(3, 1, '2026-01-25', '10:00:00', '12:00:00', 900.00, 1),
(4, 1, '2026-01-25', '18:00:00', '20:00:00', 1200.00, 0), -- Booked
(5, 1, '2026-01-25', '20:00:00', '22:00:00', 1200.00, 1),
(6, 1, '2026-01-26', '06:00:00', '08:00:00', 800.00, 1),
(7, 1, '2026-01-26', '18:00:00', '20:00:00', 1200.00, 0), -- Booked
(8, 1, '2026-01-27', '18:00:00', '20:00:00', 1200.00, 1),

-- Victory Ground (Turf 2)
(9, 2, '2026-01-25', '06:00:00', '08:00:00', 600.00, 1),
(10, 2, '2026-01-25', '16:00:00', '18:00:00', 800.00, 0), -- Booked
(11, 2, '2026-01-25', '18:00:00', '20:00:00', 1000.00, 1),
(12, 2, '2026-01-26', '18:00:00', '20:00:00', 1000.00, 1),

-- Pune Sports Hub (Turf 3)
(13, 3, '2026-01-25', '06:00:00', '08:00:00', 700.00, 1),
(14, 3, '2026-01-25', '08:00:00', '10:00:00', 700.00, 1),
(15, 3, '2026-01-25', '18:00:00', '20:00:00', 1100.00, 0), -- Booked
(16, 3, '2026-01-26', '18:00:00', '20:00:00', 1100.00, 1),
(17, 3, '2026-01-27', '06:00:00', '08:00:00', 700.00, 1),

-- Dream Eleven Arena (Turf 4)
(18, 4, '2026-01-25', '18:00:00', '20:00:00', 1000.00, 0), -- Booked
(19, 4, '2026-01-25', '20:00:00', '22:00:00', 1000.00, 1),
(20, 4, '2026-01-26', '18:00:00', '20:00:00', 1000.00, 1),

-- Orange City Sports (Turf 5)
(21, 5, '2026-01-25', '06:00:00', '08:00:00', 500.00, 1),
(22, 5, '2026-01-25', '18:00:00', '20:00:00', 800.00, 0), -- Booked
(23, 5, '2026-01-26', '18:00:00', '20:00:00', 800.00, 1),

-- Tiger Turf Arena (Turf 6)
(24, 6, '2026-01-25', '18:00:00', '20:00:00', 700.00, 1),
(25, 6, '2026-01-26', '18:00:00', '20:00:00', 700.00, 0), -- Booked

-- Kings XI Ground (Turf 7) - Premium
(26, 7, '2026-01-25', '18:00:00', '20:00:00', 1500.00, 0), -- Booked
(27, 7, '2026-01-25', '20:00:00', '22:00:00', 1500.00, 1),
(28, 7, '2026-01-26', '18:00:00', '20:00:00', 1500.00, 1),

-- Tech Park Sports Arena (Turf 8)
(29, 8, '2026-01-25', '06:00:00', '08:00:00', 750.00, 1),
(30, 8, '2026-01-25', '18:00:00', '20:00:00', 1000.00, 0), -- Booked
(31, 8, '2026-01-26', '18:00:00', '20:00:00', 1000.00, 1),
(32, 8, '2026-01-27', '18:00:00', '20:00:00', 1000.00, 1);

-- --------------------------------------------------------
-- Table structure for table `booking`
-- --------------------------------------------------------

DROP TABLE IF EXISTS `booking`;
CREATE TABLE `booking` (
  `BookingID` int NOT NULL AUTO_INCREMENT,
  `UserID` int NOT NULL,
  `SlotID` int NOT NULL,
  `BookingDate` datetime DEFAULT CURRENT_TIMESTAMP,
  `TotalAmount` decimal(10,2) NOT NULL,
  `BookingStatus` enum('Confirmed','Cancelled','Completed') DEFAULT 'Confirmed',
  `PaymentStatus` enum('Pending','Success','Refunded') DEFAULT 'Pending',
  PRIMARY KEY (`BookingID`),
  KEY `UserID` (`UserID`),
  KEY `SlotID` (`SlotID`),
  CONSTRAINT `booking_ibfk_1` FOREIGN KEY (`UserID`) REFERENCES `user` (`UserID`),
  CONSTRAINT `booking_ibfk_2` FOREIGN KEY (`SlotID`) REFERENCES `slot_master` (`SlotID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table `booking`
INSERT INTO `booking` (`BookingID`, `UserID`, `SlotID`, `BookingDate`, `TotalAmount`, `BookingStatus`, `PaymentStatus`) VALUES
-- Confirmed bookings with successful payment
(1, 8, 4, '2026-01-20 10:30:00', 1200.00, 'Confirmed', 'Success'),
(2, 9, 15, '2026-01-20 11:45:00', 1100.00, 'Confirmed', 'Success'),
(3, 11, 10, '2026-01-21 09:15:00', 800.00, 'Confirmed', 'Success'),
(4, 12, 18, '2026-01-21 14:30:00', 1000.00, 'Confirmed', 'Success'),
(5, 13, 22, '2026-01-21 16:00:00', 800.00, 'Confirmed', 'Success'),
(6, 14, 26, '2026-01-22 10:00:00', 1500.00, 'Confirmed', 'Success'),
(7, 16, 30, '2026-01-22 11:30:00', 1000.00, 'Confirmed', 'Success'),
(8, 17, 7, '2026-01-22 13:00:00', 1200.00, 'Confirmed', 'Success'),

-- Completed bookings
(9, 10, 25, '2026-01-15 08:00:00', 700.00, 'Completed', 'Success'),
(10, 15, 4, '2026-01-16 09:30:00', 1200.00, 'Completed', 'Success'),

-- Cancelled bookings with refunds
(11, 18, 10, '2026-01-18 10:00:00', 800.00, 'Cancelled', 'Refunded'),
(12, 19, 22, '2026-01-19 11:00:00', 800.00, 'Cancelled', 'Refunded');

-- --------------------------------------------------------
-- Table structure for table `payment`
-- --------------------------------------------------------

DROP TABLE IF EXISTS `payment`;
CREATE TABLE `payment` (
  `PaymentID` int NOT NULL AUTO_INCREMENT,
  `BookingID` int NOT NULL,
  `UserID` int NOT NULL,
  `PaymentAmount` decimal(10,2) NOT NULL,
  `PaymentMethod` varchar(50) NOT NULL,
  `BankDetails` varchar(200) DEFAULT NULL,
  `PaymentStatus` enum('Success','Failed','Pending') NOT NULL,
  `TransactionID` varchar(100) NOT NULL,
  `TransactionDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `PaymentDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`PaymentID`),
  UNIQUE KEY `TransactionID` (`TransactionID`),
  KEY `BookingID` (`BookingID`),
  KEY `UserID` (`UserID`),
  CONSTRAINT `payment_ibfk_1` FOREIGN KEY (`BookingID`) REFERENCES `booking` (`BookingID`),
  CONSTRAINT `payment_ibfk_2` FOREIGN KEY (`UserID`) REFERENCES `user` (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table `payment`
INSERT INTO `payment` (`PaymentID`, `BookingID`, `UserID`, `PaymentAmount`, `PaymentMethod`, `BankDetails`, `PaymentStatus`, `TransactionID`, `TransactionDate`, `PaymentDate`) VALUES
(1, 1, 8, 1200.00, 'UPI', 'Google Pay - 9988776655@paytm', 'Success', 'TXN20260120103045BOOK001', '2026-01-20 10:30:45', '2026-01-20 10:30:45'),
(2, 2, 9, 1100.00, 'Credit Card', 'XXXX-XXXX-XXXX-4567', 'Success', 'TXN20260120114520BOOK002', '2026-01-20 11:45:20', '2026-01-20 11:45:20'),
(3, 3, 11, 800.00, 'UPI', 'PhonePe - 9988776658@ybl', 'Success', 'TXN20260121091530BOOK003', '2026-01-21 09:15:30', '2026-01-21 09:15:30'),
(4, 4, 12, 1000.00, 'Debit Card', 'XXXX-XXXX-XXXX-7890', 'Success', 'TXN20260121143045BOOK004', '2026-01-21 14:30:45', '2026-01-21 14:30:45'),
(5, 5, 13, 800.00, 'Net Banking', 'HDFC Bank - Acc XXXX5678', 'Success', 'TXN20260121160015BOOK005', '2026-01-21 16:00:15', '2026-01-21 16:00:15'),
(6, 6, 14, 1500.00, 'UPI', 'Paytm - 9988776661@paytm', 'Success', 'TXN20260122100030BOOK006', '2026-01-22 10:00:30', '2026-01-22 10:00:30'),
(7, 7, 16, 1000.00, 'UPI', 'Google Pay - 9988776663@oksbi', 'Success', 'TXN20260122113045BOOK007', '2026-01-22 11:30:45', '2026-01-22 11:30:45'),
(8, 8, 17, 1200.00, 'Credit Card', 'XXXX-XXXX-XXXX-1234', 'Success', 'TXN20260122130020BOOK008', '2026-01-22 13:00:20', '2026-01-22 13:00:20'),
(9, 9, 10, 700.00, 'UPI', 'PhonePe - 9988776657@ybl', 'Success', 'TXN20260115080030BOOK009', '2026-01-15 08:00:30', '2026-01-15 08:00:30'),
(10, 10, 15, 1200.00, 'Debit Card', 'XXXX-XXXX-XXXX-9012', 'Success', 'TXN20260116093045BOOK010', '2026-01-16 09:30:45', '2026-01-16 09:30:45'),
(11, 11, 18, 800.00, 'UPI', 'Google Pay - 9988776665@paytm', 'Success', 'TXN20260118100015BOOK011', '2026-01-18 10:00:15', '2026-01-18 10:00:15'),
(12, 12, 19, 800.00, 'Credit Card', 'XXXX-XXXX-XXXX-3456', 'Success', 'TXN20260119110030BOOK012', '2026-01-19 11:00:30', '2026-01-19 11:00:30');

-- --------------------------------------------------------
-- Table structure for table `refund`
-- --------------------------------------------------------

DROP TABLE IF EXISTS `refund`;
CREATE TABLE `refund` (
  `RefundID` int NOT NULL AUTO_INCREMENT,
  `BookingID` int NOT NULL,
  `UserID` int NOT NULL,
  `TurfOwnerID` int NOT NULL,
  `ApprovedBy` int NOT NULL,
  `RefundAmount` decimal(10,2) NOT NULL,
  `RefundStatus` enum('Pending','Approved','Completed') DEFAULT 'Pending',
  `RequestDate` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`RefundID`),
  KEY `BookingID` (`BookingID`),
  KEY `UserID` (`UserID`),
  KEY `TurfOwnerID` (`TurfOwnerID`),
  KEY `ApprovedBy` (`ApprovedBy`),
  CONSTRAINT `refund_ibfk_1` FOREIGN KEY (`BookingID`) REFERENCES `booking` (`BookingID`),
  CONSTRAINT `refund_ibfk_2` FOREIGN KEY (`UserID`) REFERENCES `user` (`UserID`),
  CONSTRAINT `refund_ibfk_3` FOREIGN KEY (`TurfOwnerID`) REFERENCES `user` (`UserID`),
  CONSTRAINT `refund_ibfk_4` FOREIGN KEY (`ApprovedBy`) REFERENCES `user` (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table `refund`
INSERT INTO `refund` (`RefundID`, `BookingID`, `UserID`, `TurfOwnerID`, `ApprovedBy`, `RefundAmount`, `RefundStatus`, `RequestDate`) VALUES
(1, 11, 18, 3, 1, 800.00, 'Completed', '2026-01-18 15:00:00'), -- Full refund (>24 hours)
(2, 12, 19, 5, 1, 400.00, 'Approved', '2026-01-19 16:00:00'); -- 50% refund (2-24 hours)

-- --------------------------------------------------------
-- Table structure for table `notification`
-- --------------------------------------------------------

DROP TABLE IF EXISTS `notification`;
CREATE TABLE `notification` (
  `NotificationID` int NOT NULL AUTO_INCREMENT,
  `UserID` int NOT NULL,
  `Type` varchar(50) NOT NULL,
  `Message` text NOT NULL,
  `ReadStatus` enum('Unread','Read') DEFAULT 'Unread',
  `SentDate` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`NotificationID`),
  KEY `UserID` (`UserID`),
  CONSTRAINT `notification_ibfk_1` FOREIGN KEY (`UserID`) REFERENCES `user` (`UserID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table `notification`
INSERT INTO `notification` (`NotificationID`, `UserID`, `Type`, `Message`, `ReadStatus`, `SentDate`) VALUES
-- Booking confirmations
(1, 8, 'Booking Confirmation', 'Your booking at Champions Sports Arena for 25 Jan 2026, 6:00 PM - 8:00 PM has been confirmed. Booking ID: BOOK001', 'Read', '2026-01-20 10:30:50'),
(2, 9, 'Booking Confirmation', 'Your booking at Pune Sports Hub for 25 Jan 2026, 6:00 PM - 8:00 PM has been confirmed. Booking ID: BOOK002', 'Read', '2026-01-20 11:45:25'),
(3, 11, 'Booking Confirmation', 'Your booking at Victory Ground for 25 Jan 2026, 4:00 PM - 6:00 PM has been confirmed. Booking ID: BOOK003', 'Read', '2026-01-21 09:15:35'),

-- Payment confirmations
(4, 8, 'Payment Success', 'Payment of ₹1200.00 received successfully. Transaction ID: TXN20260120103045BOOK001', 'Read', '2026-01-20 10:31:00'),
(5, 9, 'Payment Success', 'Payment of ₹1100.00 received successfully. Transaction ID: TXN20260120114520BOOK002', 'Read', '2026-01-20 11:45:30'),

-- Booking reminders (24 hours)
(6, 8, '24 Hour Reminder', 'Reminder: Your booking at Champions Sports Arena is tomorrow at 6:00 PM. Location: Andheri West, Near Metro Station, Mumbai', 'Unread', '2026-01-24 18:00:00'),
(7, 9, '24 Hour Reminder', 'Reminder: Your booking at Pune Sports Hub is tomorrow at 6:00 PM. Location: Kothrud, Near University, Pune', 'Unread', '2026-01-24 18:00:00'),

-- Booking reminders (2 hours)
(8, 8, '2 Hour Reminder', 'Final Reminder: Your booking at Champions Sports Arena starts in 2 hours at 6:00 PM. Contact: 9123456780', 'Unread', '2026-01-25 16:00:00'),

-- Cancellation notifications
(9, 18, 'Booking Cancelled', 'Your booking at Victory Ground has been cancelled. Refund of ₹800.00 will be processed within 5-7 business days.', 'Read', '2026-01-18 15:00:10'),
(10, 19, 'Booking Cancelled', 'Your booking at Orange City Sports has been cancelled. Refund of ₹400.00 (50% as per policy) will be processed.', 'Read', '2026-01-19 16:00:10'),

-- Refund notifications
(11, 18, 'Refund Approved', 'Your refund request of ₹800.00 has been approved by admin. Amount will be credited within 5-7 business days.', 'Read', '2026-01-18 16:30:00'),
(12, 18, 'Refund Completed', 'Refund of ₹800.00 has been processed successfully to your original payment method.', 'Unread', '2026-01-20 14:00:00'),

-- Welcome messages
(13, 20, 'Welcome', 'Welcome to BookMyTurf! Start exploring and booking amazing turfs in your city.', 'Unread', '2026-01-14 13:30:15'),
(14, 21, 'Welcome', 'Welcome to BookMyTurf! Start exploring and booking amazing turfs in your city.', 'Unread', '2026-01-15 14:45:15');

-- --------------------------------------------------------
-- Table structure for table `report`
-- --------------------------------------------------------

DROP TABLE IF EXISTS `report`;
CREATE TABLE `report` (
  `ReportID` int NOT NULL AUTO_INCREMENT,
  `AdminID` int NOT NULL,
  `ReportType` varchar(50) NOT NULL,
  `ReportDate` date NOT NULL,
  `TotalBookings` int NOT NULL,
  `SuccessfulBookings` int NOT NULL,
  `CancelledBookings` int NOT NULL,
  `TotalRevenue` decimal(10,2) NOT NULL,
  `TotalUsers` int NOT NULL,
  `TotalTurfs` int NOT NULL,
  `ReportContent` text,
  `GeneratedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`ReportID`),
  KEY `AdminID` (`AdminID`),
  CONSTRAINT `report_ibfk_1` FOREIGN KEY (`AdminID`) REFERENCES `admin` (`AdminID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table `report`
INSERT INTO `report` (`ReportID`, `AdminID`, `ReportType`, `ReportDate`, `TotalBookings`, `SuccessfulBookings`, `CancelledBookings`, `TotalRevenue`, `TotalUsers`, `TotalTurfs`, `ReportContent`, `GeneratedDate`) VALUES
(1, 1, 'Daily Booking Report', '2026-01-20', 2, 2, 0, 2300.00, 22, 8, 'Daily report showing 2 bookings with 100% success rate. Peak booking time: 10:00 AM - 12:00 PM. Most popular turf: Champions Sports Arena.', '2026-01-20 23:59:00'),
(2, 1, 'Daily Booking Report', '2026-01-21', 3, 3, 0, 2800.00, 22, 8, 'Daily report showing 3 bookings. Revenue increased by 21.7% from previous day. Popular cities: Mumbai, Pune, Nagpur.', '2026-01-21 23:59:00'),
(3, 1, 'Weekly Revenue Report', '2026-01-18', 10, 8, 2, 9700.00, 22, 8, 'Weekly summary (Jan 12-18): Total bookings: 10, Success rate: 80%, Average booking value: ₹970. Top performing turf: Kings XI Ground.', '2026-01-18 18:00:00'),
(4, 2, 'Monthly Summary Report', '2026-01-01', 12, 10, 2, 11900.00, 22, 8, 'January 2026 monthly report: Total revenue ₹11,900. Total users registered: 22 (15 Players, 5 Turf Owners, 2 Admins). Active turfs: 8. Average occupancy rate: 45%.', '2026-01-22 09:00:00');

-- --------------------------------------------------------

SET FOREIGN_KEY_CHECKS=1;

-- END OF DATABASE DUMP WITH DUMMY DATA
-- Total Records Inserted:
-- user_type: 3 records
-- user: 22 records (2 admins, 5 turf owners, 15 players)
-- admin: 2 records
-- amenities: 12 records
-- turf: 8 records
-- turf_amenities: 60+ records
-- turf_photos: 17 records
-- slot_master: 32 records
-- booking: 12 records
-- payment: 12 records
-- refund: 2 records
-- notification: 14 records
-- report: 4 records
