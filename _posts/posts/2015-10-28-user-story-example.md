---
layout: post
category: posts
title: "User story example"
date: "2015-10-28"
categories: 
  - "agile"
---
# Title

Wholesale customers created with 'on-hold' account status

## Card Description

Wholesale customers created in D365 CE will initially have their account status placed 'on hold' to disallow any credit transactions before they have their account credit checked and approved

#### Story

    AS A finance manager 
    I WANT new wholesale customers to begin with their account status 'oh hold' 
    SO THAT no credit transactions are processed on their account until their account credit checked,approved and taken off hold.

_\[Any supporting notes/diagrams\]_

#### Background

- any notes / emails / links for reference

## Acceptance Criteria

#### Simple Acceptance Criteria

- new customer accounts created will have an on hold status
- accounts with on-hold status are not able to make purchases (without up front payment by credit card)
- customers on hold - scrap batteries can still be collected
- regression test suite 5, 39, 43 pass

#### Specification by Example (for automated tests)

    Given a new wholesale customer is created in D365CE 
    When that record is synced with D365FO 
    Then the customer record status will be 'On Hold'

    Given a new wholesale customer is created in D365CE 
    When a new purchase order is raised against the customer 
    Then the request is denied due to the customer being on hold

    Given a new wholesale customer is taken off hold 
    When a new purchase order is raised against the customer 
    Then the request is accepted

### Further information

https://www.mountaingoatsoftware.com/agile/user-stories