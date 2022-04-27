var rnHtml = {};

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
