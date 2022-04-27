// var rn = rn || {};
// rn.plugins = rn.plugins || {};

// rn.plugins.projects = rn.plugins.projects || {
//   data: {},
//   elTable: document.getElementById('projects-table')
// };

// // rn.plugins.projects.elTable



/*

rn.config.fn.generateBadges = function(project) {
  var div = document.createElement('div');

  if(!project.badges) {
    div.innerHTML = '-';
    return div;
  }

  (Object.getOwnPropertyNames(project.badges)).forEach(badge => {
    if(rn.config.badges.indexOf(badge) === -1) {
      return;
    }

    var badgeUrl = project.badges[badge]
      .replace('{sonarQubeProjectId}', project.sonarQubeProjectId)
      .replace('{sonarQubeToken}', project.sonarQubeToken);

    div.append(rn.config.fn.generateBadge(badgeUrl));
  });
  
  return div;
}





rn.config.fn.appendBuildScriptVersion = function(tr, project) {
  if(!canDisplay(_colEnum.BuildScriptsVersion)) { return; }
  var td = document.createElement('td');

  if(!project.hasOwnProperty('cicd')) {
    td.innerHTML = '❓';
  } else {
    td.innerHTML = project.cicd.buildScriptsVersion;
  }
  
  tr.append(td);
}




rn.config.fn.scmCheckmark = function(project, property) {
  var span = document.createElement('span');
  var isTrue = false;

  if(project.hasOwnProperty('sourceCodeMaturity') && project.sourceCodeMaturity.hasOwnProperty(property)) {
    isTrue = project.sourceCodeMaturity[property];
  }

  span.innerHTML = isTrue ? "✔️" : "❌";

  return span;
}

rn.config.fn.generateBadge = function(badgeUrl) {
  var img = document.createElement('img');
  img.src = rn.config.fn.processUrl(badgeUrl);
  img.className = 'badge';
  return img;
}

rn.config.fn.appendAction = function(tr, project) {
  if(!canDisplay(_colEnum.SonarQube)) { return; }
  var td = document.createElement('td');
  td.className = 'actions';
  td.append(rn.config.fn.createLink('actions', project.actionsUrl));
  tr.append(td);
}

rn.config.fn.appendSonarQube = function(tr, project) {
  if(!canDisplay(_colEnum.SonarQube)) { return; }
  var td = document.createElement('td');
  td.className = 'sonarqube';
  td.append(rn.config.fn.createLink('SonarQube', project.sonarQubeUrl));
  tr.append(td);
}

rn.config.fn.appendReadme = function(tr, project) {
  if(!canDisplay(_colEnum.Readme)) { return; }
  var td = document.createElement('td');
  td.append(rn.config.fn.scmCheckmark(project, 'readme'));
  tr.append(td);
}

rn.config.fn.appendGitAttributes = function(tr, project) {
  if(!canDisplay(_colEnum.GitAttributes)) { return; }
  var td = document.createElement('td');
  td.append(rn.config.fn.scmCheckmark(project, 'gitattributes'));
  tr.append(td);
}

rn.config.fn.appendEditorConfig = function(tr, project) {
  if(!canDisplay(_colEnum.EditorConfig)) { return; }
  var td = document.createElement('td');
  td.append(rn.config.fn.scmCheckmark(project, 'editorconfig'));
  tr.append(td);
}

rn.config.fn.appendPrTemplate = function(tr, project) {
  if(!canDisplay(_colEnum.PrTemplate)) { return; }
  var td = document.createElement('td');
  td.append(rn.config.fn.scmCheckmark(project, 'pr_template'));
  tr.append(td);
}

rn.config.fn.appendLicense = function(tr, project) {
  if(!canDisplay(_colEnum.License)) { return; }
  var td = document.createElement('td');
  td.className = 'license';

  if(!project.hasOwnProperty('license')) {
    td.innerHTML = '❌';
  } else {
    td.append(rn.html.createLink(project.license.name, project.license.url));
  }
  
  tr.append(td);
}

rn.config.fn.appendBadges = function(tr, project) {
  if(!canDisplay(_colEnum.Badges)) { return; }

  var td = document.createElement('td');
  td.append(rn.config.fn.generateBadges(project));
  tr.append(td);
}

rn.config.fn.appendHasBuildScripts = function(tr, project) {
  if(!canDisplay(_colEnum.HasBuildScripts)) { return; }
  var td = document.createElement('td');

  if(!project.hasOwnProperty('cicd')) {
    td.innerHTML = '❓';
  } else {
    td.innerHTML = project.cicd.hasBuildScripts ? "✔️" : "❌";
  }
  
  tr.append(td);
}
*/



(function(global) {
  global.rn = global.rn || {};
  global.rn.plugins = global.rn.plugins || {};

  var domEl = {
    table: document.getElementById('projects-table')
  };

  var self = {
    data: {},
    fn: {},
    rowFn: {}
  };
  
  var _fn = self.fn;
  var _colEnum = global.rn.enums.TableColumn;
  var _html = global.rn.html;

  // Header and data row functions
  _fn.generateTblHead = function() {
    var tr = document.createElement('tr');

    _fn.appendTblHeadColumn(tr, 'Name', _colEnum.Name);
    _fn.appendTblHeadColumn(tr, '', _colEnum.Visibility);
    _fn.appendTblHeadColumn(tr, 'Code', _colEnum.Code);
    _fn.appendTblHeadColumn(tr, 'Action', _colEnum.Action);
    _fn.appendTblHeadColumn(tr, 'SonarQube', _colEnum.SonarQube);
    _fn.appendTblHeadColumn(tr, 'Readme', _colEnum.Readme);
    _fn.appendTblHeadColumn(tr, '.gitattr', _colEnum.GitAttributes);
    _fn.appendTblHeadColumn(tr, '.editcfg', _colEnum.EditorConfig);
    _fn.appendTblHeadColumn(tr, 'PR', _colEnum.PrTemplate);
    _fn.appendTblHeadColumn(tr, 'License', _colEnum.License);
    _fn.appendTblHeadColumn(tr, 'Build', _colEnum.HasBuildScripts);
    _fn.appendTblHeadColumn(tr, '', _colEnum.BuildScriptsVersion);
    _fn.appendTblHeadColumn(tr, '', _colEnum.Badges);

    return tr;
  }

  _fn.appendTblRows = function() {
    self.data.forEach(project => {
      domEl.table.append(_fn.generateTblRow(project));
    });
  }
  
  _fn.appendTblHeadColumn = function(tr, name, colEnum) {
    if(rn.config.columns.indexOf(colEnum) === -1) {
      return;
    }

    var td = document.createElement('td');
    td.innerHTML = name;
    tr.append(td);
  }

  _fn.generateTblRow = function(project) {
    var tr = document.createElement('tr');
  
    _rowFn.appendName(tr, project);
    _rowFn.appendVisibility(tr, project);
    _rowFn.appendSourceCode(tr, project);
    // rn.config.fn.appendAction(tr, project);
    // rn.config.fn.appendSonarQube(tr, project);
    // rn.config.fn.appendReadme(tr, project);
    // rn.config.fn.appendGitAttributes(tr, project);
    // rn.config.fn.appendEditorConfig(tr, project);
    // rn.config.fn.appendPrTemplate(tr, project);
    // rn.config.fn.appendLicense(tr, project);
    // rn.config.fn.appendHasBuildScripts(tr, project);
    // rn.config.fn.appendBuildScriptVersion(tr, project);
    // rn.config.fn.appendBadges(tr, project);
  
    return tr;
  }

  var canDisplay = function(column) {
    if(rn.config.columns.indexOf(column) === -1) {
      return false;
    }
  
    return true;
  }

  // Row generation functions
  var _rowFn = self.rowFn;

  _rowFn.appendName = function(tr, project) {
    if(!canDisplay(_colEnum.Name)) { return; }
    var td = document.createElement('td');
    td.innerHTML = project.name;
    td.className = 'name';
    tr.append(td);
  }

  _rowFn.appendVisibility = function(tr, project) {
    if(!canDisplay(_colEnum.Visibility)) { return; }
    var td = document.createElement('td');
    td.append(_html.boolPill(project.isPublic, 'public', 'private'));
    tr.append(td);
  }

  _rowFn.appendSourceCode = function(tr, project) {
    if(!canDisplay(_colEnum.Code)) { return; }
    var td = document.createElement('td');
    td.className = 'code';
    td.append(_html.createLink(project.repoType, project.repoUrl));
    tr.append(td);
  }
  
  
  

  var api = {};

  api.populate = function(data) {
    self.data = data;

    domEl.table.removeChild(domEl.table.children[0]);
    domEl.table.append(_fn.generateTblHead());
    _fn.appendTblRows();
  }


  global.rn.plugins.projects = api;
}(window));
