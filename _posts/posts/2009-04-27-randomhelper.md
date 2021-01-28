---
title: "RandomHelper"
date: "2009-04-27"
categories: 
  - "net"
---

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TestHelpers
{
    public static class RandomHelper

    {
        /\* This method can be used to fill all public properties of an object with random values depending on their type.      CAUTION: it does not fill attributes that end with 'ID' or attributes which are called 'pk'. They have to be filled manually.\*/

        private static readonly Random randomSeed = new Random();
        /\* Generates a random string with the given length\*/

        public static T FillPropertiesWithRandomValues<T>(bool fillBaseObjects)
        {
            return CreateItem<T>(true);
        }

        public static T FillPropertiesWithRandomValues<T>()
        {
            return CreateItem<T>(false);
        }

        private static T CreateItem<T>(bool fillBaseObjects)
        {
            Type type = typeof (T);
            var item = (T) Activator.CreateInstance(typeof (T));

            while (type != null)
            {
                PropertyInfo\[\] infos =
                    type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                if (infos.Length == 0)
                {
                    if (item.GetType().BaseType != null)
                    {
                        infos =
                            item.GetType().BaseType.GetProperties(BindingFlags.Public | BindingFlags.Instance |
                                                                  BindingFlags.DeclaredOnly);
                    }
                }

                foreach (PropertyInfo info in infos)
                {
                    Type infoType = info.PropertyType;

                    Type nullableType = null;

                    if (infoType.IsGenericType && infoType.GetGenericTypeDefinition().Equals(typeof (Nullable<>)))
                    {
                        nullableType = ExtractTypeFromNullable(infoType);
                    }

                    if (!info.CanWrite) continue;
                    if (infoType.Equals(typeof (DateTime)) || infoType.Equals(typeof (DateTime?)))
                    {
                        info.SetValue(item, RandomDateTime(DateTime.Now, new DateTime(3000, 01, 01)), null);
                    }

                    else if (infoType.Equals(typeof (string)))
                    {
                        info.SetValue(item, info.Name + "\_" + RandomString(20, false), null);
                    }

                    else if ((infoType.Equals(typeof (long)) || infoType.Equals(typeof (double)) ||
                              infoType.Equals(typeof (int)) || infoType.Equals(typeof (long)) ||
                              infoType.Equals(typeof (int))) && !info.Name.ToLower().EndsWith("id") &&
                             !info.Name.ToLower().Equals("pk"))
                    {
                        info.SetValue(item, RandomNumber(0, 999999), null);
                    }

                    else if ((infoType.Equals(typeof (long?)) || infoType.Equals(typeof (double?)) ||
                              infoType.Equals(typeof (int?)) || infoType.Equals(typeof (long?)) ||
                              infoType.Equals(typeof (int?))) && !info.Name.ToLower().EndsWith("id") &&
                             !info.Name.ToLower().Equals("pk"))
                    {
                        Type genericType = info.PropertyType.GetGenericArguments()\[0\];

                        info.SetValue(item, Convert.ChangeType(RandomNumber(0, 999999), genericType), null);
                    }

                    else if (infoType.Equals(typeof (decimal)))
                    {
                        info.SetValue(item, 1m, null);
                    }
                    else if (infoType.Equals(typeof (bool)))
                    {
                        info.SetValue(item, true, null);
                    }

                    else if (infoType.Equals(typeof (Guid)))
                    {
                        info.SetValue(item, Guid.NewGuid(), null);
                    }

                    else if (infoType.Equals(typeof (Enum)))
                    {
                        Array values = Enum.GetValues(infoType);

                        List<object\> list = values.Cast<object\>().ToList();

                        object val = list\[new Random().Next(0, list.Count)\];

                        info.SetValue(item, val, null);
                    }

                    else if (((infoType.BaseType != null) && (infoType.BaseType.Equals(typeof (Enum)))))
                    {
                        Array values = Enum.GetValues(infoType);

                        List<object\> list = values.Cast<object\>().ToList();

                        object val = list\[new Random().Next(0, list.Count)\];

                        info.SetValue(item, val, null);
                    }

                    else if (nullableType != null)
                    {
                        if (nullableType.BaseType.Equals(typeof (Enum)))
                        {
                            Array values = Enum.GetValues(nullableType);

                            List<object\> list = values.Cast<object\>().ToList();

                            object val = list\[new Random().Next(0, list.Count)\];

                            info.SetValue(item, val, null);
                        }
                    }

                    else if (infoType.IsArray)
                    {
                        Console.WriteLine("Object contains array of objects need to fill these");
                        MethodInfo sm = info.GetSetMethod(true);
                        if (sm.ReturnType.IsArray)
                        {
                            object arrayObject = sm.Invoke(item, null);
                            foreach (object element in (Array) arrayObject)
                            {
                                foreach (PropertyInfo arrayObjPinfo in element.GetType().GetProperties())
                                {
                                    Console.WriteLine(arrayObjPinfo.Name + ":" +
                                                      arrayObjPinfo.GetGetMethod().Invoke(element, null));
                                }
                            }
                        }
                    }
                }

                if (!fillBaseObjects)
                    break;

                type = type.BaseType;
            }

            return item;
        }

        ///<summary>Indentify and extracting type from Nullable Type</summary>
        public static Type ExtractTypeFromNullable(Type type)

        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Nullable<>))

            {
                PropertyInfo valueProp = type.GetProperty("Value");

                return valueProp.PropertyType;
            }

            else

            {
                return null;
            }
        }

        public static string RandomString(int size, bool lowerCase)

        {
            var randomString = new StringBuilder(size);

            /\* Ascii start position (65 = A / 97 = a)\*/

            int start = lowerCase ? 97 : 65;

            /\* Add random chars\*/

            for (int i = 0; i < size; i++)

            {
                randomString.Append((char) ((26\*randomSeed.NextDouble()) + start));
            }

            return randomString.ToString();
        }

        public static int RandomNumber(int minimal, int maximal)

        {
            return randomSeed.Next(minimal, maximal);
        }

        /\* Returns a random boolean value\*/

        public static bool RandomBool()

        {
            return randomSeed.NextDouble() > 0.5;
        }

        /\* Returns a random color\*/

        public static DateTime RandomDateTime(DateTime min, DateTime max)

        {
            if (max <= min)

            {
                const string message = "Max must be greater than min.";

                throw new ArgumentException(message);
            }

            long minTicks = min.Ticks;
            long maxTicks = max.Ticks;
            double rn = ((Convert.ToDouble(maxTicks) - Convert.ToDouble(minTicks))\*randomSeed.NextDouble()) +
                        Convert.ToDouble(minTicks);

            return new DateTime(Convert.ToInt64(rn));
        }
    }
}

.csharpcode, .csharpcode pre<br /> {<br /> font-size: small;<br /> color: black;<br /> font-family: consolas, "Courier New", courier, monospace;<br /> background-color: #ffffff;<br /> /\*white-space: pre;\*/<br /> }<br /> .csharpcode pre { margin: 0em; }<br /> .csharpcode .rem { color: #008000; }<br /> .csharpcode .kwrd { color: #0000ff; }<br /> .csharpcode .str { color: #006080; }<br /> .csharpcode .op { color: #0000c0; }<br /> .csharpcode .preproc { color: #cc6633; }<br /> .csharpcode .asp { background-color: #ffff00; }<br /> .csharpcode .html { color: #800000; }<br /> .csharpcode .attr { color: #ff0000; }<br /> .csharpcode .alt<br /> {<br /> background-color: #f4f4f4;<br /> width: 100%;<br /> margin: 0em;<br /> }<br /> .csharpcode .lnum { color: #606060; }
