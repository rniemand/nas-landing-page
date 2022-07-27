-- Seed sample link data
INSERT INTO `UserLinks`
  (`LinkOrder`, `FollowCount`, `LinkCategory`, `LinkName`, `LinkImage`, `LinkUrl`)
VALUES
  (256, 0, 'coding', 'Github', 'github.png', 'https://github.com/rniemand?tab=repositories'),
  (1024, 0, 'coding', 'EasyEDA', 'easy-eda.png', 'https://easyeda.com/editor'),
  (10, 0, 'docker', 'Crashplan', 'crashplan.png', 'http://192.168.0.60:7810/'),
  (1, 0, 'monitoring', 'HomeAssistant', 'hass.png', 'https://192.168.0.60/'),
  (256, 0, 'monitoring', 'Grafana', 'grafana.png', 'http://192.168.0.60:3000/'),
  (257, 0, 'monitoring', 'InfluxDB 2', 'influxdb.png', 'http://192.168.0.60:9086/signin'),
  (1, 0, 'netowrking', 'FreeDNS', 'dns.jpg', 'https://freedns.afraid.org/subdomain/');

-- Seed test projects
INSERT INTO `Projects`
  (`Language`, `ProjectName`, `ProjectDescription`)
VALUES
  -- 1
  ('C#', 'cron-tool', 'JSON based job runner to automate common tasks');

-- Seed Repository Info
INSERT INTO `Repositories`
  (
    `ProjectId`, `IsPublic`, `RepoType`, `RepoId`, `RepoSize`, `DefaultBranch`, `FullName`, `RepoUrl`,
    `HtmlUrl`, `CiCdUrl`, `GitUrl`, `SshUrl`, `ApiUrl`
  )
  VALUES
  (
    1, 1, 1, 421597872, 333, 'master', 'rniemand/cron-tool',
    'https://github.com/rniemand/cron-tool',
    'https://github.com/rniemand/cron-tool',
    'https://github.com/rniemand/cron-tool/actions',
    'git://github.com/rniemand/cron-tool.git',
    'git@github.com:rniemand/cron-tool.git',
    'https://api.github.com/repos/rniemand/cron-tool'
  );

-- Insert into SonarQube
INSERT INTO `SonarQubeInfo`
  (`ProjectId`, `SonarQubeId`, `BadgeToken`, `SonarQubeUrl`, `BadgesJson`)
VALUES
  (1, 'cron-tool', 'cfc1bd29c71c5cedda1dfc7ada074b14779b8a1e', '{sonarQubeBaseUrl}/dashboard?id={id}', '[]');

-- Insert into Source Code Maturity
INSERT INTO `SourceCodeMaturity`
  (
    `ProjectId`, `SrcDir`, `TestDir`, `DocsDir`, `BuildDir`, `Readme`,
    `PrTemplate`, `EditorConfig`, `CiInfo`, `BuildScript`, `TestScript`,
    `GitAttributes`, `CiVersion`, `License`, `LicenseUrl`, `CiFlows`,
    `JsonDirectories`, `JsonFiles`
  )
  VALUES
  (
    1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, '1.0.107', 'MIT',
    'https://github.com/rniemand/cron-tool/blob/master/LICENSE',
    '[]',
'{
  "src": "https://github.com/rniemand/cron-tool/tree/master/src",
  "test": "https://github.com/rniemand/cron-tool/tree/master/test",
  "docs": "https://github.com/rniemand/cron-tool/tree/master/docs",
  "build": "https://github.com/rniemand/cron-tool/tree/master/.github"
}',
'{
  "readme": "https://github.com/rniemand/cron-tool/blob/master/README.md",
  "pr": "https://github.com/rniemand/cron-tool/blob/master/.github/pull_request_template.md",
  "editorCfg": "https://github.com/rniemand/cron-tool/blob/master/.editorconfig",
  "ciInfo": "https://github.com/rniemand/cron-tool/blob/master/.github/ci.info.json",
  "build": "https://github.com/rniemand/cron-tool/blob/master/.github/ci-build.ps1",
  "test": "https://github.com/rniemand/cron-tool/blob/master/.github/ci-test.ps1",
  "gitAttr": "https://github.com/rniemand/cron-tool/blob/master/.gitattributes"
}'
  );
  