CREATE TABLE `UserLinks` (
	`LinkId` INT(11) NOT NULL AUTO_INCREMENT,
	`LinkOrder` SMALLINT(6) NOT NULL DEFAULT '1024',
	`FollowCount` INT(11) NOT NULL DEFAULT '0',
	`DateAddedUtc` DATETIME NOT NULL DEFAULT utc_timestamp(6),
	`DateUpdatedUtc` DATETIME NOT NULL DEFAULT utc_timestamp(6),
	`LinkName` VARCHAR(128) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
	`LinkCategory` VARCHAR(128) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
	`LinkSubCategory` VARCHAR(128) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
	`LinkUrl` VARCHAR(2048) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
	`LinkImage` VARCHAR(2048) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`LinkId`) USING BTREE,
	INDEX `LinkCategory` (`LinkCategory`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB;
