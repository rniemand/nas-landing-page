CREATE TABLE `Containers` (
	`ContainerId` INT(11) NOT NULL AUTO_INCREMENT,
	`Deleted` BIT(1) NOT NULL DEFAULT b'0',
	`ShelfNumber` TINYINT(4) NOT NULL DEFAULT '0',
	`ShelfLevel` TINYINT(4) NOT NULL DEFAULT '0',
	`ShelfRow` TINYINT(4) NOT NULL DEFAULT '0',
	`ShelfRowPosition` TINYINT(4) NOT NULL DEFAULT '0',
	`ItemCount` INT(11) NOT NULL DEFAULT '0',
	`DateAdded` DATETIME NOT NULL DEFAULT utc_timestamp(6),
	`DateUpdated` DATETIME NOT NULL DEFAULT utc_timestamp(6),
	`ContainerLabel` VARCHAR(64) NOT NULL DEFAULT '0' COLLATE 'utf8mb4_general_ci',
	`ContainerName` VARCHAR(256) NOT NULL DEFAULT '0' COLLATE 'utf8mb4_general_ci',
	`Notes` TEXT NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ContainerId`) USING BTREE,
	INDEX `Deleted` (`Deleted`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB;

CREATE TABLE `ContainerItems` (
	`ItemId` INT(11) NOT NULL AUTO_INCREMENT,
	`ContainerId` INT(11) NOT NULL,
	`Quantity` INT(11) NOT NULL DEFAULT '0',
	`OrderMoreMinQty` INT(11) NOT NULL DEFAULT '0',
	`Deleted` BIT(1) NOT NULL DEFAULT b'0',
	`OrderMore` BIT(1) NULL DEFAULT b'0',
	`OrderPlaced` BIT(1) NOT NULL DEFAULT b'0',
	`AutoFlagOrderMore` BIT(1) NOT NULL DEFAULT b'0',
	`DateAdded` DATETIME NOT NULL DEFAULT utc_timestamp(6),
	`DateUpdated` DATETIME NOT NULL DEFAULT utc_timestamp(6),
	`Category` VARCHAR(128) NOT NULL DEFAULT '' COLLATE 'utf8mb3_general_ci',
	`SubCategory` VARCHAR(128) NOT NULL DEFAULT '' COLLATE 'utf8mb3_general_ci',
	`InventoryName` VARCHAR(256) NOT NULL DEFAULT '' COLLATE 'utf8mb3_general_ci',
	`OrderUrl` VARCHAR(1024) NOT NULL DEFAULT '' COLLATE 'utf8mb3_general_ci',
	PRIMARY KEY (`ItemId`) USING BTREE,
	INDEX `ContainerId` (`ContainerId`) USING BTREE,
	INDEX `Deleted` (`Deleted`) USING BTREE,
	INDEX `OrderMore` (`OrderMore`) USING BTREE,
	INDEX `OrderPlaced` (`OrderPlaced`) USING BTREE
)
COLLATE='utf8mb3_general_ci'
ENGINE=InnoDB;
