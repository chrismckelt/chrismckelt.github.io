---
layout: post
category: posts
title: "Charge Id - Lean Canvas"
date: "2018-07-13"
tags: dotnet
---

# Lean Canvas

## Problem

This is the start of a series of blog posts where I try to build an online personal budgeting system.

The motivation for this was me trying to do a household budget usin

With [Open Banking](https://en.wikipedia.org/wiki/Open_banking) approaching a common service to classify income and expenditure for bank statement transactions is not currently available on the market.

The ability for a consumer to categorise their transactions sits within varied personal finance providers (eg. pocketbook, mint).

## Solution

An API that uses [Named Entity Recognition](https://en.wikipedia.org/wiki/Named-entity_recognition) to identifies a consumers bank statement into the below groups:

Why –> classification (eg Holiday travel) & sub-category (flights) Who –> company originating the charge When –> Date time & location How –> Method of transfer (EftPos, cash withdraw, direct debit, credit card) What –> Type of transaction is it? Dishonour, Overdrawn, Interest, Fees, Credit, Debit

\--------------------------------------------------------------------------------

## Key Metrics

- Correct classifications
- API Usage
- Website signups
- Website classification requests
- Percent of statements marked as correct (accurately identified)
- Unique Value Proposition
- We offer an open solution to personal financial statement classification.

## Similar products

- pocketbook – automated classification
- pocketsmith.com – automated classification
- youneedabudget.com – manual classification
- myprosperity.com.au - – manual classification
- Mint – US based  no AU presence

#### Difference

We use [online machine learning](https://en.wikipedia.org/wiki/Online_machine_learning) to classify all transactions on a bank statement and categorise them into categories for visualisation.

#### Potential future features

- Personal finance advice
- Weekly email summary statement of financial portfolio
- Fraud alerts
- Better products alert (mortgage/insurance)
- Tax savings calculator --------------------------------------------------------------------------------

#### Channels

Free for consumers.  Paid for financial institutions that want to use the API

#### Customer Segments

Retail consumers wanting to understand their financial statement Business consumers wanting financial insight into their customers

#### Cost Structure

- Free for website use
- API cost

* * *

 

## Posts in this series

[Charge Id – scratching the tech itch \[ part 1 \]](/blog/?p=460) [Charge Id – lean canvas \[ part 2 \]](/blog/?p=485) [Charge Id – solution overview \[ part 3 \]](/blog/?p=505) [Charge Id – analysing the data \[ part 4 \]](/blog/?p=507) [Charge Id – the prediction model \[ part 5 \]](/blog/?p=668) [Charge Id – deploying a ML.Net Model to Azure \[ part 6 \]](/blog/?p=705)

 

## Code

[https://github.com/chrismckelt/vita](https://github.com/chrismckelt/vita "https://github.com/chrismckelt/vita")
