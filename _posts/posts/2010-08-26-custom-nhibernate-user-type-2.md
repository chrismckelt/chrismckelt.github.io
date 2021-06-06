---
layout: post
category: posts
title: "Custom NHibernate User Type"
date: "2010-08-26"
tags: dotnet
---

# How to use a custom NHibernate User Type

To store the state of a Risk (ie RiskState) as a value in a column we can use a NHibernate.UserType


The RiskState can be one of the following: (these are all derived from RiskState which implements IRiskState)

Our user type

     public class RiskStateNameUserType: IUserType {
      #region IUserType Members
  public object Assemble(object cached, object owner)
  {
    return cached;
  }
  public object DeepCopy(object value)
  {
    return value;
  }
  public object Disassemble(object value)
  {
    return value;
  }
  public int GetHashCode(object x)
  {
    return x.GetHashCode();
  }
  public bool IsMutable
  {
    get {
      return false;
    }
  }
  public object NullSafeGet(System.Data.IDataReader dr, string\[\] names, object owner)
  {
    var property0 = NHibernateUtil.String.NullSafeGet(dr, names\[0\]);

    if (property0 == null)
    {
      return null;
    }
    IRiskState state;
    if (owner is Risk)
    {
      state =
        (IRiskState)
      Activator.CreateInstance(Type.GetType(typeof (IRiskState).Namespace + "." + (string) property0),
        owner);
    } else
    {
      state =
        (IRiskState)
      Activator.CreateInstance(Type.GetType(typeof (IRiskState).Namespace + "." + (string) property0));
    }
    return state;
  }
  public void NullSafeSet(System.Data.IDbCommand cmd, object value, int index)
  {
    if (value == null)
 {
      ((IDataParameter) cmd.Parameters\[index\]).Value = DBNull.Value;
    } else
    {
      var state = (IRiskState) value;
      ((IDataParameter) cmd.Parameters\[index\]).Value = state.GetType().Name;
    }
  }
  public object Replace(object original, object target, object owner)
  {
    return original;
  }
  public Type ReturnedType
  {
    get {
      return typeof (RiskState);
    }
  }
  public NHibernate.SqlTypes.SqlType\[\] SqlTypes
  {
    get {
      return new\ [\] {
        NHibernateUtil.String.SqlType
      };
    }
  }
  public new bool Equals(object x, object y)
  {
    if (x == null && y == null) return true;
    if (x == null || y == null) return false;
    return x.GetType() == y.GetType();
  }
  #endregion
}

Using this on a property as follows:

     [Property(ColumnType = "Demo.Core.Risks.RiskStateNameUserType, Demo.Core")\]
 public IRiskState RequiredState { get; set; }
 

We can now store types:

![image](https://user-images.githubusercontent.com/662868/120916396-faa22580-c6db-11eb-8ef2-34c567e23567.png)


If importing from a file and we want to parse a string to our user type


>         protected T GetCustomType<T>(XElement dsl, string attributeName)
>         {
>             string attribute = (string)dsl.Attribute(attributeName);
>             var customType = (T)Activator.CreateInstance(Type.GetType(typeof(T).Namespace + "." + attribute));
>             return customType;
>         }
 