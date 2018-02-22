let ko = require('knockout'),
    $ = require('jquery'),
    Coffee = require('./Coffee.js');

function BaristaViewModel() {
  let self = this;

  self.coffees = ko.observableArray();

  getCoffees().done(function(data) {
    self.coffees(data);
  });
  
  self.complete = function(coffee) {
    $.ajax({
      type: 'POST',
      url: 'http://localhost:5001/Coffees/' + coffee.id,
      data: JSON.stringify({
        id: coffee.id,
        originalOrderId: coffee.orderId,
        type: coffee.type,
        numberOfSugars: coffee.sugars,
        numberOfCreamers: coffee.creamers,
        isComplete: true
      }),
      contentType: 'application/json'
    }).done(function() {
      self.coffees.remove(coffee);
    });
  }

  setInterval(function() {
    getCoffees().done(function(data) {
      self.coffees(data);
    });
  }, 5000);
}

function getCoffees() {
  return $.get('http://localhost:5001/Coffees')
    .then(function(data) {
      return data.map(function(coffee) {
        return new Coffee(coffee);
      });
    });
}

module.exports = BaristaViewModel;