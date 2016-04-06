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
using System.IO;

namespace ImageTagger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnShowTags_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> TagsList = PropertyManager.GetTags("D:\\H8855\\testikuva.jpg");
                //TagsList.Remove("Kissa");
                foreach (var tag in TagsList)
                {
                    lbTags.Items.Add(tag);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lbSaveTags_Click(object sender, RoutedEventArgs e)
        {
            List<string> TestTagsList = new List<string>();
            TestTagsList.Add("Koira");
            TestTagsList.Add("Hevonen");

            try
            {
                PropertyManager.SaveTags(TestTagsList, "D:\\H8855\\testikuva.jpg");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
