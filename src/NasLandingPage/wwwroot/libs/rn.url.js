(function(global) {
  global.rn = global.rn || {};
  var url = global.rn.url || {};

  url.process = function(url) {
    if(url.indexOf('{') === -1) {
      return url;
    }
  
    global.rn.config.urlKeys.forEach(key => {
      var strFind = `{${key}}`;
      var strReplace = global.rn.config.urlValues[key];
      url = url.replace(strFind, strReplace);
    });
  
    return url;
  }
  
  global.rn.url = url;
}(window));
