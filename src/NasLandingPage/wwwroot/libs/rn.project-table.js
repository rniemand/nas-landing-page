var rn = rn || {};
rn.plugins = rn.plugins || {};

rn.plugins.projects = rn.plugins.projects || {
  data: {},
  elTable: document.getElementById('projects-table')
};

// rn.plugins.projects.elTable

rn.plugins.projects.populate = function(data) {
  rn.plugins.projects.data = data;

  console.log('here');
}
