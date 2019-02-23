using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MentalPrepApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NavToPlayPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Views.Pages.PlayPage));
        }

        private void NavToManage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Views.Pages.ManagePage));
        }
        private void NavToManagePage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Views.Pages.ManagePage));
        }

        private void NavBack_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        private void NavForward_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoForward)
                Frame.GoForward();
        }
    }
}
