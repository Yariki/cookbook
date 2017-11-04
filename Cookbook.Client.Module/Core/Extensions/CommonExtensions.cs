using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public static bool IsBeetwen(this decimal value, decimal min, decimal max)
        {
            return value >= min && value < max;
        }

        public static bool IsBeetwen(this int value, int min, int max)
        {
            return value >= min && value < max;
        }
    }
}
