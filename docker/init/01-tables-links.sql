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
