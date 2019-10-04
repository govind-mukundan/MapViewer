#region copyright
/*
Copyright 2015 Govind Mukundan

This file is part of MapViewer.

MapViewer is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MapViewer is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MapViewer.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace MapViewer
{
    /// <summary>
    /// Nice tutorial on customizing the property editor - https://msdn.microsoft.com/en-us/library/aa302334.aspx
    /// </summary>
    public class FileBrowserEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context,IServiceProvider provider,object value)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            if(context.PropertyDescriptor.Name == "NMPath")
                OFD.Filter = "NM Executable|*.exe";
            else if (context.PropertyDescriptor.Name == "ObjDumpPath")
                OFD.Filter = "ObjDump Executable|*.exe";

            if (OFD.ShowDialog() == DialogResult.OK)
            {
                value = OFD.FileName;
            }

            return value;
        }
    }

    // https://msdn.microsoft.com/en-us/library/ms973902.aspx
    public class Settings
    {
        List<string> _textSeg2SecMap;
        // see: http://stackoverflow.com/questions/6307006/how-can-i-use-a-winforms-propertygrid-to-edit-a-list-of-strings
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(@"System.Windows.Forms.Design.StringCollectionEditor," + "System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
        typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(CsvConverter))]
        [DefaultValue(".text, ._pm, .reset, .init, .user_init, .handle, .isr, .libc, .libm, .libdsp, .lib, usercode, userconst, .dinit, .const"), Category("Segment To Section Map")]
        public List<string> TextSeg2SecMap
        { 
            get { return _textSeg2SecMap; }
            set
            {
                if (_textSeg2SecMap != value)
                {
                    _textSeg2SecMap = value;
                    appSettingsChanged = true;
                }
            }
        }

        List<string> _dataSeg2SecMap;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(@"System.Windows.Forms.Design.StringCollectionEditor," + "System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
        typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(CsvConverter))]
        [DefaultValue(".data, .rodata, .strings, .ndata"), Category("Segment To Section Map")]
        public List<string> DataSeg2SecMap
        {
            get { return _dataSeg2SecMap; }
            set
            {
                if (_dataSeg2SecMap != value)
                {
                    _dataSeg2SecMap = value;
                    appSettingsChanged = true;
                }
            }
        }


        List<string> _bssSeg2SecMap;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(@"System.Windows.Forms.Design.StringCollectionEditor," + "System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
        typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(CsvConverter))]
        [DefaultValue(".bss, COMMON, .nbss"), Category("Segment To Section Map")]
        public List<string> BSSSeg2SecMap
        {
            get { return _bssSeg2SecMap; }
            set
            {
                if (_bssSeg2SecMap != value)
                {
                    _bssSeg2SecMap = value;
                    appSettingsChanged = true;
                }
            }
        }

        string _mapPath;
        [DefaultValue("Path to Map file"), Category("Path")]
        [Browsable(false)] // Hide
        public string MapPath
        {
            get { return _mapPath; }
            set
            {
                if (value != _mapPath)
                {
                    appSettingsChanged = true;
                    _mapPath = value;
                }
            }
        }

        string _elfPath;
        [DefaultValue("Path to Elf File"), Category("Path")]
        [Browsable(false)] // Hide
        public string ElfPath
        {
            get { return _elfPath; }
            set
            {
                if (value != _elfPath)
                {
                    appSettingsChanged = true;
                    _elfPath = value;
                }
            }
        }

        string _nmPath;
        [Editor(typeof(FileBrowserEditor),typeof(System.Drawing.Design.UITypeEditor))] // To display elepsis button and file browser
        [DefaultValue("Path to NM"), Category("Path")]
        public string NMPath
        {
            get { return _nmPath; }
            set
            {
                if (value != _nmPath)
                {
                    appSettingsChanged = true;
                    _nmPath = value;
                }
            }
        }

        string _readElfPath;
        [Editor(typeof(FileBrowserEditor), typeof(System.Drawing.Design.UITypeEditor))] // To display elepsis button and file browser
        [DefaultValue("Path to ReadElf"), Category("Path")]
        public string ReadElfPath
        {
            get { return _readElfPath; }
            set
            {
                if (value != _readElfPath)
                {
                    appSettingsChanged = true;
                    _readElfPath = value;
                }
            }
        }
        private bool appSettingsChanged;

        public int TARGET_FT32 = 1;
        public int TARGET_MICROCHIP_XC = 2;


        public class CsvConverter : TypeConverter
        {
            // Overrides the ConvertTo method of TypeConverter.
            public override object ConvertTo(ITypeDescriptorContext context,
               CultureInfo culture, object value, Type destinationType)
            {
                List<String> v = value as List<String>;
                if (destinationType == typeof(string))
                {
                    return String.Join(",", v.ToArray());
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }
        }

        // A way to load default settings by looking at the DefaultSetting attribute
        void LoadDefaults()
        {
            // Set up defaults
            foreach (PropertyInfo pi in this.GetType().GetProperties())
            {
                var value = pi.GetValue(this, null);
                if (value == null)
                {
                    // Load the defaults
                    DefaultValueAttribute def = (DefaultValueAttribute)Attribute.GetCustomAttribute(pi, typeof(DefaultValueAttribute));
                    if (def != null)
                    {
                        if (pi.Name == "DataSeg2SecMap" || pi.Name == "BSSSeg2SecMap" || pi.Name == "TextSeg2SecMap")
                        {
                            pi.SetValue(this, def.Value.ToString().Split(',').Select(x => x.Trim()).ToList(), null);
                        }
                        else
                        {
                            Debug.Write(pi.GetType().ToString());
                            pi.SetValue(this, def.Value, null);
                        }

                    }

                }
            }
        }

        // Serializes the class to the config file
        // if any of the settings have changed.
        public bool SaveAppSettings()
        {
            if (this.appSettingsChanged)
            {
                StreamWriter myWriter = null;
                XmlSerializer mySerializer = null;
                try
                {
                    // Create an XmlSerializer for the 
                    // ApplicationSettings type.
                    mySerializer = new XmlSerializer(typeof(Settings));
                    myWriter = new StreamWriter(Application.LocalUserAppDataPath + @"\myApplication.config", false);
                    // Serialize this instance of the ApplicationSettings 
                    // class to the config file.
                    mySerializer.Serialize(myWriter, this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    // If the FileStream is open, close it.
                    if (myWriter != null)
                    {
                        myWriter.Close();
                    }
                }
            }
            return appSettingsChanged;
        }

        // Deserializes the class from the config file.
        public bool LoadAppSettings()
        {
            XmlSerializer mySerializer = null;
            FileStream myFileStream = null;
            bool fileExists = false;

            try
            {
                // Create an XmlSerializer for the ApplicationSettings type.
                mySerializer = new XmlSerializer(typeof(Settings));
                FileInfo fi = new FileInfo(Application.LocalUserAppDataPath + @"\myApplication.config");
                // If the config file exists, open it.
                if (fi.Exists)
                {
                    myFileStream = fi.OpenRead();
                    // Create a new instance of the ApplicationSettings by
                    // deserializing the config file.
                    Settings myAppSettings = (Settings)mySerializer.Deserialize(myFileStream);
                    // Assign the property values to this instance of 
                    // the ApplicationSettings class.
                    this.MapPath = myAppSettings.MapPath;
                    this.ElfPath = myAppSettings.ElfPath;
                    this.TextSeg2SecMap = myAppSettings.TextSeg2SecMap;
                    this.BSSSeg2SecMap = myAppSettings.BSSSeg2SecMap;
                    this.DataSeg2SecMap = myAppSettings.DataSeg2SecMap;
                    this.NMPath = myAppSettings.NMPath;
                    this.ReadElfPath = myAppSettings.ReadElfPath;
                    fileExists = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // If the FileStream is open, close it.
                if (myFileStream != null)
                {
                    myFileStream.Close();
                }
                LoadDefaults();
            }
            return fileExists;
        }
    }
}
