---
layout: post
category: posts
title: "ISpecification"
date: "2010-02-03"
tags: dotnet tdd
---

> using System;
> using System.Collections.Generic;
> 
> namespace Demo.Core.Specification {    
>    public interface ISpecification < T > {      
>     bool IsSatisfiedBy(T candidate);    
> ISpecification < T > And(ISpecification < T > other);  
> ISpecification < T > Or(ISpecification < T > other);  
> ISpecification < T > XOr(ISpecification < T > other);  
> ISpecification < T > AndAllOf(IEnumerable < ISpecification < T >> specifications);  
> T Target {
>       get;
>       set;
>     }  
>     void GetResults(ResultsVisitor visitor);IEnumerable < Type > WhatWasAssessed();
> Risks.IAmACommand GetCommand();  
>   }
> }
> 
 

> using System;
> using System.Collections.Generic;
> using Demo.Core.Commands;
> using Demo.Core.Risks;
> using Demo.Core.Specification.Common;
> 
> namespace Demo.Core.Specification
> {
>     public abstract class AbstractSpecification<T> : ISpecification<T>
>     {
>         protected SpecificationResult result;
>         protected IList<Type> ruleList = new List<Type>();
>         protected bool SoftCheck { get; set; }
> 
>         public T Candidate { get; set; }
>         public T Target { get; set; }
>         public bool IsReplay {protected get; set;}
> 
>         public ISpecification<T> And(ISpecification<T> other)
>         {
>             return new AndSpecification<T>(this, other);
>         }
> 
>         public ISpecification<T> Or(ISpecification<T> other)
>         {
>             return new OrSpecification<T>(this, other);
>         }
> 
>         public ISpecification<T> XOr(ISpecification<T> other)
>         {
>             return new XORSpecification<T>(this, other);
>         }
> 
>         public ISpecification<T> AndAllOf(IEnumerable<ISpecification<T>> specifications)
>         {
>             return new AndAllOfSpecification<T>(this, specifications);    
>         }
> 
>         public virtual bool Evaluate(T candidate)
>         {
>             throw new NotImplementedException("You need to provide a predicate or override IsSatisfiedBy");
>         }
> 
>         public virtual IDemoCommand GetCommand()
>         {
>             return new NoCommand();
>         }
>         
>         public virtual void GetResults(ResultsVisitor visitor)
>         {
>             if (result != null && !string.IsNullOrEmpty(result.Message))
>             {
>                 visitor.Add(result);
>             }
>         }
> 
>         public virtual IEnumerable<Type> WhatWasAssessed()
>         {
>             return ruleList;
>         }
> 
>         public virtual string MessageFormat()
>         {
>             throw new NotImplementedException("You need to provide a result a message or override IsSatisfiedBy");
>         }
> 
>         public virtual bool IsSatisfiedBy(T candidate)
>         {
>             if (IsReplay)
>             {
>                 return true;
>             }
>             
>             Type ruleType = GetType(); 
>             ruleList.Add(ruleType);
> 
>             Candidate = candidate;
> 
>             bool satisfied = Evaluate(candidate);
>             if (!satisfied || (SoftCheck && !string.IsNullOrEmpty(result.Message)))
>             {
>                 result = new SpecificationResult
>                              {
>                                 Satisfied = SoftCheck, 
>                                 Message = string.Format(MessageFormat(), candidate, Target)
>                              };
>             }
> 
>             return satisfied;
>         }
> 
>         public AbstractSpecification<T> SuccessOrRhs(ISpecification<T> specification)
>         {
>             return new SuccessOrRhsSpecification<T>(specification);
>         }
>     }
> }
