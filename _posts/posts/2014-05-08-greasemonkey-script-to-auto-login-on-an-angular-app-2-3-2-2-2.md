---
layout: post
category: posts
title: "GreaseMonkey script to auto login on an Angular app"
date: "2014-05-08"
categories: 
  - "angular"
---

angular.element(document).ready(function () { var $scope \= angular.element(document.getElementById('username')) .scope() if (window.location.href.toString().indexOf('#/admin/signin') !== \-1) { console.log('auto-login enabled admin'); $scope.$apply(function () { // perform any model changes or method invocations here on angular app. $scope.Username \= 'adminusername'; $scope.Password \= 'adminpassword#'; }); } else if (window.location.href.toString().indexOf('#/signin') !== \-1) { console.log('auto-login enabled'); $scope.$apply(function () { // perform any model changes or method invocations here on angular app. $scope.Username \= 'username'; $scope.Password \= 'password'; }); } });
