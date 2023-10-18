CREATE TABLE `Users` (
	`UserID` INT(11) NOT NULL AUTO_INCREMENT,
	`CanSetPass` BIT(1) NOT NULL DEFAULT b'0',
	`DateAdded` DATE NOT NULL DEFAULT curdate(),
	`DateDeleted` DATE NULL DEFAULT NULL,
	`Email` VARCHAR(256) NOT NULL COLLATE 'utf8mb4_general_ci',
	`PasswordHash` VARCHAR(128) NOT NULL COLLATE 'utf8mb4_general_ci',
	`FirstName` VARCHAR(128) NOT NULL COLLATE 'utf8mb4_general_ci',
	`Surname` VARCHAR(128) NOT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`UserID`) USING BTREE
) COLLATE='utf8mb4_general_ci' ENGINE=InnoDB;

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
) COLLATE = 'utf8mb4_general_ci' ENGINE = InnoDB;

CREATE TABLE `HomeFloors` (
  `FloorId` INT(11) NOT NULL AUTO_INCREMENT,
  `HomeId` INT(11) NOT NULL,
  `DateAddedUtc` DATETIME NOT NULL DEFAULT utc_timestamp(6),
  `DateDeletedUtc` DATETIME NULL DEFAULT NULL,
  `FloorName` VARCHAR(128) NOT NULL DEFAULT '' COLLATE 'utf8mb3_general_ci',
  PRIMARY KEY (`FloorId`) USING BTREE
) COLLATE = 'utf8mb3_general_ci' ENGINE = InnoDB;

CREATE TABLE `HomeRooms` (
  `RoomId` INT(11) NOT NULL AUTO_INCREMENT,
  `FloorId` INT(11) NOT NULL DEFAULT '0',
  `DateAddedUtc` DATETIME NOT NULL DEFAULT utc_timestamp(6),
  `DateDeletedUtc` DATETIME NULL DEFAULT NULL,
  `RoomName` VARCHAR(128) NOT NULL DEFAULT '' COLLATE 'utf8mb3_general_ci',
  PRIMARY KEY (`RoomId`) USING BTREE
) COLLATE = 'utf8mb3_general_ci' ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `HomeChores` (
  `ChoreId` INT(11) NOT NULL AUTO_INCREMENT,
  `RoomId` INT(11) NOT NULL DEFAULT '0',
  `CompletedCount` INT(11) NOT NULL DEFAULT '0',
  `ChorePoints` SMALLINT(6) NOT NULL DEFAULT '0',
  `DateAdded` DATETIME NOT NULL DEFAULT curdate(),
  `DateScheduled` DATE NOT NULL DEFAULT curdate(),
  `DateDeleted` DATE NULL DEFAULT NULL,
  `DateDisabled` DATE NULL DEFAULT NULL,
  `Priority` VARCHAR(8) NOT NULL DEFAULT 'low' COLLATE 'utf8mb3_general_ci',
  `IntervalModifier` VARCHAR(16) NOT NULL DEFAULT 'Weeks' COLLATE 'utf8mb3_general_ci',
  `Interval` VARCHAR(64) NOT NULL DEFAULT '1' COLLATE 'utf8mb3_general_ci',
  `ChoreName` VARCHAR(128) NOT NULL COLLATE 'utf8mb3_general_ci',
  `ChoreDescription` TEXT NULL DEFAULT NULL COLLATE 'utf8mb3_general_ci',
  PRIMARY KEY (`ChoreId`) USING BTREE,
  INDEX `Priority` (`Priority`) USING BTREE,
  INDEX `RoomId` (`RoomId`) USING BTREE
) COLLATE = 'utf8mb3_general_ci' ENGINE = InnoDB;

CREATE TABLE `HomeChoreHistory` (
  `ChoreHistoryId` BIGINT(20) NOT NULL AUTO_INCREMENT,
  `ChoreId` INT(11) NOT NULL,
  `UserId` INT(11) NOT NULL,
  `PointsClaimed` BIT(1) NOT NULL DEFAULT b'0',
  `Points` INT(11) NOT NULL DEFAULT '0',
  `DateAdded` DATE NOT NULL DEFAULT curdate(),
  `DateClaimed` DATE NULL DEFAULT NULL,
  PRIMARY KEY (`ChoreHistoryId`) USING BTREE,
  INDEX `ChoreId` (`ChoreId`) USING BTREE
) COLLATE = 'utf8mb4_general_ci' ENGINE = InnoDB;
