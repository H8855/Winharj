using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
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
using System.Collections.ObjectModel;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Globalization;

namespace ImageTagger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Picture> pictures = new ObservableCollection<Picture>();
        string testFile = "F:\\testi\\nwmain.jpg";
        string folder = "F:\\testi";   

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnShowTags_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BindingList<string> TagsList = PropertyManager.GetTags(testFile);
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
            try
            {
                PropertyManager.SaveTags(pictures.ElementAt(lbFiles.SelectedIndex).TagsList, pictures.ElementAt(lbFiles.SelectedIndex).FilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void LoadImage(string filePath)
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri(filePath);
            b.EndInit();
            imgMain.Source = b;
            //return b;
        }

        private void ModifyTestData()
        {
            pictures.First().TagsList.Add("Uusitagi");
        }

        private void SetTestData(string filePath)
        {
            BindingList<string> TestTagsList = new BindingList<string>();
            TestTagsList.Add("Koira");
            TestTagsList.Add("Hevonen");

            Picture temp = new Picture(filePath, TestTagsList);
            pictures.Add(temp);
        }

        private void SetTestData2(string filePath)
        {
            pictures.Add(FileManager.LoadFile(testFile));
        }

        private void btnTestModifyingTags_Click(object sender, RoutedEventArgs e)
        {
            ModifyTestData();           
        }

        private void btnSelectFolder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonOpenFileDialog dlg = new CommonOpenFileDialog();
                dlg.Title = "Choose the target folder";
                dlg.IsFolderPicker = true;
                dlg.AllowNonFileSystemItems = false;
                dlg.EnsureFileExists = true;
                dlg.EnsurePathExists = true;
                dlg.EnsureReadOnly = false;
                dlg.EnsureValidNames = true;
                dlg.Multiselect = false;
                dlg.ShowPlacesList = true;

                if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    folder = dlg.FileName;
                    pictures = FileManager.LoadFolder(folder);
                    lbFiles.DataContext = pictures;                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lbFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbFiles.SelectedIndex >= 0 && lbFiles.SelectedIndex < pictures.Count)
            {
                lbTags.DataContext = lbFiles.SelectedItem;
                //Picture temp = (Picture)lbFiles.SelectedItem;
                //BitmapImage kuva = LoadImage(testFile);
                //imgMain.Source = kuva;   
                //LoadImage(testFile);   
                imgMain.DataContext = lbFiles.SelectedItem;                                      
            }
        }

        private void btnAddTag_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTag.Text)){
                    pictures.ElementAt(lbFiles.SelectedIndex).TagsList.Add(txtTag.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
