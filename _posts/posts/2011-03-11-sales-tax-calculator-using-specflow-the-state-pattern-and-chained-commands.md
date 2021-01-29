---
layout: post
category: posts
title: "Sales Tax Calculator using SpecFlow & The State Pattern And Chained Commands"
date: "2011-03-11"
categories: 
  - "net"
  - "code"
  - "tdd-bdd"
---

Using [SpecFlow](http://www.specflow.org/) heres how I solved the following problem

###### PROBLEM : SALES TAXES

_Basic sales tax is applicable at a rate of 10% on all goods, except books, food, and medical products that are exempt. Import duty is an additional sales tax applicable on all imported goods at a rate of 5%, with no exemptions._

_When I purchase items I receive a receipt which lists the name of all the items and their price (including tax), finishing with the total cost of the items, and the total amounts of sales taxes paid. The rounding rules for sales tax are that for a tax rate of n%, a shelf price of p contains (np/100 rounded up to the nearest 0.05) amount of sales tax._

_Write an application that prints out the receipt details for these shopping baskets..._

###### INPUT:

Input 1: 1 book at 12.49 1 music CD at 14.99 1 chocolate bar at 0.85

Input 2: 1 imported box of chocolates at 10.00 1 imported bottle of perfume at 47.50

Input 3: 1 imported bottle of perfume at 27.99 1 bottle of perfume at 18.99 1 packet of headache pills at 9.75 1 box of imported chocolates at 11.25

###### OUTPUT

Output 1: 1 book : 12.49 1 music CD: 16.49 1 chocolate bar: 0.85 Sales Taxes: 1.50 Total: 29.83

Output 2: 1 imported box of chocolates: 10.50 1 imported bottle of perfume: 54.65 Sales Taxes: 7.65 Total: 65.15

Output 3: 1 imported bottle of perfume: 32.19 1 bottle of perfume: 20.89 1 packet of headache pills: 9.75 1 imported box of chocolates: 11.85 Sales Taxes: 6.70 Total: 74.68 ==========

 

### A Quick overview of the application

The solution is a console application that parses given files (i.e. Input1.txt) and creates an output formatted as requested in the problem.

It looks like this:

 

[![image](images/image_thumb.png "image")](http://www.mckelt.com/app_data/files/Sales-Tax-Calculator-using-SpecFlow--The_11294/image.png)

 

#### \[Projects\]

SalesTaxApplication    -- Console Application that imports text files ie input1.txt -- these a copied to the bin folder at compile time -- but you just just pass in a file path McKelt.SalesTaxApplication \[/s filename\] -- McKelt.SalesTaxApplication /s input3.txt Domain                       -- contains core model & other shared classes Acceptance Tests        -- uses SpecFlow for BDD acceptance tests - was hoping to have reusable steps but didnt get round to this sorry -- [http://stackoverflow.com/questions/5228030/specflow-re-usable-step-definitions](http://stackoverflow.com/questions/5228030/specflow-re-usable-step-definitions) Unit Tests                    -- used MBUnit with Resharper for testing TDD style

#### \[Design\]

I choose to use a state pattern for managing the order object. As the order transitions to completed I choose to use chained commands to execute tasks So once the order is complete the following commands are kicked off: -- CalculateSalesTaxCommand -- CalculateImportedSalesTaxCommand -- CalculateTotalCostWithoutTaxCommand

Probably YAGNI but this is just a demo

#### \[Other\]

NuGet used to get the following packages. Args            -- Command Line processor Castle.Core Castle.Windsor Rhino Mocks MBUnit SpecFlow SoftwareApproach.TestingExtensions -- VisualStudioTestingExtensions.1.1.0.0

SystemWrapper used to wrap all IO calls for ease of testing -- [http://systemwrapper.codeplex.com/](http://systemwrapper.codeplex.com/)

 

The order can be seen below – its contains a CurrentState object that manages he order process:

public class Order {
      public Order()
      {
          OrderItems = new List<OrderItem\>();
          this.CurrentState = new NotCreated(this);
      }

      public virtual int Id { get; set; }
      public virtual string Name { get; set; }
      public IList<OrderItem\> OrderItems { get; set; }
      public virtual IOrderState CurrentState { get; set; }
      public Decimal TotalTax { get; set; }
      public Decimal TotalCostWithoutTax { get; set; }
      public decimal TotalOrderCost
      {
          get { return TotalTax + TotalCostWithoutTax; }
      }

      public virtual void AddProduct(Product product)
      {
          AddProduct(product, 1);
      }

      public virtual void AddProduct(Product product, int quantity)
      {
          var oi = new OrderItem { Product = product, Quantity = quantity };

          if (OrderItems.Any(a=>a.Product.Id == product.Id))
          {
              int originalQuantity = OrderItems.Single(a => a.Product.Id == product.Id).Quantity;
              OrderItems.Single(a => a.Product.Id == product.Id).Quantity = (originalQuantity + quantity);
          }
          else {
              OrderItems.Add(oi);   
          }
      }

      public virtual void Become(IOrderState orderState)
      {
          orderState.BecomeCommands.Execute();
          CurrentState.Become(orderState);
      }
  }

 

The states the order can be in are:

- NotCreated
- Created
- Completed

As the order moves to its next stage a chain of commands is kicked off.

- NotCreated  –  NoCommand (does nothing)
- Created       –  NoCommand (does nothing)
- Completed   -- CalculateSalesTaxCommand, CalculateImportedSalesTaxCommand, CalculateTotalCostWithoutTaxCommand –> run in order these do the calculations

In starting development, I started with the BDD Acceptance test – this runs red so then I develop unit tests which after red/green/refactor I rerun the acceptance test which goes green

Here is the Spec flow feature file for the order creation

Feature: Customer Order Creation
    To create an order
    As a user
    I enter the customers details
    And the Order is created

@CustomerOrderCreation Scenario: Create an order
    Given a Customer
    When the Customers name is Test Customer
    When I create an order
    Then a new order is created

 

Ands here the BDD code file

\[Binding\]
   public class CustomerOrderCreationFlow {
       protected Customer customer;
       protected IOrderService orderService;
       protected Order order;

       \[Given(@"a Customer")\]
       public void GivenACustomer()
       {
           customer = new Customer();
       }

       \[When(@"I create an order")\]
       public void WhenICreateAnOrder()
       {
           orderService = new OrderService();
           order = orderService.Create(customer);
       }

       \[When(@"the Customers name is Test Customer")\]
       public void WhenTheCustomersNameIsTestCustomer()
       {
           customer.FirstName = "Test";
           customer.LastName = "Customer";
       }

       \[Then(@"a new order is created")\]
       public void ThenANewOrderIsCreated()
       {
           order.ShouldNotBeNull("Order is null");
           order.CurrentState.ShouldBeOfType(typeof (Created));
       }

Adding orders

 

Feature: Manage an order
    As a user
    I want to change the customer order
    I add a product
    Then an order item is added to the order

@AddSingleProduct Scenario: Add single product to an order
    Given A Customer with Name Joe Smith
    Given an order
    When I add a product
    Then an order item should be added

@AddMultipleProducts Scenario: Add Multiple Products to an Order
    Given A Customer with Name Joe Smith
    Given an order
    When I add the following products
      | Id        | Name            | ProductType     | Imported    | Price    |
      | 1 | A-Book | Book | True | 33.45 |
      | 2 | CD | Medical | false | 15.00 |
      | 3 | Chocolates | Food | true | 5.00 |
      | 4 | Gym Socks | Other | false | 3.00 |
    Then an order item should be added

And the code:

 

\[Binding\]
    public class OrderInProgressFlow {
        private Order order;
        private IOrderService orderService;
        private Customer customer;
        private Product product;
        private IList<Product\> tableProducts;

        \[Given(@"an order")\]
        public void GivenAnOrder()
        {
            var stubOrder = new Order()
            {
                Name = string.Format("Order for {0} {1}", customer.FirstName, customer.LastName),
            };
            stubOrder.CurrentState = new Created(stubOrder);
            orderService = MockRepository.GenerateMock<IOrderService\>();
            orderService.Stub(a => a.Create(customer)).Return(stubOrder);
            order = orderService.Create(customer);
        }

        \[Given(@"A Customer with Name Joe Smith")\]
        public void GivenACustomerWithNameJoeSmith()
        {
            customer = new Customer {FirstName = "Joe", LastName = "Smith"};
        }

        \[When(@"I add a product")\]
        public void WhenIAddAProduct()
        {
            product = new ProductBuilder()
                .WithId(1)
                .WithPrice(5)
                .WithName("AAA")
                .WithProductType(ProductType.Other)
                .Build();

            order.AddProduct(product);
        }

        \[Then(@"an order item should be added")\]
        public void ThenAnOrderItemShouldBeAdded()
        {
            if (product != null)
            {
                order.OrderItems.Any(a => a.Product.Id == product.Id).ShouldBeTrue();    
            }
            else {
                tableProducts.Each(a => order.OrderItems.Any(b => b.Product.Id == a.Id).ShouldBeTrue());
            }
            
        }

        \[When(@"I add the following products")\]
        public void WhenIAddTheFollowingProducts(Table table)
        {
            tableProducts = new List<Product\>();
            SpecFlowHelpers.ConvertTableToProductList(table);
            tableProducts.Each(order.AddProduct);
        }

And finally the big one – Order Completion

 

Feature: Sales tax should be 10% for all goods except books food and medical products
    When an order is completed
    As a user
    I want to calculate the sales taxes

@CalculateSalesTax1 Scenario: Calculate Sales Tax
    Given A Customer with Name Joe Smith
    Given an order that is in progress
    When I add the following products to the inprogress order
      | Id        | Name            | ProductType    | Imported     | Price    |
      | 1 | A-Book | Book | false | 12.49 |
      | 2 | Music CD | Other | false | 14.99 |
      | 3 | Chocolates | Food | false | 00.85 |
    When the order is completed
    Then the sales tax should be 1.50 
    And the total should be 29.83

@CalculateSalesTax2 Scenario: Calculate Sales Tax for imported items
    Given A Customer with Name Joe Smith
    Given an order that is in progress
    When I add the following imported products to the inprogress order
      | Id        | Name            | ProductType    | Imported     | Price    |
      | 1 | Chocolates | Food | true | 10.00 |
      | 2 | Perfume CD | Other | true | 47.50 |
    When the order is completed
    Then the sales tax should be 7.65 

@CalculateSalesTax3 Scenario: Calculate Sales Tax for both types of items
Given A Customer with Name Joe Smith
Given an order that is in progress
When I add the following imported products to the inprogress order
    | Id           | Name            | ProductType    | Imported    | Price    |
    | 1 | perfume | Other | true | 27.99 |
    | 2 | perfume | Other | false | 18.99 |
    | 3 | pills | Medical | false | 9.75 |
    | 4 | chocolate | Food | true | 11.25 |
When the order is completed
Then the sales tax should be 6.70

And the code:

Note: Really this should be refactored into different re-usable steps and separate feature files – see this [http://stackoverflow.com/questions/5228030/specflow-re-usable-step-definitions](http://stackoverflow.com/questions/5228030/specflow-re-usable-step-definitions "http://stackoverflow.com/questions/5228030/specflow-re-usable-step-definitions")

 

\[Binding\]
    public class OrderCompleteFlow {
        private Order order;
        private IOrderService orderService;
        private Customer customer;
        private IList<Product\> tableProducts;

        \[Given(@"an order thats in progress")\]
        public void GivenAnOrderThatsInProgress()
        {
            customer = MockRepository.GenerateStub<Customer\>();
            order = new Order()
            {
                Name = string.Format("Order for {0} {1}", customer.FirstName, customer.LastName),
            };
            order.CurrentState = new Created(order);
            
        }

        \[When(@"I add the following products to the inprogress order")\]
        public void WhenIAddTheFollowingProductsToTheInprogressOrder(Table table)
        {
            AddSpecFlowTableProductsToProductList(table);
        }

        private void AddSpecFlowTableProductsToProductList(Table table)
        {
            tableProducts = new List<Product\>();
            tableProducts = SpecFlowHelpers.ConvertTableToProductList(table);
            tableProducts.Each(order.AddProduct);
        }

        \[When(@"I add the following imported products to the inprogress order")\]
        public void WhenIAddTheFollowingImportedProductsToTheInprogressOrder(Table table)
        {
            AddSpecFlowTableProductsToProductList(table);
        }

        \[When(@"the order is completed")\]
        public void WhenTheOrderIsCompleted()
        {
            orderService = IocContainer.Instance.Resolve<IOrderService\>();
            orderService.Complete(order);
        }

        \[Then(@"the sales tax should be 1\\.50")\]
        public void ThenTheSalesTaxShouldBe1\_50()
        {
            order.TotalTax.ShouldEqual(1.5m);
        }

        \[Then(@"the total should be 29\\.83")\]
        public void ThenTheTotalShouldBe29\_83()
        {
            order.TotalOrderCost.ShouldEqual(29.83m);
        }

        \[Then(@"the sales tax should be 7\\.65")\]
        public void ThenTheSalesTaxShouldBe7\_65()
        {
            order.TotalTax.ShouldEqual(7.65m);
        }

        \[Then(@"the sales tax should be 6\\.70")\]
        public void ThenTheSalesTaxShouldBe6\_70()
        {
            order.TotalTax.ShouldEqual(6.70m);
        }

    }

 

Download the code [here](http://www.mckelt.com/app_data/files/Sales-Tax-Calculator-using-SpecFlow--The_11294/SalesTaxCalculator.zip)
