---
layout: post
category: posts
title: "Context Specification"
date: "2009-10-12"
tags: dotnet code tdd
---
# Context Specification Base Class

> 
> namespace Example
> {
>     using System;
>     using Microsoft.VisualStudio.TestTools.UnitTesting;
>     using Rhino.Mocks;
> 
>     public abstract class ContextSpecification<T>
>     {
>         protected Exception executionException;
> 
>         protected T sut { get; set; }
> 
>         [TestInitialize\]
>         public void Start()
>         {
>             this.Context();
>             this.SetupMockResults();
>             this.Because();
>         }
> 
>         [TestCleanup\]
>         public void CleanUp()
>         {
>             this.Clean();
>         }
> 
>         protected virtual void Context()
>         {
>         }
> 
>         protected virtual void SetupMockResults()
>         {
>         }
> 
>         protected virtual void Because()
>         {
>         }
> 
>         protected virtual void Clean()
>         {
>         }
> 
>         protected TInterface GetDependency<TInterface>() where TInterface : class
>         {
>             return MockRepository.GenerateMock<TInterface>();
>         }
> 
>         public void Execute(Action action)
>         {
>             try
>             {
>                 action();
>             }
>             catch (Exception ex)
>             {
>                 executionException = ex;
>             }
>         }
> 
>     }
> }
> 