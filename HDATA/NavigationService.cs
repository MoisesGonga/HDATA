using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HDATA
{
    public class NavigationService
    {
        public static void GridNavigationUsercontrol(Grid grid, UserControl control) {
            if (grid.Children.Count>=0)
            {
                grid.Children.Clear();
                grid.Children.Add(control);

            }
            else
            {
                grid.Children.Clear();
                grid.Children.Add(control);
            }
        }

        //public static void GridNavigationPage(Grid grid, Page control)
        //{
        //    Frame fr = new Frame();
        //    if (grid.Children.Count > 0)
        //    {
        //        fr.Navigate(control);
        //        grid.Children.Clear();
        //        grid.RowDefinitions.Add(new RowDefinition());
        //        grid.RowDefinitions.Add(new RowDefinition());
                           
        //        grid.Children.Add(fr);

        //    }
        //    else
        //    {
        //        grid.Children.Clear();

        //        fr.Navigate(control);
        //        grid.Children.Add(fr);
        //        grid.Children.Add(control);
        //    }
        //}
    }
}
