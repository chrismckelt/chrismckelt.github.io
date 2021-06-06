---
layout: post
category: posts
title: "RhinoMocks – WhenCalled"
date: "2010-07-06"
tags: dotnet tdd
---

The following test would fail without this

    .WhenCalled(invocation => invocation.ReturnValue = new TestResult(){IsTrue = true, Message = "BBB"})

 
          
    [TestClass]
    public class ChrisTest
    {
        private IRuleService ruleService;

       [TestMethod]
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
