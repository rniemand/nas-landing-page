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
  ]
};

rnProjects.fn.appendTblHeadColumn = function(tr, name) {
  var td = document.createElement('td');
  td.innerHTML = name;
  tr.append(td);
}

rnProjects.fn.generateTblHead = function() {
  var tr = document.createElement('tr');
  
  rnProjects.fn.appendTblHeadColumn(tr, 'Name');
  rnProjects.fn.appendTblHeadColumn(tr, '');
  rnProjects.fn.appendTblHeadColumn(tr, 'Code');
  rnProjects.fn.appendTblHeadColumn(tr, 'Action');
  rnProjects.fn.appendTblHeadColumn(tr, 'SonarQube');
  rnProjects.fn.appendTblHeadColumn(tr, '');

  return tr;
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
  return img;
}

rnProjects.fn.generateBadges = function(project) {
  var div = document.createElement('div');

  if(!project.badges) {
    div.innerHTML = '-';
    return div;
  }

  (Object.getOwnPropertyNames(project.badges)).forEach(badge => {
    var badgeUrl = project.badges[badge]
      .replace('{sonarQubeProjectId}', project.sonarQubeProjectId)
      .replace('{sonarQubeToken}', project.sonarQubeToken);

    div.append(rnProjects.fn.generateBadge(badgeUrl));
  });
  
  return div;
}

rnProjects.fn.generateTblRow = function(project) {
  var tr = document.createElement('tr');
  var currentTd = null;

  currentTd = document.createElement('td');
  currentTd.innerHTML = project.name;
  currentTd.className = 'name';
  tr.append(currentTd);

  currentTd = document.createElement('td');
  currentTd.append(rnProjects.fn.boolPill(project.isPublic, 'public', 'private'));
  tr.append(currentTd);

  currentTd = document.createElement('td');
  currentTd.className = 'code';
  currentTd.append(rnProjects.fn.createLink(project.repoType, project.repoUrl));
  tr.append(currentTd);

  currentTd = document.createElement('td');
  currentTd.className = 'actions';
  currentTd.append(rnProjects.fn.createLink('actions', project.actionsUrl));
  tr.append(currentTd);

  currentTd = document.createElement('td');
  currentTd.className = 'sonarqube';
  currentTd.append(rnProjects.fn.createLink('SonarQube', project.sonarQubeUrl));
  tr.append(currentTd);

  currentTd = document.createElement('td');
  currentTd.className = 'sonarqube';
  currentTd.append(rnProjects.fn.generateBadges(project));
  tr.append(currentTd);

  return tr;
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