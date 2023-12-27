CREATE TABLE `Homes` (
  `HomeId` INT(11) NOT NULL AUTO_INCREMENT,
  `Longitude` DOUBLE NOT NULL DEFAULT '0',
  `Latitude` DOUBLE NOT NULL DEFAULT '0',
  `DateAdded` DATETIME NOT NULL DEFAULT utc_timestamp(6),
  `DateDeleted` DATETIME NULL DEFAULT NULL,
  `Country` VARCHAR(32) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
  `PostalCode` VARCHAR(32) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
  `City` VARCHAR(64) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
  `Province` VARCHAR(64) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
  `HomeName` VARCHAR(64) NOT NULL COLLATE 'utf8mb4_general_ci',
  `AddressLine1` VARCHAR(512) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
  `AddressLine2` VARCHAR(512) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
  PRIMARY KEY (`HomeId`) USING BTREE
) COLLATE = 'utf8mb4_general_ci' ENGINE = InnoDB;

CREATE TABLE `Users` (
	`UserID` INT(11) NOT NULL AUTO_INCREMENT,
	`CurrentHomeID` INT(11) NOT NULL,
	`CanSetPass` BIT(1) NOT NULL DEFAULT b'0',
	`DateAdded` DATE NOT NULL DEFAULT curdate(),
	`DateDeleted` DATE NULL DEFAULT NULL,
	`Email` VARCHAR(256) NOT NULL COLLATE 'utf8mb4_general_ci',
	`PasswordHash` VARCHAR(128) NOT NULL COLLATE 'utf8mb4_general_ci',
	`FirstName` VARCHAR(128) NOT NULL COLLATE 'utf8mb4_general_ci',
	`Surname` VARCHAR(128) NOT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`UserID`) USING BTREE
) COLLATE='utf8mb4_general_ci' ENGINE=InnoDB;

CREATE TABLE `HomeFloors` (
  `FloorId` INT(11) NOT NULL AUTO_INCREMENT,
  `HomeId` INT(11) NOT NULL,
  `DateAdded` DATETIME NOT NULL DEFAULT utc_timestamp(6),
  `DateDeleted` DATETIME NULL DEFAULT NULL,
  `FloorName` VARCHAR(128) NOT NULL DEFAULT '' COLLATE 'utf8mb3_general_ci',
  PRIMARY KEY (`FloorId`) USING BTREE
) COLLATE = 'utf8mb3_general_ci' ENGINE = InnoDB;

CREATE TABLE `HomeRooms` (
  `RoomId` INT(11) NOT NULL AUTO_INCREMENT,
  `FloorId` INT(11) NOT NULL DEFAULT '0',
  `DateAdded` DATETIME NOT NULL DEFAULT utc_timestamp(6),
  `DateDeleted` DATETIME NULL DEFAULT NULL,
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

CREATE TABLE `UserHomeMappings` (
	`UserID` INT(11) NOT NULL,
	`HomeID` INT(11) NOT NULL,
	`DateAdded` DATE NOT NULL DEFAULT curdate(),
	`DateDeleted` DATE NULL DEFAULT NULL,
	INDEX `UserID` (`UserID`) USING BTREE,
	INDEX `HomeID` (`HomeID`) USING BTREE
) COLLATE='utf8mb4_general_ci' ENGINE=InnoDB;

CREATE TABLE `UserLinks` (
  `LinkId` INT(11) NOT NULL AUTO_INCREMENT,
  `UserID` INT(11) NOT NULL DEFAULT '0',
  `Deleted` BIT(1) NOT NULL DEFAULT b'0',
  `LinkOrder` SMALLINT(6) NOT NULL DEFAULT '1024',
  `FollowCount` INT(11) NOT NULL DEFAULT '0',
  `DateAdded` DATETIME NOT NULL DEFAULT utc_timestamp(6),
  `DateUpdated` DATETIME NOT NULL DEFAULT utc_timestamp(6),
  `DateLastFollowed` DATETIME NOT NULL DEFAULT utc_timestamp(6),
  `LinkName` VARCHAR(128) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
  `LinkCategory` VARCHAR(128) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
  `LinkUrl` VARCHAR(2048) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
  `LinkImage` VARCHAR(2048) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
  PRIMARY KEY (`LinkId`) USING BTREE,
  INDEX `LinkCategory` (`LinkCategory`) USING BTREE,
  INDEX `Deleted` (`Deleted`) USING BTREE,
  INDEX `UserID` (`UserID`) USING BTREE
) COLLATE = 'utf8mb4_general_ci' ENGINE = InnoDB;

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

CREATE TABLE `GitHubRepoContent` (
	`RepoId` BIGINT(20) NOT NULL,
	`Deleted` BIT(1) NOT NULL DEFAULT b'0',
	`ContentSize` BIGINT(20) NOT NULL DEFAULT '0',
	`ContentType` VARCHAR(8) NOT NULL COLLATE 'utf8mb4_general_ci',
	`DateAdded` DATETIME NOT NULL DEFAULT utc_timestamp(6),
	`DateUpdated` DATETIME NULL DEFAULT NULL,
	`ContentSha` VARCHAR(64) NOT NULL COLLATE 'utf8mb4_general_ci',
	`ContentName` VARCHAR(128) NOT NULL COLLATE 'utf8mb4_general_ci',
	`ContentPath` VARCHAR(256) NOT NULL COLLATE 'utf8mb4_general_ci',
	`HtmlUrl` VARCHAR(512) NOT NULL COLLATE 'utf8mb4_general_ci',
	INDEX `RepoId` (`RepoId`) USING BTREE,
	INDEX `Deleted` (`Deleted`) USING BTREE,
	INDEX `ContentType` (`ContentType`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB;

CREATE TABLE `UserTasks` (
  `TaskID` INT(11) NOT NULL AUTO_INCREMENT,
  `UserID` INT(11) NOT NULL DEFAULT '0',
  `TaskPriority` INT(11) NOT NULL DEFAULT '256',
  `DateAdded` DATETIME NOT NULL DEFAULT utc_timestamp(6),
  `DateCompleted` DATETIME NULL DEFAULT NULL,
  `DateDeleted` DATETIME NULL DEFAULT NULL,
  `TaskName` VARCHAR(512) NOT NULL COLLATE 'utf8mb4_general_ci',
  `TaskCategory` VARCHAR(128) NOT NULL COLLATE 'utf8mb4_general_ci',
  `TaskSubCategory` VARCHAR(128) NOT NULL COLLATE 'utf8mb4_general_ci',
  `TaskDescription` TEXT NOT NULL COLLATE 'utf8mb4_general_ci',
  PRIMARY KEY (`TaskID`) USING BTREE,
  INDEX `UserID` (`UserID`) USING BTREE
) COLLATE = 'utf8mb4_general_ci' ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `Games` (
  `GameID` INT(11) NOT NULL AUTO_INCREMENT,
  `PlatformID` INT(11) NOT NULL,
  `LocationID` INT(11) NOT NULL,
  `HasGameBox` BIT(1) NOT NULL DEFAULT b'0',
  `HasProtection` BIT(1) NOT NULL DEFAULT b'0',
  `GameRating` TINYINT(4) NOT NULL DEFAULT '0',
  `GamePrice` DOUBLE NOT NULL DEFAULT '0',
  `GameName` VARCHAR(256) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
  `GameCaseLocation` VARCHAR(32) NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
  PRIMARY KEY (`GameID`) USING BTREE,
  INDEX `PlatformID` (`PlatformID`) USING BTREE,
  INDEX `LocationID` (`LocationID`) USING BTREE
) COLLATE = 'utf8mb4_general_ci' ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `GameImages` (
  `GameID` bigint(20) NOT NULL,
  `ImageType` varchar(128) NOT NULL,
  `ImageOrder` smallint(6) NOT NULL DEFAULT 256,
  `ImagePath` varchar(512) NOT NULL,
  KEY `GameID` (`GameID`)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_general_ci;

CREATE TABLE IF NOT EXISTS `GameLocations` (
  `LocationID` int(11) NOT NULL AUTO_INCREMENT,
  `PlatformID` int(11) NOT NULL,
  `LocationName` varchar(256) NOT NULL,
  PRIMARY KEY (`LocationID`) USING BTREE,
  KEY `PlatformID` (`PlatformID`) USING BTREE
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_general_ci;

CREATE TABLE IF NOT EXISTS `GamePlatforms` (
  `PlatformID` int(11) NOT NULL AUTO_INCREMENT,
  `PlatformName` varchar(128) NOT NULL,
  PRIMARY KEY (`PlatformID`) USING BTREE
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_general_ci;

CREATE TABLE IF NOT EXISTS `GameReceipts` (
  `ReceiptID` int(11) NOT NULL AUTO_INCREMENT,
  `HaveReceipt` bit(1) NOT NULL DEFAULT b'0',
  `ReceiptScanned` bit(1) NOT NULL DEFAULT b'0',
  `ReceiptAssociated` bit(1) NOT NULL DEFAULT b'0',
  `ReceiptDate` date DEFAULT NULL,
  `Store` varchar(256) DEFAULT NULL,
  `ReceiptNumber` varchar(64) DEFAULT NULL,
  `ReceiptUrl` varchar(512) DEFAULT NULL,
  `ReceiptName` varchar(32) DEFAULT NULL,
  PRIMARY KEY (`ReceiptID`)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_general_ci;

CREATE TABLE `OAuth` (
	`OAuthType` VARCHAR(64) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`ClientID` VARCHAR(16) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`ClientSecret` VARCHAR(64) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`PkceCodeVerifier` VARCHAR(256) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`PkceCodeChallenge` VARCHAR(64) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`State` VARCHAR(64) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`RedirectUri` VARCHAR(128) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`AuthorizationCode` VARCHAR(128) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`TokenUrl` VARCHAR(128) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`AuthUrl` VARCHAR(128) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`AccessToken` TEXT NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`RefreshToken` VARCHAR(128) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`TokenExpiryDate` DATETIME NULL DEFAULT NULL
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB;

CREATE TABLE `FitBitSummaryData` (
	`UserId` INT(11) NOT NULL DEFAULT '0',
	`Date` DATE NOT NULL,
	`CaloriesOut` INT(11) NOT NULL DEFAULT '0',
	`Distance` FLOAT NOT NULL DEFAULT '0',
	`Elevation` FLOAT NOT NULL DEFAULT '0',
	`Floors` INT(11) NOT NULL DEFAULT '0',
	`LightlyActiveMinutes` INT(11) NOT NULL DEFAULT '0',
	`MarginalCalories` INT(11) NOT NULL DEFAULT '0',
	`RestingHeartRate` INT(11) NOT NULL DEFAULT '0',
	`SedentaryMinutes` INT(11) NOT NULL DEFAULT '0',
	`Steps` INT(11) NOT NULL DEFAULT '0',
	`VeryActiveMinutes` INT(11) NOT NULL DEFAULT '0',
	INDEX `UserId` (`UserId`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB;

CREATE TABLE `NetworkDevices` (
	`DeviceID` INT(11) NOT NULL AUTO_INCREMENT,
	`IsPhysical` BIT(1) NOT NULL DEFAULT b'1',
	`IsActive` BIT(1) NOT NULL DEFAULT b'1',
	`DeviceName` VARCHAR(64) NOT NULL COLLATE 'utf8mb4_general_ci',
	`Floor` VARCHAR(64) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`Room` VARCHAR(64) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`RoomLocation` VARCHAR(64) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`DeviceID`) USING BTREE,
	INDEX `IsPhysical` (`IsPhysical`) USING BTREE,
	INDEX `IsActive` (`IsActive`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB;

CREATE TABLE `NetworkClassification` (
	`DeviceID` INT(11) NOT NULL,
	`Category` VARCHAR(64) NOT NULL COLLATE 'utf8mb4_general_ci',
	`SubCategory` VARCHAR(64) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`Manufacturer` VARCHAR(64) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`Model` VARCHAR(64) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	INDEX `Category` (`Category`) USING BTREE,
	INDEX `SubCategory` (`SubCategory`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB;

CREATE TABLE `NetworkIPv4` (
	`DeviceID` INT(11) NOT NULL,
	`MacAddress` VARCHAR(64) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`IPv4` VARCHAR(16) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`IPv4Int` BIGINT(20) NULL DEFAULT NULL,
	`Connection` VARCHAR(4) NOT NULL DEFAULT 'LAN' COLLATE 'utf8mb4_general_ci',
	`NetworkName` VARCHAR(64) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	INDEX `DeviceID` (`DeviceID`) USING BTREE,
	INDEX `Connection` (`Connection`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB;

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

CREATE TABLE `ShoppingList` (
	`ItemId` BIGINT(20) NOT NULL AUTO_INCREMENT,
	`HomeId` INT(11) NOT NULL,
	`AddedByUserId` INT(11) NOT NULL,
	`LastKnownPrice` DECIMAL(20,6) NULL DEFAULT NULL,
	`Quantity` INT(11) NOT NULL DEFAULT '1',
	`DateAdded` DATE NOT NULL DEFAULT curdate(),
	`DateDeleted` DATE NULL DEFAULT NULL,
	`DatePurchased` DATE NULL DEFAULT NULL,
	`StoreName` VARCHAR(128) NOT NULL COLLATE 'utf8mb4_general_ci',
	`Category` VARCHAR(128) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`ItemName` VARCHAR(512) NOT NULL COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ItemId`) USING BTREE,
	INDEX `HomeId` (`HomeId`) USING BTREE,
	INDEX `AddedByUserId` (`AddedByUserId`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB;
