
-- Seed sample link data
INSERT INTO `UserLinks`
  (`LinkOrder`, `FollowCount`, `LinkCategory`, `LinkSubCategory`, `LinkName`, `LinkImage`, `LinkUrl`)
VALUES
  -- Coding related links
  (256, 0, 'coding', 'repos', 'Github', 'github.png', 'https://github.com/rniemand?tab=repositories'),
  (1024, 0, 'coding', 'pcb', 'EasyEDA', 'easy-eda.png', 'https://easyeda.com/editor'),

  -- Docker related links
  (10, 0, 'docker', 'backup', 'Crashplan', 'crashplan.png', 'http://192.168.0.60:7810/'),

  -- Monitoring related links
  (1, 0, 'monitoring', 'home-automation', 'HomeAssistant', 'hass.png', 'https://192.168.0.60/'),
  (256, 0, 'monitoring', 'dashboards', 'Grafana', 'grafana.png', 'http://192.168.0.60:3000/'),
  (257, 0, 'monitoring', 'tsdb', 'InfluxDB 2', 'influxdb.png', 'http://192.168.0.60:9086/signin'),
  
  -- Network related links
  (1, 0, 'netowrking', 'dns', 'FreeDNS', 'dns.jpg', 'https://freedns.afraid.org/subdomain/');
