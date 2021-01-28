---
title: "Enum extension methods"
date: "2009-10-23"
categories: 
  - "net"
---

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Challenger.Global.Util.Extensions
{
    public static class EnumExtensionMethods
    {

        public static T ParseAsEnumByDescriptionAttribute<T>(this string description)  // where T : enum
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException(description, @"Cannot parse an empty description");
            }

            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException(string.Format("Invalid Enum type{0}", typeof(T)));
            }

            foreach (T item in Enum.GetValues(typeof(T)))
            {
                DescriptionAttribute\[\] attributes = (DescriptionAttribute\[\])item.GetType().GetField(item.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length > 0 && attributes\[0\].Description.ToUpper() == description.ToUpper())
                {
                    return item;
                }
            }
            throw new InvalidOperationException(string.Format("Couldn't find enum of type {0} with attribute of '{1}'", typeof(T), description));
        }

        public static string GetDescription(this Enum enumerationValue)
        {
            DescriptionAttribute\[\] attributes = (DescriptionAttribute\[\])enumerationValue.GetType().GetField(enumerationValue.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes\[0\].Description : enumerationValue.ToString();
        }

        public static EnumDto GetDto(this Enum enumerationValue)
        {
            return new EnumDto { Value = enumerationValue, Description = GetDescription(enumerationValue) };
        }

        public static IList<EnumDto> ToEnumDtoList(this Enum enumerationValue)
        {
            var vals = Enum.GetValues(typeof (Enum));
            IList<EnumDto> list = (from object val in vals
                                   let desc = ((Enum) val).GetDescription()
                                   select new EnumDto() {Description = desc, Value = (Enum) val}).ToList();

            return list;
        }
       
        /// <summary>
        /// Gets all items for an enum value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static IEnumerable<T> GetAllItems<T>(this Enum value)
        {
            return from object item in Enum.GetValues(typeof(T)) select (T)item;
        }

        /// <summary>
        /// Gets all items for an enum type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static IEnumerable<T> GetAllItems<T>() where T : struct
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        /// <summary>
        /// Gets all combined items from an enum value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <example>
        /// Displays ValueA and ValueB.
        /// <code>
        /// EnumExample dummy = EnumExample.Combi;
        /// foreach (var item in dummy.GetAllSelectedItems<EnumExample>())
        /// {
        ///    Console.WriteLine(item);
        /// }
        /// </code>
        /// </example>
        public static IEnumerable<T> GetAllSelectedItems<T>(this Enum value)
        {
            int valueAsInt = Convert.ToInt32(value, CultureInfo.InvariantCulture);

            return from object item in Enum.GetValues(typeof(T)) let itemAsInt = Convert.ToInt32(item, CultureInfo.InvariantCulture) where itemAsInt == (valueAsInt & itemAsInt) select (T)item;
        }

        /// <summary>
        /// Determines whether the enum value contains a specific value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="request">The request.</param>
        /// <returns>
        ///     <c>true</c> if value contains the specified value; otherwise, <c>false</c>.
        /// </returns>
        /// <example>
        /// <code>
        /// EnumExample dummy = EnumExample.Combi;
        /// if (dummy.Contains<EnumExample>(EnumExample.ValueA))
        /// {
        ///     Console.WriteLine("dummy contains EnumExample.ValueA");
        /// }
        /// </code>
        /// </example>
        public static bool Contains<T>(this Enum value, T request)
        {
            int valueAsInt = Convert.ToInt32(value, CultureInfo.InvariantCulture);
            int requestAsInt = Convert.ToInt32(request, CultureInfo.InvariantCulture);

            if (requestAsInt == (valueAsInt & requestAsInt))
            {
                return true;
            }

            return false;
        }

        public static T ToEnum<T>(this string enumerationString)
        {
            return (T)Enum.Parse(typeof(T), enumerationString);
        }
    }
}

.csharpcode, .csharpcode pre<br />{<br /> font-size: small;<br /> color: black;<br /> font-family: consolas, "Courier New", courier, monospace;<br /> background-color: #ffffff;<br /> /\*white-space: pre;\*/<br />}<br />.csharpcode pre { margin: 0em; }<br />.csharpcode .rem { color: #008000; }<br />.csharpcode .kwrd { color: #0000ff; }<br />.csharpcode .str { color: #006080; }<br />.csharpcode .op { color: #0000c0; }<br />.csharpcode .preproc { color: #cc6633; }<br />.csharpcode .asp { background-color: #ffff00; }<br />.csharpcode .html { color: #800000; }<br />.csharpcode .attr { color: #ff0000; }<br />.csharpcode .alt<br />{<br /> background-color: #f4f4f4;<br /> width: 100%;<br /> margin: 0em;<br />}<br />.csharpcode .lnum { color: #606060; } <p>.csharpcode, .csharpcode pre<br />{<br /> font-size: small;<br /> color: black;<br /> font-family: consolas, "Courier New", courier, monospace;<br /> background-color: #ffffff;<br /> /\*white-space: pre;\*/<br />}<br />.csharpcode pre { margin: 0em; }<br />.csharpcode .rem { color: #008000; }<br />.csharpcode .kwrd { color: #0000ff; }<br />.csharpcode .str { color: #006080; }<br />.csharpcode .op { color: #0000c0; }<br />.csharpcode .preproc { color: #cc6633; }<br />.csharpcode .asp { background-color: #ffff00; }<br />.csharpcode .html { color: #800000; }<br />.csharpcode .attr { color: #ff0000; }<br />.csharpcode .alt<br />{<br /> background-color: #f4f4f4;<br /> width: 100%;<br /> margin: 0em;<br />}<br />.csharpcode .lnum { color: #606060; }
