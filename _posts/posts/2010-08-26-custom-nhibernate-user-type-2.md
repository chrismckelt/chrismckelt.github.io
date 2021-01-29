---
layout: post
category: posts
title: "Custom NHibernate User Type"
date: "2010-08-26"
categories: 
  - "net"
  - "code"
---

# How to use a custom NHibernate User Type

 

To store the state of a Risk (ie RiskState) as a value in a column we can use a NHibernate.UserType

 

The RiskState can be one of the following: (these are all derived from RiskState which implements IRiskState)

[![image](images/image.axd?picture=image_thumb_30.png "image")](http://www.mckelt.com/blog/image.axd?picture=image_30.png)

Our user type

 public class RiskStateNameUserType : IUserType

    {

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

            get { return false; }

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

                    Activator.CreateInstance(Type.GetType(typeof(IRiskState).Namespace + "." + (string)property0),

                                             owner);

            }

            else

            {

                state =

                    (IRiskState)

                    Activator.CreateInstance(Type.GetType(typeof(IRiskState).Namespace + "." + (string)property0));

            }

            return state;

        }

        public void NullSafeSet(System.Data.IDbCommand cmd, object value, int index)

        {

            if (value == null)

            {

                ((IDataParameter)cmd.Parameters\[index\]).Value = DBNull.Value;

            }

            else

            {

                var state = (IRiskState)value;

                ((IDataParameter)cmd.Parameters\[index\]).Value = state.GetType().Name;

            }

        }

        public object Replace(object original, object target, object owner)

        {

            return original;

        }

        public Type ReturnedType

        {

            get { return typeof(RiskState); }

        }

        public NHibernate.SqlTypes.SqlType\[\] SqlTypes

        {

            get { return new\[\] { NHibernateUtil.String.SqlType }; }

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

<p>.csharpcode, .csharpcode pre<br /> {<br /> font-size: small;<br /> color: black;<br /> font-family: consolas, "Courier New", courier, monospace;<br /> background-color: #ffffff;<br /> /\*white-space: pre;\*/<br /> }<br /> .csharpcode pre { margin: 0em; }<br /> .csharpcode .rem { color: #008000; }<br /> .csharpcode .kwrd { color: #0000ff; }<br /> .csharpcode .str { color: #006080; }<br /> .csharpcode .op { color: #0000c0; }<br /> .csharpcode .preproc { color: #cc6633; }<br /> .csharpcode .asp { background-color: #ffff00; }<br /> .csharpcode .html { color: #800000; }<br /> .csharpcode .attr { color: #ff0000; }<br /> .csharpcode .alt<br /> {<br /> background-color: #f4f4f4;<br /> width: 100%;<br /> margin: 0em;<br /> }<br /> .csharpcode .lnum { color: #606060; }

 \[Property(ColumnType = "Matlock.Core.Risks.RiskStateNameUserType, Matlock.Core")\]
 public IRiskState RequiredState { get; set; }

<p>.csharpcode, .csharpcode pre<br /> {<br /> font-size: small;<br /> color: black;<br /> font-family: consolas, "Courier New", courier, monospace;<br /> background-color: #ffffff;<br /> /\*white-space: pre;\*/<br /> }<br /> .csharpcode pre { margin: 0em; }<br /> .csharpcode .rem { color: #008000; }<br /> .csharpcode .kwrd { color: #0000ff; }<br /> .csharpcode .str { color: #006080; }<br /> .csharpcode .op { color: #0000c0; }<br /> .csharpcode .preproc { color: #cc6633; }<br /> .csharpcode .asp { background-color: #ffff00; }<br /> .csharpcode .html { color: #800000; }<br /> .csharpcode .attr { color: #ff0000; }<br /> .csharpcode .alt<br /> {<br /> background-color: #f4f4f4;<br /> width: 100%;<br /> margin: 0em;<br /> }<br /> .csharpcode .lnum { color: #606060; }

 

We can now store types:

 

[![image](images/image.axd?picture=image_thumb_31.png "image")](http://www.mckelt.com/blog/image.axd?picture=image_31.png)

 

 

If importing from a file and we want to parse a string to our user type

 

        protected T GetCustomType<T>(XElement dsl, string attributeName)
        {
            string attribute = (string)dsl.Attribute(attributeName);
            var customType = (T)Activator.CreateInstance(Type.GetType(typeof(T).Namespace + "." + attribute));
            return customType;
        }

<p>.csharpcode, .csharpcode pre<br /> {<br /> font-size: small;<br /> color: black;<br /> font-family: consolas, "Courier New", courier, monospace;<br /> background-color: #ffffff;<br /> /\*white-space: pre;\*/<br /> }<br /> .csharpcode pre { margin: 0em; }<br /> .csharpcode .rem { color: #008000; }<br /> .csharpcode .kwrd { color: #0000ff; }<br /> .csharpcode .str { color: #006080; }<br /> .csharpcode .op { color: #0000c0; }<br /> .csharpcode .preproc { color: #cc6633; }<br /> .csharpcode .asp { background-color: #ffff00; }<br /> .csharpcode .html { color: #800000; }<br /> .csharpcode .attr { color: #ff0000; }<br /> .csharpcode .alt<br /> {<br /> background-color: #f4f4f4;<br /> width: 100%;<br /> margin: 0em;<br /> }<br /> .csharpcode .lnum { color: #606060; }

<p>.csharpcode, .csharpcode pre<br /> {<br /> font-size: small;<br /> color: black;<br /> font-family: consolas, "Courier New", courier, monospace;<br /> background-color: #ffffff;<br /> /\*white-space: pre;\*/<br /> }<br /> .csharpcode pre { margin: 0em; }<br /> .csharpcode .rem { color: #008000; }<br /> .csharpcode .kwrd { color: #0000ff; }<br /> .csharpcode .str { color: #006080; }<br /> .csharpcode .op { color: #0000c0; }<br /> .csharpcode .preproc { color: #cc6633; }<br /> .csharpcode .asp { background-color: #ffff00; }<br /> .csharpcode .html { color: #800000; }<br /> .csharpcode .attr { color: #ff0000; }<br /> .csharpcode .alt<br /> {<br /> background-color: #f4f4f4;<br /> width: 100%;<br /> margin: 0em;<br /> }<br /> .csharpcode .lnum { color: #606060; }
