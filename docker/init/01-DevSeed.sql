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

-- Dumping data for table NasLandingPage.UserLinks: ~46 rows (approximately)
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
