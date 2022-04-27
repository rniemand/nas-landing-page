console.log(rn);

var canDisplay = function(column) {
  if(rn.config.columns.indexOf(column) === -1) {
    return false;
  }

  return true;
}


rn.config.fn.generateTblHead = function() {
  var tr = document.createElement('tr');
  
  rn.config.fn.appendTblHeadColumn(tr, 'Name', rn.enums.TableColumn.Name);
  rn.config.fn.appendTblHeadColumn(tr, '', rn.enums.TableColumn.Visibility);
  rn.config.fn.appendTblHeadColumn(tr, 'Code', rn.enums.TableColumn.Code);
  rn.config.fn.appendTblHeadColumn(tr, 'Action', rn.enums.TableColumn.Action);
  rn.config.fn.appendTblHeadColumn(tr, 'SonarQube', rn.enums.TableColumn.SonarQube);
  rn.config.fn.appendTblHeadColumn(tr, 'Readme', rn.enums.TableColumn.Readme);
  rn.config.fn.appendTblHeadColumn(tr, '.gitattr', rn.enums.TableColumn.GitAttributes);
  rn.config.fn.appendTblHeadColumn(tr, '.editcfg', rn.enums.TableColumn.EditorConfig);
  rn.config.fn.appendTblHeadColumn(tr, 'PR', rn.enums.TableColumn.PrTemplate);
  rn.config.fn.appendTblHeadColumn(tr, 'License', rn.enums.TableColumn.License);
  rn.config.fn.appendTblHeadColumn(tr, 'Build', rn.enums.TableColumn.HasBuildScripts);
  rn.config.fn.appendTblHeadColumn(tr, '', rn.enums.TableColumn.BuildScriptsVersion);
  rn.config.fn.appendTblHeadColumn(tr, '', rn.enums.TableColumn.Badges);

  return tr;
}

rn.config.fn.generateTblRow = function(project) {
  var tr = document.createElement('tr');
  
  rn.config.fn.appendName(tr, project);
  rn.config.fn.appendVisibility(tr, project);
  rn.config.fn.appendSourceCode(tr, project);
  rn.config.fn.appendAction(tr, project);
  rn.config.fn.appendSonarQube(tr, project);
  rn.config.fn.appendReadme(tr, project);
  rn.config.fn.appendGitAttributes(tr, project);
  rn.config.fn.appendEditorConfig(tr, project);
  rn.config.fn.appendPrTemplate(tr, project);
  rn.config.fn.appendLicense(tr, project);
  rn.config.fn.appendHasBuildScripts(tr, project);
  rn.config.fn.appendBuildScriptVersion(tr, project);
  rn.config.fn.appendBadges(tr, project);

  return tr;
}



rn.config.fn.appendTblHeadColumn = function(tr, name, colEnum) {
  if(rn.config.columns.indexOf(colEnum) === -1) {
    return;
  }

  var td = document.createElement('td');
  td.innerHTML = name;
  tr.append(td);
}

rn.config.fn.appendName = function(tr, project) {
  if(!canDisplay(rn.enums.TableColumn.Name)) { return; }
  var td = document.createElement('td');
  td.innerHTML = project.name;
  td.className = 'name';
  tr.append(td);
}

rn.config.fn.appendVisibility = function(tr, project) {
  if(!canDisplay(rn.enums.TableColumn.Visibility)) { return; }
  var td = document.createElement('td');
  td.append(rn.config.fn.boolPill(project.isPublic, 'public', 'private'));
  tr.append(td);
}

rn.config.fn.appendSourceCode = function(tr, project) {
  if(!canDisplay(rn.enums.TableColumn.Code)) { return; }
  var td = document.createElement('td');
  td.className = 'code';
  td.append(rn.config.fn.createLink(project.repoType, project.repoUrl));
  tr.append(td);
}

rn.config.fn.appendAction = function(tr, project) {
  if(!canDisplay(rn.enums.TableColumn.SonarQube)) { return; }
  var td = document.createElement('td');
  td.className = 'actions';
  td.append(rn.config.fn.createLink('actions', project.actionsUrl));
  tr.append(td);
}

rn.config.fn.appendSonarQube = function(tr, project) {
  if(!canDisplay(rn.enums.TableColumn.SonarQube)) { return; }
  var td = document.createElement('td');
  td.className = 'sonarqube';
  td.append(rn.config.fn.createLink('SonarQube', project.sonarQubeUrl));
  tr.append(td);
}

rn.config.fn.appendReadme = function(tr, project) {
  if(!canDisplay(rn.enums.TableColumn.Readme)) { return; }
  var td = document.createElement('td');
  td.append(rn.config.fn.scmCheckmark(project, 'readme'));
  tr.append(td);
}

rn.config.fn.appendGitAttributes = function(tr, project) {
  if(!canDisplay(rn.enums.TableColumn.GitAttributes)) { return; }
  var td = document.createElement('td');
  td.append(rn.config.fn.scmCheckmark(project, 'gitattributes'));
  tr.append(td);
}

rn.config.fn.appendEditorConfig = function(tr, project) {
  if(!canDisplay(rn.enums.TableColumn.EditorConfig)) { return; }
  var td = document.createElement('td');
  td.append(rn.config.fn.scmCheckmark(project, 'editorconfig'));
  tr.append(td);
}

rn.config.fn.appendPrTemplate = function(tr, project) {
  if(!canDisplay(rn.enums.TableColumn.PrTemplate)) { return; }
  var td = document.createElement('td');
  td.append(rn.config.fn.scmCheckmark(project, 'pr_template'));
  tr.append(td);
}

rn.config.fn.appendLicense = function(tr, project) {
  if(!canDisplay(rn.enums.TableColumn.License)) { return; }
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
  if(!canDisplay(rn.enums.TableColumn.Badges)) { return; }

  var td = document.createElement('td');
  td.append(rn.config.fn.generateBadges(project));
  tr.append(td);
}

rn.config.fn.appendHasBuildScripts = function(tr, project) {
  if(!canDisplay(rn.enums.TableColumn.HasBuildScripts)) { return; }
  var td = document.createElement('td');

  if(!project.hasOwnProperty('cicd')) {
    td.innerHTML = '❓';
  } else {
    td.innerHTML = project.cicd.hasBuildScripts ? "✔️" : "❌";
  }
  
  tr.append(td);
}

rn.config.fn.appendBuildScriptVersion = function(tr, project) {
  if(!canDisplay(rn.enums.TableColumn.BuildScriptsVersion)) { return; }
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

rn.config.fn.processUrl = function(url) {
  if(url.indexOf('{') === -1) {
    return url;
  }

  rn.config.urlKeys.forEach(key => {
    var strFind = `{${key}}`;
    var strReplace = rn.config.config[key];
    url = url.replace(strFind, strReplace);
  });

  return url;
}

rn.config.fn.createLink = function(title, url) {
  if(!url) {
    var div = document.createElement('div');
    div.innerHTML = '-';
    return div;
  }

  var link = document.createElement('a');
  link.innerHTML = title;
  link.href = rn.config.fn.processUrl(url);
  link.setAttribute('target', '_blank');
  return link;
}

rn.config.fn.boolPill = function(value, txtTrue, txtFalse) {
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

rn.config.fn.generateBadge = function(badgeUrl) {
  var img = document.createElement('img');
  img.src = rn.config.fn.processUrl(badgeUrl);
  img.className = 'badge';
  return img;
}

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

rn.config.fn.appendTblRows = function() {
  rn.config.data.forEach(project => {
    rn.config.el.tbl.append(rn.config.fn.generateTblRow(project));
  });
}

rn.config.fn.populateTable = function() {
  rn.config.el.tbl.removeChild(rn.config.el.tbl.children[0]);
  rn.config.el.tbl.append(rn.config.fn.generateTblHead());
  rn.config.fn.appendTblRows();
};

fetch('projects.json')
  .then(response => response.json())
  .then(data => {
    rn.config.data = data;
    rn.config.fn.populateTable();
  });