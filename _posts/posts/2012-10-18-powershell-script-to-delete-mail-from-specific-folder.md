---
layout: post
category: posts
title: "Powershell script to delete mail from specific folder"
date: "2012-10-18"
tags: code
---
```

function Remove-MailItem {            
	[CmdletBinding(SupportsShouldProcess=$true)]            
param (            
 [parameter(Mandatory=$true)]            
 [string]$mailfolder,                    
 [string]$parentfolder                                 
)            
$ol = new-object -comobject "Outlook.Application";
# Map to the MAPI namespace
$mapi = $ol.getnamespace("mapi");
$targetfolder  = ''
if ($parentfolder.Length -eq 0)
{
	$targetfolder = $mapi.Folders.Item(1).Folders.Item("Inbox").Folders.Item($mailfolder)
}
else
{
	$targetfolder = $mapi.Folders.Item(1).Folders.Item("Inbox").Folders.Item($parentfolder).Folders.Item($mailfolder)
}
foreach ($item in $targetfolder.Items)  {     
		try {
			$item.Delete()       
		}
		catch [Exception]{
			Write-Host "Failed to delete item: " + $item.Subject
			Write-Host $_.Exception.ToString()
		}
   
}
function Clear-DeletedMail {            
            
$outlook = New-Object -ComObject Outlook.Application            
foreach ($folder in $outlook.Session.Folders){            
  foreach($mailfolder in $folder.Folders ) {            
    if ($mailfolder.Name -eq "Deleted Items" -and $mailfolder.Items.Count -gt 0){            
      foreach ($item in $mailfolder.Items){$item.Delete()}            
    }               
  }             
}            
}
}
Remove-MailItem -mailfolder "Cafe"
Remove-MailItem -mailfolder "COLSupport"
Remove-MailItem -mailfolder "Failed Logins" -parentfolder "COLSupport"
Remove-MailItem -mailfolder "IISReset" -parentfolder "COLSupport"
Remove-MailItem -mailfolder "Not Urgent" -parentfolder "COLSupport"
Remove-MailItem -mailfolder "Production" -parentfolder "COLSupport"

```
![image](https://user-images.githubusercontent.com/662868/120943129-9de84e80-c75f-11eb-8afe-d55f54735e3f.png)