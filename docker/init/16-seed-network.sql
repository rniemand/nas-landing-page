-- =================================================================================
-- NetworkDevices
-- =================================================================================
INSERT INTO `NetworkDevices`
  (`IsPhysical`, `IsActive`, `DeviceName`, `Floor`, `Room`, `RoomLocation`)
VALUES
  (TRUE, TRUE, 'NAS', 'Basement', 'Server Room', NULL); -- 1

-- =================================================================================
-- NetworkClassification
-- =================================================================================
INSERT INTO `NetworkClassification`
  (`DeviceID`, `Category`, `SubCategory`, `Manufacturer`, `Model`)
VALUES
  (1, 'Server', NULL, 'HP', 'Custom');

-- =================================================================================
-- NetworkIPv4
-- =================================================================================
INSERT INTO `NetworkIPv4`
  (`DeviceID`, `MacAddress`, `IPv4`, `IPv4Int`, `Connection`, `NetworkName`)
VALUES
  (1, '52:52:52:52:52:52', '192.168.0.60', INET_ATON('192.168.0.60'), 'LAN', '-');
