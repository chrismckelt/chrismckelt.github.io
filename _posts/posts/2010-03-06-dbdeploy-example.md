---
layout: post
category: posts
title: "DBDeploy Example"
date: "2010-03-06"
categories: 
  - "net"
  - "code"
---

Download source files:[DBDeployExample.zip (4.81 mb)](file:///C:/blog/file.axd?file=DBDeployExample.zip)

Here is a quick example of how to use [DB Deploy](http://dbdeploy.com/)

This example uses NANT to run DBDeploy which in turn generates the _output.sql_ file.  This is then run against the database causing our changes to be run in and putting an entry in the ChangeLog table to tell you which sql files have been run.

To run this sample

1. Download to a location on your computer e.g. _C:\\Examples\\DBDeployExample_
2. Create a database onyour localhost called _DBDeployExample_
3. In a command prompt (Or [PowerShell](http://www.microsoft.com/windowsserver2003/technologies/management/powershell/download.mspx)) use [NANT](http://nant.sourceforge.net/) to build the solution --(I have this setup as an environment variable setup and simply type 'NANT')
4. NANT will build the default.build file and run the database changes

The NANT output should look like this

![](images/image.axd?picture=dbdeploy-cmd1.png) ![](images/image.axd?picture=dbdeploy-cmd2.png) Opening the solution file looks like this ![](images/image.axd?picture=DBDeployExample-DirStructure.png) The database should now look like this ![](images/image.axd?picture=dbdeploy-dbview.png) The ChangeLog table in the database is used by DBDeploy for change management and will look like this:

![](images/image.axd?picture=dbdeploy-ChangeLogTable.png)

The default.build file contains a target that actions DBDeploy

 <target name="script.generate" description="generate a sql upgrade script"> 
 <call target="setup.changelogtable"/> <call target="setup.builddir"/>

 <echo message="Calling dbdeploy with dbConnection=${connstring}..." />

 <dbdeploy dbType="mssql" dbConnection="${connstring}" dir="${script.dir}" outputFile="${output.file}" undoOutputFile="${undo.output.file}" />

<echo message="...finished calling dbdeploy." /> </target>

The output file contains all change files found on the file system that are not contained in the ChangeLog table

The following command runs the output script against the database

 <target name="script.execute"> <echo message="Executing script ${script.execute.filename} against database ${database.name}..." />

<exec program="${sqlcmd}"> <arg value="${script.execute.extraparams}" /> <arg value="-i" />

> <arg value="${script.execute.filename}" /> <arg value="-d" /> <arg value="${database.name}" />

<arg value="-S"/> <arg value="${server}" />

</exec>

<echo message="...finished executing script." />

</target>
