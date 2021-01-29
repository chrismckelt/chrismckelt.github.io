---
layout: post
category: posts
title: "NServiceBus Powershell Install"
date: "2013-03-03"
categories: 
  - "code"
---

$Arguments = '/install /startManually /serviceName:{0} /displayName:{0} NServiceBus.Production /username:{1} /password:{2}' -f 'McKelt.securitytokenservice.nservicebus.messagehandler', 'xxxx@au.McKelt.net', 'PasswordGoesHere'

$nServiceBus = Resolve-Path -Path nServiceBus.Host.exe;

[Write](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=Write&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99)\-Host -Object ('Argument string is: {0}' -f $Arguments);

[Write](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=Write&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99)\-Host -Object ('Path to nServiceBus.Host.exe is: {0}' -f $nServiceBus);

Start-Process -Wait -NoNewWindow -FilePath $nServiceBus -ArgumentList $Arguments -RedirectStandardOutput tmp.txt;
