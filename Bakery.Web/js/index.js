let ko = require('knockout'),
BakeryViewModel = require('./BakeryViewModel.js');

document.addEventListener("DOMContentLoaded", function() {
  ko.applyBindings(new BakeryViewModel());
});
