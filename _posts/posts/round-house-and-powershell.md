---
layout: post
category: posts
title: "Round house and powershell"
date: "2012-06-20"
categories: 
  - "powershell"
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

 

[![image](images/image.axd?picture=image_thumb_39.png "image")](/blog/image.axd?picture=image_39.png)

 

With the following scripts run

 

[![image](images/image.axd?picture=image_thumb_40.png "image")](/blog/image.axd?picture=image_40.png)
