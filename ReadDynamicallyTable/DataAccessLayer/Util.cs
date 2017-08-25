using System;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.CSharp.RuntimeBinder;
using Binder = Microsoft.CSharp.RuntimeBinder.Binder;

namespace DataAccessLayer
{
    public static class Util
    {
        public static string Read() => Console.ReadLine();

        public static void Write(this string str) => Console.WriteLine(str);

        public static void BlankLine() => Console.WriteLine("");

        public static void Wait() => Console.ReadLine();

        public static bool IsUpperCase(this string str) => str.Equals(str.ToUpper());

        public static bool IsLowerCase(this string str) => str.Equals(str.ToLower());

        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

        public static bool IsGreatherThan(this string str, int referenceValue) => str.Length > referenceValue;

        public static bool IsGreatherThan(this int value, int referenceValue) => value.ToString().Length > value;

        public static bool LenghtIsEqualsTo(this string str, int referenceValue) => str.Length == referenceValue;

        public static object GetValue(dynamic source, string propertyName)
        {
            var teste = source.GetType().GetProperties();
            return source.GetType().GetProperty(propertyName).GetValue(source, null);
        }

        public static string GetPropertyValue(this object o, string propertyName)
        {
            if (o == null) throw new ArgumentNullException("o");
            if (propertyName == null) throw new ArgumentNullException("propertyName");
            var scope = o.GetType();
            var provider = o as IDynamicMetaObjectProvider;
            if (provider != null)
            {
                var param = Expression.Parameter(typeof(object));
                var mobj = provider.GetMetaObject(param);
                var binder = (GetMemberBinder)Binder.GetMember(0, propertyName, scope, new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(0, null) });
                var ret = mobj.BindGetMember(binder);
                var final = Expression.Block(
                    Expression.Label(CallSiteBinder.UpdateLabel),
                    ret.Expression
                );
                var lambda = Expression.Lambda(final, param);
                var del = lambda.Compile();
                return del.DynamicInvoke(o).ToString();
            }
            else
            {
                return o.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance).GetValue(o, null).ToString();
            }
        }

        public static bool IsInteger(this string str)
        {
            int result;
            return int.TryParse(str, out result);
        }

        public static bool IsLong(this string str)
        {
            long result;
            return long.TryParse(str, out result);
        }

        public static bool IsFloat(this string str)
        {
            float result;
            return float.TryParse(str, out result);
        }

        public static int NumberOfFractionalPart(this string str)
        {
            var separator = str.Contains(",") ? ',' : '.';
            return str.Split(separator).Last().Length;
        }

        public static bool IsConvertibleToBool(this string str)
        {
            try
            {
                var result = Convert.ToBoolean(int.Parse(str));
                return result.GetType() == typeof(bool);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsDate(this string str)
        {
            DateTime dt;
            return DateTime.TryParseExact(str, "yyyyMMdd-HHmmss", CultureInfo.InvariantCulture,
                DateTimeStyles.AdjustToUniversal, out dt);
        }

        public static bool IsNumeric(this string str)
        {
            var regex = new Regex(@"^\d+$");
            return regex.IsMatch(str);
        }
    }
}
