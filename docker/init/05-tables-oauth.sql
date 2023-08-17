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
ENGINE=InnoDB
;
