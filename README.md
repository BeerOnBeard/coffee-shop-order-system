# Coffee Shop Eventing System

This project is meant for learning event-driven architecture using [MassTransit](http://masstransit-project.com/MassTransit/).

The CoffeeShop is the front-of-house POS system where orders for coffee and/or bakery items are submitted. The Barista will fulfill orders for coffees. The Bakery will fulfill orders for bakery items. It's up to the CoffeeShop domain to coordinate the Barista and Bakery domains to successfully fulfill an order.

## Basics

**Web** projects are meant for dumb interfaces that communicate to smart **APIs**.

**API** projects house available commands within a specific domain as well as the ability to query for state. These projects are only allowed to read from the database. Any write actions are propagated as events that the corresponding **Coordinator** project will persist to the database.

**Saga** projects are state machines that listen for events and issue commands. For example, when an OrderCreated event occurs, the `CoffeeShop.Saga` project would initialize a new instance of a state machine for that specific order. The state machine would track events in the system that are related to the order and issue commands when necessary.

**Coordinator** projects are used to accept events and translate them into a new domain. These projects handle writes to the database for that domain. For example, the `Barista.Coordinator` listens for a coffees ordered event from the `CoffeeShop.Saga`. It then translates that into multiple rows in the Barista database because the order may contain multiple coffees and the Barista domain acts on one coffee at a time.

**EventContracts** projects are used for communication contracts. `CoffeeShop`, as the coordinating domain, knows about the contracts from other domains. However, the sub-domains do not need to be concerned with higher-level domains. This keeps the sub-domains of Barista and Bakery simpler while pushing the complexity of coordination up to the `CoffeeShop`.

## Docker

First compose the back-end before the front-end. That way, the back-end services have some time to start. Bonus: you can keep the back-end services running even when you tear down the front-end.

```bash
docker-compose -f compose-backend.yml up
```

In another terminal window (if you're not using the `-d` switch):

```bash
docker-compose -f compose-frontend.yml up
```

The CoffeeShop API is available at `http://localhost:5000`, the Barista API is available at `http://localhost:5001`, and the Bakery API is available at `http://localhost:5002`.

## How To Order

Until I get a set of web projects built, I'm using requests directly to the APIs. To start, make a call to the `CoffeeShop.Api`:

```bash
curl -d '{ "CustomerName": "Phil", "Coffees": [ { "Type": "Black", "NumberOfSugars": 0, "NumberOfCreamers": 0 } ] }' -H 'Content-Type: application/json' http://localhost:5000/Orders
```

You can then query the `CoffeeShop.Api`, `Barista.Api`, and the `Bakery.Api` to see the request as it's broken down from a full order to the separate domains.

```bash
curl -i -H 'Accept: application/json' http://localhost:5000/Orders
```

```bash
curl -i -H 'Accept: application/json' http://localhost:5001/Coffees
```

```bash
curl -i -H 'Accept: application/json' http://localhost:5002/Bagels
```

Once the Barista has some coffees to make, POST to complete each coffee.

```bash
curl -d '{ "id": "57c60899-2f7e-4b72-b11f-bed0f5b19b95", "originalOrderId": "8ee26d33-9960-42b9-b896-981fe103c702", "type": "Black", "numberOfSugars": 0, "numberOfCreamers": 0, "isComplete": true }' -H 'Content-Type: application/json' http://localhost:5001/Coffees/57c60899-2f7e-4b72-b11f-bed0f5b19b95
```

Once the Bakery has some bagels to make, POST to complete each coffee.

```bash
curl -d '{ "id": "043c6bcc-9e92-4aba-8098-bf8f816ea7de", "orderId": "8ee26d33-9960-42b9-b896-981fe103c702", "type": "Boring", "hasCreamCheese": false, "hasLox": false, "isComplete": true }' -H 'Content-Type: application/json' http://localhost:5002/Bagels/043c6bcc-9e92-4aba-8098-bf8f816ea7de
```

Using an application like [Postman](https://www.getpostman.com/) makes it a bit easier to set up the requests and see the results in a sane way.
