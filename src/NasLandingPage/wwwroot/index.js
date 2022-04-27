fetch('projects.json')
  .then(response => response.json())
  .then(data => {
    rn.plugins.projects.populate(data);
  });
