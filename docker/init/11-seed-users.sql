INSERT INTO `Users`
	(`CurrentHomeID`, `Email`, `FirstName`, `Surname`, `PasswordHash`)
VALUES
	(1, 'niemand.richard@gmail.com', 'Richard', 'Niemand', ''),  -- 1
  (1, '1@2.com', 'Kelsie', 'Mackie', ''),                      -- 2
  (1, '2@2.com', 'Sam', 'Niemand', '');                        -- 3

INSERT INTO `UserHomeMappings`
  (`UserID`, `HomeID`)
VALUES
  (1, 1),
  (1, 2),
  (2, 1),
  (2, 2),
  (3, 1);