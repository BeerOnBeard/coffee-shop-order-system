let ko = require('knockout'),
    CoffeeShopViewModel = require("./CoffeeShopViewModel.js");

document.addEventListener("DOMContentLoaded", function() {
  ko.applyBindings(new CoffeeShopViewModel());
});
