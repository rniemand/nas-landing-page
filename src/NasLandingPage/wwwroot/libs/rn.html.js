(function(global) {
  global.rn = global.rn || {};
  global.rn.html = global.rn.html || {};

  var html = global.rn.html;

  html.createSpan = function(innerHtml) {
    var span = document.createElement('span');
    span.innerHTML = innerHtml;
    return span;
  }
  
  html.createLink = function(title, url) {
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
}(window));
