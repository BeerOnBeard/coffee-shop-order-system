let ko = require('knockout'),
    BaristaViewModel = require('./BaristaViewModel.js');

document.addEventListener("DOMContentLoaded", function() {
  ko.applyBindings(new BaristaViewModel());
});
