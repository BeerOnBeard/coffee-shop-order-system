let ko = require('knockout'),
    $ = require('jquery'),
    Order = require('./Order'),
    OrderRequest = require('./OrderRequest');

function CoffeeShopViewModel() {
  let self = this;

  self.orders = ko.observableArray();
  self.newOrder = ko.observable(new OrderRequest());
  self.selectedOrder = ko.observable();
  self.selectOrder = function(order) {
    let clone = $.extend({}, order);
    self.selectedOrder(clone);
    return true;
  };

  getOrders().done(function(orders) {
    self.orders(orders);
  });
  
  setInterval(function() {
    getOrders().done(function(orders) {
      self.orders(orders);
    });
  }, 5000);
}

function getOrders() {
  return $.get('http://localhost:5000/Orders')
   .then(function(data) {
     return data.map(function(order) {
       return new Order(order);
     });
   });
}

module.exports = CoffeeShopViewModel;