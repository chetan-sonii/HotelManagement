-- MySQL dump 10.13  Distrib 8.0.44, for Win64 (x86_64)
--
-- Host: localhost    Database: db_hotel
-- ------------------------------------------------------
-- Server version	8.0.44

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
-- Table structure for table `tbl_bookings`
--

DROP TABLE IF EXISTS `tbl_bookings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_bookings` (
  `book_id` int NOT NULL AUTO_INCREMENT,
  `room_no` varchar(20) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `cust_id` int DEFAULT NULL,
  `check_in` date DEFAULT NULL,
  `check_out` date DEFAULT NULL,
  `fees` decimal(10,2) DEFAULT NULL,
  `status` varchar(20) COLLATE utf8mb4_unicode_ci DEFAULT 'Pending',
  PRIMARY KEY (`book_id`),
  KEY `room_no` (`room_no`),
  KEY `cust_id` (`cust_id`),
  CONSTRAINT `tbl_bookings_ibfk_1` FOREIGN KEY (`room_no`) REFERENCES `tbl_rooms` (`room_no`) ON UPDATE CASCADE,
  CONSTRAINT `tbl_bookings_ibfk_2` FOREIGN KEY (`cust_id`) REFERENCES `tbl_guests` (`cust_id`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_bookings`
--

LOCK TABLES `tbl_bookings` WRITE;
/*!40000 ALTER TABLE `tbl_bookings` DISABLE KEYS */;
INSERT INTO `tbl_bookings` VALUES (1,'101',1,'2025-10-01','2025-10-05',6000.00,'Returned'),(2,'201',2,'2025-11-10','2025-11-12',7000.00,'Returned'),(3,'301',3,'2025-12-01','2025-12-03',14000.00,'Returned'),(4,'103',4,'2026-02-09','2026-02-12',6000.00,'Active'),(5,'205',5,'2026-02-09','2026-02-14',22500.00,'Active');
/*!40000 ALTER TABLE `tbl_bookings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_guests`
--

DROP TABLE IF EXISTS `tbl_guests`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_guests` (
  `cust_id` int NOT NULL AUTO_INCREMENT,
  `cust_name` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `address` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `phone` varchar(20) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `email` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `password` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT '1234',
  PRIMARY KEY (`cust_id`),
  UNIQUE KEY `email` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_guests`
--

LOCK TABLES `tbl_guests` WRITE;
/*!40000 ALTER TABLE `tbl_guests` DISABLE KEYS */;
INSERT INTO `tbl_guests` VALUES (1,'Amitabh Bachchan','Jalsa, Juhu, Mumbai','9876543210','amitabh@gmail.com','1234'),(2,'Shah Rukh Khan','Mannat, Bandra, Mumbai','9876543211','srk@gmail.com','1234'),(3,'Virat Kohli','Worli, Mumbai','9876543212','virat@gmail.com','1234'),(4,'Deepika Padukone','Prabhadevi, Mumbai','9876543213','deepika@gmail.com','1234'),(5,'Ratan Tata','Colaba, Mumbai','9876543214','ratan@gmail.com','1234'),(6,'Mukesh Ambani','Antilia, Altamount Road','9876543215','mukesh@gmail.com','1234'),(7,'Elon Musk','Texas, USA','9876543216','elon@tesla.com','1234'),(8,'Sundar Pichai','California, USA','9876543217','sundar@google.com','1234'),(9,'Narendra Modi','Lok Kalyan Marg, Delhi','9876543218','narendra@gmail.com','1234'),(10,'MS Dhoni','Ranchi, Jharkhand','9876543219','mahi@gmail.com','1234');
/*!40000 ALTER TABLE `tbl_guests` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_rooms`
--

DROP TABLE IF EXISTS `tbl_rooms`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_rooms` (
  `room_no` varchar(20) COLLATE utf8mb4_unicode_ci NOT NULL,
  `room_type` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `bed_type` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `price` decimal(10,2) NOT NULL,
  `available` varchar(10) COLLATE utf8mb4_unicode_ci DEFAULT 'Yes',
  PRIMARY KEY (`room_no`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_rooms`
--

LOCK TABLES `tbl_rooms` WRITE;
/*!40000 ALTER TABLE `tbl_rooms` DISABLE KEYS */;
INSERT INTO `tbl_rooms` VALUES ('101','Standard','Single Bed',1500.00,'Yes'),('102','Standard','Single Bed',1500.00,'Yes'),('103','Standard','Double Bed',2000.00,'No'),('104','Standard','Double Bed',2000.00,'Yes'),('105','Standard','Twin Bed',1800.00,'Yes'),('201','Deluxe','Queen Bed',3500.00,'Yes'),('202','Deluxe','Queen Bed',3500.00,'Yes'),('203','Deluxe','King Bed',4000.00,'Yes'),('204','Deluxe','King Bed',4000.00,'Yes'),('205','Deluxe','Ocean View',4500.00,'No'),('301','Executive Suite','King Bed',7000.00,'Yes'),('302','Executive Suite','King Bed',7000.00,'Yes'),('303','Presidential','Emperor Bed',15000.00,'Yes');
/*!40000 ALTER TABLE `tbl_rooms` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_users`
--

DROP TABLE IF EXISTS `tbl_users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_users` (
  `user_id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `password` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `username` (`username`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_users`
--

LOCK TABLES `tbl_users` WRITE;
/*!40000 ALTER TABLE `tbl_users` DISABLE KEYS */;
INSERT INTO `tbl_users` VALUES (1,'admin','1234');
/*!40000 ALTER TABLE `tbl_users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-02-09  4:11:04
