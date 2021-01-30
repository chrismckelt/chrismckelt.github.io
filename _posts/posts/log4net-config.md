---
layout: post
category: posts
title: "Log4Net config"
date: "2009-10-15"
categories: 
  - "net"
---

<?xml version\="1.0" encoding\="utf-8" ?\>
<configuration\>
  <log4net\>
    <appender name\="WindowsEventAppender" type\="log4net.Appender.EventLogAppender"\>
      <param name\="LogName" value\="Application" />
      <applicationName value\="Phoenix" />
      <layout type\="log4net.Layout.PatternLayout"\>
        <conversionPattern value\="%date \[%thread\] %-5level %logger%newline =&gt; %message%newline" />
      </layout\>
      <filter type\="log4net.Filter.LevelRangeFilter"\>
        <param name\="LevelMin" value\="DEBUG" />
      </filter\>
    </appender\>
    <appender name\="RollingFile" type\="log4net.Appender.RollingFileAppender"\>
      <file type\="log4net.Util.PatternString"   value\="logs\\\\phoenix.log" />
      <appendToFile value\="true" />
      <maximumFileSize value\="500KB" />
      <maxSizeRollBackups value\="2" />
      <layout type\="log4net.Layout.PatternLayout"\>
        <conversionPattern value\="%date \[%thread\] %-5level %logger%newline =&gt; %message%newline" />
      </layout\>
    </appender\>

    <root\>
      <level value\="DEBUG" />
      <appender-ref ref\="WindowsEventAppender" />
      <appender-ref ref\="RollingFile" />
    </root\>
  </log4net\>
</configuration\>

.csharpcode, .csharpcode pre<br /> {<br /> font-size: small;<br /> color: black;<br /> font-family: consolas, "Courier New", courier, monospace;<br /> background-color: #ffffff;<br /> /\*white-space: pre;\*/<br /> }<br /> .csharpcode pre { margin: 0em; }<br /> .csharpcode .rem { color: #008000; }<br /> .csharpcode .kwrd { color: #0000ff; }<br /> .csharpcode .str { color: #006080; }<br /> .csharpcode .op { color: #0000c0; }<br /> .csharpcode .preproc { color: #cc6633; }<br /> .csharpcode .asp { background-color: #ffff00; }<br /> .csharpcode .html { color: #800000; }<br /> .csharpcode .attr { color: #ff0000; }<br /> .csharpcode .alt<br /> {<br /> background-color: #f4f4f4;<br /> width: 100%;<br /> margin: 0em;<br /> }<br /> .csharpcode .lnum { color: #606060; }
