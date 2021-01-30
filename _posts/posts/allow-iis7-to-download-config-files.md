---
layout: post
category: posts
title: "Allow IIS7 to download .config files"
date: "2010-03-11"
categories: 
  - "net"
  - "code"
---

1\. In the following file

C:\\Windows\\System32\\inetsrv\\config\\applicationHost.config

Ensure the following

<section name\="requestFiltering" overrideModeDefault\="Allow" />

.csharpcode, .csharpcode pre<br /> {<br /> font-size: small;<br /> color: black;<br /> font-family: consolas, "Courier New", courier, monospace;<br /> background-color: #ffffff;<br /> /\*white-space: pre;\*/<br /> }<br /> .csharpcode pre { margin: 0em; }<br /> .csharpcode .rem { color: #008000; }<br /> .csharpcode .kwrd { color: #0000ff; }<br /> .csharpcode .str { color: #006080; }<br /> .csharpcode .op { color: #0000c0; }<br /> .csharpcode .preproc { color: #cc6633; }<br /> .csharpcode .asp { background-color: #ffff00; }<br /> .csharpcode .html { color: #800000; }<br /> .csharpcode .attr { color: #ff0000; }<br /> .csharpcode .alt<br /> {<br /> background-color: #f4f4f4;<br /> width: 100%;<br /> margin: 0em;<br /> }<br /> .csharpcode .lnum { color: #606060; }

2

This is the web.config file

 

<?xml version\="1.0" encoding\="UTF-8"?\>
<configuration\>
    <system.webServer\>
         <handlers\>
           <clear />
            <add 
                name\="StaticFile" 
                path\="\*" verb\="\*" 
                modules\="StaticFileModule,DefaultDocumentModule,DirectoryListingModule" 
                resourceType\="Either" 
                requireAccess\="Read" />
        </handlers\>
        <staticContent\>
            <mimeMap fileExtension\=".\*" mimeType\="application/octet-stream" />
        </staticContent\>
        <security\>
            <requestFiltering\>
                <fileExtensions allowUnlisted\="true"\>
                    <remove fileExtension\=".config" />
                    <add fileExtension\=".config" allowed\="true" />
                </fileExtensions\>
            </requestFiltering\>
        </security\>
    </system.webServer\>
</configuration\>

.csharpcode, .csharpcode pre<br /> {<br /> font-size: small;<br /> color: black;<br /> font-family: consolas, "Courier New", courier, monospace;<br /> background-color: #ffffff;<br /> /\*white-space: pre;\*/<br /> }<br /> .csharpcode pre { margin: 0em; }<br /> .csharpcode .rem { color: #008000; }<br /> .csharpcode .kwrd { color: #0000ff; }<br /> .csharpcode .str { color: #006080; }<br /> .csharpcode .op { color: #0000c0; }<br /> .csharpcode .preproc { color: #cc6633; }<br /> .csharpcode .asp { background-color: #ffff00; }<br /> .csharpcode .html { color: #800000; }<br /> .csharpcode .attr { color: #ff0000; }<br /> .csharpcode .alt<br /> {<br /> background-color: #f4f4f4;<br /> width: 100%;<br /> margin: 0em;<br /> }<br /> .csharpcode .lnum { color: #606060; }
