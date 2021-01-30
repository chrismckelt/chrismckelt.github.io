---
layout: post
category: posts
title: "PSAKE- Gallio–SpecFlow–XUnit–on the build server"
date: "2011-11-14"
categories: 
  - "code"
  - "powershell"
  - "tdd-bdd"
---

This is a quick post to show how to get PowerShell with [PSAKE](http://codebetter.com/jameskovacs/2010/04/12/psake-v4-00/) to run [Specflow](http://specflow.org/) acceptance tests on the build server using [Gallio](http://www.gallio.org/).

[https://github.com/chrismckelt/PS-Sake-BuildScripts](https://github.com/chrismckelt/PS-Sake-BuildScripts "https://github.com/chrismckelt/PS-Sake-BuildScripts")

 

The folder with our build scripts looks like this

 

[![image](https://raw.githubusercontent.com/chrismckelt/chrismckelt.github.io/master/_posts/posts/images//image.axd?picture=image_thumb_38.png "image")](http://www.mckelt.com/image.axd?picture=image_38.png)

 

The environments folder contains all of the configuration files for each environment

 

[![image](https://raw.githubusercontent.com/chrismckelt/chrismckelt.github.io/master/_posts/posts/images//image.axd?picture=image_thumb_34.png "image")](http://www.mckelt.com/image.axd?picture=image_34.png)

 

Build server process

 

The build/check-in process is now as follows.

1\. Developer checks in.

2\. Build server detects changes – cleans everything and then pulls down latest SVN code

3\. Cruise control builds code in release mode –> If compile fails –> build fail

4\. Unit tests run in place à if any test fails –> build fail

5\. On success a new folder with the VERSION number is created      --- _this is our artefacts_

6\. This build is then deployed to the integration server and the Acceptance tests are run – Results are output to a folder

7\. If the Acceptance tests fails -> build fail

8\. On Success the build is packaged for deployment (config files modified for environment)

 

Cruise control originally calls default.ps1 passing in the environment.

<powershell> <scriptsDirectory>C:\\CCWorking\\Phoenix\\CodeBase\\Build\\BuildScripts</scriptsDirectory><!--Scrips folder--> <script>default.ps1</script> <buildArgs>-environment:integration</buildArgs><!-- Project working folder -workingDir C:\\project1\\working--> <executable>C:\\Windows\\System32\\WindowsPowerShell\\v1.0\\powershell.exe</executable> <buildTimeoutSeconds>10000</buildTimeoutSeconds> <description>Phoenix Build</description> </powershell>

 

This calls build.ps1

PSAKE tasks include

 

Task default -depends PrintInformation, CleanEnvironment, Clean, SetupEnvironment, MakeBuild, BuildDatabase, DeployIntegrationBuild, IntegrationTest, DeployBuild, UpdateConfigFiles

 

# To run the acceptance tests using Gallio

The following Powershell function is used:@

_Usage for Gallio tests:_

_Task Test-System -Description "Runs the system tests via Gallio" { Test-Gallio $proj $configuration $platform $dll "system" "Test.System" # Test-Gallio ".\\src\\Test.Unit\\Test.Unit.csproj" Release x64 ".\\src\\Test.Unit\\bin\\Release\\x64\\Test.Unit.dll" "unit" } } #> function Test-Gallio($proj, $configuration, $platform, $dll, $report\_name, $report\_dir, $namespace\_filter){ Write-Host "GALLIO TEST proj --  $proj" Write-Host "GALLIO TEST configuration --  $configuration" Write-Host "GALLIO TEST platform --  $platform" Write-Host "GALLIO TEST dll --  $dll" Write-Host "GALLIO TEST report\_name --  $report\_name" Write-Host "GALLIO TEST namespace\_filter --  $namespace\_filter"_

 _if ( (Get-PSSnapin -Name Gallio -ErrorAction SilentlyContinue) -eq $null ) { Add-PSSnapin Gallio } $result = Run-Gallio $dll -filter Namespace:/$namespace\_filter/ -ReportTypes text,html,xml -ReportDirectory $report\_dir  -ReportNameFormat $report\_name-test-report  #-NoProgress -NoEchoResults get-content "$report\_dir\\$report\_name-test-report.txt" | Write-Host Write-Host $result.ResultsSummary if ($result.Statistics.FailedCount -gt 0) { Write-Warning "Some unit tests have failed." $Error = $result.ResultsSummary exit $result.ResultCode }

Remove-PSSnapin Gallio

}_

 

From an acceptance test entered into SpecFlow as follows:

[![image](https://raw.githubusercontent.com/chrismckelt/chrismckelt.github.io/master/_posts/posts/images//image.axd?picture=image_thumb_35.png "image")](http://www.mckelt.com/image.axd?picture=image_35.png)

On checkin the build will run and output the result of this test in a report called _AcceptanceTests-test-report.html_ 

[![image](https://raw.githubusercontent.com/chrismckelt/chrismckelt.github.io/master/_posts/posts/images//image.axd?picture=image_thumb_36.png "image")](http://www.mckelt.com/image.axd?picture=image_36.png)

[![image](https://raw.githubusercontent.com/chrismckelt/chrismckelt.github.io/master/_posts/posts/images//image.axd?picture=image_thumb_37.png "image")](http://www.mckelt.com/image.axd?picture=image_37.png)
