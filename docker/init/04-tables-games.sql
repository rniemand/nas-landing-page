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
