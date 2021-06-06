---
layout: post
category: posts
title: "ConvertPropertiesAndValuesToHashtable Extension method"
date: "2010-01-27"
tags: dotnet
---
> 
> public static class Extensions
>     {
>         public static Hashtable ConvertPropertiesAndValuesToHashtable(this object obj)
>         {
>             var ht = new Hashtable();
> 
>             // get all public static properties of obj type
>             var propertyInfos = obj.GetType().GetProperties().Where(a=>a.MemberType.Equals(MemberTypes.Property)).ToArray();
>             // sort properties by name
>             Array.Sort(propertyInfos, (propertyInfo1, propertyInfo2) => propertyInfo1.Name.CompareTo(propertyInfo2.Name));
> 
>             // write property names
>             foreach (PropertyInfo propertyInfo in propertyInfos)
>             {
>                 ht.Add(propertyInfo.Name, propertyInfo.GetValue(obj, BindingFlags.Public, null, null, CultureInfo.CurrentCulture));
>             }
> 
>             return ht;
>         }
>     }
> 
> Tests
> 
>  using System;
>     using System.Collections;
>     using System.Globalization;
>     using System.Linq;
>     using System.Reflection;
> 
>     using Microsoft.VisualStudio.TestTools.UnitTesting;
> 
>     using Presentation;
>     using Presentation.Mappers;
>     using Presentation.Model;
> 
>     using SoftwareApproach.TestingExtensions;
> 
>     using TestHelpers;
> 
>     \[Concern(typeof(DocumentMetaDataToDmsDocumentMetaDataMapper))\]
>     \[TestClass\]
>     public class WhenUsingExtensions : ContextSpecification<DocumentMetaDataToDmsDocumentMetaDataMapper>
>     {
>         private DocumentMetadata metadata;
> 
>         private Hashtable ht;
> 
>         protected override void Context()
>         {
>             metadata = RandomHelper.FillPropertiesWithRandomValues<DocumentMetadata>(new DocumentMetadata());
>             metadata.RiskId = 12;
>         }
> 
>         protected override void Because()
>         {
>             ht = metadata.ConvertPropertiesAndValuesToHashtable();
>         }
> 
>         \[TestMethod\]
>         public void ShouldContainCorrectCountForNumberOfMembers()
>         {
>             ht.Count.ShouldEqual(metadata.GetType().GetProperties().Where(a=>a.MemberType.Equals(MemberTypes.Property)).Count());
>         }
> 
>         \[TestMethod\]
>         public void ShouldMapAllPropertiesToHashtableCorrectly()
>         {
>             metadata.Account.ShouldEqual(ht\["Account"\].ToString());
>             metadata.AssociatedNames.ShouldEqual(ht\["AssociatedNames"\].ToString());
>             metadata.Broker.ShouldEqual(ht\["Broker"\].ToString());
>             metadata.BrokerContact.ShouldEqual(ht\["BrokerContact"\].ToString());
>             metadata.CreatedDate.ShouldEqual(Convert.ToDateTime(ht\["CreatedDate"\], CultureInfo.CurrentCulture));
>             metadata.ExpiryDate.ShouldEqual(Convert.ToDateTime(ht\["ExpiryDate"\], CultureInfo.CurrentCulture));
>             metadata.InceptionDate.ShouldEqual(Convert.ToDateTime(ht\["InceptionDate"\], CultureInfo.CurrentCulture));
>             metadata.PolicyReference.ShouldEqual(ht\["PolicyReference"\].ToString());
>             metadata.QuoteReference.ShouldEqual(ht\["QuoteReference"\].ToString());
>             metadata.RiskId.ShouldEqual(Convert.ToInt32(ht\["RiskId"\]));
>             metadata.Status.ShouldEqual(ht\["Status"\].ToString());
>             metadata.Underwriter.ShouldEqual(ht\["Underwriter"\].ToString());
>             metadata.UniqueMarketReference.ShouldEqual(ht\["UniqueMarketReference"\].ToString());
>             metadata.YearOfAccount.ShouldEqual(Convert.ToInt32(ht\["YearOfAccount"\]));
>             
>         }
>     }
 