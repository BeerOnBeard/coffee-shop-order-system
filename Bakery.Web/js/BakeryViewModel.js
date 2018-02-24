let ko = require('knockout'),
    $ = require('jquery');

function BakeryViewModel() {
  let self = this;

  self.bagels = ko.observableArray();

  self.complete = function(bagel) {
    $.ajax({
      type: 'POST',
      url: 'http://localhost:5002/Bagels/' + bagel.id,
      data: JSON.stringify({
        id: bagel.id,
        orderId: bagel.orderId,
        type: bagel.type,
        hasCreamCheese: bagel.hasCreamCheese,
        hasLox: bagel.hasLox,
        isComplete: true
      }),
      contentType: 'application/json'
    }).done(function() {
      self.bagels.remove(bagel);
    });
  }

  getBagels().done(function(data) {
    self.bagels(data);
  });

  setInterval(function() {
    getBagels().done(function(data) {
      self.bagels(data);
    });
  }, 5000);
}

function getBagels() {
  return $.get('http://localhost:5002/Bagels');
}

module.exports = BakeryViewModel;