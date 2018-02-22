let ko = require('knockout');

function Order(model) {
  let self = this;

  self.id = model.id;
  self.customerName = model.customerName;
  self.coffees = model.coffees;
  self.bagels = model.bagels;

  self.numberOfCoffees = ko.pureComputed(function() {
    return self.coffees.length;
  })
  self.numberOfBagels = ko.pureComputed(function() {
    return self.bagels.length;
  });
}

module.exports = Order;