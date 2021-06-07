---
layout: post
category: posts
title: "XUnit Ignore test at runtime (with SpecFlow tag @IgnoreLocally)"
date: "2013-10-06"
tags: dotnet tdd
---

Code --> [https://github.com/chrismckelt/XUnit.OptionallyIgnore](https://github.com/chrismckelt/XUnit.OptionallyIgnore)

NuGet --> [https://www.nuget.org/packages/Xunit.SpecFlow.AssertSkip/](https://www.nuget.org/packages/Xunit.SpecFlow.AssertSkip/)

As Xunit has no Assert.Ignore() using the OptionallyIgnoreTestFactAttribute attribute on a method and setting McKeltCustom.SpecflowPlugin.Settings.IgnoreLocally == true will ignore the test at runtime

In SpecFlow set this as a plugin and use the tag **@IgnoreLocally**-- before each test scenario is run turn on/off the setting to run this test.

**Sample usage:**

```
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
        [OptionallyIgnoreTestFact\]
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

```
SpecFlow tag can be on a feature or a scenario

![image](https://user-images.githubusercontent.com/662868/120943288-9aa19280-c760-11eb-80bf-af9b3d444e98.png)

Add the following to the .config file

```
```
    <specFlow>
    <!--<unitTestProvider name="xUnit" />-->
    <generator allowDebugGeneratedFiles="false" allowRowTests="true" generateAsyncTests="false" path=".\lib"/>
    <runtime stopAtFirstError="false" missingOrPendingStepsOutcome="Ignore">
      <!--<dependencies>
        <register name="CustomGeneratorPlugin" type="PhoenixCustom.Generator.SpecflowPlugin.CustomGeneratorPlugin, PhoenixCustom.Generator.SpecflowPlugin" as="PhoenixCustom.Generator.SpecflowPlugin.CustomGeneratorPlugin, PhoenixCustom.Generator.SpecflowPlugin" />
        <register name="CustomGeneratorProvider" type="PhoenixCustom.Generator.SpecflowPlugin.CustomGeneratorProvider, PhoenixCustom.Generator.SpecflowPlugin" as="PhoenixCustom.Generator.SpecflowPlugin.CustomGeneratorProvider, PhoenixCustom.Generator.SpecflowPlugin" />
      </dependencies>-->
    </runtime>
    <trace traceSuccessfulSteps="true" traceTimings="false" minTracedDuration="0:0:0.1" stepDefinitionSkeletonStyle="RegexAttribute"/>
    <plugins>
      <add name="McKeltCustom" path=".\lib" type="GeneratorAndRuntime"/>
    </plugins>
    <!-- For additional details on SpecFlow configuration options see http://go.specflow.org/doc-config -->
    <stepAssemblies>
      <!-- This attribute is required in order to use StepArgument Transformation as described here; 
    https://github.com/marcushammarberg/SpecFlow.Assist.Dynamic/wiki/Step-argument-transformations  -->
      <!-- This attribute is required in order to use StepArgument Transformation as described here; 
    https://github.com/marcusoftnet/SpecFlow.Assist.Dynamic/wiki/Step-argument-transformations  -->
      <stepAssembly assembly="SpecFlow.Assist.Dynamic"/>
    </stepAssemblies>
    </specFlow>
```

```
Finally put the all compiled assemblies into a **_lib_** folder in the root folder of your SpecFlow project  

![image](https://user-images.githubusercontent.com/662868/120943303-b73dca80-c760-11eb-9743-5f76c5043b76.png)


Tests marked with @IgnoreLocally  or OptionallyIgnoreTest will now skip the test if McKeltCustom.SpecflowPlugin.Settings.IgnoreLocally = true

![image](https://user-images.githubusercontent.com/662868/120943325-cfade500-c760-11eb-9937-1bd1ece4efea.png)
```


