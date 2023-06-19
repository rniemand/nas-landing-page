CREATE TABLE `GitHubRepos` (
	`RepoID` BIGINT(20) NOT NULL,
	`RepoSize` BIGINT(20) NOT NULL DEFAULT '0',
	`IsTemplate` BIT(1) NOT NULL DEFAULT b'0',
	`IsPrivate` BIT(1) NOT NULL DEFAULT b'0',
	`IsFork` BIT(1) NOT NULL DEFAULT b'0',
	`HasIssues` BIT(1) NOT NULL DEFAULT b'0',
	`HasWiki` BIT(1) NOT NULL DEFAULT b'0',
	`HasDownloads` BIT(1) NOT NULL DEFAULT b'0',
	`HasPages` BIT(1) NOT NULL DEFAULT b'0',
	`IsArchived` BIT(1) NOT NULL DEFAULT b'0',
	`ForksCount` INT(11) NOT NULL DEFAULT '0',
	`StargazersCount` INT(11) NOT NULL DEFAULT '0',
	`OpenIssuesCount` INT(11) NOT NULL DEFAULT '0',
	`SubscribersCount` INT(11) NOT NULL DEFAULT '0',
	`PushedAt` DATETIME NOT NULL,
	`CreatedAt` DATETIME NOT NULL,
	`UpdatedAt` DATETIME NULL DEFAULT NULL,
	`License` VARCHAR(32) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
	`DefaultBranch` VARCHAR(64) NOT NULL COLLATE 'utf8mb4_general_ci',
	`GitUrl` VARCHAR(256) NOT NULL COLLATE 'utf8mb4_general_ci',
	`SshUrl` VARCHAR(256) NOT NULL COLLATE 'utf8mb4_general_ci',
	`Name` VARCHAR(128) NOT NULL COLLATE 'utf8mb4_general_ci',
	`FullName` VARCHAR(128) NOT NULL COLLATE 'utf8mb4_general_ci',
	`Description` VARCHAR(512) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	INDEX `License` (`License`) USING BTREE,
	INDEX `IsTemplate` (`IsTemplate`) USING BTREE,
	INDEX `IsPrivate` (`IsPrivate`) USING BTREE,
	INDEX `IsArchived` (`IsArchived`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB;

