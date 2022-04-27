var rnEnum = {
  tblCol: {
    Name: 1,
    Visibility: 2,
    Code: 3,
    Action: 4,
    SonarQube: 5,
    Badges: 6,
    Readme: 7,
    GitAttributes: 8,
    EditorConfig: 9,
    PrTemplate: 10,
    License: 11,
    HasBuildScripts: 12,
    BuildScriptsVersion: 13
  }
};

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

var rnHtml = {};

var canDisplay = function(column) {
  if(rnProjects.columns.indexOf(column) === -1) {
    return false;
  }

  return true;
}

rnHtml.createSpan = function(innerHtml) {
  var span = document.createElement('span');
  span.innerHTML = innerHtml;
  return span;
}

rnHtml.createLink = function(title, url) {
  if(!url) {
    var div = document.createElement('div');
    div.innerHTML = '-';
    return div;
  }

  var link = document.createElement('a');
  link.innerHTML = title;
  link.href = rnProjects.fn.processUrl(url);
  link.setAttribute('target', '_blank');
  return link;
}



rnProjects.fn.generateTblHead = function() {
  var tr = document.createElement('tr');
  
  rnProjects.fn.appendTblHeadColumn(tr, 'Name', rnEnum.tblCol.Name);
  rnProjects.fn.appendTblHeadColumn(tr, '', rnEnum.tblCol.Visibility);
  rnProjects.fn.appendTblHeadColumn(tr, 'Code', rnEnum.tblCol.Code);
  rnProjects.fn.appendTblHeadColumn(tr, 'Action', rnEnum.tblCol.Action);
  rnProjects.fn.appendTblHeadColumn(tr, 'SonarQube', rnEnum.tblCol.SonarQube);
  rnProjects.fn.appendTblHeadColumn(tr, 'Readme', rnEnum.tblCol.Readme);
  rnProjects.fn.appendTblHeadColumn(tr, '.gitattr', rnEnum.tblCol.GitAttributes);
  rnProjects.fn.appendTblHeadColumn(tr, '.editcfg', rnEnum.tblCol.EditorConfig);
  rnProjects.fn.appendTblHeadColumn(tr, 'PR', rnEnum.tblCol.PrTemplate);
  rnProjects.fn.appendTblHeadColumn(tr, 'License', rnEnum.tblCol.License);
  rnProjects.fn.appendTblHeadColumn(tr, 'Build', rnEnum.tblCol.HasBuildScripts);
  rnProjects.fn.appendTblHeadColumn(tr, '', rnEnum.tblCol.BuildScriptsVersion);
  rnProjects.fn.appendTblHeadColumn(tr, '', rnEnum.tblCol.Badges);

  return tr;
}

rnProjects.fn.generateTblRow = function(project) {
  var tr = document.createElement('tr');
  
  rnProjects.fn.appendName(tr, project);
  rnProjects.fn.appendVisibility(tr, project);
  rnProjects.fn.appendSourceCode(tr, project);
  rnProjects.fn.appendAction(tr, project);
  rnProjects.fn.appendSonarQube(tr, project);
  rnProjects.fn.appendReadme(tr, project);
  rnProjects.fn.appendGitAttributes(tr, project);
  rnProjects.fn.appendEditorConfig(tr, project);
  rnProjects.fn.appendPrTemplate(tr, project);
  rnProjects.fn.appendLicense(tr, project);
  rnProjects.fn.appendHasBuildScripts(tr, project);
  rnProjects.fn.appendBuildScriptVersion(tr, project);
  rnProjects.fn.appendBadges(tr, project);

  return tr;
}



rnProjects.fn.appendTblHeadColumn = function(tr, name, colEnum) {
  if(rnProjects.columns.indexOf(colEnum) === -1) {
    return;
  }

  var td = document.createElement('td');
  td.innerHTML = name;
  tr.append(td);
}

rnProjects.fn.appendName = function(tr, project) {
  if(!canDisplay(rnEnum.tblCol.Name)) { return; }
  var td = document.createElement('td');
  td.innerHTML = project.name;
  td.className = 'name';
  tr.append(td);
}

rnProjects.fn.appendVisibility = function(tr, project) {
  if(!canDisplay(rnEnum.tblCol.Visibility)) { return; }
  var td = document.createElement('td');
  td.append(rnProjects.fn.boolPill(project.isPublic, 'public', 'private'));
  tr.append(td);
}

rnProjects.fn.appendSourceCode = function(tr, project) {
  if(!canDisplay(rnEnum.tblCol.Code)) { return; }
  var td = document.createElement('td');
  td.className = 'code';
  td.append(rnProjects.fn.createLink(project.repoType, project.repoUrl));
  tr.append(td);
}

rnProjects.fn.appendAction = function(tr, project) {
  if(!canDisplay(rnEnum.tblCol.SonarQube)) { return; }
  var td = document.createElement('td');
  td.className = 'actions';
  td.append(rnProjects.fn.createLink('actions', project.actionsUrl));
  tr.append(td);
}

rnProjects.fn.appendSonarQube = function(tr, project) {
  if(!canDisplay(rnEnum.tblCol.SonarQube)) { return; }
  var td = document.createElement('td');
  td.className = 'sonarqube';
  td.append(rnProjects.fn.createLink('SonarQube', project.sonarQubeUrl));
  tr.append(td);
}

rnProjects.fn.appendReadme = function(tr, project) {
  if(!canDisplay(rnEnum.tblCol.Readme)) { return; }
  var td = document.createElement('td');
  td.append(rnProjects.fn.scmCheckmark(project, 'readme'));
  tr.append(td);
}

rnProjects.fn.appendGitAttributes = function(tr, project) {
  if(!canDisplay(rnEnum.tblCol.GitAttributes)) { return; }
  var td = document.createElement('td');
  td.append(rnProjects.fn.scmCheckmark(project, 'gitattributes'));
  tr.append(td);
}

rnProjects.fn.appendEditorConfig = function(tr, project) {
  if(!canDisplay(rnEnum.tblCol.EditorConfig)) { return; }
  var td = document.createElement('td');
  td.append(rnProjects.fn.scmCheckmark(project, 'editorconfig'));
  tr.append(td);
}

rnProjects.fn.appendPrTemplate = function(tr, project) {
  if(!canDisplay(rnEnum.tblCol.PrTemplate)) { return; }
  var td = document.createElement('td');
  td.append(rnProjects.fn.scmCheckmark(project, 'pr_template'));
  tr.append(td);
}

rnProjects.fn.appendLicense = function(tr, project) {
  if(!canDisplay(rnEnum.tblCol.License)) { return; }
  var td = document.createElement('td');
  td.className = 'license';

  if(!project.hasOwnProperty('license')) {
    td.innerHTML = '❌';
  } else {
    td.append(rnHtml.createLink(project.license.name, project.license.url));
  }
  
  tr.append(td);
}

rnProjects.fn.appendBadges = function(tr, project) {
  if(!canDisplay(rnEnum.tblCol.Badges)) { return; }

  var td = document.createElement('td');
  td.append(rnProjects.fn.generateBadges(project));
  tr.append(td);
}

rnProjects.fn.appendHasBuildScripts = function(tr, project) {
  if(!canDisplay(rnEnum.tblCol.HasBuildScripts)) { return; }
  var td = document.createElement('td');

  if(!project.hasOwnProperty('cicd')) {
    td.innerHTML = '❓';
  } else {
    td.innerHTML = project.cicd.hasBuildScripts ? "✔️" : "❌";
  }
  
  tr.append(td);
}

rnProjects.fn.appendBuildScriptVersion = function(tr, project) {
  if(!canDisplay(rnEnum.tblCol.BuildScriptsVersion)) { return; }
  var td = document.createElement('td');

  if(!project.hasOwnProperty('cicd')) {
    td.innerHTML = '❓';
  } else {
    td.innerHTML = project.cicd.buildScriptsVersion;
  }
  
  tr.append(td);
}




rnProjects.fn.scmCheckmark = function(project, property) {
  var span = document.createElement('span');
  var isTrue = false;

  if(project.hasOwnProperty('sourceCodeMaturity') && project.sourceCodeMaturity.hasOwnProperty(property)) {
    isTrue = project.sourceCodeMaturity[property];
  }

  span.innerHTML = isTrue ? "✔️" : "❌";

  return span;
}

rnProjects.fn.processUrl = function(url) {
  if(url.indexOf('{') === -1) {
    return url;
  }

  rnProjects.urlKeys.forEach(key => {
    var strFind = `{${key}}`;
    var strReplace = rnProjects.config[key];
    url = url.replace(strFind, strReplace);
  });

  return url;
}

rnProjects.fn.createLink = function(title, url) {
  if(!url) {
    var div = document.createElement('div');
    div.innerHTML = '-';
    return div;
  }

  var link = document.createElement('a');
  link.innerHTML = title;
  link.href = rnProjects.fn.processUrl(url);
  link.setAttribute('target', '_blank');
  return link;
}

rnProjects.fn.boolPill = function(value, txtTrue, txtFalse) {
  var span = document.createElement('span');

  if(!value) {
    span.innerHTML = txtFalse;
    span.className = 'pill-false';
  } else {
    span.innerHTML = txtTrue;
    span.className = 'pill-true';
  }

  return span;
}

rnProjects.fn.generateBadge = function(badgeUrl) {
  var img = document.createElement('img');
  img.src = rnProjects.fn.processUrl(badgeUrl);
  img.className = 'badge';
  return img;
}

rnProjects.fn.generateBadges = function(project) {
  var div = document.createElement('div');

  if(!project.badges) {
    div.innerHTML = '-';
    return div;
  }

  (Object.getOwnPropertyNames(project.badges)).forEach(badge => {
    if(rnProjects.badges.indexOf(badge) === -1) {
      return;
    }

    var badgeUrl = project.badges[badge]
      .replace('{sonarQubeProjectId}', project.sonarQubeProjectId)
      .replace('{sonarQubeToken}', project.sonarQubeToken);

    div.append(rnProjects.fn.generateBadge(badgeUrl));
  });
  
  return div;
}

rnProjects.fn.appendTblRows = function() {
  rnProjects.data.forEach(project => {
    rnProjects.el.tbl.append(rnProjects.fn.generateTblRow(project));
  });
}

rnProjects.fn.populateTable = function() {
  rnProjects.el.tbl.removeChild(rnProjects.el.tbl.children[0]);
  rnProjects.el.tbl.append(rnProjects.fn.generateTblHead());
  rnProjects.fn.appendTblRows();
};

fetch('projects.json')
  .then(response => response.json())
  .then(data => {
    rnProjects.data = data;
    rnProjects.fn.populateTable();
  });