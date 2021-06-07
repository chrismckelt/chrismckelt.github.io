---
layout: post
category: posts
title: "Design Principles"
date: "2009-03-08"
excerpt: 'SOLID'
tags: code software
---

## The Open/Closed Principle

Software entities (classes, modules, etc) should be open for extension, but closed for modification

## The Liskov Substitution Principle

Liskov's notion of "subtype" is based on the notion of [substitutability](http://en.wikipedia.org/wiki/Substitutability "Substitutability"); that is, if S is a subtype of T, then objects of type T in a program may be replaced with objects of type S without altering any of the desirable properties of that program (e.g., [correctness](http://en.wikipedia.org/wiki/Correctness "Correctness")). ie Derived classes must be usable through the base class interface without the need for the user to know the difference

## The Dependency Inversion Principle

High level modules should not depend upon low level modules. Both should depend upon abstractions. Abstractions should not depend upon details. Details should depend upon abstractions.

## The Interface Segregation Principle

The dependency of one class to another one should depend on the smallest possible interface. Many client specific interfaces are better than one general purpose interface/

## The Reuse/Release Equivalency Principle

The unit of reuse is the unit of release. Effective reuse requires tracking of releases from a change control system. The package is the effective unit of reuse and release.

## The Common Closure Principle

Classes that change together, belong together.

## Common Reuse Principle

The classes in a package are reused together. If you reuse one of the classes in a package, you reuse them all

## The Acyclic Dependencies Principle

The dependency structure between packages must be a [Directed Acyclic Graph (DAG)](http://en.wikipedia.org/wiki/Directed_acyclic_graph "http://en.wikipedia.org/wiki/Directed_acyclic_graph").

## The Stable Dependencies Principle

Dependencies between released categories must run in the direction of stability. The dependee must be more stable than the depender.

## The Stable Abstractions Principle

The more stable a class category is, the more it must consist of abstract classes. A completely stable category should consist of nothing but abstract classes.
