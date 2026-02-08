-- ====================================================
-- 1. SETUP DATABASE
-- ====================================================
CREATE DATABASE IF NOT EXISTS db_hotel
  CHARACTER SET = utf8mb4
  COLLATE = utf8mb4_unicode_ci;
USE db_hotel;

-- ====================================================
-- 2. CREATE TABLES (Matching VB.NET Code Schema)
-- ====================================================

-- 2.1 USERS (Admin Login)
CREATE TABLE IF NOT EXISTS tbl_users (
    user_id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(50) NOT NULL
);

-- 2.2 ROOMS (Mapped from Cars)
-- NOTE: We keep 'brand' and 'model' because FrmRooms.vb uses them in UPDATE queries.
-- We will store 'Room Type' in 'brand' and 'Bed Type' in 'model'.
CREATE TABLE IF NOT EXISTS tbl_rooms (
    room_no VARCHAR(20) PRIMARY KEY,  -- Matches 'room_no' in your queries
    brand VARCHAR(50) NOT NULL,       -- Stores: Standard, Deluxe, Suite
    model VARCHAR(50) NOT NULL,       -- Stores: Single, Double, King
    price DECIMAL(10,2) NOT NULL,
    available VARCHAR(10) DEFAULT 'Yes'
);

-- 2.3 GUESTS (Mapped from Customers)
-- Matches FrmGuests.vb queries
CREATE TABLE IF NOT EXISTS tbl_guests (
    cust_id INT AUTO_INCREMENT PRIMARY KEY,
    cust_name VARCHAR(100) NOT NULL,
    address VARCHAR(255),
    phone VARCHAR(20),
    email VARCHAR(100) UNIQUE,
    password VARCHAR(50) DEFAULT '1234'
);

-- 2.4 BOOKINGS (Mapped from Rentals)
-- Matches FrmRental.vb queries (check_in, check_out)
CREATE TABLE IF NOT EXISTS tbl_bookings (
    rent_id INT AUTO_INCREMENT PRIMARY KEY, -- Code likely still uses rent_id/book_id
    room_no VARCHAR(20),
    cust_id INT,
    check_in DATE,    -- Matches 'check_in' in your FrmRental code
    check_out DATE,   -- Matches 'check_out' in your FrmRental code
    fees DECIMAL(10,2),
    status VARCHAR(20) DEFAULT 'Pending',
    FOREIGN KEY (room_no) REFERENCES tbl_rooms(room_no) ON UPDATE CASCADE,
    FOREIGN KEY (cust_id) REFERENCES tbl_guests(cust_id) ON UPDATE CASCADE
);

-- ====================================================
-- 3. SEED DATA (Hotel Themed)
-- ====================================================

-- Clear old data safely
SET FOREIGN_KEY_CHECKS = 0;
TRUNCATE TABLE tbl_bookings;
TRUNCATE TABLE tbl_users;
TRUNCATE TABLE tbl_guests;
TRUNCATE TABLE tbl_rooms;
SET FOREIGN_KEY_CHECKS = 1;

-- 3.1 Insert Admin
INSERT INTO tbl_users (username, password) VALUES ('admin', '1234');

-- 3.2 Insert ROOMS (Using 'brand' for Type, 'model' for Bed)
-- Floor 1: Standard Rooms
INSERT INTO tbl_rooms (room_no, brand, model, price, available) VALUES
('101', 'Standard', 'Single Bed', 1500.00, 'Yes'),
('102', 'Standard', 'Single Bed', 1500.00, 'Yes'),
('103', 'Standard', 'Double Bed', 2000.00, 'Yes'),
('104', 'Standard', 'Double Bed', 2000.00, 'Yes'),
('105', 'Standard', 'Twin Bed', 1800.00, 'Yes');

-- Floor 2: Deluxe Rooms
INSERT INTO tbl_rooms (room_no, brand, model, price, available) VALUES
('201', 'Deluxe', 'Queen Bed', 3500.00, 'Yes'),
('202', 'Deluxe', 'Queen Bed', 3500.00, 'Yes'),
('203', 'Deluxe', 'King Bed', 4000.00, 'Yes'),
('204', 'Deluxe', 'King Bed', 4000.00, 'Yes'),
('205', 'Deluxe', 'Ocean View', 4500.00, 'Yes');

-- Floor 3: Suites
INSERT INTO tbl_rooms (room_no, brand, model, price, available) VALUES
('301', 'Executive Suite', 'King Bed', 7000.00, 'Yes'),
('302', 'Executive Suite', 'King Bed', 7000.00, 'Yes'),
('303', 'Presidential', 'Emperor Bed', 15000.00, 'Yes');

-- 3.3 Insert GUESTS (Real Names)
INSERT INTO tbl_guests (cust_name, address, phone, email, password) VALUES
('Amitabh Bachchan', 'Jalsa, Juhu, Mumbai', '9876543210', 'amitabh@gmail.com', '1234'),
('Shah Rukh Khan', 'Mannat, Bandra, Mumbai', '9876543211', 'srk@gmail.com', '1234'),
('Virat Kohli', 'Worli, Mumbai', '9876543212', 'virat@gmail.com', '1234'),
('Deepika Padukone', 'Prabhadevi, Mumbai', '9876543213', 'deepika@gmail.com', '1234'),
('Ratan Tata', 'Colaba, Mumbai', '9876543214', 'ratan@gmail.com', '1234'),
('Mukesh Ambani', 'Antilia, Altamount Road', '9876543215', 'mukesh@gmail.com', '1234'),
('Elon Musk', 'Texas, USA', '9876543216', 'elon@tesla.com', '1234'),
('Sundar Pichai', 'California, USA', '9876543217', 'sundar@google.com', '1234'),
('Narendra Modi', 'Lok Kalyan Marg, Delhi', '9876543218', 'narendra@gmail.com', '1234'),
('MS Dhoni', 'Ranchi, Jharkhand', '9876543219', 'mahi@gmail.com', '1234');

-- 3.4 Insert BOOKING HISTORY
-- Past Bookings (Checked Out)
INSERT INTO tbl_bookings (room_no, cust_id, check_in, check_out, fees, status) VALUES
('101', 1, '2025-10-01', '2025-10-05', 6000.00, 'Returned'), -- 'Returned' logic maps to Checked Out
('201', 2, '2025-11-10', '2025-11-12', 7000.00, 'Returned'),
('301', 3, '2025-12-01', '2025-12-03', 14000.00, 'Returned');

-- Active Bookings (Guests currently in rooms)
INSERT INTO tbl_bookings (room_no, cust_id, check_in, check_out, fees, status) VALUES
('103', 4, CURDATE(), DATE_ADD(CURDATE(), INTERVAL 3 DAY), 6000.00, 'Active'),
('205', 5, CURDATE(), DATE_ADD(CURDATE(), INTERVAL 5 DAY), 22500.00, 'Active');

-- 3.5 Update Room Availability for Active Bookings
UPDATE tbl_rooms SET available = 'No' WHERE room_no IN ('103', '205');