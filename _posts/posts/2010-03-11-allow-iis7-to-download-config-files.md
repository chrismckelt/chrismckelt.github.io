---
layout: post
category: posts
title: "Allow IIS7 to download .config files"
date: "2010-03-11"
categories: 
  - "net"
  - "code"
---

### In the following file

    C:\\Windows\\System32\\inetsrv\\config\\applicationHost.config

Ensure the following

    <section name\="requestFiltering" overrideModeDefault\="Allow" />

### In the Web.Config
 

>     <?xml version\="1.0" encoding\="UTF-8"?\>
> <configuration\>   
>     <system.webServer\>  
>          <handlers\>  
>            <clear />  
>             <add 
>                 name\="StaticFile" 
>                 path\="\*" verb\="\*" 
>                 modules\="StaticFileModule,DefaultDocumentModule,DirectoryListingModule" 
>                 resourceType\="Either" 
>                 requireAccess\="Read" />  
>         </handlers\>
>         <staticContent\>  
>             <mimeMap fileExtension\=".\*" mimeType\="application/octet-stream" />  
>         </staticContent\>  
>         <security\>  
>             <requestFiltering\>  
>                 <fileExtensions allowUnlisted\="true"\>  
>                     <remove fileExtension\=".config" />  
>                     <add fileExtension\=".config" allowed\="true" />  
>                 </fileExtensions\>  
>             </requestFiltering\>  
>         </security\>  
>     </system.webServer\>  
> </configuration\>
