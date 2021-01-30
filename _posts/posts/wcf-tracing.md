---
layout: post
category: posts
title: "WCF Tracing"
date: "2009-11-13"
categories: 
  - "net"
  - "code"
---

In the web.config in between the <configuration></configuration> tags

 

<system.diagnostics\>
            <sources\>
                  <source name\="System.ServiceModel"
                              switchValue\="Information, ActivityTracing"
                              propagateActivity\="true"\>
                        <listeners\>
                              <add name\="traceListener"
                                    type\="System.Diagnostics.XmlWriterTraceListener"
                                    initializeData\= "WCFTrace.svclog" />
                        </listeners\>
                  </source\>
            </sources\>
            <trace autoflush\="true" />
      </system.diagnostics\>

.csharpcode, .csharpcode pre<br />{<br /> font-size: small;<br /> color: black;<br /> font-family: consolas, "Courier New", courier, monospace;<br /> background-color: #ffffff;<br /> /\*white-space: pre;\*/<br />}<br />.csharpcode pre { margin: 0em; }<br />.csharpcode .rem { color: #008000; }<br />.csharpcode .kwrd { color: #0000ff; }<br />.csharpcode .str { color: #006080; }<br />.csharpcode .op { color: #0000c0; }<br />.csharpcode .preproc { color: #cc6633; }<br />.csharpcode .asp { background-color: #ffff00; }<br />.csharpcode .html { color: #800000; }<br />.csharpcode .attr { color: #ff0000; }<br />.csharpcode .alt<br />{<br /> background-color: #f4f4f4;<br /> width: 100%;<br /> margin: 0em;<br />}<br />.csharpcode .lnum { color: #606060; }
