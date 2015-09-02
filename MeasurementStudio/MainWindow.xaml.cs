﻿using IVCharacterization.Experiments;
using IVCharacterization.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeasurementStudio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IPageTransitionView
    {
        public MainWindow()
        {
            InitializeComponent();
            var dc = DataContext as IMainViewModel;
            if (dc == null)
                return;
            dc.View = this;
            dc.DataContextIsSet();
        }

        //private void MenuItem_Click(object sender, RoutedEventArgs e)
        //{
        //    var a = new UserControl { /*ContentTemplate = (DataTemplate)Resources["ivtemplate"], */ Content = new OutputIVViewModel() };
        //    //var a = new UserControl { Content = new Border{ BorderThickness = new Thickness(5), BorderBrush = Brushes.Red, Background = Brushes.Green} };//new OutputIVViewModel() };
        //    //c.Content = new OutputIVViewModel();
        //    //var a = new OutputCurveMeasurement();
        //    //var a = new IVCharacterization.IVMainView();
        //    //var vm = new OutputIVViewModel();
        //    //a.DataContext = vm;
        //    PageTransitionControl.ShowPage(a);
        //}


        public void ShowPage(UserControl page)
        {
            PageTransitionControl.ShowPage(page);
        }

        //private void Window_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    var dc = DataContext as IMainViewModel;
        //    if (dc == null)
        //        return;
        //    dc.View = this;
        //    dc.DataContextIsSet();
        //}
    }

}
