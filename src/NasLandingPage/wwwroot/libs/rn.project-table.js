(function(global) {
  global.rn = global.rn || {};
  global.rn.plugins = global.rn.plugins || {};

  var domEl = { table: document.getElementById('projects-table') };
  var self = { data: {}, fn: {}, rowFn: {} };
  var _fn = self.fn;
  var _colEnum = global.rn.enums.TableColumn;
  var _html = global.rn.html;
  var _config = global.rn.config;
  var _ascii = global.rn.config.ascii;
  var _url = global.rn.url;

  var canDisplay = function(column) {
    if(rn.config.columns.indexOf(column) === -1) {
      return false;
    }
  
    return true;
  }

  // Header and data row functions
  _fn.generateTblHead = function() {
    var tr = document.createElement('tr');

    _fn.appendTblHeadColumn(tr, 'Name', _colEnum.Name);
    _fn.appendTblHeadColumn(tr, '', _colEnum.Visibility);
    _fn.appendTblHeadColumn(tr, 'Code', _colEnum.Code);
    _fn.appendTblHeadColumn(tr, 'Languages', _colEnum.Languages);
    _fn.appendTblHeadColumn(tr, 'Action', _colEnum.Action);
    _fn.appendTblHeadColumn(tr, 'SonarQube', _colEnum.SonarQube);
    _fn.appendTblHeadColumn(tr, 'Readme', _colEnum.Readme);
    _fn.appendTblHeadColumn(tr, '.gitattr', _colEnum.GitAttributes);
    _fn.appendTblHeadColumn(tr, '.editcfg', _colEnum.EditorConfig);
    _fn.appendTblHeadColumn(tr, 'PR', _colEnum.PrTemplate);
    _fn.appendTblHeadColumn(tr, 'License', _colEnum.License);
    _fn.appendTblHeadColumn(tr, 'Build', _colEnum.HasBuildScripts);
    _fn.appendTblHeadColumn(tr, '', _colEnum.BuildScriptsVersion);
    _fn.appendTblHeadColumn(tr, 'GH Actions', _colEnum.GithubActions);
    _fn.appendTblHeadColumn(tr, `${_ascii.folder} src`, _colEnum.DirSrc);
    _fn.appendTblHeadColumn(tr, `${_ascii.folder} test`, _colEnum.DirTest);
    _fn.appendTblHeadColumn(tr, `${_ascii.folder} docs`, _colEnum.DirDocs);
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
    _rowFn.appendLanguages(tr, project);
    _rowFn.appendAction(tr, project);
    _rowFn.appendSonarQube(tr, project);
    _rowFn.appendReadme(tr, project);
    _rowFn.appendGitAttributes(tr, project);
    _rowFn.appendEditorConfig(tr, project);
    _rowFn.appendPrTemplate(tr, project);
    _rowFn.appendLicense(tr, project);
    _rowFn.appendHasBuildScripts(tr, project);
    _rowFn.appendBuildScriptVersion(tr, project);
    _rowFn.appendGithubActions(tr, project);
    _rowFn.appendDirExists(tr, project, _colEnum.DirSrc, 'src');
    _rowFn.appendDirExists(tr, project, _colEnum.DirTest, 'test');
    _rowFn.appendDirExists(tr, project, _colEnum.DirDocs, 'docs');
    _rowFn.appendBadges(tr, project);
  
    return tr;
  }


  // ROW GENERATION
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

  _rowFn.appendLanguages = function(tr, project) {
    if(!canDisplay(_colEnum.Languages)) { return; }
    var td = document.createElement('td');

    if(project.hasOwnProperty('languages')) {
      td.innerHTML = project.languages.join(', ');
    } else {
      td.innerHTML = '-';
    }
    
    tr.append(td);
  }

  _rowFn.appendAction = function(tr, project) {
    if(!canDisplay(_colEnum.Action)) { return; }
    var td = document.createElement('td');
    td.className = 'actions';
    td.append(_html.createLink('actions', project.actionsUrl));
    tr.append(td);
  }

  _rowFn.appendSonarQube = function(tr, project) {
    if(!canDisplay(_colEnum.SonarQube)) { return; }
    var td = document.createElement('td');
    td.className = 'sonarqube';
    td.append(_html.createLink('SonarQube', project.sonarQubeUrl));
    tr.append(td);
  }

  _rowFn.appendReadme = function(tr, project) {
    if(!canDisplay(_colEnum.Readme)) { return; }
    var td = document.createElement('td');
    td.append(_html.scmCheckmark(project, 'readme'));
    tr.append(td);
  }

  _rowFn.appendGitAttributes = function(tr, project) {
    if(!canDisplay(_colEnum.GitAttributes)) { return; }
    var td = document.createElement('td');
    td.append(_html.scmCheckmark(project, 'gitattributes'));
    tr.append(td);
  }

  _rowFn.appendEditorConfig = function(tr, project) {
    if(!canDisplay(_colEnum.EditorConfig)) { return; }
    var td = document.createElement('td');
    td.append(_html.scmCheckmark(project, 'editorconfig'));
    tr.append(td);
  }

  _rowFn.appendPrTemplate = function(tr, project) {
    if(!canDisplay(_colEnum.PrTemplate)) { return; }
    var td = document.createElement('td');
    td.append(_html.scmCheckmark(project, 'pr_template'));
    tr.append(td);
  }

  _rowFn.appendLicense = function(tr, project) {
    if(!canDisplay(_colEnum.License)) { return; }
    var td = document.createElement('td');
    td.className = 'license';
  
    if(!project.hasOwnProperty('license')) {
      td.innerHTML = _ascii.cross;
    } else {
      td.append(_html.createLink(project.license.name, project.license.url));
    }
    
    tr.append(td);
  }

  _rowFn.appendHasBuildScripts = function(tr, project) {
    if(!canDisplay(_colEnum.HasBuildScripts)) { return; }
    var td = document.createElement('td');
  
    if(!project.hasOwnProperty('cicd')) {
      td.innerHTML = _ascii.question;
    } else {
      td.innerHTML = project.cicd.hasBuildScripts ? _ascii.tick : _ascii.cross;
    }
    
    tr.append(td);
  }

  _rowFn.appendBuildScriptVersion = function(tr, project) {
    if(!canDisplay(_colEnum.BuildScriptsVersion)) { return; }
    var td = document.createElement('td');
  
    if(!project.hasOwnProperty('cicd')) {
      td.innerHTML = _ascii.question;
    } else {
      td.innerHTML = project.cicd.buildScriptsVersion;
    }
    
    tr.append(td);
  }

  _rowFn.appendGithubActions = function(tr, project) {
    if(!canDisplay(_colEnum.GithubActions)) { return; }
    var td = document.createElement('td');
    var tdValue = _ascii.cross;

    if(project.hasOwnProperty('cicd')) {
      if(project.cicd.hasOwnProperty('githubWorkflows')) {
        if(project.cicd.githubWorkflows.length > 0) {
          tdValue = `${_ascii.check} (${project.cicd.githubWorkflows.length})`;
        }
      }
    }
    
    td.innerHTML = tdValue;
    tr.append(td);
  }

  _rowFn.appendDirExists = function(tr, project, enumDir, key) {
    if(!canDisplay(enumDir)) { return; }
    var td = document.createElement('td');
    var tdValue = _ascii.cross;

    if(project.hasOwnProperty('folderStructure')) {
      if(project.folderStructure.hasOwnProperty(key)) {
        if(project.folderStructure[key] === true) {
          tdValue = _ascii.check;
        }
      }
    }
    
    td.innerHTML = tdValue;
    tr.append(td);
  }

  _rowFn.appendBadges = function(tr, project) {
    if(!canDisplay(_colEnum.Badges)) { return; }
  
    var td = document.createElement('td');
    td.append(_rowFn.generateBadges(project));
    tr.append(td);
  }

  _rowFn.generateBadges = function(project) {
    var div = document.createElement('div');
  
    if(!project.badges) {
      div.innerHTML = '-';
      return div;
    }
  
    (Object.getOwnPropertyNames(project.badges)).forEach(badge => {
      if(_config.badges.indexOf(badge) === -1) {
        return;
      }
  
      var badgeUrl = project.badges[badge]
        .replace('{sonarQubeProjectId}', project.sonarQubeProjectId)
        .replace('{sonarQubeToken}', project.sonarQubeToken);
  
      div.append(_rowFn.generateBadge(badgeUrl));
    });
    
    return div;
  }

  _rowFn.generateBadge = function(badgeUrl) {
    var img = document.createElement('img');
    img.src = _url.process(badgeUrl);
    img.className = 'badge';
    return img;
  }
  

  // PUBLIC API
  var api = {};

  api.populate = function(data) {
    self.data = data;

    domEl.table.removeChild(domEl.table.children[0]);
    domEl.table.append(_fn.generateTblHead());
    _fn.appendTblRows();
  }

  global.rn.plugins.projects = api;
}(window));
