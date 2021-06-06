---
layout: post
category: posts
title: "NServiceBus Powershell Install"
date: "2013-03-03"
tags: code
---

## NServiceBus PowerShell Install
```
$Arguments = '/install /startManually /serviceName:{0} /displayName:{0} NServiceBus.Production /username:{1} /password:{2}' -f 'McKelt.securitytokenservice.nservicebus.messagehandler', 'xxxx@au.McKelt.net', 'PasswordGoesHere'

$nServiceBus = Resolve-Path -Path nServiceBus.Host.exe;
Write-Host -Object ('Argument string is: {0}' -f $Arguments);
Write-Host -Object ('Path to nServiceBus.Host.exe is: {0}' -f $nServiceBus);
Start-Process -Wait -NoNewWindow -FilePath $nServiceBus -ArgumentList $Arguments -RedirectStandardOutput tmp.txt;
```