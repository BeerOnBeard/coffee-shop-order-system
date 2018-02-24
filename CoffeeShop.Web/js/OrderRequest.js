let ko = require('knockout'),
    $ = require('jquery');

function Coffee(type, sugars, creamers) {
  let self = this;
  self.type = type;
  self.sugars = sugars;
  self.creamers = creamers;
}

function Bagel(type, creamCheese, lox) {
  let self = this;
  self.type = type;
  self.hasCreamCheese = creamCheese;
  self.hasLox = lox;
}

function OrderRequest() {
  let self = this;

  self.customerName = ko.observable();
  
  // use to hold coffee information until user adds it to collection
  self.coffeeType = ko.observable('');
  self.coffeeSugars = ko.observable(0);
  self.coffeeCreamers = ko.observable(0);
  self.addCoffee = function() {
    self.coffees.push(new Coffee(self.coffeeType(), self.coffeeSugars(), self.coffeeCreamers()));
    resetCoffeeForm();
  };

  self.bagelType = ko.observable('');
  self.bagelHasCreamCheese = ko.observable(false);
  self.bagelHasLox = ko.observable(false);
  self.addBagel = function() {
    self.bagels.push(new Bagel(self.bagelType(), self.bagelHasCreamCheese(), self.bagelHasLox()));
    resetBagelForm();
  };

  self.coffees = ko.observableArray();
  self.bagels = ko.observableArray();

  function resetCoffeeForm() {
    self.coffeeType('');
    self.coffeeSugars(0);
    self.coffeeCreamers(0);
  }

  function resetBagelForm() {
    self.bagelType('');
    self.bagelHasCreamCheese(false);
    self.bagelHasLox(false);
  }

  self.submit = function() {
    $.ajax({
      type: 'POST',
      url: 'http://localhost:5000/Orders',
      data: JSON.stringify({
        CustomerName: self.customerName(),
        Coffees: self.coffees().map(function(coffee) {
          return {
            Type: coffee.type,
            NumberOfSugars: coffee.sugars,
            NumberOfCreamers: coffee.creamers
          };
        }),
        Bagels: self.bagels().map(function(bagel) {
          return {
            Type: bagel.type,
            HasCreamCheese: bagel.hasCreamCheese,
            HasLox: bagel.hasLox
          };
        })
      }),
      contentType: 'application/json'
    })
    .done(function() {
      self.customerName('');
      resetCoffeeForm();
      resetBagelForm();
      self.coffees([]);
      self.bagels([]);
      window.location = '#'
    });
  };
}

module.exports = OrderRequest;