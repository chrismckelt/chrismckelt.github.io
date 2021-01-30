---
layout: post
category: posts
title: "XUnit Ignore test at runtime (with SpecFlow tag @IgnoreLocally)"
date: "2013-10-06"
categories: 
  - "net"
  - "code"
  - "tdd-bdd"
---

XUnit Ignore test at runtime (with SpecFlow tag @IgnoreLocally)

Code --> [https://github.com/chrismckelt/XUnit.OptionallyIgnore](https://github.com/chrismckelt/XUnit.OptionallyIgnore)

NuGet --> [https://www.nuget.org/packages/Xunit.SpecFlow.AssertSkip/](https://www.nuget.org/packages/Xunit.SpecFlow.AssertSkip/)

As Xunit has no Assert.Ignore() using the OptionallyIgnoreTestFactAttribute attribute on a method and setting McKeltCustom.SpecflowPlugin.Settings.IgnoreLocally == true will ignore the test at runtime

In SpecFlow set this as a plugin and use the tag @IgnoreLocally -- before each test scenario is run turn on/off the setting to run this test.

 

 

 

Sample usage:

 

namespace Tester

{

    public class Dummy

    {

        public Dummy()

        {

            McKeltCustom.SpecflowPlugin.Settings.IgnoreLocally = true;

        }

    }

    public class TestFixture : IUseFixture<Dummy>

    {

        \[OptionallyIgnoreTestFact\]

        public void DoesThisWork()

        {

            Assert.True(false, "This should not be run");

        }

        public void SetFixture(Dummy data)

        {

            throw new NotImplementedException();

        }

    }

}

.csharpcode, .csharpcode pre<br />{<br /> font-size: small;<br /> color: black;<br /> font-family: consolas, "Courier New", courier, monospace;<br /> background-color: #ffffff;<br /> /\*white-space: pre;\*/<br />}<br />.csharpcode pre { margin: 0em; }<br />.csharpcode .rem { color: #008000; }<br />.csharpcode .kwrd { color: #0000ff; }<br />.csharpcode .str { color: #006080; }<br />.csharpcode .op { color: #0000c0; }<br />.csharpcode .preproc { color: #cc6633; }<br />.csharpcode .asp { background-color: #ffff00; }<br />.csharpcode .html { color: #800000; }<br />.csharpcode .attr { color: #ff0000; }<br />.csharpcode .alt<br />{<br /> background-color: #f4f4f4;<br /> width: 100%;<br /> margin: 0em;<br />}<br />.csharpcode .lnum { color: #606060; }

 

SpecFlow tag can be on a feature or a scenario

[![image](images/image_thumb_42.png "image")](files/image_42.png)

Add the following to the .config file

 

<specFlow\>
    <!--<unitTestProvider name="xUnit" />-->
    <generator allowDebugGeneratedFiles\="false" allowRowTests\="true" generateAsyncTests\="false" path\=".\\lib"/>
    <runtime stopAtFirstError\="false" missingOrPendingStepsOutcome\="Ignore"\>
      <!--<dependencies>
 <register name="CustomGeneratorPlugin" type="PhoenixCustom.Generator.SpecflowPlugin.CustomGeneratorPlugin, PhoenixCustom.Generator.SpecflowPlugin" as="PhoenixCustom.Generator.SpecflowPlugin.CustomGeneratorPlugin, PhoenixCustom.Generator.SpecflowPlugin" />
 <register name="CustomGeneratorProvider" type="PhoenixCustom.Generator.SpecflowPlugin.CustomGeneratorProvider, PhoenixCustom.Generator.SpecflowPlugin" as="PhoenixCustom.Generator.SpecflowPlugin.CustomGeneratorProvider, PhoenixCustom.Generator.SpecflowPlugin" />
 </dependencies>-->
    </runtime\>
    <trace traceSuccessfulSteps\="true" traceTimings\="false" minTracedDuration\="0:0:0.1" stepDefinitionSkeletonStyle\="RegexAttribute"/>
    <plugins\>
      <add name\="McKeltCustom" path\=".\\lib" type\="GeneratorAndRuntime"/>
    </plugins\>
    <!-- For additional details on SpecFlow configuration options see http://go.specflow.org/doc-config -->
    <stepAssemblies\>
      <!-- This attribute is required in order to use StepArgument Transformation as described here; 
 https://github.com/marcushammarberg/SpecFlow.Assist.Dynamic/wiki/Step-argument-transformations  -->
      <!-- This attribute is required in order to use StepArgument Transformation as described here; 
 https://github.com/marcusoftnet/SpecFlow.Assist.Dynamic/wiki/Step-argument-transformations  -->
      <stepAssembly assembly\="SpecFlow.Assist.Dynamic"/>
    </stepAssemblies\>
  </specFlow\>

.csharpcode, .csharpcode pre<br />{<br /> font-size: small;<br /> color: black;<br /> font-family: consolas, "Courier New", courier, monospace;<br /> background-color: #ffffff;<br /> /\*white-space: pre;\*/<br />}<br />.csharpcode pre { margin: 0em; }<br />.csharpcode .rem { color: #008000; }<br />.csharpcode .kwrd { color: #0000ff; }<br />.csharpcode .str { color: #006080; }<br />.csharpcode .op { color: #0000c0; }<br />.csharpcode .preproc { color: #cc6633; }<br />.csharpcode .asp { background-color: #ffff00; }<br />.csharpcode .html { color: #800000; }<br />.csharpcode .attr { color: #ff0000; }<br />.csharpcode .alt<br />{<br /> background-color: #f4f4f4;<br /> width: 100%;<br /> margin: 0em;<br />}<br />.csharpcode .lnum { color: #606060; }

 

Finally put the all compiled assemblies into a **_lib_** folder in the root folder of your SpecFlow project

 

[![image](images/image_thumb_43.png "image")](files/image_43.png)

 

Tests marked with @IgnoreLocally  or OptionallyIgnoreTest will now skip the test if McKeltCustom.SpecflowPlugin.Settings.IgnoreLocally = true

 

[![image](images/image_thumb_44.png "image")](files/image_44.png)
