CREATE TABLE `UserTasks` (
	`TaskID` INT(11) NOT NULL AUTO_INCREMENT,
	`Deleted` BIT(1) NOT NULL DEFAULT b'0',
	`Completed` BIT(1) NOT NULL DEFAULT b'0',
	`TaskPriority` INT(11) NOT NULL DEFAULT '256',
	`DateAddedUtc` DATETIME NOT NULL DEFAULT utc_timestamp(6),
	`DateCompletedUtc` DATETIME NOT NULL DEFAULT utc_timestamp(6),
	`TaskName` VARCHAR(512) NOT NULL COLLATE 'utf8mb4_general_ci',
	`TaskCategory` VARCHAR(128) NOT NULL COLLATE 'utf8mb4_general_ci',
	`TaskDescription` TEXT NOT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`TaskID`) USING BTREE,
	INDEX `Completed` (`Completed`) USING BTREE,
	INDEX `Deleted` (`Deleted`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB;
