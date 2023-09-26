CREATE TABLE `Users` (
	`UserID` INT(11) NOT NULL AUTO_INCREMENT,
	`CanSetPass` BIT(1) NOT NULL DEFAULT b'0',
	`Email` VARCHAR(256) NOT NULL COLLATE 'utf8mb4_general_ci',
	`PasswordHash` VARCHAR(128) NOT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`UserID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB;

CREATE TABLE `Homes` (
	`HomeId` INT(11) NOT NULL AUTO_INCREMENT,
	`DefaultHome` BIT(1) NOT NULL DEFAULT b'0',
	`Longitude` DOUBLE NOT NULL DEFAULT '0',
	`Latitude` DOUBLE NOT NULL DEFAULT '0',
	`DateAddedUtc` DATETIME NOT NULL DEFAULT utc_timestamp(6),
	`DateDeletedUtc` DATETIME NULL DEFAULT NULL,
	`Country` VARCHAR(32) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`PostalCode` VARCHAR(32) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`City` VARCHAR(64) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`Province` VARCHAR(64) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`HomeName` VARCHAR(64) NOT NULL COLLATE 'utf8mb4_general_ci',
	`AddressLine1` VARCHAR(512) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`AddressLine2` VARCHAR(512) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`HomeId`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB;

CREATE TABLE `HomeFloors` (
	`FloorId` INT(11) NOT NULL AUTO_INCREMENT,
	`HomeId` INT(11) NOT NULL,
	`DateAddedUtc` DATETIME NOT NULL DEFAULT utc_timestamp(6),
	`DateDeletedUtc` DATETIME NULL DEFAULT NULL,
	`FloorName` VARCHAR(128) NOT NULL DEFAULT '' COLLATE 'utf8mb3_general_ci',
	PRIMARY KEY (`FloorId`) USING BTREE
)
COLLATE='utf8mb3_general_ci'
ENGINE=InnoDB;

CREATE TABLE `HomeRooms` (
	`RoomId` INT(11) NOT NULL AUTO_INCREMENT,
	`FloorId` INT(11) NOT NULL DEFAULT '0',
	`DateAddedUtc` DATETIME NOT NULL DEFAULT utc_timestamp(6),
	`DateDeletedUtc` DATETIME NULL DEFAULT NULL,
	`RoomName` VARCHAR(128) NOT NULL DEFAULT '' COLLATE 'utf8mb3_general_ci',
	PRIMARY KEY (`RoomId`) USING BTREE
)
COLLATE='utf8mb3_general_ci'
ENGINE=InnoDB;

CREATE TABLE `HomeChores` (
	`ChoreId` INT(11) NOT NULL AUTO_INCREMENT,
	`RoomId` INT(11) NOT NULL DEFAULT '0',
	`CompletedCount` INT(11) NOT NULL DEFAULT '0',
	`Frequency` TINYINT(4) NOT NULL DEFAULT '1',
	`Priority` TINYINT(4) NOT NULL DEFAULT '1',
	`ChorePoints` SMALLINT(6) NOT NULL DEFAULT '0',
	`DateAddedUtc` DATETIME NOT NULL DEFAULT utc_timestamp(6),
	`DateDeletedUtc` DATETIME NULL DEFAULT NULL,
	`DateDisabledUtc` DATETIME NULL DEFAULT NULL,
	`DateLastCompletedUtc` DATETIME NULL DEFAULT NULL,
	`DateScheduledUtc` DATETIME NOT NULL DEFAULT utc_timestamp(6),
	`FrequencyValue` VARCHAR(64) NOT NULL DEFAULT '7' COLLATE 'utf8mb3_general_ci',
	`ChoreName` VARCHAR(128) NOT NULL COLLATE 'utf8mb3_general_ci',
	`ChoreDescription` TEXT NULL DEFAULT NULL COLLATE 'utf8mb3_general_ci',
	PRIMARY KEY (`ChoreId`) USING BTREE
)
COLLATE='utf8mb3_general_ci'
ENGINE=InnoDB;
