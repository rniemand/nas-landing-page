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
    PrTemplate: 10
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
    //rnEnum.tblCol.Readme,
    //rnEnum.tblCol.GitAttributes,
    rnEnum.tblCol.EditorConfig,
    rnEnum.tblCol.PrTemplate,
    rnEnum.tblCol.Badges,
  ],
  badges: [
    'quality',
    //'bugs',
    'codeSmells',
    //'coverage',
  ]
};


rnProjects.fn.appendTblHeadColumn = function(tr, name, colEnum) {
  if(rnProjects.columns.indexOf(colEnum) === -1) {
    return;
  }

  var td = document.createElement('td');
  td.innerHTML = name;
  tr.append(td);
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
  rnProjects.fn.appendTblHeadColumn(tr, '', rnEnum.tblCol.Badges);

  return tr;
}

rnProjects.fn.generateTblRow = function(project) {
  var tr = document.createElement('tr');
  var currentTd = null;

  if(rnProjects.columns.indexOf(rnEnum.tblCol.Name) !== -1) {
    currentTd = document.createElement('td');
    currentTd.innerHTML = project.name;
    currentTd.className = 'name';
    tr.append(currentTd);
  }

  if(rnProjects.columns.indexOf(rnEnum.tblCol.Visibility) !== -1) {
    currentTd = document.createElement('td');
    currentTd.append(rnProjects.fn.boolPill(project.isPublic, 'public', 'private'));
    tr.append(currentTd);
  }

  if(rnProjects.columns.indexOf(rnEnum.tblCol.Code) !== -1) {
    currentTd = document.createElement('td');
    currentTd.className = 'code';
    currentTd.append(rnProjects.fn.createLink(project.repoType, project.repoUrl));
    tr.append(currentTd);
  }

  if(rnProjects.columns.indexOf(rnEnum.tblCol.Action) !== -1) {
    currentTd = document.createElement('td');
    currentTd.className = 'actions';
    currentTd.append(rnProjects.fn.createLink('actions', project.actionsUrl));
    tr.append(currentTd);
  }

  if(rnProjects.columns.indexOf(rnEnum.tblCol.SonarQube) !== -1) {
    currentTd = document.createElement('td');
    currentTd.className = 'sonarqube';
    currentTd.append(rnProjects.fn.createLink('SonarQube', project.sonarQubeUrl));
    tr.append(currentTd);
  }

  if(rnProjects.columns.indexOf(rnEnum.tblCol.Readme) !== -1) {
    currentTd = document.createElement('td');
    currentTd.append(rnProjects.fn.scmCheckmark(project, 'readme'));
    tr.append(currentTd);
  }

  if(rnProjects.columns.indexOf(rnEnum.tblCol.GitAttributes) !== -1) {
    currentTd = document.createElement('td');
    currentTd.append(rnProjects.fn.scmCheckmark(project, 'gitattributes'));
    tr.append(currentTd);
  }

  if(rnProjects.columns.indexOf(rnEnum.tblCol.EditorConfig) !== -1) {
    currentTd = document.createElement('td');
    currentTd.append(rnProjects.fn.scmCheckmark(project, 'editorconfig'));
    tr.append(currentTd);
  }

  if(rnProjects.columns.indexOf(rnEnum.tblCol.PrTemplate) !== -1) {
    currentTd = document.createElement('td');
    currentTd.append(rnProjects.fn.scmCheckmark(project, 'pr_template'));
    tr.append(currentTd);
  }

  if(rnProjects.columns.indexOf(rnEnum.tblCol.Badges) !== -1) {
    currentTd = document.createElement('td');
    currentTd.append(rnProjects.fn.generateBadges(project));
    tr.append(currentTd);
  }
  
  return tr;
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