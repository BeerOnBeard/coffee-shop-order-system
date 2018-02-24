let $ = require('jquery');

function Coffee(model) {
  let self = this;

  self.id = model.id;
  self.orderId = model.originalOrderId,
  self.type = model.type;
  self.sugars = model.numberOfSugars;
  self.creamers = model.numberOfCreamers;
}

module.exports = Coffee;