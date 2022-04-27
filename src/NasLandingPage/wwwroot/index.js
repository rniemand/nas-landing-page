

var canDisplay = function(column) {
  if(rnProjects.columns.indexOf(column) === -1) {
    return false;
  }

  return true;
}


rnProjects.fn.generateTblHead = function() {
  var tr = document.createElement('tr');
  
  rnProjects.fn.appendTblHeadColumn(tr, 'Name', rn.enums.TableColumn.Name);
  rnProjects.fn.appendTblHeadColumn(tr, '', rn.enums.TableColumn.Visibility);
  rnProjects.fn.appendTblHeadColumn(tr, 'Code', rn.enums.TableColumn.Code);
  rnProjects.fn.appendTblHeadColumn(tr, 'Action', rn.enums.TableColumn.Action);
  rnProjects.fn.appendTblHeadColumn(tr, 'SonarQube', rn.enums.TableColumn.SonarQube);
  rnProjects.fn.appendTblHeadColumn(tr, 'Readme', rn.enums.TableColumn.Readme);
  rnProjects.fn.appendTblHeadColumn(tr, '.gitattr', rn.enums.TableColumn.GitAttributes);
  rnProjects.fn.appendTblHeadColumn(tr, '.editcfg', rn.enums.TableColumn.EditorConfig);
  rnProjects.fn.appendTblHeadColumn(tr, 'PR', rn.enums.TableColumn.PrTemplate);
  rnProjects.fn.appendTblHeadColumn(tr, 'License', rn.enums.TableColumn.License);
  rnProjects.fn.appendTblHeadColumn(tr, 'Build', rn.enums.TableColumn.HasBuildScripts);
  rnProjects.fn.appendTblHeadColumn(tr, '', rn.enums.TableColumn.BuildScriptsVersion);
  rnProjects.fn.appendTblHeadColumn(tr, '', rn.enums.TableColumn.Badges);

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
  if(!canDisplay(rn.enums.TableColumn.Name)) { return; }
  var td = document.createElement('td');
  td.innerHTML = project.name;
  td.className = 'name';
  tr.append(td);
}

rnProjects.fn.appendVisibility = function(tr, project) {
  if(!canDisplay(rn.enums.TableColumn.Visibility)) { return; }
  var td = document.createElement('td');
  td.append(rnProjects.fn.boolPill(project.isPublic, 'public', 'private'));
  tr.append(td);
}

rnProjects.fn.appendSourceCode = function(tr, project) {
  if(!canDisplay(rn.enums.TableColumn.Code)) { return; }
  var td = document.createElement('td');
  td.className = 'code';
  td.append(rnProjects.fn.createLink(project.repoType, project.repoUrl));
  tr.append(td);
}

rnProjects.fn.appendAction = function(tr, project) {
  if(!canDisplay(rn.enums.TableColumn.SonarQube)) { return; }
  var td = document.createElement('td');
  td.className = 'actions';
  td.append(rnProjects.fn.createLink('actions', project.actionsUrl));
  tr.append(td);
}

rnProjects.fn.appendSonarQube = function(tr, project) {
  if(!canDisplay(rn.enums.TableColumn.SonarQube)) { return; }
  var td = document.createElement('td');
  td.className = 'sonarqube';
  td.append(rnProjects.fn.createLink('SonarQube', project.sonarQubeUrl));
  tr.append(td);
}

rnProjects.fn.appendReadme = function(tr, project) {
  if(!canDisplay(rn.enums.TableColumn.Readme)) { return; }
  var td = document.createElement('td');
  td.append(rnProjects.fn.scmCheckmark(project, 'readme'));
  tr.append(td);
}

rnProjects.fn.appendGitAttributes = function(tr, project) {
  if(!canDisplay(rn.enums.TableColumn.GitAttributes)) { return; }
  var td = document.createElement('td');
  td.append(rnProjects.fn.scmCheckmark(project, 'gitattributes'));
  tr.append(td);
}

rnProjects.fn.appendEditorConfig = function(tr, project) {
  if(!canDisplay(rn.enums.TableColumn.EditorConfig)) { return; }
  var td = document.createElement('td');
  td.append(rnProjects.fn.scmCheckmark(project, 'editorconfig'));
  tr.append(td);
}

rnProjects.fn.appendPrTemplate = function(tr, project) {
  if(!canDisplay(rn.enums.TableColumn.PrTemplate)) { return; }
  var td = document.createElement('td');
  td.append(rnProjects.fn.scmCheckmark(project, 'pr_template'));
  tr.append(td);
}

rnProjects.fn.appendLicense = function(tr, project) {
  if(!canDisplay(rn.enums.TableColumn.License)) { return; }
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
  if(!canDisplay(rn.enums.TableColumn.Badges)) { return; }

  var td = document.createElement('td');
  td.append(rnProjects.fn.generateBadges(project));
  tr.append(td);
}

rnProjects.fn.appendHasBuildScripts = function(tr, project) {
  if(!canDisplay(rn.enums.TableColumn.HasBuildScripts)) { return; }
  var td = document.createElement('td');

  if(!project.hasOwnProperty('cicd')) {
    td.innerHTML = '❓';
  } else {
    td.innerHTML = project.cicd.hasBuildScripts ? "✔️" : "❌";
  }
  
  tr.append(td);
}

rnProjects.fn.appendBuildScriptVersion = function(tr, project) {
  if(!canDisplay(rn.enums.TableColumn.BuildScriptsVersion)) { return; }
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