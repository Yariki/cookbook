using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;

namespace Cookbook.Client.Module.Core.Extensions
{
    public static class CommonExtensions
    {
        public static Visibility ToVisibility(this bool arg)
        {
            return arg ? Visibility.Visible : Visibility.Collapsed;
        }


        public static bool ToBool(this Visibility arg)
        {
            return arg == Visibility.Visible;
        }

        public static bool IsNull(this object arg)
        {
            return ReferenceEquals(arg, null);
        }

        public static bool IsNotNull(this object arg)
        {
            return !ReferenceEquals(arg, null);
        }

        public static bool IsOneExist<T>(this IEnumerable<T> list)
        {
            return list.IsNotNull() && list.Count() == 1;
        }

        public static T Resolve<T>(this IUnityContainer container) where T: class
        {
            return container.Resolve(typeof(T)) as T;
        }

    }
}
