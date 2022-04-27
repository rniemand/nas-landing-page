var rnProjects = {
  el: {
    tbl: document.getElementById('projects-table')
  },
  data: {},
  fn: {},
  config: {
    sonarQubeBaseUrl: "http://niemandr.duckdns.org:9001"
  },
  urlKeys: [
    'sonarQubeBaseUrl'
  ],
  columns: [
    rnEnum.tblCol.Name,
    rnEnum.tblCol.ProjectType,
    rnEnum.tblCol.Visibility,
    rnEnum.tblCol.Code,
    rnEnum.tblCol.Action,
    rnEnum.tblCol.SonarQube,
    rnEnum.tblCol.Readme,
    rnEnum.tblCol.GitAttributes,
    rnEnum.tblCol.EditorConfig,
    rnEnum.tblCol.PrTemplate,
    rnEnum.tblCol.License,
    rnEnum.tblCol.HasBuildScripts,
    rnEnum.tblCol.BuildScriptsVersion,
    rnEnum.tblCol.Badges,
  ],
  badges: [
    'quality',
    //'bugs',
    //'codeSmells',
    //'coverage',
  ]
};
