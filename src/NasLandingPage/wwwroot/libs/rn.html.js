(function(global) {
  global.rn = global.rn || {};
  var html = global.rn.html || {};

  var _url = global.rn.url;

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

  html.boolPill = function(value, txtTrue, txtFalse) {
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

  html.createLink = function(title, url) {
    if(!url) {
      var div = document.createElement('div');
      div.innerHTML = '-';
      return div;
    }
  
    var link = document.createElement('a');
    link.innerHTML = title;
    link.href = _url.process(url);
    link.setAttribute('target', '_blank');
    return link;
  }

  global.rn.html = html;
}(window));
