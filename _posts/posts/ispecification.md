---
layout: post
category: posts
title: "ISpecification"
date: "2010-02-03"
categories: 
  - "net"
  - "code"
---

using System;
using System.Collections.Generic;

namespace Matlock.Core.Specification { public interface ISpecification<T> { bool IsSatisfiedBy(T candidate); ISpecification<T> And(ISpecification<T> other); ISpecification<T> Or(ISpecification<T> other); ISpecification<T> XOr(ISpecification<T> other); ISpecification<T> AndAllOf(IEnumerable<ISpecification<T>> specifications); T Target { get; set; } void GetResults(ResultsVisitor visitor); IEnumerable<Type> WhatWasAssessed(); Risks.IMatlockCommand GetCommand(); } }

 

 

using System;
using System.Collections.Generic;
using Matlock.Core.Commands;
using Matlock.Core.Risks;
using Matlock.Core.Specification.Common;

namespace Matlock.Core.Specification
{
    public abstract class AbstractSpecification<T> : ISpecification<T>
    {
        protected SpecificationResult result;
        protected IList<Type> ruleList = new List<Type>();
        protected bool SoftCheck { get; set; }

        public T Candidate { get; set; }
        public T Target { get; set; }
        public bool IsReplay {protected get; set;}

        public ISpecification<T> And(ISpecification<T> other)
        {
            return new AndSpecification<T>(this, other);
        }

        public ISpecification<T> Or(ISpecification<T> other)
        {
            return new OrSpecification<T>(this, other);
        }

        public ISpecification<T> XOr(ISpecification<T> other)
        {
            return new XORSpecification<T>(this, other);
        }

        public ISpecification<T> AndAllOf(IEnumerable<ISpecification<T>> specifications)
        {
            return new AndAllOfSpecification<T>(this, specifications);    
        }

        public virtual bool Evaluate(T candidate)
        {
            throw new NotImplementedException("You need to provide a predicate or override IsSatisfiedBy");
        }

        public virtual IMatlockCommand GetCommand()
        {
            return new NoCommand();
        }
        
        public virtual void GetResults(ResultsVisitor visitor)
        {
            if (result != null && !string.IsNullOrEmpty(result.Message))
            {
                visitor.Add(result);
            }
        }

        public virtual IEnumerable<Type> WhatWasAssessed()
        {
            return ruleList;
        }

        public virtual string MessageFormat()
        {
            throw new NotImplementedException("You need to provide a result a message or override IsSatisfiedBy");
        }

        public virtual bool IsSatisfiedBy(T candidate)
        {
            if (IsReplay)
            {
                return true;
            }
            
            Type ruleType = GetType(); 
            ruleList.Add(ruleType);

            Candidate = candidate;

            bool satisfied = Evaluate(candidate);
            if (!satisfied || (SoftCheck && !string.IsNullOrEmpty(result.Message)))
            {
                result = new SpecificationResult
                             {
                                Satisfied = SoftCheck, 
                                Message = string.Format(MessageFormat(), candidate, Target)
                             };
            }

            return satisfied;
        }

        public AbstractSpecification<T> SuccessOrRhs(ISpecification<T> specification)
        {
            return new SuccessOrRhsSpecification<T>(specification);
        }
    }
}

.csharpcode, .csharpcode pre<br /> {<br /> font-size: small;<br /> color: black;<br /> font-family: consolas, "Courier New", courier, monospace;<br /> background-color: #ffffff;<br /> /\*white-space: pre;\*/<br /> }<br /> .csharpcode pre { margin: 0em; }<br /> .csharpcode .rem { color: #008000; }<br /> .csharpcode .kwrd { color: #0000ff; }<br /> .csharpcode .str { color: #006080; }<br /> .csharpcode .op { color: #0000c0; }<br /> .csharpcode .preproc { color: #cc6633; }<br /> .csharpcode .asp { background-color: #ffff00; }<br /> .csharpcode .html { color: #800000; }<br /> .csharpcode .attr { color: #ff0000; }<br /> .csharpcode .alt<br /> {<br /> background-color: #f4f4f4;<br /> width: 100%;<br /> margin: 0em;<br /> }<br /> .csharpcode .lnum { color: #606060; }
