using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Collections.ObjectModel;


namespace MiniTC
{
    /// <summary>
    /// Logika interakcji dla klasy PanelTC.xaml
    /// </summary>
    public partial class PanelTC : UserControl
    {
        public PanelTC()
        {
            InitializeComponent();
        }

        #region Właściwości zależne

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(PanelTC),
                new FrameworkPropertyMetadata(null)
            );

        // ComboBox
        public static readonly DependencyProperty CbItemsSourceProperty = DependencyProperty.Register(
                "CbItemsSource",
                typeof(string[]),
                typeof(PanelTC),
                new FrameworkPropertyMetadata(null)
            );

        public static readonly DependencyProperty CbSelectedItemProperty = DependencyProperty.Register(
                "CbSelectedItem",
                typeof(string),
                typeof(PanelTC),
                new FrameworkPropertyMetadata(null)
            );

        // ListBox
        public static readonly DependencyProperty LsItemsSourceProperty = DependencyProperty.Register(
                "LsItemsSource",
                typeof(ObservableCollection<string>),
                typeof(PanelTC),
                new FrameworkPropertyMetadata(null)
            );

        public static readonly DependencyProperty LsSelectedItemProperty = DependencyProperty.Register(
                "LsSelectedItem",
                typeof(string),
                typeof(PanelTC),
                new FrameworkPropertyMetadata(null)
            );

        #endregion

        #region Właściwości

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Drives
        public string[] CbItemsSource
        {
            get { return (string[])GetValue(CbItemsSourceProperty); }
            set { SetValue(CbItemsSourceProperty, value); }
        }
        public string CbSelectedItem
        {
            get { return (string)GetValue(CbSelectedItemProperty); }
            set { SetValue(CbSelectedItemProperty, value); }
        }

        // Content of folder
        public ObservableCollection<string> LsItemsSource
        {
            get { return (ObservableCollection<string>)GetValue(LsItemsSourceProperty); }
            set { SetValue(LsItemsSourceProperty, value); }
        }
        public string LsSelectedItem
        {
            get { return (string)GetValue(LsSelectedItemProperty); }
            set { SetValue(LsSelectedItemProperty, value); }
        }

        #endregion

        #region Zdarzenia własne
        // Combo box

        public static readonly RoutedEvent DriveChangedEvent =
            EventManager.RegisterRoutedEvent(
                "OtherDriveSelected",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(PanelTC)
            );

        public event RoutedEventHandler DriverChanged
        {
            add { AddHandler(DriveChangedEvent, value); }
            remove { RemoveHandler(DriveChangedEvent, value); }
        }

        void RaiseDriveChanged(object sender, SelectionChangedEventArgs e)
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(DriveChangedEvent);
            RaiseEvent(newEventArgs);
        }

        // Listbox

        public static readonly RoutedEvent SelectionChangedEvent =
        EventManager.RegisterRoutedEvent(
            "OtherItemSelected",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(PanelTC)
        );

        public event RoutedEventHandler SelectionChanged
        {
            add { AddHandler(SelectionChangedEvent, value); }
            remove { RemoveHandler(SelectionChangedEvent, value); }
        }

        void RaiseSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(SelectionChangedEvent);
            RaiseEvent(newEventArgs);
        }
        #endregion
    }
}
