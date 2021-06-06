---
layout: post
category: posts
title: "Spec Flow, Specifications and Chained Commands"
date: "2010-10-16"
tags: code dotnet tdd
---

# A quick experiment with Spec Flow -- [http://www.specflow.org/](http://www.specflow.org/ "http://www.specflow.org/")

## Spec flow feature
 

> Feature: Run Specification rules
>     To stop large postage costs
>     As a user
>     I want to be stopped from buying to much if I am in Australia
> 
> @CountryAllowedToProcessOrderWithLargeCost Scenario: Orders of 20 cannot be sent to Australia
> 
>     Given I have a customer with an order for socks
>     And the order is to be sent to Australia
>     When I process the order
>     Then I should not be able to place the order
> 
>  
> 
## Spec flow acceptance test

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class StepDefinitions
    {
        private Customer customer;

        private Order order;

        private Product product;

        private bool orderOk;

        [Given(@"I have a customer with an order for socks")]

        public void GivenIHaveACustomerWithAnOrderForSocks()

        {

            customer = new Customer(){FirstName = "chris", LastName = "mckelt"};

            product = new SockProduct() {Id = 1, Name = "AAA", Price = 30};

            order = new Order(customer);

            order.AddProduct(product);

        }

        \[Given(@"the order is to be sent to Australia")\]

        public void GivenTheOrderIsToBeSentToAustralia()

        {

            order.SetShippingDestination(ShippingDestination.Australia);

        }

        \[Then(@"I should not be able to place the order")\]

        public void ThenIShouldNotBeAbleToPlaceTheOrder()

        {

            Assert.IsFalse(orderOk);

        }

        \[When(@"I process the order")\]

        public void WhenIProcessTheOrder()

        {

            orderOk = order.CanBecome(new OrderConfirmed());

        }

    }

}

 

The state change from ‘order placed’ to ‘order confirmed’ is not allowed due to 2 specifications

    public ISpecification<Order> CreateCanBecomeSpecification(IOrderState newState)
      {
          ISpecification<Order> spec = new CustomerNameCannotBeEmptySpecification();

          spec = spec.And(new ShippingToAustraliaWithPriceOver20NotAllowedSpecification());

          return spec;

      }
 

 # Use Linq

 Update from the future - use LINQ!