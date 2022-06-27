CREATE TABLE `UserLinks` (
	`LinkId` INT(11) NOT NULL AUTO_INCREMENT,
	`Deleted` BIT(1) NOT NULL DEFAULT b'0',
	`LinkOrder` SMALLINT(6) NOT NULL DEFAULT '1024',
	`FollowCount` INT(11) NOT NULL DEFAULT '0',
	`DateAddedUtc` DATETIME NOT NULL DEFAULT utc_timestamp(6),
	`DateUpdatedUtc` DATETIME NOT NULL DEFAULT utc_timestamp(6),
	`DateLastFollowedUtc` DATETIME NOT NULL DEFAULT utc_timestamp(6),
	`LinkName` VARCHAR(128) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
	`LinkCategory` VARCHAR(128) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
	`LinkUrl` VARCHAR(2048) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
	`LinkImage` VARCHAR(2048) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`LinkId`) USING BTREE,
	INDEX `LinkCategory` (`LinkCategory`) USING BTREE,
	INDEX `Deleted` (`Deleted`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB;

CREATE TABLE `Projects` (
	`ProjectId` INT(11) NOT NULL AUTO_INCREMENT,
	`Deleted` BIT(1) NOT NULL DEFAULT b'0',
	`Language` VARCHAR(128) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
	`ProjectName` VARCHAR(256) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
	`ProjectDescription` VARCHAR(1024) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
	`LastUpdatedUtc` DATETIME NOT NULL DEFAULT utc_timestamp(6),
	`DateAddedUtc` DATETIME NOT NULL DEFAULT utc_timestamp(6),
	PRIMARY KEY (`ProjectId`) USING BTREE,
	INDEX `Language` (`Language`) USING BTREE,
	INDEX `Deleted` (`Deleted`) USING BTREE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB;

CREATE TABLE `Repositories` (
	`ProjectRepoId` INT(11) NOT NULL AUTO_INCREMENT,
	`ProjectId` INT(11) NOT NULL DEFAULT '0',
	`ForkCount` INT(11) NOT NULL DEFAULT '0',
	`OpenIssueCount` INT(11) NOT NULL DEFAULT '0',
	`IsPublic` BIT(1) NOT NULL DEFAULT b'0',
	`RepoType` SMALLINT(6) NOT NULL DEFAULT '0',
	`RepoId` BIGINT(20) NOT NULL DEFAULT '0',
	`RepoSize` BIGINT(20) NOT NULL DEFAULT '0',
	`LastUpdatedUtc` DATETIME NOT NULL DEFAULT utc_timestamp(6),
	`DateAddedUtc` DATETIME NOT NULL DEFAULT utc_timestamp(6),
	`DefaultBranch` VARCHAR(64) NOT NULL DEFAULT 'master' COLLATE 'utf8mb4_general_ci',
	`FullName` VARCHAR(128) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
	`RepoUrl` VARCHAR(1024) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
	`HtmlUrl` VARCHAR(1024) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
	`CiCdUrl` VARCHAR(1024) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
	`GitUrl` VARCHAR(1024) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
	`SshUrl` VARCHAR(1024) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
	`ApiUrl` VARCHAR(1024) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ProjectRepoId`) USING BTREE,
	INDEX `IsPublic` (`IsPublic`) USING BTREE,
	INDEX `FK_ProjectRepo_Projects` (`ProjectId`) USING BTREE,
	CONSTRAINT `FK_ProjectRepo_Projects` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`ProjectId`) ON UPDATE NO ACTION ON DELETE NO ACTION
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB;

CREATE TABLE `SonarQubeInfo` (
	`SonarQubeInfoId` INT(11) NOT NULL AUTO_INCREMENT,
	`ProjectId` INT(11) NOT NULL DEFAULT '0',
	`Deleted` BIT(1) NOT NULL DEFAULT b'0',
	`SonarQubeId` VARCHAR(64) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
	`BadgeToken` VARCHAR(64) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
	`SonarQubeUrl` VARCHAR(1024) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
	`BadgesJson` TEXT NOT NULL DEFAULT '[]' COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`SonarQubeInfoId`) USING BTREE,
	INDEX `FK_SonarQubeInfo_Projects` (`ProjectId`) USING BTREE,
	INDEX `Deleted` (`Deleted`) USING BTREE,
	CONSTRAINT `FK_SonarQubeInfo_Projects` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`ProjectId`) ON UPDATE NO ACTION ON DELETE NO ACTION
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB;

CREATE TABLE `SourceCodeMaturity` (
	`ScmId` INT(11) NOT NULL AUTO_INCREMENT,
	`ProjectId` INT(11) NULL DEFAULT NULL,
	`SrcDir` BIT(1) NOT NULL DEFAULT b'0',
	`TestDir` BIT(1) NOT NULL DEFAULT b'0',
	`DocsDir` BIT(1) NOT NULL DEFAULT b'0',
	`BuildDir` BIT(1) NOT NULL DEFAULT b'0',
	`Readme` BIT(1) NOT NULL DEFAULT b'0',
	`PrTemplate` BIT(1) NOT NULL DEFAULT b'0',
	`EditorConfig` BIT(1) NOT NULL DEFAULT b'0',
	`CiInfo` BIT(1) NOT NULL DEFAULT b'0',
	`BuildScript` BIT(1) NOT NULL DEFAULT b'0',
	`TestScript` BIT(1) NOT NULL DEFAULT b'0',
	`GitAttributes` BIT(1) NOT NULL DEFAULT b'0',
	`CiVersion` VARCHAR(32) NOT NULL DEFAULT '0.0.0' COLLATE 'utf8mb4_general_ci',
	`License` VARCHAR(32) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
	`LicenseUrl` VARCHAR(1024) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
	`CiFlows` TEXT NOT NULL DEFAULT '[]' COLLATE 'utf8mb4_general_ci',
	`JsonDirectories` TEXT NOT NULL DEFAULT '{}' COLLATE 'utf8mb4_general_ci',
	`JsonFiles` TEXT NOT NULL DEFAULT '{}' COLLATE 'utf8mb4_general_ci',
	PRIMARY KEY (`ScmId`) USING BTREE,
	INDEX `FK_SourceCodeMaturity_Projects` (`ProjectId`) USING BTREE,
	CONSTRAINT `FK_SourceCodeMaturity_Projects` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`ProjectId`) ON UPDATE NO ACTION ON DELETE NO ACTION
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB;
