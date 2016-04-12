using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;

namespace ImageTagger
{
    class FileManager
    {
        public static ObservableCollection<Picture> LoadFolder(string folderPath)
        {
            try
            {
                ObservableCollection<Picture> pictureList = new ObservableCollection<Picture>();
                String[] fileList = Directory.GetFiles(folderPath);
                foreach (string file in fileList)
                {
                    if (Path.GetExtension(file) == ".jpg")
                    {
                        Picture temp = LoadFile(file);
                        pictureList.Add(temp);
                    }
                }
                return pictureList;
            }
            catch (Exception ex)
            {         
                throw ex;
            }
        }

        public static Picture LoadFile(string filePath)
        {

            try
            {
                Picture temp = new Picture(filePath, PropertyManager.GetTags(filePath));
                return temp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }           
    }
}
