---
layout: post
category: posts
title: "How to use DB Deploy"
date: "2008-08-28"
tags: dotnet code
---

Here is a quick example of how to use [DB Deploy](http://dbdeploy.com/ "http://dbdeploy.com/")

This example uses NANT to run DBDeploy which in turn generates the _output.sql_ file.  This is then run against the database causing our changes to be run in and putting an entry in the ChangeLog table to tell you which sql files have been run.

To run this sample

1. Download to a location on your computer e.g. _C:\\Examples\\DBDeployExample_
2. Create a database onyour localhost called _DBDeployExample_
3. In a command prompt (Or [PowerShell](http://www.microsoft.com/windowsserver2003/technologies/management/powershell/download.mspx "http://www.microsoft.com/windowsserver2003/technologies/management/powershell/download.mspx")) use [NANT](http://nant.sourceforge.net/ "http://nant.sourceforge.net/") to build the solution --(I have this setup as an environment variable setup and simply type 'NANT')
4. NANT will build the default.build file and run the database changes

Opening the solution file looks like this

![DBDeployExample-DirStructure](https://user-images.githubusercontent.com/662868/120909014-e68e0200-c6a2-11eb-9061-589509d95243.png)

### Setup DB Deploy change tables

![dbdeploy-cmd2](https://user-images.githubusercontent.com/662868/120908996-b9415400-c6a2-11eb-9a56-6e88167e4988.png)

The ChangeLog table in the database is used by DBDeploy for change management and will look like this:

 ![dbdeploy-ChangeLogTable](https://user-images.githubusercontent.com/662868/120908997-bba3ae00-c6a2-11eb-9c20-34b0d1eb226a.png)
 

The default.build file contains a target that actions DBDeploy

> <target name\="script.generate" description\="generate a sql upgrade script"\>    
> <call target\="setup.changelogtable"/>     
> <call target\="setup.builddir"/>     
> <echo message\="Calling dbdeploy with dbConnection=${connstring}..." />     
> <dbdeploy dbType\="mssql"dbConnection\="${connstring}"  dir\="${script.dir}> outputFile\="${output.file}"  undoOutputFile\="${undo.output.file}" />   
> <echo message\="...finished calling dbdeploy." />   
> </target>



The output file contains all change files found on the file system that are not contained in the ChangeLog table


The following command runs the output script against the database

> <target name\="script.execute"\>  
> <echo message\="Executing script ${script.execute.filename} against database ${database.name}..." />      
> <exec program\="${sqlcmd}"\>  
> <arg value\="${script.execute.extraparams}" />    
> <arg value\="-i" />    
> <arg value\="${script.execute.filename}" />    
> <arg value\="-d" />  
> <arg value\="${database.name}" />  
> <arg value\="-S"/>  
> <arg value\="${server}" />  
> </exec\>  
> <echo message\="...finished executing script." />  
> </target\> 


## Output

SQL change scripts are executed against the DB & recorded in the DBDeploy table

![cmd1](https://user-images.githubusercontent.com/662868/120909197-3077e780-c6a5-11eb-9429-194ab0a26bab.png)
