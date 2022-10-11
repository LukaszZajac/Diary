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
using Diary.ViewModels;
using MahApps.Metro.Controls;

namespace Diary.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            //var trig = new Microsoft.Xaml.Behaviors.EventTrigger(); trig.SourceName = "foo";
            InitializeComponent();
            
            //wskazanie na to z czym będziemy bindować
            //widok wie gdzie ma szukać powiązanych danych
            //Takim zapisem wskazujemy, że dla tego okna MainWindow – ViewModelem jest klasa MainViewModel.
            DataContext = new MainViewModel();
            //var _ = new Microsoft.Xaml.Behaviors.DefaultTriggerAttribute(typeof(Trigger), typeof(Microsoft.Xaml.Behaviors.TriggerBase), null);
        }
    }
}
