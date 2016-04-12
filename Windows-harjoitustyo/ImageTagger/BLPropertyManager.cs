using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using System.ComponentModel;

namespace ImageTagger
{
    public class PropertyManager
    {
        private static string GetValue(IShellProperty value)
        {
            if (value == null || value.ValueAsObject == null)
                return String.Empty;
            return value.ValueAsObject.ToString();
        } 

        public static BindingList<string> GetTags(string filePath)
        {
            try
            {
                ShellFile file = ShellFile.FromFilePath(filePath);
                var tagsArray = file.Properties.GetProperty(SystemProperties.System.Keywords).ValueAsObject;
                BindingList<string> tagsList = new BindingList<string>();
                if (tagsArray != null)
                {
                    foreach (string item in (string[])tagsArray)
                    {
                        tagsList.Add(item);
                    }
                }          
                return tagsList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void SaveTags(BindingList<String> tagsList, string filePath)
        {
            string tags = "";
            foreach (string tag in tagsList)
            {
                tags = tags + tag + "; ";
            }
            tags = tags.Remove(tags.Length - 2);
            try
            {
                ShellObject file = ShellFile.FromFilePath(filePath);
                var writer = file.Properties.GetPropertyWriter();
                writer.WriteProperty(SystemProperties.System.Keywords, tags);
                writer.Close();     
             }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
