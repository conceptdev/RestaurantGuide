using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using Microsoft.Phone.Controls;
using Clarity.Phone.Controls;
using System.Linq;

/*
 * Source of the alphabet jump list
 * http://blogs.claritycon.com/blogs/kevin_marshall/archive/2010/10/06/wp7-quick-jump-grid-sample-code.aspx
 */
namespace RestGuide
{
    public class RestaurantNameSelector : IQuickJumpGridSelector
    {
        public Func<object, IComparable> GetGroupBySelector()
        {
            return (p) => ((Restaurant)p).Name.FirstOrDefault();
        }

        public Func<object, string> GetOrderByKeySelector()
        {
            return (p) => ((Restaurant)p).Name;
        }

        public Func<object, string> GetThenByKeySelector()
        {
            return (p) => (string.Empty);
        }
    }
}
