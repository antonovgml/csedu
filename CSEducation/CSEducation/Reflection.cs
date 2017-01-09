using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Util;

/* Develop TypeInfo class that displays certain info about inputted class (list of methods, constructors, etc) */

namespace Reflection
{

    public class TypeInfo
    {

        /* Display type information about a class */
        public static string AsString<T>()
        {
            StringBuilder sbResult = new StringBuilder();

            Type type = typeof(T);
            
            sbResult.Append("\n  ************************* Type Information for ").Append(type.Name);
            sbResult.Append("\n  Assembly full name: ").Append(type.Assembly.FullName);
            sbResult.Append("\n  Assembly qualified name: ").Append(type.AssemblyQualifiedName);
            sbResult.Append("\n  Attributes: ").Append(type.Attributes.ToString());
            sbResult.Append("\n  Base type: ").Append(type.BaseType);            
            sbResult.Append("\n  Type hierarchy: ").Append(TypeHierarchyString(type, new StringBuilder()));
            sbResult.Append("\n  Custom attributes: ").Append(ReduceStr<CustomAttributeData>(type.CustomAttributes,  attr => attr.AttributeType.Name));
            sbResult.Append("\n  Implemented interfaces: ").Append(ReduceStr<Type>(type.GetInterfaces(), interf => interf.Name));
            sbResult.Append("\n  Members: ").Append(ReduceStr<MemberInfo>(type.GetMembers(), member => member.Name));
            sbResult.Append("\n  Public constructors: ").Append( ReduceStr<ConstructorInfo>(type.GetConstructors(), constr => constr.ToString()));
            sbResult.Append("\n  Public fields: ").Append(ReduceStr<FieldInfo>(type.GetFields(), field => field.Name));
            sbResult.Append("\n  Non-Public fields: ").Append(ReduceStr<FieldInfo>(type.GetFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance) , field => field.Name));
            sbResult.Append("\n  Public methods: ").Append(ReduceStr<MethodInfo>(type.GetMethods(), method => method.Name));
            sbResult.Append("\n  Non-Public methods: ").Append(ReduceStr<MethodInfo>(type.GetMethods(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance), method => method.Name));

            return sbResult.ToString();
            
        }

        /* simple analogy of collection reduce to string */
        private static string ReduceStr<T>(IEnumerable<T> coll, Func<T, string> func)
        {
            StringBuilder sbResult = new StringBuilder();

            foreach (var elem in coll)
            {
                sbResult.Append(func(elem)).Append("; ");
            }

            return sbResult.ToString();
        }

        private static string TypeHierarchyString(Type baseType, StringBuilder sbAccum)
        {
            if (!baseType.IsClass) return "";
            sbAccum.Append(baseType.Name);
            if (baseType == typeof(object))
            {
                return "";
            }
            else
            {
                sbAccum.Append(" -> ");
                TypeHierarchyString(baseType.BaseType, sbAccum);
            }

            return sbAccum.ToString();              
        }
        
        
    }

/* Playground */
/*
    [My1, My2]
    class My
    {
        public string message;

        [My1, My2, AddText("Blah")]
        public string sayHello()
        {
            return "hello";
        }

        public delegate string D();
        public static D d;

        public My() {
            d = new D(sayHello);
        }
        public My(string msg):base()
        {
            this.message = msg;
        }



    }

    class MyChild: My
    {

    }

    

    internal class My1Attribute : Attribute
    {
    }

    internal class My2Attribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    internal class AddTextAttribute: Attribute
    {
        public string Text { get; set; }

        public AddTextAttribute(string text)
        {
            this.Text = text;
        }
    }
*/
}
