---
layout: post
category: posts
title: "NANT CruiseControl NUnit   - How to get unit tests running and displaying on the build server"
date: "2008-12-23"
categories: 
  - "net"
  - "tdd-bdd"
  - "tools"
---

## NANT CruiseControl NUnit   - How to get unit tests running and displaying on the build server

>     <target name="webtest" haltonfailure="false" failonerror="true"\>
>     <echo message="Running unit tests"/>
>         <echo message="${project.local.folder}"/>
>         <property name="test\_dll\_folder" value\="${project.local.folder}\\McKelt.Tests\\bin\\Debug\\"/>
>         <exec failonerror="true" program="${nunit-console.exe}" workingdir="${project.local.folder}" verbose="true" append="true" commandline="${test\_dll\_folder}McKelt.Tests.dll /xml=${test\_dll\_folder}TestResults.xml /output=${test\_dll\_folder}TestOutput.txt /err=${test\_dll\_folder}TestErrorOutput.txt">
>     </exec>
>     <echo message="-- TEST COMPLETE -- "/>
> </target>

 

In the following file – c:\\program files\\CruiseControl.Net\\Server\\CCNet.config

>      <msbuild\>
> <executable\>C:\\WINDOWS\\Microsoft.NET\\Framework\\v4.0.30319\\MSBuild.exe</executable\>
> <workingDirectory\>C:\\CCWorking\\McKeltOnlineForms\\CodeBase\\</workingDirectory\>
> <projectFile\>McKeltOnlineForms.sln</projectFile\>
> <buildArgs\> /noconsolelogger /p:Configuration=Debug /v:quiet </buildArgs\>
> <targets\>Rebuild</targets\>
> <timeout\>120</timeout\>
> <logger\>c:\\Program Files\\CruiseControl.NET\\server\\Rodemeyer.MsBuildToCCNet.dll</logger\>
> </msbuild\>
> </tasks\>
> <publishers\>
> <merge\>
> <files\>
> <file\>C:\\CCWorking\\McKeltOnlineForms\\CodeBase\\McKelt.Web.Application.Tests\\bin\\Debug\\testresults.xml</file\>
> <file\>C:\\CCWorking\\McKeltOnlineForms\\CodeBase\\McKelt.Service.Implementations.Tests\\bin\\Debug\\testresults.xml</file\>
> <file\>C:\\CCWorking\\McKeltOnlineForms\\CodeBase\\Tests\\AcceptanceTests\\bin\\Debug\\TestResults.xml</file\>
> <file\>C:\\CCWorking\\McKeltOnlineForms\\CodeBase\\McKelt.Web.Application.Tests\\bin\\Debug\\\*Output.txt</file\>
> <file\>C:\\CCWorking\\McKeltOnlineForms\\CodeBase\\McKelt.Service.Implementations.Tests\\bin\\Debug\\\*Output.txt</file\>
> <file\>C:\\CCWorking\\McKeltOnlineForms\\CodeBase\\Tests\\AcceptanceTests\\bin\\Debug\\\*Output.txt</file\>
> </files\>
> </merge\>
> <xmllogger/>
> </publishers\>

In your dashboard.config

>     <?xml version\="1.0" encoding\="utf-8"?\>  
> <dashboard\>
> <remoteServices\>
> <servers\>  
> <!-- Update this list to include all the servers you want to connect to. NB - each server name must be unique -->  
> <server name\="local" url\="tcp://localhost:21234/CruiseManager.rem" allowForceBuild\="true" allowStartStopBuild\="true" backwardsCompatible\="false" />  
> </servers\>
> </remoteServices\>
> <xslFileNames\>  
> <xslFile\>xsl\\header.xsl</xslFile\>  
> <xslFile\>xsl\\msbuild2ccnet.xsl</xslFile\>  
> <xslFile\>xsl\\modifications.xsl</xslFile\>  
> <xslFile\>xsl\\unittests.xsl</xslFile\>  
> </xslFileNames\>
> </buildReportBuildPlugin\>
> <buildLogBuildPlugin />  
> <xslReportBuildPlugin description\="NAnt Output" actionName\="NAntOutputBuildReport" xslFileName\="xsl\\NAnt.xsl"\></xslReportBuildPlugin\>  
> <xslReportBuildPlugin description\="NAnt Timings" actionName\="NAntTimingsBuildReport" xslFileName\="xsl\\NAntTiming.xsl"\></xslReportBuildPlugin\>  
> <xslReportBuildPlugin description\="NUnit Details" actionName\="NUnitDetailsBuildReport" xslFileName\="xsl\\tests.xsl"\></xslReportBuildPlugin\>  
> <xslReportBuildPlugin description\="NUnit Timings" actionName\="NUnitTimingsBuildReport" xslFileName\="xsl\\timing.xsl"\></xslReportBuildPlugin\>  
> </buildPlugins\>
> <securityPlugins\>
> <simpleSecurity />
> </securityPlugins\>
> </plugins\>
> </dashboard\>