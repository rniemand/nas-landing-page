CREATE TABLE `ShoppingList` (
	`ItemId` BIGINT(20) NOT NULL AUTO_INCREMENT,
	`HomeId` INT(11) NOT NULL,
	`AddedByUserId` INT(11) NOT NULL,
	`DateAdded` DATE NOT NULL DEFAULT curdate(),
	`DateDeleted` DATE NULL DEFAULT NULL,
	`DatePurchased` DATE NULL DEFAULT NULL,
	`LastKnownPrice` DECIMAL(20,6) NULL DEFAULT NULL,
	`StoreName` VARCHAR(128) NOT NULL COLLATE 'utf8mb4_general_ci',
	`Category` VARCHAR(128) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`ItemName` VARCHAR(512) NOT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ItemId`) USING BTREE,
	INDEX `HomeId` (`HomeId`) USING BTREE,
	INDEX `AddedByUserId` (`AddedByUserId`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB;
