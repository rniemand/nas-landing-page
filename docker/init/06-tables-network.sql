CREATE TABLE `NetworkDevices` (
	`DeviceID` INT(11) NOT NULL AUTO_INCREMENT,
	`IsPhysical` BIT(1) NOT NULL DEFAULT b'1',
	`IsActive` BIT(1) NOT NULL DEFAULT b'1',
	`DeviceName` VARCHAR(64) NOT NULL COLLATE 'utf8mb4_general_ci',
	`Floor` VARCHAR(64) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`Room` VARCHAR(64) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`RoomLocation` VARCHAR(64) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`DeviceID`) USING BTREE,
	INDEX `IsPhysical` (`IsPhysical`) USING BTREE,
	INDEX `IsActive` (`IsActive`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;

CREATE TABLE `NetworkClassification` (
	`DeviceID` INT(11) NOT NULL,
	`Category` VARCHAR(64) NOT NULL COLLATE 'utf8mb4_general_ci',
	`SubCategory` VARCHAR(64) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`Manufacturer` VARCHAR(64) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`Model` VARCHAR(64) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	INDEX `Category` (`Category`) USING BTREE,
	INDEX `SubCategory` (`SubCategory`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;

CREATE TABLE `NetworkIPv4` (
	`DeviceID` INT(11) NOT NULL,
	`MacAddress` VARCHAR(64) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`IPv4` VARCHAR(16) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`IPv4Int` BIGINT(20) NULL DEFAULT NULL,
	`Connection` VARCHAR(4) NOT NULL DEFAULT 'LAN' COLLATE 'utf8mb4_general_ci',
	`NetworkName` VARCHAR(64) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	INDEX `DeviceID` (`DeviceID`) USING BTREE,
	INDEX `Connection` (`Connection`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;
