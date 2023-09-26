INSERT INTO `Users`
	(`Email`, `PasswordHash`)
VALUES
	('niemand.richard@gmail.com', 'AQAAAAIAAYagAAAAEKGjbkZvWeUEDyWmUKEw7NFmLPKbDXSpIrquaQI6XcJCX4DAkA41pOqCmxaCPs7xbQ==');

INSERT INTO `Homes`
  (`DefaultHome`,`Longitude`,`Latitude`,`Country`,`PostalCode`,`City`,`Province`,`HomeName`,`AddressLine1`,`AddressLine2`)
VALUES
  (1,12,21,'ca','xxx','city','province','Home','address 1','address 2');

INSERT INTO `HomeFloors`
	(`HomeId`,`FloorName`)
SELECT
	h.HomeId,
	floors.*
FROM `Homes` h
INNER JOIN (VALUES ('Floor 1'),('Floor 2'),('Floor 3')) floors ON 1=1
WHERE h.DefaultHome = 1;

INSERT INTO `HomeRooms`
	(`FloorId`,`RoomName`)
SELECT
	hf.FloorId,
	rooms.`Name`
FROM `Homes` h
 INNER JOIN (with t(`Name`) as (VALUES ('Floor 1'),('Floor 2'),('Floor 3')) SELECT `Name` FROM t) floorNames ON 1=1
INNER JOIN `HomeFloors` hf ON hf.HomeId = h.HomeId AND hf.FloorName = floorNames.Name
INNER JOIN (WITH t(`Name`) AS (VALUES ('Room 1'),('Room 2'),('Room 3')) SELECT `Name` FROM t) rooms ON 1=1
WHERE h.DefaultHome = 1;
