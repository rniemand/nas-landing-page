INSERT INTO `Homes`
  (`Longitude`,`Latitude`,`Country`,`PostalCode`,`City`,`Province`,`HomeName`,`AddressLine1`,`AddressLine2`)
VALUES
  (12, 21,'ca','xxx','city','province','Home (Default)','address 1','address 2'), -- 1
  (12, 21,'ca','xxx','city','province','Home 2','address 1','address 2');         -- 2

INSERT INTO `HomeFloors`
  (`HomeId`, `FloorName`)
VALUES
  (1, 'Basement'),   -- 1
  (1, 'Main Floor'), -- 2
  (1, 'Upstairs'),   -- 3
  (2, 'Basement'),   -- 4
  (2, 'Main Floor'), -- 5
  (2, 'Upstairs');   -- 6

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