---
layout: post
category: posts
title: "Powershell & SVN – Export files to CSV"
date: "2013-04-15"
categories: 
  - "net"
  - "code"
  - "powershell"
---

Function Get\-SvnLogData()
{
    (\[xml\](svn log -v --xml  'http://svn/trunk')).log.logentry | % {
        $nestedEntry = $\_
        $\_.paths.path | % {
            $path = $\_
            $nestedEntry | Select\-Object -Property \`
                Author, \`
                @{n='Revision'; e={(\[int\]$\_.Revision)}}, \`
                @{n='Date';     e={Get-Date $\_.Date  }}, \`
                @{n='Action';   e={$path.action      }}, \`
                @{n='Path';     e={$path.InnerText   }}\`
        }
    }
}

Get\-SvnLogData |
where { $\_.Date -ge (Get\-Date '16-04-2013') } |
Select\-Object  Revision,Date,Path,Author | 
sort -property \`
    @{ Expression="Path";     Descending=$false }, \`
    @{ Expression="Revision"; Descending=$true  } |
Export-CSV c:\\test.csv

.csharpcode, .csharpcode pre<br /> {<br /> font-size: small;<br /> color: black;<br /> font-family: consolas, "Courier New", courier, monospace;<br /> background-color: #ffffff;<br /> /\*white-space: pre;\*/<br /> }<br /> .csharpcode pre { margin: 0em; }<br /> .csharpcode .rem { color: #008000; }<br /> .csharpcode .kwrd { color: #0000ff; }<br /> .csharpcode .str { color: #006080; }<br /> .csharpcode .op { color: #0000c0; }<br /> .csharpcode .preproc { color: #cc6633; }<br /> .csharpcode .asp { background-color: #ffff00; }<br /> .csharpcode .html { color: #800000; }<br /> .csharpcode .attr { color: #ff0000; }<br /> .csharpcode .alt<br /> {<br /> background-color: #f4f4f4;<br /> width: 100%;<br /> margin: 0em;<br /> }<br /> .csharpcode .lnum { color: #606060; }
