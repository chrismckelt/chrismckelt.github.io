---
layout: post
category: posts
title: "WCF Tracing"
date: "2009-11-13"
tags: dotnet
---

In the web.config in between the <configuration></configuration> tags
> 
>  <system.diagnostics\>  
>             <sources\>  
>                   <source name\="System.ServiceModel"
>                               switchValue\="Information, ActivityTracing"
>                               propagateActivity\="true"\>  
>                         <listeners\>  
>                               <add name\="traceListener"
>                                     type\="System.Diagnostics.XmlWriterTraceListener"
>                                     initializeData\= "WCFTrace.svclog" />  
>                         </listeners\>  
>                   </source\>  
>             </sources\>  
>             <trace autoflush\="true" />  
>       </system.diagnostics\>  
> 
 
