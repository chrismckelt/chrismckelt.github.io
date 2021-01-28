---
title: "RhinoMocks – WhenCalled"
date: "2010-07-06"
categories: 
  - "net"
  - "tdd-bdd"
---

The following test would fail without this

 

.WhenCalled(invocation => invocation.ReturnValue = new TestResult(){IsTrue = true, Message = "BBB"})

 

 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Matlock.Core.Shared;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Rhino.Mocks;

namespace Matlock.Tests
{
    \[TestClass\]
    public class ChrisTest
    {

        private IRuleService ruleService;

        \[TestMethod\]
        public void ShouldNotChangeReturnedTestResult()
        {
            ruleService = MockRepository.GenerateMock<IRuleService>();
            var testResult = new TestResult();
            testResult.IsTrue = true;
            testResult.Message = "AAA";
            ruleService.Stub(a => a.GetTestResult()).Return(testResult)
                .WhenCalled(invocation => invocation.ReturnValue = new TestResult(){IsTrue = true, Message = "BBB"});

            var testClass = new TestClass(ruleService);
            testClass.KillTheString();
            Assert.IsTrue(testClass.StringIsThere());
            
        }

        public interface IRuleService
        {
            TestResult GetTestResult();
        }

        public class TestResult
        {
            public bool IsTrue { get; set; }
            public string Message { get; set; }
        }

        private class TestClass
        {
            private readonly IRuleService ruleService;

            public TestClass(IRuleService ruleService)
            {
                this.ruleService = ruleService;
            }

            public void KillTheString()
            {
                var result = ruleService.GetTestResult();
                result.Message = string.Empty;
            }

            public bool StringIsThere()
            {
                var result = ruleService.GetTestResult();
                return !string.IsNullOrEmpty(result.Message);
            }
        }
    }
}

.csharpcode, .csharpcode pre<br /> {<br /> font-size: small;<br /> color: black;<br /> font-family: consolas, "Courier New", courier, monospace;<br /> background-color: #ffffff;<br /> /\*white-space: pre;\*/<br /> }<br /> .csharpcode pre { margin: 0em; }<br /> .csharpcode .rem { color: #008000; }<br /> .csharpcode .kwrd { color: #0000ff; }<br /> .csharpcode .str { color: #006080; }<br /> .csharpcode .op { color: #0000c0; }<br /> .csharpcode .preproc { color: #cc6633; }<br /> .csharpcode .asp { background-color: #ffff00; }<br /> .csharpcode .html { color: #800000; }<br /> .csharpcode .attr { color: #ff0000; }<br /> .csharpcode .alt<br /> {<br /> background-color: #f4f4f4;<br /> width: 100%;<br /> margin: 0em;<br /> }<br /> .csharpcode .lnum { color: #606060; }
