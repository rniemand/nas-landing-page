INSERT INTO `Users`
	(`Email`, `FirstName`, `Surname`, `PasswordHash`)
VALUES
	('niemand.richard@gmail.com', 'Richard', 'Niemand', ''),
  ('1@2.com', 'Kelsie', 'Mackie', ''),
  ('2@2.com', 'Sam', 'Niemand', '');

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


INSERT INTO `HomeChores`
	(`RoomId`,`ChorePoints`,`DateScheduled`,`Priority`,`IntervalModifier`,`Interval`,`ChoreName`,`ChoreDescription`)
VALUES
	(1,1,'2023-10-17 00:00:00','low','Weeks','1','Weekly Chore 1','Occurs once a week on Mondays'),
	(2,1,'2023-10-17 00:00:00','med','DaysOfMonth','1','Monthly Chore 1','Occurs on the first of every month'),
	(3,1,'2023-10-17 00:00:00','high','DaysOfWeek','mon,wed,fri','3 Times a week every week','Occurs on every Monday, Wednesday and Friday'),
	(4,1,'2023-10-17 00:00:00','med','Weeks','2','Every 2nd week from completion date','Occurs exactly 2 weeks after the last completion date'),
  (5,1,'2023-10-17 00:00:00','low','DaysOfMonth','15','Every 15th of a month','Occurs on the 15th of every month'),
  (1,1,'2023-10-17 00:00:00','low','Weeks','1','Weekly Chore 1','Occurs once a week on Mondays'),
	(2,1,'2023-10-17 00:00:00','med','DaysOfMonth','1','Monthly Chore 1','Occurs on the first of every month'),
	(3,1,'2023-10-17 00:00:00','high','DaysOfWeek','mon,wed,fri','3 Times a week every week','Occurs on every Monday, Wednesday and Friday'),
	(4,1,'2023-10-17 00:00:00','med','Weeks','2','Every 2nd week from completion date','Occurs exactly 2 weeks after the last completion date'),
  (5,1,'2023-10-17 00:00:00','low','DaysOfMonth','15','Every 15th of a month','Occurs on the 15th of every month'),
  (1,1,'2023-10-17 00:00:00','low','Weeks','1','Weekly Chore 1','Occurs once a week on Mondays'),
	(2,1,'2023-10-17 00:00:00','med','DaysOfMonth','1','Monthly Chore 1','Occurs on the first of every month'),
	(3,1,'2023-10-17 00:00:00','high','DaysOfWeek','mon,wed,fri','3 Times a week every week','Occurs on every Monday, Wednesday and Friday'),
	(4,1,'2023-10-17 00:00:00','med','Weeks','2','Every 2nd week from completion date','Occurs exactly 2 weeks after the last completion date'),
  (5,1,'2023-10-17 00:00:00','low','DaysOfMonth','15','Every 15th of a month','Occurs on the 15th of every month'),
  (1,1,'2023-10-17 00:00:00','low','Weeks','1','Weekly Chore 1','Occurs once a week on Mondays'),
	(2,1,'2023-10-17 00:00:00','med','DaysOfMonth','1','Monthly Chore 1','Occurs on the first of every month'),
	(3,1,'2023-10-17 00:00:00','high','DaysOfWeek','mon,wed,fri','3 Times a week every week','Occurs on every Monday, Wednesday and Friday'),
	(4,1,'2023-10-17 00:00:00','med','Weeks','2','Every 2nd week from completion date','Occurs exactly 2 weeks after the last completion date'),
  (5,1,'2023-10-17 00:00:00','low','DaysOfMonth','15','Every 15th of a month','Occurs on the 15th of every month'),
  (1,1,'2023-10-17 00:00:00','low','Weeks','1','Weekly Chore 1','Occurs once a week on Mondays'),
	(2,1,'2023-10-17 00:00:00','med','DaysOfMonth','1','Monthly Chore 1','Occurs on the first of every month'),
	(3,1,'2023-10-17 00:00:00','high','DaysOfWeek','mon,wed,fri','3 Times a week every week','Occurs on every Monday, Wednesday and Friday'),
	(4,1,'2023-10-17 00:00:00','med','Weeks','2','Every 2nd week from completion date','Occurs exactly 2 weeks after the last completion date'),
  (5,1,'2023-10-17 00:00:00','low','DaysOfMonth','15','Every 15th of a month','Occurs on the 15th of every month'),
  (1,1,'2023-10-17 00:00:00','low','Weeks','1','Weekly Chore 1','Occurs once a week on Mondays'),
	(2,1,'2023-10-17 00:00:00','med','DaysOfMonth','1','Monthly Chore 1','Occurs on the first of every month'),
	(3,1,'2023-10-17 00:00:00','high','DaysOfWeek','mon,wed,fri','3 Times a week every week','Occurs on every Monday, Wednesday and Friday'),
	(4,1,'2023-10-17 00:00:00','med','Weeks','2','Every 2nd week from completion date','Occurs exactly 2 weeks after the last completion date'),
  (5,1,'2023-10-17 00:00:00','low','DaysOfMonth','15','Every 15th of a month','Occurs on the 15th of every month');
