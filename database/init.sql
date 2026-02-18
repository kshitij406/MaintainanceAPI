CREATE DATABASE IF NOT EXISTS maintainance_api;
USE maintainance_api;

-- 1. CREATE TABLES

-- Table: Drivers (Must be created before Assets for linking)
CREATE TABLE drivers (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    license_number VARCHAR(50) NOT NULL UNIQUE,
    phone_number VARCHAR(20),
    status VARCHAR(20) DEFAULT 'Active', -- Active, OnLeave, Terminated
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Table: Assets (Vehicles)
CREATE TABLE assets (
    id INT AUTO_INCREMENT PRIMARY KEY,
    asset_tag VARCHAR(50) NOT NULL UNIQUE,
    plate_no VARCHAR(20) NULL,
    make VARCHAR(50) NOT NULL,
    model VARCHAR(50) NOT NULL,
    status VARCHAR(20) NOT NULL DEFAULT 'Active',
    odometer INT NOT NULL DEFAULT 0,
    current_driver_id INT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_asset_driver FOREIGN KEY (current_driver_id) REFERENCES drivers(id) ON DELETE SET NULL
);

-- Table: Work Orders (Tickets)
CREATE TABLE work_orders (
    id INT AUTO_INCREMENT PRIMARY KEY,
    asset_id INT NOT NULL,
    title VARCHAR(100) NOT NULL,
    description TEXT NULL,
    priority VARCHAR(20) NOT NULL DEFAULT 'Medium',
    status VARCHAR(20) NOT NULL DEFAULT 'Open',
    opened_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    closed_at DATETIME NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_workorder_asset FOREIGN KEY (asset_id) REFERENCES assets(id) ON DELETE CASCADE
);

-- Table: Work Order Logs (Chat/History)
CREATE TABLE work_order_logs (
    id INT AUTO_INCREMENT PRIMARY KEY,
    work_order_id INT NOT NULL,
    message TEXT NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_log_workorder FOREIGN KEY (work_order_id) REFERENCES work_orders(id) ON DELETE CASCADE
);

-- Table: Fuel Logs (Cost Tracking)
CREATE TABLE fuel_logs (
    id INT AUTO_INCREMENT PRIMARY KEY,
    asset_id INT NOT NULL,
    driver_id INT NULL,
    odometer_reading INT NOT NULL,
    liters DECIMAL(10, 2) NOT NULL,
    price_per_liter DECIMAL(10, 2) NOT NULL,
    total_cost DECIMAL(10, 2) GENERATED ALWAYS AS (liters * price_per_liter) STORED,
    fill_date DATETIME DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_fuel_asset FOREIGN KEY (asset_id) REFERENCES assets(id) ON DELETE CASCADE,
    CONSTRAINT fk_fuel_driver FOREIGN KEY (driver_id) REFERENCES drivers(id) ON DELETE SET NULL
);

-- Table: Service Schedules (Preventative Maintenance)
CREATE TABLE service_schedules (
    id INT AUTO_INCREMENT PRIMARY KEY,
    asset_id INT NOT NULL,
    service_type VARCHAR(50) NOT NULL, -- e.g., "Oil Change", "Tire Rotation"
    last_service_date DATETIME,
    last_service_odometer INT,
    next_due_date DATETIME,
    next_due_odometer INT,
    CONSTRAINT fk_schedule_asset FOREIGN KEY (asset_id) REFERENCES assets(id) ON DELETE CASCADE
);

-- 2. SEED DATA (Insert Demo Data)

-- Insert Drivers
INSERT INTO drivers (id, name, license_number, phone_number, status) VALUES
(1, 'John Doe', 'LIC-9823-XA', '+230 5765 4321', 'Active'),
(2, 'Jane Smith', 'LIC-5541-MB', '+230 5888 1122', 'Active'),
(3, 'Raj Patel', 'LIC-1102-PP', '+230 5999 3344', 'OnLeave'),
(4, 'Sarah Connor', 'LIC-0000-SKY', '+230 5123 9876', 'Active'),
(5, 'Michael Scott', 'LIC-DUNDER-01', '+230 5555 0199', 'Active');

-- Insert Assets
INSERT INTO assets (id, asset_tag, plate_no, make, model, status, odometer, current_driver_id, created_at) VALUES
(1, 'FLEET-001', 'AB 123 CD', 'Toyota', 'HiAce', 'Active', 125000, 1, NOW()),
(2, 'FLEET-002', 'XY 987 ZZ', 'Nissan', 'NV350', 'Active', 45000, 2, NOW()),
(3, 'FLEET-003', 'QQ 111 AA', 'Isuzu', 'D-Max', 'Maintenance', 210500, NULL, NOW()), -- In shop, no driver
(4, 'FLEET-004', 'ZZ 555 BB', 'Toyota', 'Corolla', 'Active', 32000, 5, NOW()),
(5, 'FLEET-005', 'TR-999-XX', 'Mitsubishi', 'Canter', 'Inactive', 350000, NULL, NOW());

-- Insert Work Orders
INSERT INTO work_orders (id, asset_id, title, description, priority, status, opened_at, created_at) VALUES
-- Asset 1: Routine check
(1, 1, '5000km Routine Service', 'Standard oil change and filter replacement.', 'Low', 'Closed', DATE_SUB(NOW(), INTERVAL 30 DAY), NOW()),
-- Asset 3: Major issue (matches "Maintenance" status)
(2, 3, 'Brake Failure', 'Driver reported screeching noise and poor braking performance.', 'High', 'InProgress', DATE_SUB(NOW(), INTERVAL 2 DAY), NOW()),
-- Asset 2: Minor cosmetic
(3, 2, 'Broken Tail Light', 'Left rear tail light cracked.', 'Medium', 'Open', DATE_SUB(NOW(), INTERVAL 5 HOUR), NOW()),
-- Asset 4: Closed issue
(4, 4, 'AC Not Cooling', 'Air conditioning blowing hot air.', 'Medium', 'Closed', DATE_SUB(NOW(), INTERVAL 10 DAY), NOW());

-- Insert Work Order Logs
INSERT INTO work_order_logs (work_order_id, message, created_at) VALUES
-- Logs for Brake Failure (Asset 3)
(2, 'Vehicle towed to workshop.', DATE_SUB(NOW(), INTERVAL 2 DAY)),
(2, 'Initial inspection reveals worn pads and warped rotors.', DATE_SUB(NOW(), INTERVAL 1 DAY)),
(2, 'Waiting for parts quote.', DATE_SUB(NOW(), INTERVAL 20 HOUR)),
(2, 'Parts approved by Fleet Manager. Ordering now.', DATE_SUB(NOW(), INTERVAL 2 HOUR)),
-- Logs for Tail Light (Asset 2)
(3, 'Reported by Jane during morning inspection.', DATE_SUB(NOW(), INTERVAL 5 HOUR)),
-- Logs for AC (Asset 4 - Closed)
(4, 'AC compressor clutch not engaging.', DATE_SUB(NOW(), INTERVAL 10 DAY)),
(4, 'Replaced relay. System holding pressure.', DATE_SUB(NOW(), INTERVAL 9 DAY)),
(4, 'Tested cooling output. 4 degrees C. Working fine.', DATE_SUB(NOW(), INTERVAL 8 DAY));

-- Insert Fuel Logs
INSERT INTO fuel_logs (asset_id, driver_id, odometer_reading, liters, price_per_liter, fill_date) VALUES
(1, 1, 124500, 45.5, 54.00, DATE_SUB(NOW(), INTERVAL 5 DAY)),
(1, 1, 124800, 40.0, 54.00, DATE_SUB(NOW(), INTERVAL 2 DAY)),
(2, 2, 44800, 35.0, 54.00, DATE_SUB(NOW(), INTERVAL 1 DAY)),
(4, 5, 31500, 30.0, 54.00, DATE_SUB(NOW(), INTERVAL 7 DAY));

-- Insert Service Schedules
INSERT INTO service_schedules (asset_id, service_type, last_service_date, last_service_odometer, next_due_date, next_due_odometer) VALUES
(1, 'Oil Change', DATE_SUB(NOW(), INTERVAL 29 DAY), 123000, DATE_ADD(NOW(), INTERVAL 60 DAY), 128000),
(2, 'Tire Rotation', DATE_SUB(NOW(), INTERVAL 90 DAY), 40000, DATE_ADD(NOW(), INTERVAL 10 DAY), 50000),
(3, 'Major Service', DATE_SUB(NOW(), INTERVAL 1 YEAR), 180000, NOW(), 210000);