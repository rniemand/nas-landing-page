CREATE TABLE `UserTasks` (
  `TaskID` INT(11) NOT NULL AUTO_INCREMENT,
  `UserID` INT(11) NOT NULL DEFAULT '0',
  `TaskPriority` INT(11) NOT NULL DEFAULT '256',
  `DateAddedUtc` DATETIME NOT NULL DEFAULT utc_timestamp(6),
  `DateCompletedUtc` DATETIME NULL DEFAULT NULL,
  `DateDeletedUtc` DATETIME NULL DEFAULT NULL,
  `TaskName` VARCHAR(512) NOT NULL COLLATE 'utf8mb4_general_ci',
  `TaskCategory` VARCHAR(128) NOT NULL COLLATE 'utf8mb4_general_ci',
  `TaskSubCategory` VARCHAR(128) NOT NULL COLLATE 'utf8mb4_general_ci',
  `TaskDescription` TEXT NOT NULL COLLATE 'utf8mb4_general_ci',
  PRIMARY KEY (`TaskID`) USING BTREE,
  INDEX `UserID` (`UserID`) USING BTREE
) COLLATE = 'utf8mb4_general_ci' ENGINE = InnoDB;