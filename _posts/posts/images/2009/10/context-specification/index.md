---
title: "Context Specification"
date: "2009-10-12"
categories: 
  - "net"
  - "software"
  - "tdd-bdd"
---

tnamespace Example
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rhino.Mocks;

    public abstract class ContextSpecification<T>
    {
        protected Exception executionException;

        protected T sut { get; set; }

        \[TestInitialize\]
        public void Start()
        {
            this.Context();
            this.SetupMockResults();
            this.Because();
        }

        \[TestCleanup\]
        public void CleanUp()
        {
            this.Clean();
        }

        protected virtual void Context()
        {
        }

        protected virtual void SetupMockResults()
        {
        }

        protected virtual void Because()
        {
        }

        protected virtual void Clean()
        {
        }

        protected TInterface GetDependency<TInterface>() where TInterface : class
        {
            return MockRepository.GenerateMock<TInterface>();
        }

        public void Execute(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                executionException = ex;
            }
        }

    }
}

.csharpcode, .csharpcode pre<br />{<br /> font-size: small;<br /> color: black;<br /> font-family: consolas, "Courier New", courier, monospace;<br /> background-color: #ffffff;<br /> /\*white-space: pre;\*/<br />}<br />.csharpcode pre { margin: 0em; }<br />.csharpcode .rem { color: #008000; }<br />.csharpcode .kwrd { color: #0000ff; }<br />.csharpcode .str { color: #006080; }<br />.csharpcode .op { color: #0000c0; }<br />.csharpcode .preproc { color: #cc6633; }<br />.csharpcode .asp { background-color: #ffff00; }<br />.csharpcode .html { color: #800000; }<br />.csharpcode .attr { color: #ff0000; }<br />.csharpcode .alt<br />{<br /> background-color: #f4f4f4;<br /> width: 100%;<br /> margin: 0em;<br />}<br />.csharpcode .lnum { color: #606060; }
