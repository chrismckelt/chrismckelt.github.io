---
layout: post
category: posts
title: "Log4Net config"
tags: dotnet log4net
---

## Log4Net config
> 
>     <?  xml version\="1.0" encoding\="utf-8" ?\>
> <configuration\>  
>   <log4net\>  
>     <appender name\="WindowsEventAppender" type\="log4net.Appender.EventLogAppender"\>  
>       <param name\="LogName" value\="Application" />  
>       <applicationName value\="Phoenix" />  
>       <layout type\="log4net.Layout.PatternLayout"\>  
>         <conversionPattern value\="%date \[%thread\] %-5level %logger%newline =&gt; %message%newline" />  
>       </layout\>  
>       <filter type\="log4net.Filter.LevelRangeFilter"\>   
>         <param name\="LevelMin" value\="DEBUG" />  
>       </filter\>  
>     </appender\>  
>     <appender name\="RollingFile" type\="log4net.Appender.RollingFileAppender"\>  
>       <file type\="log4net.Util.PatternString"   value\="logs\\\\phoenix.log" />  
>       <appendToFile value\="true" />  
>       <maximumFileSize value\="500KB" />  
>       <maxSizeRollBackups value\="2" />  
>       <layout type\="log4net.Layout.PatternLayout"\>  
>         <conversionPattern value\="%date \[%thread\] %-5level %logger%newline =&gt; %message%newline" />  
>       </layout\>  
>     </appender\>    
>     <root\>  
>       <level value\="DEBUG" />    
>       <appender-ref ref\="WindowsEventAppender" />  
>       <appender-ref ref\="RollingFile" />  
>     </root\>  
>   </log4net\>  
> </configuration\>  
 