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
        string folder;
        Picture currentPicture;

        public MainWindow()
        {
            InitializeComponent();
        }

        
        private void lbSaveTags_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PropertyManager.SaveTags(currentPicture.TagsList, currentPicture.FilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
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
                    ApplyFilter();
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
                currentPicture = (Picture)lbFiles.SelectedItem;
                imgMain.Source = LoadImage(currentPicture.FilePath);                                   
            }
        }

        private void btnAddTag_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentPicture != null)
                {
                    if (!string.IsNullOrEmpty(txtTag.Text))
                    {
                        currentPicture.TagsList.Add(txtTag.Text);
                    }
                    else
                        MessageBox.Show("Type the tag first");
                }
                else
                    MessageBox.Show("No picture selected");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRemoveTag_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbTags.SelectedIndex >= 0 && lbTags.SelectedIndex < currentPicture.TagsList.Count)
                    currentPicture.TagsList.RemoveAt(lbTags.SelectedIndex);
                else
                    MessageBox.Show("No tag selected");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbiShow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilter();
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilter();
        }

        #region FILTERS
        private bool FilterTagless(object item)
        {
            Picture picture = (Picture)item;
            if (picture.TagsList.Count() > 0)
                return false;
            else
                return true;
        }

        private bool FilterWith(object item)
        {
            Picture picture = (Picture)item;
            if (picture.TagsList.Count() > 0)
            {

                foreach (string tag in picture.TagsList)
                {
                    if (tag.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                        return true;
                }
                return false;
            }
            else
                return false;
        }
        #endregion

        #region METHODS
        private void ApplyFilter()
        {
            if (cbiShow.SelectedItem == cbiShowAll && folder != null)
                CollectionViewSource.GetDefaultView(lbFiles.ItemsSource).Filter = null;
            if (cbiShow.SelectedItem == cbiShowTagless && folder != null)
                CollectionViewSource.GetDefaultView(lbFiles.ItemsSource).Filter = FilterTagless;
            if (cbiShow.SelectedItem == cbiShowWithTag && folder != null)
            {
                if (txtFilter.Text.Any())
                    CollectionViewSource.GetDefaultView(lbFiles.ItemsSource).Filter = FilterWith;
                else
                    CollectionViewSource.GetDefaultView(lbFiles.ItemsSource).Filter = null;
            }         
        }

        private BitmapImage LoadImage(string filePath)
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.StreamSource = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            b.CacheOption = BitmapCacheOption.OnLoad;
            b.EndInit();
            b.StreamSource.Dispose();
            return b;
        }
        #endregion
    }
}
