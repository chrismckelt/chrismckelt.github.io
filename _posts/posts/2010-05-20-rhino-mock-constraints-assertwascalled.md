---
title: "Rhino Mock Constraints  -- AssertWasCalled"
date: "2010-05-20"
categories: 
  - "net"
  - "tdd-bdd"
---

Rhino Mock Constraints allow use to test a methods parameters were called with the correct arguments.

public interface IDocumentService
{
 void Save(string userName, Document document, Stream stream);
}

Some ways to ensure the method that contains the save method passes the correct internally constructed arguments include

documentService.AssertWasCalled(

> a=>a.Save("chris", doc, adaptor.InputStream),

> b => b.Constraints(Is.Equal(“chris”), Is.NotNull(), Is.AnyThing()));

Passing in Property.AllPropertiesMatch(this.MyTestObjectWithPropertiesThatShouldMatch)

will check values against each object
