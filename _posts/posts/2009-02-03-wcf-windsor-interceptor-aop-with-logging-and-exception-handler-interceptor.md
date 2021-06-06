---
layout: post
category: posts
title: "WCF Windsor Interceptor, AOP with Logging and Exception Handler Interceptor"
date: "2009-02-03"
tags: code dotnet
---


The above example uses Windows Communication Foundation (WCF) Integration Facility

The main idea here is that WCF should get the service instances from Windsor, which gives us all the usual advantages of [IoC](http://en.wikipedia.org/wiki/Inversion_of_control "http://en.wikipedia.org/wiki/Inversion_of_control") _plus Windsor's interceptors_ capabilities. It isn't a lot of code, but it makes it _much_ easier to work with WCF services.

The service contains 2 simple methods on the HelloWorld class.

> - SayHello -- returns a string "Hello World"   
> - ThrowException -- throws an exception

Every single call to each of these is intercepted and logged.  Whilst the throw exception will always be caught by the exceptionhandler interceptor.

The first project in the solution is the WCF service, whilst the second is just a WPF client tester. The project outlook is as follows:

![image](https://user-images.githubusercontent.com/662868/120915624-4900f580-c6d7-11eb-894f-27850d270539.png)
