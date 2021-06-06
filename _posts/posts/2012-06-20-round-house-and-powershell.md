---
layout: post
category: posts
title: "Round house and powershell"
date: "2012-06-20"
tags: code
---

Using [https://github.com/chucknorris/roundhouse](https://github.com/chucknorris/roundhouse "https://github.com/chucknorris/roundhouse") for powershell to get automated database change management



    Task UpgradeDatabase{

	if (($environment -eq  'integration') -or ($environment -eq  'uat'))
	{
		# round house variables
		$rh = "C:\\dev\\Phoenix\\trunk\\Tools\\RoundhousE\\console\\rh.exe"
		$connectionString = "Data Source=localhost;Database=XXX; User Id=XXXuser; Password=XXX;Connect Timeout=100;"
		$indexesFolder = "C:\\dev\\Phoenix\\trunk\\Database\\10-Indexes"
		$schemaChangesFolder = "C:\\dev\\Phoenix\\trunk\\Database\\11-SchemaChanges"
		## build server?
		if (Test-Path "D:\\Code\\") 
		{
 			$rh = "D:\\Code\\Phoenix\\trunk\\Tools\\RoundhousE\\console\\rh.exe"
			$indexesFolder = "D:\\Code\\Phoenix\\trunk\\Database\\10-Indexes"
			$schemaChangesFolder = "D:\\Code\\Phoenix\\trunk\\Database\\11-SchemaChanges"
			if ($environment -eq  'integration')
			{
				$connectionString = "Data Source=xxxxx;Database=rrrr\_Integration; User Id=rrrr; Password=rrrr;"
			}
			else
			{
				$connectionString = "Data Source=xxxx;Database=rrrrr\_UAT; User Id=rrrrr; Password=rrrrr;"
			}
		}

		Write\-Host "Database upgrade started"

		$s1 = " -c "
		$s2 = " $connectionString "
		$s3 = " -ix "
		$s4 = "$indexesFolder"
		$s5 = " -u "
		$s6 = "$schemaChangesFolder" 

		$args = $s1 +  " ""$s2" ""  + $s3  + " ""$s4" "" +  $s5 +  " ""$s6""" + " -ni -ct 300"

		Write\-Host $args

		Start-Process -FilePath $rh -ArgumentList $args -PassThru

		Write\-Host "Database upgrade complete"	
	}  
    }

 
The following tables are created

 
![image](https://user-images.githubusercontent.com/662868/120925729-7320da80-c70c-11eb-9fa2-7a5bcce79fab.png)


With the following scripts run

![image](https://user-images.githubusercontent.com/662868/120925760-99467a80-c70c-11eb-8c8e-15f6986d4b9a.png)
