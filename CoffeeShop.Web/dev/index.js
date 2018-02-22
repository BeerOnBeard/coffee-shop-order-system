let ko = require('knockout'),
    CoffeeShopViewModel = require("./CoffeeShopViewModel.js");

// NOTE: For debugging, remove for production
window.ko = ko;

document.addEventListener("DOMContentLoaded", function() {
  window.myModel = new CoffeeShopViewModel()
  ko.applyBindings(window.myModel);
});
