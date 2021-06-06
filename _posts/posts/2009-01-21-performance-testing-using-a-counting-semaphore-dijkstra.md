---
layout: post
category: posts
title: "Performance Testing using a counting semaphore -- Dijkstra"
date: "2009-01-21"
tags: code dotnet
---

Running some performance tests using Fitnesse to test the limits of a new Sharepoint DMS solution produced this solution.

The code we are trying to test is the AddDocument and the time it takes each call to this to succeed.

So we need to add _n_ number of documents to Sharepoint using _x_ number of threads through a WCF service_,_ where _n_ and _x_ are configurable via the Fitnesse wiki

![FitnesseTest1](https://user-images.githubusercontent.com/662868/120909419-72a22880-c6a7-11eb-8507-38df2888b1d2.png)
 

The code used to perform the test uses a [counting semaphore](http://en.wikipedia.org/wiki/Semaphore_(programming) "http://en.wikipedia.org/wiki/Semaphore_(programming)") to kick off _x_ threads which loop while decrementing a [volatile](http://msdn.microsoft.com/en-us/library/x13ttww7(VS.71).aspx "http://msdn.microsoft.com/en-us/library/x13ttww7(VS.71).aspx") variable _AddtoDMSXtimes._

 

> public string FileName;  
> public int ConcurrentThreads = 10;  
> public volatile int AddtoDMSXtimes = 1000;  
> private DijkstraSemaphore semaphore; // counting semaphore  
> private volatile Stack<Guid> guidStack = new Stack<Guid>();  
> public volatile List<Guid> guidList = new List<Guid>();  
> private ManualResetEvent resetEvent = new ManualResetEvent(false);  
> 
> private void Form1\_Load(object sender, EventArgs e)  
> {  
> semaphore = new DijkstraSemaphore(0, ConcurrentThreads);  
> }  
 

In the attached example Each loop just counts to 100 and outputs some info as seen below 



(in the real test this was adding a 275KB document to Sharepoint via a  new DMSServiceClient().AddGeneratedDocument(dmsDocument, documentContent); call).



> private void ExecuteTest()  
> {  
> Console.WriteLine(Thread.CurrentThread.Name);  
> semaphore.Release(); while  (AddtoDMSXtimes >= 0)  
> {  
> AddtoDMSXtimes--;   
> guidList.Add(guidStack.Pop());  
>  for(int exampleCount=0;exampleCount<100-1;exampleCount++)  
> {  
> Console.WriteLine(Thread.CurrentThread.Name + "   " + AddtoDMSXtimes.ToString() + " " + exampleCount.ToString());      
> }  
> }   
> semaphore.Acquire();  
> }  
> 

Initially new guids were being created in the worker threads -- however I noticed when I had more than multiple threads creating Guids at one time I was getting duplicate guids!

So an initial thread is created which branches off and populates a guid stack object with the total number of Guids required.

Whilst this is off processing the required number of threads is created (_x_)
> 
> Thread populateGuidStackThread = new Thread((ThreadStart)
> delegate
> {
> for (int x = 0; x < AddtoDMSXtimes+1; x++)
> {
> guidStack.Push(Guid.NewGuid());
> }
> resetEvent.Set();
> }
> );
> populateGuidStackThread.Start();

The guid creation thread then lets the main thread know it has finished processing and to begin running the worker threads

Once it have finished it reports back the total seconds taken to create the threads and total seconds taken for the threads to finish their work.
> 
> 
> Thread\[\] threads = new Thread\[ConcurrentThreads\];  
> Stopwatch watch = new Stopwatch();  
> watch.Start();  
> int i;  
> for (i = 0; i < ConcurrentThreads; i++)  
> {  
> threads\[i\] = new Thread(new ThreadStart(ExecuteTest));  
> threads\[i\].Name = "Thread " + i.ToString();  
> }  
> watch.Stop();   
> TimeSpan ts1 = watch.Elapsed;  
> if (resetEvent.WaitOne())  
> {  
> watch.Start();  
> foreach (Thread thread in threads)  
> {  
> thread.Start();  
> }  
> watch.Stop();  
> }  
> TimeSpan ts2 = watch.Elapsed;  
> semaphore.WaitForStarvation();  
> Console.WriteLine("----------------- FINISHED -----------------------");  
> Console.WriteLine("Time to build threads in seconds " + ts1.TotalSeconds);  
> Console.WriteLine("Time for threads to finish in seconds " + ts2.TotalSeconds);

Code project class taken from http://www.codeproject.com/KB/recipes/dijkstracountingsemaphore.aspx


