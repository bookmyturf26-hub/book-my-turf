-- MySQL dump 10.13  Distrib 8.0.25, for Win64 (x86_64)
--
-- Host: localhost    Database: bookmyturf_db
-- ------------------------------------------------------
-- Server version	8.2.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `admin`
--

DROP TABLE IF EXISTS `admin`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `admin` (
  `AdminID` int NOT NULL AUTO_INCREMENT,
  `UserID` int NOT NULL,
  `UserName` varchar(50) NOT NULL,
  PRIMARY KEY (`AdminID`),
  UNIQUE KEY `UserName` (`UserName`),
  KEY `UserID` (`UserID`),
  CONSTRAINT `admin_ibfk_1` FOREIGN KEY (`UserID`) REFERENCES `user` (`UserID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `admin`
--

LOCK TABLES `admin` WRITE;
/*!40000 ALTER TABLE `admin` DISABLE KEYS */;
/*!40000 ALTER TABLE `admin` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `amenities`
--

DROP TABLE IF EXISTS `amenities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `amenities` (
  `AmenityID` int NOT NULL AUTO_INCREMENT,
  `AmenityName` varchar(50) NOT NULL,
  `Description` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`AmenityID`),
  UNIQUE KEY `AmenityName` (`AmenityName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `amenities`
--

LOCK TABLES `amenities` WRITE;
/*!40000 ALTER TABLE `amenities` DISABLE KEYS */;
/*!40000 ALTER TABLE `amenities` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `booking`
--

DROP TABLE IF EXISTS `booking`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `booking`
--

LOCK TABLES `booking` WRITE;
/*!40000 ALTER TABLE `booking` DISABLE KEYS */;
/*!40000 ALTER TABLE `booking` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `notification`
--

DROP TABLE IF EXISTS `notification`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `notification`
--

LOCK TABLES `notification` WRITE;
/*!40000 ALTER TABLE `notification` DISABLE KEYS */;
/*!40000 ALTER TABLE `notification` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payment`
--

DROP TABLE IF EXISTS `payment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `payment` (
  `PaymentID` int NOT NULL AUTO_INCREMENT,
  `BookingID` int NOT NULL,
  `UserID` int NOT NULL,
  `PaymentAmount` decimal(10,2) NOT NULL,
  `PaymentMethod` varchar(50) NOT NULL,
  `BankDetails` varchar(200) DEFAULT NULL,
  `PaymentStatus` enum('Success','Failed','Pending') NOT NULL,
  `TransactionID` varchar(100) NOT NULL,
  `PaymentDate` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`PaymentID`),
  UNIQUE KEY `TransactionID` (`TransactionID`),
  KEY `BookingID` (`BookingID`),
  KEY `UserID` (`UserID`),
  CONSTRAINT `payment_ibfk_1` FOREIGN KEY (`BookingID`) REFERENCES `booking` (`BookingID`),
  CONSTRAINT `payment_ibfk_2` FOREIGN KEY (`UserID`) REFERENCES `user` (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payment`
--

LOCK TABLES `payment` WRITE;
/*!40000 ALTER TABLE `payment` DISABLE KEYS */;
/*!40000 ALTER TABLE `payment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `refund`
--

DROP TABLE IF EXISTS `refund`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `refund`
--

LOCK TABLES `refund` WRITE;
/*!40000 ALTER TABLE `refund` DISABLE KEYS */;
/*!40000 ALTER TABLE `refund` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `report`
--

DROP TABLE IF EXISTS `report`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
  `GeneratedDate` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`ReportID`),
  KEY `AdminID` (`AdminID`),
  CONSTRAINT `report_ibfk_1` FOREIGN KEY (`AdminID`) REFERENCES `admin` (`AdminID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `report`
--

LOCK TABLES `report` WRITE;
/*!40000 ALTER TABLE `report` DISABLE KEYS */;
/*!40000 ALTER TABLE `report` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `slot_master`
--

DROP TABLE IF EXISTS `slot_master`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `slot_master`
--

LOCK TABLES `slot_master` WRITE;
/*!40000 ALTER TABLE `slot_master` DISABLE KEYS */;
/*!40000 ALTER TABLE `slot_master` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sports`
--

DROP TABLE IF EXISTS `sports`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sports` (
  `SportID` int NOT NULL AUTO_INCREMENT,
  `SportName` varchar(50) NOT NULL,
  `DefaultRules` text,
  `IsActive` tinyint(1) DEFAULT '1',
  `CreatedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`SportID`),
  UNIQUE KEY `SportName` (`SportName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sports`
--

LOCK TABLES `sports` WRITE;
/*!40000 ALTER TABLE `sports` DISABLE KEYS */;
/*!40000 ALTER TABLE `sports` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `turf`
--

DROP TABLE IF EXISTS `turf`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `turf` (
  `TurfID` int NOT NULL AUTO_INCREMENT,
  `TurfOwnerID` int NOT NULL,
  `TurfName` varchar(100) NOT NULL,
  `Location` varchar(200) NOT NULL,
  `City` varchar(50) NOT NULL,
  `Description` text,
  `TurfStatus` enum('Active','Inactive') DEFAULT 'Active',
  `CreatedDate` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`TurfID`),
  KEY `TurfOwnerID` (`TurfOwnerID`),
  CONSTRAINT `turf_ibfk_1` FOREIGN KEY (`TurfOwnerID`) REFERENCES `user` (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `turf`
--

LOCK TABLES `turf` WRITE;
/*!40000 ALTER TABLE `turf` DISABLE KEYS */;
/*!40000 ALTER TABLE `turf` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `turf_amenities`
--

DROP TABLE IF EXISTS `turf_amenities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `turf_amenities` (
  `TurfID` int NOT NULL,
  `AmenityID` int NOT NULL,
  PRIMARY KEY (`TurfID`,`AmenityID`),
  KEY `AmenityID` (`AmenityID`),
  CONSTRAINT `turf_amenities_ibfk_1` FOREIGN KEY (`TurfID`) REFERENCES `turf` (`TurfID`) ON DELETE CASCADE,
  CONSTRAINT `turf_amenities_ibfk_2` FOREIGN KEY (`AmenityID`) REFERENCES `amenities` (`AmenityID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `turf_amenities`
--

LOCK TABLES `turf_amenities` WRITE;
/*!40000 ALTER TABLE `turf_amenities` DISABLE KEYS */;
/*!40000 ALTER TABLE `turf_amenities` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `turf_photos`
--

DROP TABLE IF EXISTS `turf_photos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `turf_photos` (
  `PhotoID` int NOT NULL AUTO_INCREMENT,
  `TurfID` int NOT NULL,
  `PhotoURL` varchar(500) NOT NULL,
  `IsMain` tinyint(1) DEFAULT '0',
  `Caption` varchar(100) DEFAULT NULL,
  `PhotoType` varchar(50) DEFAULT NULL,
  `UploadDate` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`PhotoID`),
  KEY `TurfID` (`TurfID`),
  CONSTRAINT `turf_photos_ibfk_1` FOREIGN KEY (`TurfID`) REFERENCES `turf` (`TurfID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `turf_photos`
--

LOCK TABLES `turf_photos` WRITE;
/*!40000 ALTER TABLE `turf_photos` DISABLE KEYS */;
/*!40000 ALTER TABLE `turf_photos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `turf_sports`
--

DROP TABLE IF EXISTS `turf_sports`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `turf_sports` (
  `TurfID` int NOT NULL,
  `SportID` int NOT NULL,
  PRIMARY KEY (`TurfID`,`SportID`),
  KEY `SportID` (`SportID`),
  CONSTRAINT `turf_sports_ibfk_1` FOREIGN KEY (`TurfID`) REFERENCES `turf` (`TurfID`) ON DELETE CASCADE,
  CONSTRAINT `turf_sports_ibfk_2` FOREIGN KEY (`SportID`) REFERENCES `sports` (`SportID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `turf_sports`
--

LOCK TABLES `turf_sports` WRITE;
/*!40000 ALTER TABLE `turf_sports` DISABLE KEYS */;
/*!40000 ALTER TABLE `turf_sports` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
  `CreatedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `account_status` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`UserID`),
  UNIQUE KEY `EmailAddress` (`EmailAddress`),
  KEY `UserTypeID` (`UserTypeID`),
  CONSTRAINT `user_ibfk_1` FOREIGN KEY (`UserTypeID`) REFERENCES `user_type` (`UserTypeID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,3,'John','Doe','john.doe@example.com','123456','Delhi, India','Delhi','9876543210','2026-01-24 02:55:35',NULL,'active'),(2,3,'Amit','Sharma','amit.sharma@gmail.com','amit@123','Andheri East, Mumbai','Mumbai','9123456789','2026-01-24 02:58:19',NULL,'active'),(3,2,'Priya','Verma','priya.verma@gmail.com','priya@123','Whitefield, Bangalore','Bangalore','9988776655','2026-01-24 02:59:11',NULL,'active'),(4,1,'Sanjana','Oza','sanjana@gmail.com','sanj@123','Navrangpura, Ahmedabad','Ahmedabad','9090909090','2026-01-24 03:00:29',NULL,'inactive');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_session`
--

DROP TABLE IF EXISTS `user_session`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_session` (
  `SessionID` int NOT NULL AUTO_INCREMENT,
  `UserID` int NOT NULL,
  `SessionToken` varchar(255) NOT NULL,
  `LoginTime` datetime DEFAULT CURRENT_TIMESTAMP,
  `ExpiryTime` datetime NOT NULL,
  `IsActive` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`SessionID`),
  UNIQUE KEY `SessionToken` (`SessionToken`),
  KEY `UserID` (`UserID`),
  CONSTRAINT `user_session_ibfk_1` FOREIGN KEY (`UserID`) REFERENCES `user` (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_session`
--

LOCK TABLES `user_session` WRITE;
/*!40000 ALTER TABLE `user_session` DISABLE KEYS */;
/*!40000 ALTER TABLE `user_session` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_type`
--

DROP TABLE IF EXISTS `user_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_type` (
  `UserTypeID` int NOT NULL AUTO_INCREMENT,
  `TypeName` varchar(50) NOT NULL,
  `Description` varchar(200) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT '1',
  `CreatedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`UserTypeID`),
  UNIQUE KEY `TypeName` (`TypeName`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_type`
--

LOCK TABLES `user_type` WRITE;
/*!40000 ALTER TABLE `user_type` DISABLE KEYS */;
INSERT INTO `user_type` VALUES (1,'Admin','Administrator with all rights',1,'2026-01-23 11:53:54'),(2,'TurfOwner','Owner of turf',1,'2026-01-23 11:53:54'),(3,'Player','Regular player/customer',1,'2026-01-23 11:53:54');
/*!40000 ALTER TABLE `user_type` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-01-24  8:40:30
