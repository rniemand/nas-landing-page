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

// (function(global) {
//   global.rn = global.rn || {};
//   global.rn.plugins = global.rn.plugins || {};
  
//   var plugin = {};
//   var data = {};
//   var domElements = {};


//   global.rn.plugins.projects = plugin;
// }(window));
