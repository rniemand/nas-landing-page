CREATE TABLE IF NOT EXISTS `UserLinks` (
  `LinkId` int(11) NOT NULL AUTO_INCREMENT,
  `Deleted` bit(1) NOT NULL DEFAULT b'0',
  `LinkOrder` smallint(6) NOT NULL DEFAULT 1024,
  `FollowCount` int(11) NOT NULL DEFAULT 0,
  `DateAddedUtc` datetime NOT NULL DEFAULT utc_timestamp(6),
  `DateUpdatedUtc` datetime NOT NULL DEFAULT utc_timestamp(6),
  `DateLastFollowedUtc` datetime NOT NULL DEFAULT utc_timestamp(6),
  `LinkName` varchar(128) NOT NULL DEFAULT '',
  `LinkCategory` varchar(128) NOT NULL DEFAULT '',
  `LinkUrl` varchar(2048) NOT NULL DEFAULT '',
  `LinkImage` varchar(2048) NOT NULL DEFAULT '',
  PRIMARY KEY (`LinkId`) USING BTREE,
  KEY `LinkCategory` (`LinkCategory`) USING BTREE,
  KEY `Deleted` (`Deleted`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

INSERT INTO `UserLinks` (`Deleted`, `LinkOrder`, `FollowCount`, `DateAddedUtc`, `DateUpdatedUtc`, `DateLastFollowedUtc`, `LinkName`, `LinkCategory`, `LinkUrl`, `LinkImage`) VALUES
	(b'0', 256, 11, '2022-06-27 22:12:44', '2022-06-27 22:12:44', '2022-12-22 21:49:05', 'Github', 'coding', 'https://github.com/rniemand?tab=repositories', 'github.png'),
	(b'0', 1024, 51, '2022-06-27 22:16:53', '2022-06-27 22:16:53', '2023-05-19 18:57:25', 'Crashplan', 'daily', 'http://192.168.0.60:7810/', 'crashplan.png'),
	(b'0', 1024, 1, '2022-06-27 22:21:21', '2022-06-27 22:21:21', '2022-09-30 21:11:11', 'EasyEDA', 'coding', 'https://easyeda.com/editor', 'easy-eda.png'),
  (b'0', 1024, 1, '2022-06-27 22:21:21', '2022-06-27 22:21:21', '2022-09-30 21:11:11', 'EasyEDA', 'coding', 'https://easyeda.com/editor', 'easy-eda.png'),
  (b'0', 1024, 1, '2022-06-27 22:21:21', '2022-06-27 22:21:21', '2022-09-30 21:11:11', 'EasyEDA', 'coding', 'https://easyeda.com/editor', 'easy-eda.png'),
  (b'0', 1024, 1, '2022-06-27 22:21:21', '2022-06-27 22:21:21', '2022-09-30 21:11:11', 'EasyEDA', 'coding', 'https://easyeda.com/editor', 'easy-eda.png'),
  (b'0', 1024, 1, '2022-06-27 22:21:21', '2022-06-27 22:21:21', '2022-09-30 21:11:11', 'EasyEDA', 'coding', 'https://easyeda.com/editor', 'easy-eda.png'),
  (b'0', 1024, 1, '2022-06-27 22:21:21', '2022-06-27 22:21:21', '2022-09-30 21:11:11', 'EasyEDA', 'coding', 'https://easyeda.com/editor', 'easy-eda.png'),
  (b'0', 1024, 1, '2022-06-27 22:21:21', '2022-06-27 22:21:21', '2022-09-30 21:11:11', 'EasyEDA', 'coding', 'https://easyeda.com/editor', 'easy-eda.png'),
  (b'0', 1024, 1, '2022-06-27 22:21:21', '2022-06-27 22:21:21', '2022-09-30 21:11:11', 'EasyEDA', 'coding', 'https://easyeda.com/editor', 'easy-eda.png'),
  (b'0', 1024, 1, '2022-06-27 22:21:21', '2022-06-27 22:21:21', '2022-09-30 21:11:11', 'EasyEDA', 'coding', 'https://easyeda.com/editor', 'easy-eda.png'),
  (b'0', 1024, 1, '2022-06-27 22:21:21', '2022-06-27 22:21:21', '2022-09-30 21:11:11', 'EasyEDA', 'coding', 'https://easyeda.com/editor', 'easy-eda.png'),
  (b'0', 1024, 1, '2022-06-27 22:21:21', '2022-06-27 22:21:21', '2022-09-30 21:11:11', 'EasyEDA', 'coding', 'https://easyeda.com/editor', 'easy-eda.png'),
  (b'0', 1024, 1, '2022-06-27 22:21:21', '2022-06-27 22:21:21', '2022-09-30 21:11:11', 'EasyEDA', 'coding', 'https://easyeda.com/editor', 'easy-eda.png'),
  (b'0', 1024, 1, '2022-06-27 22:21:21', '2022-06-27 22:21:21', '2022-09-30 21:11:11', 'EasyEDA', 'coding', 'https://easyeda.com/editor', 'easy-eda.png'),
	(b'0', 1024, 0, '2022-06-27 22:21:21', '2022-06-27 22:21:21', '2022-06-27 22:21:21', 'FreeDNS', 'networking', 'https://freedns.afraid.org/subdomain/', 'dns.jpg'),
	(b'0', 1024, 5, '2022-06-27 22:23:47', '2022-06-27 22:23:47', '2022-08-20 15:54:19', 'Octoprint', 'servers', 'http://192.168.0.241/', 'octoprint.png'),
	(b'0', 1024, 15, '2022-06-27 22:26:13', '2022-06-27 22:26:13', '2022-12-19 18:31:31', 'Portainer (Ubuntu)', 'servers', 'https://192.168.0.101:9443/#!/home', 'portainer.png'),
	(b'0', 1024, 2, '2022-06-27 22:28:15', '2022-06-27 22:28:15', '2023-02-22 20:06:06', 'replit', 'coding', 'https://replit.com/~', 'replit.png');

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
ENGINE=InnoDB
;

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
ENGINE=InnoDB
;

CREATE TABLE `Users` (
	`UserID` INT(11) NOT NULL AUTO_INCREMENT,
	`Email` VARCHAR(256) NOT NULL COLLATE 'utf8mb4_general_ci',
	`PasswordHash` VARCHAR(128) NOT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`UserID`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;

INSERT INTO `Users`
	(`Email`, `PasswordHash`)
VALUES
	('niemand.richard@gmail.com', '');