(function(global) {
  global.rn = global.rn || {};
  var config = global.rn.config || {};

  config.urlKeys = [
    'sonarQubeBaseUrl'
  ];

  config.urlValues = {
    sonarQubeBaseUrl: "http://niemandr.duckdns.org:9001"
  };

  config.columns = [
    rn.enums.TableColumn.Name,
    rn.enums.TableColumn.ProjectType,
    rn.enums.TableColumn.Visibility,
    rn.enums.TableColumn.Code,
    rn.enums.TableColumn.Languages,
    rn.enums.TableColumn.Action,
    rn.enums.TableColumn.SonarQube,
    rn.enums.TableColumn.Readme,
    //rn.enums.TableColumn.GitAttributes,
    //rn.enums.TableColumn.EditorConfig,
    //rn.enums.TableColumn.PrTemplate,
    //rn.enums.TableColumn.License,
    //rn.enums.TableColumn.HasBuildScripts,
    //rn.enums.TableColumn.BuildScriptsVersion,
    rn.enums.TableColumn.GithubActions,
    rn.enums.TableColumn.DirSrc,
    rn.enums.TableColumn.DirTest,
    rn.enums.TableColumn.DirDocs,
    rn.enums.TableColumn.Badges,
  ];

  config.badges = [
    //'quality',
    //'bugs',
    //'codeSmells',
    'coverage',
  ];

  config.ascii = {
    "question": "❓",
    "tick": "✔️",
    "check": "✔️",
    "cross": "❌",
    "folder": "📁",
    "open": "🔓",
    "locked": "🔒"
  };

  global.rn.config = config;
}(window));
