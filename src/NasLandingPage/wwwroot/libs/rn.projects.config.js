(function(global) {
  global.rn = global.rn || {};
  global.rn.config = global.rn.config || {
    fn: {},
    config: {
      sonarQubeBaseUrl: "http://niemandr.duckdns.org:9001"
    },
    urlKeys: [
      'sonarQubeBaseUrl'
    ],
    columns: [
      rn.enums.TableColumn.Name,
      rn.enums.TableColumn.ProjectType,
      rn.enums.TableColumn.Visibility,
      rn.enums.TableColumn.Code,
      rn.enums.TableColumn.Action,
      rn.enums.TableColumn.SonarQube,
      rn.enums.TableColumn.Readme,
      rn.enums.TableColumn.GitAttributes,
      rn.enums.TableColumn.EditorConfig,
      rn.enums.TableColumn.PrTemplate,
      rn.enums.TableColumn.License,
      rn.enums.TableColumn.HasBuildScripts,
      rn.enums.TableColumn.BuildScriptsVersion,
      rn.enums.TableColumn.Badges,
    ],
    badges: [
      'quality',
      //'bugs',
      //'codeSmells',
      //'coverage',
    ]
  };

}(window));
