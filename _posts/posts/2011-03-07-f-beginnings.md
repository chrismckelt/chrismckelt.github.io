---
layout: post
category: posts
title: "F# Beginnings"
date: "2011-03-07"
categories: 
  - "f"
---

open System

#light // Hello world let a = "Hello, world!";

let mutable b = 0;
while b < 10 do b <- b+1
     Console.WriteLine(a + b.ToString())

Console.ReadLine() |> ignore
            

// recursive binary search  - http://rosettacode.org/wiki/Binary\_search#F.23 let items = \[| 10; 20; 30; 40; 50; 60; 70; 80; 90; 100; |\]

let rec binarySearch (arr:int\[\], low:int, high:int, value:int) =
    if (high < low) then
        false
    else
        let mid = (low + high) / 2
 
        if (arr.\[mid\] > value) then binarySearch (arr, low, mid-1, value)
        else if (arr.\[mid\] < value) then binarySearch (arr, mid+1, high, value)
        else
            true

let result = binarySearch (items, 0, 10, 80);
let any\_to\_string = sprintf "%A" printf "Binary Search found: %s\\n" (any\_to\_string result);

Console.ReadLine() |> ignore

// run for one minute let time = DateTime.Now.AddHours(0.5).Ticks;
let mutable continueRunning = true
let dt = DateTime.Now.AddMinutes(1.0)
while continueRunning do
    let currentTime = System.DateTime.Now.ToString()
    Console.WriteLine currentTime |> ignore
    if System.DateTime.Now.Ticks > dt.Ticks then continueRunning <- false
