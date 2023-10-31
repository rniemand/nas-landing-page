-- =============================================================================
-- Homes
-- =============================================================================
INSERT INTO `Homes`
  (`Longitude`,`Latitude`,`Country`,`PostalCode`,`City`,`Province`,`HomeName`,`AddressLine1`,`AddressLine2`)
VALUES
  (12, 21,'ca','xxx','city','province','Home (Default)','address 1','address 2'), -- 1
  (12, 21,'ca','xxx','city','province','Home 2','address 1','address 2');         -- 2

-- =============================================================================
-- Users
-- =============================================================================
INSERT INTO `Users`
	(`CurrentHomeID`, `Email`, `FirstName`, `Surname`, `PasswordHash`)
VALUES
	(1, 'niemand.richard@gmail.com', 'Richard', 'Niemand', ''),  -- 1
  (1, '1@2.com', 'Kelsie', 'Mackie', ''),                      -- 2
  (1, '2@2.com', 'Sam', 'Niemand', '');                        -- 3

-- =============================================================================
-- User Home Mappings
-- =============================================================================
INSERT INTO `UserHomeMappings`
  (`UserID`, `HomeID`)
VALUES
  (1, 1),
  (1, 2),
  (2, 1),
  (2, 2),
  (3, 1);

-- =============================================================================
-- Home Floors
-- =============================================================================
INSERT INTO `HomeFloors`
  (`HomeId`, `FloorName`)
VALUES
  (1, 'Basement'),   -- 1
  (1, 'Main Floor'), -- 2
  (1, 'Upstairs'),   -- 3
  (2, 'Basement'),   -- 4
  (2, 'Main Floor'), -- 5
  (2, 'Upstairs');   -- 6

-- =============================================================================
-- Home Rooms
-- =============================================================================
INSERT INTO `HomeRooms`
  (`FloorId`, `RoomName`)
VALUES
  (1, 'Richard Office'),    -- 1
  (1, 'Server Room'),       -- 2
  (1, 'Store Room'),        -- 3
  (2, 'Lounge'),            -- 4
  (2, 'Dining Room'),       -- 5
  (2, 'Kitchen'),           -- 6
  (2, 'Entryway'),          -- 7
  (3, 'Master Bedroom'),    -- 8
  (3, 'Master Washroom'),   -- 9
  (3, 'Office'),            -- 10
  (3, 'Sam Room'),          -- 11
  (4, 'Basement Room 1'),   -- 12
  (4, 'Basement Room 2'),   -- 13
  (5, 'Mian Floor Room 1'), -- 14
  (5, 'Mian Floor Room 2'), -- 15
  (6, 'Upstairs Room 1'),   -- 16
  (6, 'Upstairs Room 2');   -- 17

-- =============================================================================
-- Home Chores
-- =============================================================================
INSERT INTO `HomeChores`
  (`RoomId`, `ChorePoints`, `Priority`, `IntervalModifier`, `Interval`, `ChoreName`, `ChoreDescription`)
VALUES
  -- Main Home | Basement | Richard Office
  (1, 1, 'low',   'Weeks',        '1',        'Weekly Chore 1',       'Occurs every week'),
  -- Main Home | Basement | Server Room
  (2, 1, 'low',   'DaysOfWeek',   'Mon,Fri',  'Server Room Chore 1',  'Occurs every Monday and Friday'),
  -- Main Home | Basement | Store Room
  (3, 1, 'med',   'DaysOfMonth',  '1,15',     'Store Room Chore 1',   'Occurs on the 1st and 15th of every month'),
  -- Main Home | Main Floor | Dining Room
  (4, 1, 'high',  'Days',         '2',        'Clean Dining Room',    'Occurs every second day'),
  -- Main Home | Main Floor | Entryway
  (7, 1, 'med',   'Weeks',        '2',        'Clean Door Glass',     'Occurs every 2 weeks')
;
