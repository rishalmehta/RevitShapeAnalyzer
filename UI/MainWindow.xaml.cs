using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RevitShapeAnalyzer.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow(Dictionary<string, int> results)
        {
            InitializeComponent();

            dgCounts.ItemsSource =
                results.Select(x => new
                {
                    Category = x.Key,
                    Count = x.Value
                });
        }
    }
}