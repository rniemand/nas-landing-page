CREATE TABLE `Users` (
	`UserID` INT(11) NOT NULL AUTO_INCREMENT,
	`CanSetPass` BIT(1) NOT NULL DEFAULT b'0',
	`Email` VARCHAR(256) NOT NULL COLLATE 'utf8mb4_general_ci',
	`PasswordHash` VARCHAR(128) NOT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`UserID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB;
