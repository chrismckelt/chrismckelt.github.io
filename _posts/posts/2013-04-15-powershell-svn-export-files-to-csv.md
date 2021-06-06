---
layout: post
category: posts
title: "Powershell & SVN – Export files to CSV"
date: "2013-04-15"
tags: code
---

> Function Get\-SvnLogData()  
> {  
>     (\[xml\](svn log -v --xml  'http://svn/trunk')).log.logentry | % {  
>         $nestedEntry = $\_  
>         $\_.paths.path | % {  
>             $path = $\_  
>             $nestedEntry | Select\-Object -Property \`  
>                 Author, \`  
>                 @{n='Revision'; e={(\[int\]$\_.Revision)}}, \`  
>                 @{n='Date';     e={Get-Date $\_.Date  }}, \`  
>                 @{n='Action';   e={$path.action      }}, \`  
>                 @{n='Path';     e={$path.InnerText   }}\`  
>         }  
>     }  
> }  
>   
> Get\-SvnLogData |  
> where { $\_.Date -ge (Get\-Date '16-04-2013') } |  
> Select\-Object  Revision,Date,Path,Author |   
> sort -property \`  
>     @{ Expression="Path";     Descending=$false }, \`  
>     @{ Expression="Revision"; Descending=$true  } |  
> Export-CSV c:\\test.csv  
> 
 