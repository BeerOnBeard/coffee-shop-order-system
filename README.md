# Coffee Shop Eventing System

This project is meant for learning event-driven architecture using MassTransit.

## Basics

**UI** projects are meant for dumb interfaces that communicate to smart **APIs**.

**API** projects house available commands within a specific domain as well as the ability to query for state.

**Saga** projects are state machines that listen for events and issue commands. For example, when an OrderCreated event occurs, the `coffeeshop.saga` project would initialize a new instance of a state machine for that specific order. The state machine would track events in the system that are related to the order and issue commands when necessary.

**EventContracts** projects are used for inter-domain event contracts. These interfaces should not be consumed by projects outside of that specific domain.

**PublicEventContracts** project are used for extra-domain event contracts. These events are meant to be consumed by projects outside of that specific domain.

It is important to segregate inter- and extra-domain event contracts to limit the coupling between projects.

## Domains

There are three domains: CoffeeShop, Barista, Bakery. The CoffeeShop is the front-of-house POS system where orders for coffee and/or bakery items are submitted. The Barista will fulfill orders for coffees. The Bakery will fulfill orders for bakery items. It's up to the CoffeeShop domain to coordinate the Barista and Bakery domains to successfully fulfill an order.
