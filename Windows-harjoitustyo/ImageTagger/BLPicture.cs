using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

namespace ImageTagger
{
    public class Picture
    {
        #region PROPERTIES
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string filePath;

        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        private string tags;

        public string Tags
        {
            get { return tags; }
            set { tags = value; }
        }

        #endregion
        #region CONSTRUCTORS

        #endregion
        #region METHODS
        
        #endregion
    }
}
