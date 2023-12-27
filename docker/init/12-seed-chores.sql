INSERT INTO `HomeChores`
  (`RoomId`, `ChorePoints`, `Priority`, `IntervalModifier`, `Interval`, `ChoreName`, `ChoreDescription`)
VALUES
  -- Main Home | Basement | Richard Office
  (1, 1, 'low',   'Weeks',        '1',        'Weekly Chore 1',             'Occurs every week'),
  -- Main Home | Basement | Server Room
  (2, 1, 'low',   'DaysOfWeek',   'Mon,Fri',  'Server Room Chore 1',        'Occurs every Monday and Friday'),
  (2, 1, 'low',   'DaysOfMonth',  '1,15',     'Clean server fans',          'Ensure that there is no dust'),
  -- Main Home | Basement | Store Room
  (3, 1, 'med',   'DaysOfMonth',  '1,15',     'Store Room Chore 1',         'Occurs on the 1st and 15th of every month'),
  -- Main Home | Main Floor | Dining Room
  (4, 1, 'high',  'Days',         '2',        'Clean Dining Room',          'Occurs every second day'),
  -- Main Home | Main Floor | Kitchen
  (6, 1, 'low',   'Days',         '2',        'Chore with no description',  ''),
  -- Main Home | Main Floor | Entryway
  (7, 1, 'med',   'Weeks',        '2',        'Clean Door Glass',           'Occurs every 2 weeks');
