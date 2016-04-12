using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using System.IO;

namespace ImageTagger
{
    public class Picture : INotifyPropertyChanged
    {
        #region PROPERTIES
        public string Name
        {
            get { return Path.GetFileName(filePath); }
        }

        private string filePath;

        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        private BindingList<string> tagsList;

        public BindingList<string> TagsList
        {
            get { return tagsList; }
            set { tagsList = value; Notify("TagsList"); }
        }


        #endregion
        #region CONSTRUCTORS
        public Picture(string filePath, BindingList<string> tagsList)
        {
            this.filePath = filePath;
            this.tagsList = tagsList;
        }

        #endregion
        #region METHODS
        public override string ToString()
        {
            return Name;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void Notify(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        #endregion
    }
}
