
-- Seed sample link data
INSERT INTO `UserLinks`
  (`LinkOrder`, `FollowCount`, `LinkCategory`, `LinkName`, `LinkImage`, `LinkUrl`)
VALUES
  -- Coding related links
  (256, 0, 'coding', 'Github', 'github.png', 'https://github.com/rniemand?tab=repositories'),
  (1024, 0, 'coding', 'EasyEDA', 'easy-eda.png', 'https://easyeda.com/editor'),

  -- Docker related links
  (10, 0, 'docker', 'Crashplan', 'crashplan.png', 'http://192.168.0.60:7810/'),

  -- Monitoring related links
  (1, 0, 'monitoring', 'HomeAssistant', 'hass.png', 'https://192.168.0.60/'),
  (256, 0, 'monitoring', 'Grafana', 'grafana.png', 'http://192.168.0.60:3000/'),
  (257, 0, 'monitoring', 'InfluxDB 2', 'influxdb.png', 'http://192.168.0.60:9086/signin'),
  
  -- Network related links
  (1, 0, 'netowrking', 'FreeDNS', 'dns.jpg', 'https://freedns.afraid.org/subdomain/');
