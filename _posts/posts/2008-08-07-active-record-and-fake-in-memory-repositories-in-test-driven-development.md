---
layout: post
category: posts
title: "Active Record and Fake In Memory Repositories in Test Driven Development"
date: "2008-08-07"
categories: 
  - "net"
---

Download Project source files:[ExampleFakeRepository.zip](/file.axd?file=FakeRepositoryExample.zip)

### Introduction

Here is a simple example of how to use fake repositories in test driven development.

By using Fake Repositories generated from a binary file we are able to quickly use 'real data' in all our tests.

It is a Visual Studio 2008 solution and uses the Castle project's [Active Record](http://www.castleproject.org/ "http://www.castleproject.org/") as an ORM.

### Step 1 – Our Core Model

 

Our model will consist of 3 classes

- SuperHero e.g. Superman
    - SupeHeroId
    - RealName
    - SuperHeroName
- Power e.g. Flight
    - PowerId
    - PowerName
- SuperHeroPower e.g. Superman’s flight
    - SuperHeroPowerId
    - Comments (e.g. Faster than a speeding bullet)

Each of our classes implements an Interface called _IIdRetriever_

 /// <summary> /// This was created so that the BaseRepositoryFake class can provide a standard /// implementation for fake repositories despite not knowing what property holds /// the Id. /// </summary> public interface IIdRetriever { int GetId(); void SetId(int id); }

First we define a base interface that defines all our methods needed for an ‘in-memory’ database

This base interface is called _IRepository_

public interface IRepository<T>
where T : class
{
T Save(T item);
T SaveOrUpdate(T item);
T Get(object
id);
long
Count();
ICollection<T>
FindAll(params ICriterion[]
criteria);
void
Delete(T item);

    }   
 

Now for each class we need a repository so we define 3 repository interfaces that implement the _IRepository_ interface with their corresponding class 

- ISuperHeroRepository
- IPowerRepository
- ISuperHeroPowerRepository

public interface ISuperHeroRepository : IRepository<SuperHero\>{}

 

Now for the concrete implementations of our repositories the following classes are created

 

- SuperHeroRepository
- PowerRepository
- SuperHeroPowerRepository

public class PowerRepository : ARRepository<Power\>, IPowerRepository{}

### Step 2 – Create the database, populate data and generate the binary file

In the attached project there is a command line utility project that accepts 3 arguments 

- CreateSchema – will create the tables in the designated database in the app.config
- PopulateData – will populate the tables with some sample data
- GenerateBinary – will get our ‘core’ object which contains relations to all our data and binary serialize it into a file

### Step 3 – Creating our fakes for use in tests

We will now create fake ‘in-memory’ repositories which use the pre-populated binary data for use in out tests 

Firstly there are some integration tests in the database which ensure the add/edit behaviour is correct when talking to the database.

Secondly there is a folder entitled _Fakes_ 

The main file here is the BaseRepositoryFake which contains all our base methods to mimic a database – yet in memory through an internal collection. By inheriting this class our other fake repositories get the ability to Save,SaveOrUpdate,Delete and find all.

Our inheriting fake repositories are as follows

- SuperHeroPowerRepositoryFake
- PowerHeroRepositoryFake
- SuperHeroPowerRepositoryFake 

These simply inherit the BaseRepositoryFake and implement their corresponding interface

public class SuperHeroRepositoryFake : BaseRepositoryFake<SuperHero\>, ISuperHeroRepository

An explanation of the files in the root of the Tests project

- ActiveRecordFixture – singleton one time setup for active record
- RandomHelper – Set Ids and values for our fake objects (As SQL Server identity columns these will be seeded in the DB)
- SeededRepositoryFakes – contains a fake representation of each repository (e.g. SuperHeroRepository) – data is read from the binary file in these
- SeededRepositoryFakesCountTest – test to ensure the SeededRepositoryFakes are filled
- SuperHeroSeed – class that reads from the binary file

 

Download the above sample and run the tests to see this all in action.
