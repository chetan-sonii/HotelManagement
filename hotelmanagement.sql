-- ====================================================
-- 1. SETUP DATABASE
-- ====================================================
CREATE DATABASE IF NOT EXISTS db_hotel
  CHARACTER SET = utf8mb4
  COLLATE = utf8mb4_unicode_ci;
USE db_hotel;

-- ====================================================
-- 2. CREATE TABLES
-- ====================================================

-- 2.1 USERS (Admin Login)
CREATE TABLE IF NOT EXISTS tbl_users (
    user_id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(50) NOT NULL
);

-- 2.2 ROOMS (Inventory)
CREATE TABLE IF NOT EXISTS tbl_rooms (
    room_no VARCHAR(10) PRIMARY KEY, -- e.g. "101", "205"
    room_type VARCHAR(50) NOT NULL,  -- e.g. "Standard", "Deluxe", "Suite"
    bed_type VARCHAR(50) NOT NULL,   -- e.g. "Single", "Double", "King"
    price DECIMAL(10,2) NOT NULL,
    available VARCHAR(10) DEFAULT 'Yes' -- 'Yes' or 'No'
);

-- 2.3 GUESTS (Customers)
CREATE TABLE IF NOT EXISTS tbl_guests (
    guest_id INT AUTO_INCREMENT PRIMARY KEY,
    guest_name VARCHAR(100) NOT NULL,
    address VARCHAR(255),
    phone VARCHAR(20),
    email VARCHAR(100) UNIQUE,
    password VARCHAR(50) DEFAULT '1234'
);

-- 2.4 BOOKINGS (Transactions)
CREATE TABLE IF NOT EXISTS tbl_bookings (
    book_id INT AUTO_INCREMENT PRIMARY KEY,
    room_no VARCHAR(10),
    guest_id INT,
    check_in DATE,
    check_out DATE,
    fees DECIMAL(10,2),
    status VARCHAR(20) DEFAULT 'Pending', -- 'Pending', 'Active', 'CheckedOut'
    FOREIGN KEY (room_no) REFERENCES tbl_rooms(room_no) ON UPDATE CASCADE,
    FOREIGN KEY (guest_id) REFERENCES tbl_guests(guest_id) ON UPDATE CASCADE
);

-- ====================================================
-- 3. SEED MASSIVE MOCK DATA
-- ====================================================

-- Clear old data to prevent duplicates if run twice
SET FOREIGN_KEY_CHECKS = 0;
TRUNCATE TABLE tbl_bookings;
TRUNCATE TABLE tbl_users;
TRUNCATE TABLE tbl_guests;
TRUNCATE TABLE tbl_rooms;
SET FOREIGN_KEY_CHECKS = 1;

-- 3.1 Insert Admin
INSERT INTO tbl_users (username, password) VALUES ('admin', '1234');

-- 3.2 Insert 50+ ROOMS (Floors 1-5)
-- Floor 1: Standard / Single
INSERT INTO tbl_rooms (room_no, room_type, bed_type, price, available) VALUES
('101', 'Standard', 'Single', 1500.00, 'Yes'),
('102', 'Standard', 'Single', 1500.00, 'Yes'),
('103', 'Standard', 'Single', 1500.00, 'Yes'),
('104', 'Standard', 'Single', 1500.00, 'Yes'),
('105', 'Standard', 'Single', 1500.00, 'Yes'),
('106', 'Standard', 'Double', 1800.00, 'Yes'),
('107', 'Standard', 'Double', 1800.00, 'Yes'),
('108', 'Standard', 'Double', 1800.00, 'Yes'),
('109', 'Standard', 'Double', 1800.00, 'Yes'),
('110', 'Standard', 'Double', 1800.00, 'Yes');

-- Floor 2: Deluxe / Queen
INSERT INTO tbl_rooms (room_no, room_type, bed_type, price, available) VALUES
('201', 'Deluxe', 'Queen', 2500.00, 'Yes'),
('202', 'Deluxe', 'Queen', 2500.00, 'Yes'),
('203', 'Deluxe', 'Queen', 2500.00, 'Yes'),
('204', 'Deluxe', 'Queen', 2500.00, 'Yes'),
('205', 'Deluxe', 'Queen', 2500.00, 'Yes'),
('206', 'Deluxe', 'Twin', 2400.00, 'Yes'),
('207', 'Deluxe', 'Twin', 2400.00, 'Yes'),
('208', 'Deluxe', 'Twin', 2400.00, 'Yes'),
('209', 'Deluxe', 'Twin', 2400.00, 'Yes'),
('210', 'Deluxe', 'Twin', 2400.00, 'Yes');

-- Floor 3: Executive / King
INSERT INTO tbl_rooms (room_no, room_type, bed_type, price, available) VALUES
('301', 'Executive', 'King', 3500.00, 'Yes'),
('302', 'Executive', 'King', 3500.00, 'Yes'),
('303', 'Executive', 'King', 3500.00, 'Yes'),
('304', 'Executive', 'King', 3500.00, 'Yes'),
('305', 'Executive', 'King', 3500.00, 'Yes'),
('306', 'Executive', 'King', 3500.00, 'Yes'),
('307', 'Executive', 'King', 3500.00, 'Yes'),
('308', 'Executive', 'King', 3500.00, 'Yes'),
('309', 'Executive', 'King', 3500.00, 'Yes'),
('310', 'Executive', 'King', 3500.00, 'Yes');

-- Floor 4: Suite / King
INSERT INTO tbl_rooms (room_no, room_type, bed_type, price, available) VALUES
('401', 'Suite', 'King', 5000.00, 'Yes'),
('402', 'Suite', 'King', 5000.00, 'Yes'),
('403', 'Suite', 'King', 5000.00, 'Yes'),
('404', 'Suite', 'King', 5000.00, 'Yes'),
('405', 'Suite', 'King', 5000.00, 'Yes');

-- Floor 5: Presidential / King
INSERT INTO tbl_rooms (room_no, room_type, bed_type, price, available) VALUES
('501', 'Presidential', 'King', 10000.00, 'Yes'),
('502', 'Presidential', 'King', 10000.00, 'Yes');

-- 3.3 Insert 30+ GUESTS
INSERT INTO tbl_guests (guest_name, address, phone, email, password) VALUES
('Rahul Sharma', '12, MG Road, Mumbai', '9876543210', 'rahul@gmail.com', '1234'),
('Priya Patel', '45, Navrangpura, Ahmedabad', '9876543211', 'priya@gmail.com', '1234'),
('Amit Singh', '78, Civil Lines, Delhi', '9876543212', 'amit@gmail.com', '1234'),
('Sneha Reddy', '34, Banjara Hills, Hyderabad', '9876543213', 'sneha@gmail.com', '1234'),
('Vikram Malhotra', '56, Koramangala, Bangalore', '9876543214', 'vikram@gmail.com', '1234'),
('Anjali Das', '89, Park Street, Kolkata', '9876543215', 'anjali@gmail.com', '1234'),
('Rohit Verma', '23, Sector 17, Chandigarh', '9876543216', 'rohit@gmail.com', '1234'),
('Pooja Gupta', '67, Gomti Nagar, Lucknow', '9876543217', 'pooja@gmail.com', '1234'),
('Suresh Nair', '90, Panampilly Nagar, Kochi', '9876543218', 'suresh@gmail.com', '1234'),
('Neha Joshi', '11, Deccan Gymkhana, Pune', '9876543219', 'neha@gmail.com', '1234'),
('Arjun Mehta', '101, Satellite, Ahmedabad', '9876543220', 'arjun@gmail.com', '1234'),
('Kavita Iyer', '202, Mylapore, Chennai', '9876543221', 'kavita@gmail.com', '1234'),
('Ravi Kumar', '303, Boring Road, Patna', '9876543222', 'ravi@gmail.com', '1234'),
('Meera Rao', '404, Indiranagar, Bangalore', '9876543223', 'meera@gmail.com', '1234'),
('Sanjay Mishra', '505, Kankerbagh, Patna', '9876543224', 'sanjay@gmail.com', '1234'),
('Divya Kapoor', '606, Vasant Vihar, Delhi', '9876543225', 'divya@gmail.com', '1234'),
('Manish Jain', '707, Malviya Nagar, Jaipur', '9876543226', 'manish@gmail.com', '1234'),
('Riya Sen', '808, Salt Lake, Kolkata', '9876543227', 'riya@gmail.com', '1234'),
('Karan Johar', '909, Juhu, Mumbai', '9876543228', 'karan@gmail.com', '1234'),
('Simran Kaur', '111, Model Town, Ludhiana', '9876543229', 'simran@gmail.com', '1234'),
('Aditya Roy', '222, Bandra, Mumbai', '9876543230', 'aditya@gmail.com', '1234'),
('Nisha Yadav', '333, Hinoo, Ranchi', '9876543231', 'nisha@gmail.com', '1234'),
('Varun Dhawan', '444, Andheri, Mumbai', '9876543232', 'varun@gmail.com', '1234'),
('Ishita Bhalla', '555, Green Park, Delhi', '9876543233', 'ishita@gmail.com', '1234'),
('Rajesh Koothrappali', '666, Indrapuri, Bhopal', '9876543234', 'rajesh@gmail.com', '1234'),
('Penny Hofstadter', '777, Civil Lines, Nagpur', '9876543235', 'penny@gmail.com', '1234'),
('Sheldon Cooper', '888, University Road, Pune', '9876543236', 'sheldon@gmail.com', '1234'),
('Leonard Hofstadter', '999, Ashram Road, Ahmedabad', '9876543237', 'leonard@gmail.com', '1234'),
('Howard Wolowitz', '121, Space City, Bangalore', '9876543238', 'howard@gmail.com', '1234'),
('Bernadette Rostenkowski', '131, Microbiology Lab, Mumbai', '9876543239', 'bernadette@gmail.com', '1234');

-- 3.4 Insert BOOKING HISTORY
-- PAST Bookings (Checked Out)
INSERT INTO tbl_bookings (room_no, guest_id, check_in, check_out, fees, status) VALUES
('101', 1, '2025-12-01', '2025-12-05', 6000.00, 'CheckedOut'),
('201', 2, '2025-12-10', '2025-12-12', 5000.00, 'CheckedOut'),
('301', 3, '2026-01-01', '2026-01-03', 7000.00, 'CheckedOut'),
('401', 4, '2026-01-05', '2026-01-10', 25000.00, 'CheckedOut'),
('501', 5, '2026-01-15', '2026-01-20', 50000.00, 'CheckedOut');

-- ACTIVE Bookings (Currently in Room)
INSERT INTO tbl_bookings (room_no, guest_id, check_in, check_out, fees, status) VALUES
('102', 6, CURDATE(), DATE_ADD(CURDATE(), INTERVAL 3 DAY), 4500.00, 'Active'),
('202', 7, CURDATE(), DATE_ADD(CURDATE(), INTERVAL 2 DAY), 5000.00, 'Active'),
('305', 8, CURDATE(), DATE_ADD(CURDATE(), INTERVAL 5 DAY), 17500.00, 'Active');

-- PENDING Requests (Future Bookings)
INSERT INTO tbl_bookings (room_no, guest_id, check_in, check_out, fees, status) VALUES
('405', 9, DATE_ADD(CURDATE(), INTERVAL 1 DAY), DATE_ADD(CURDATE(), INTERVAL 3 DAY), 10000.00, 'Pending'),
('105', 10, DATE_ADD(CURDATE(), INTERVAL 2 DAY), DATE_ADD(CURDATE(), INTERVAL 5 DAY), 4500.00, 'Pending');

-- 3.5 UPDATE ROOM STATUS
-- Mark "Active" rooms as 'No' (Not Available)
UPDATE tbl_rooms SET available = 'No' WHERE room_no IN ('102', '202', '305');