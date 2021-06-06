---
layout: post
category: posts
title: "Powershell script to delete mail from specific folder"
date: "2012-10-18"
tags: code
---

[function](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=function&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99)         Remove-MailItem {            

	\[CmdletBinding(SupportsShouldProcess=$[true](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=true&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99))\]            

param (            

 \[parameter(Mandatory=$[true](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=true&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99))\]            

 \[string\]$mailfolder,                    

 \[string\]$parentfolder                                 

)            

$ol = new-object -comobject "Outlook.Application";

\# Map [to](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=to&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99) the MAPI namespace

$mapi = $ol.getnamespace("mapi");

$targetfolder  = ''

[if](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=if&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99) ($parentfolder.Length -eq 0)

{

	$targetfolder = $mapi.Folders.Item(1).Folders.Item("Inbox").Folders.Item($mailfolder)

}

[else](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=else&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99)

{

	$targetfolder = $mapi.Folders.Item(1).Folders.Item("Inbox").Folders.Item($parentfolder).Folders.Item($mailfolder)

}

foreach ($item [in](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=in&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99) $targetfolder.Items)  {     

		try {

			$item.[Delete](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=Delete&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99)()       

		}

		catch \[[Exception](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=Exception&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99)\]{

			[Write](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=Write&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99)\-Host "Failed to delete item: " + $item.Subject

			[Write](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=Write&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99)\-Host $\_.[Exception](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=Exception&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99).ToString()

		}

}

[function](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=function&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99) Clear-DeletedMail {            

$outlook = New-Object -ComObject Outlook.Application            

foreach ($folder [in](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=in&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99) $outlook.[Session](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=Session&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99).Folders){            

  foreach($mailfolder [in](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=in&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99) $folder.Folders ) {            

    [if](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=if&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99) ($mailfolder.Name -eq "Deleted Items" -[and](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=and&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99) $mailfolder.Items.[Count](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=Count&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99) -gt 0){            

      foreach ($item [in](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=in&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99) $mailfolder.Items){$item.[Delete](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=Delete&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99)()}            

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

 

[![image](images/image.axd?picture=image_thumb_41.png "image")](/blog/image.axd?picture=image_41.png)
