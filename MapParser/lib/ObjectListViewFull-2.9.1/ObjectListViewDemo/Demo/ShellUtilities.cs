/*
 * [File purpose]
 * Author: Phillip Piper
 * Date: 1 May 2007 7:44 PM
 * 
 * CHANGE LOG:
 * 2009-07-08  JPP  Don't cache the image collections
 * 1 May 2007  JPP  Initial Version
 */

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using BrightIdeasSoftware;

namespace ObjectListViewDemo
{
    /// <summary>
    /// This helper class allows listviews and tree views to use image from the system image list.
    /// </summary>
    /// <remarks>Instances of this helper class know how to retrieve icon from the Windows shell for
    /// a given file path. These icons are then added to the imagelist on the given control. ListViews need 
    /// special handling since they have two image lists which need to be kept in sync.</remarks>
    public class SysImageListHelper
    {
        private SysImageListHelper()
        {
        }

        protected ImageList.ImageCollection SmallImageCollection {
            get {
                if (this.listView != null)
                    return this.listView.SmallImageList.Images;
                if (this.treeView != null)
                    return this.treeView.ImageList.Images;
                return null;
            }
        }

        protected ImageList.ImageCollection LargeImageCollection {
            get {
                if (this.listView != null)
                    return this.listView.LargeImageList.Images;
                return null;
            }
        }

        protected ImageList SmallImageList {
            get {
                if (this.listView != null)
                    return this.listView.SmallImageList;
                if (this.treeView != null)
                    return this.treeView.ImageList;
                return null;
            }
        }

        protected ImageList LargeImageList {
            get {
                if (this.listView != null)
                    return this.listView.LargeImageList;
                return null;
            }
        }


        /// <summary>
        /// Create a SysImageListHelper that will fetch images for the given tree control
        /// </summary>
        /// <param name="treeView">The tree view that will use the images</param>
        public SysImageListHelper(TreeView treeView)
        {
            if (treeView.ImageList == null) {
                treeView.ImageList = new ImageList();
                treeView.ImageList.ImageSize = new Size(16, 16);
            }
            this.treeView = treeView;
        }
        protected TreeView treeView;

        /// <summary>
        /// Create a SysImageListHelper that will fetch images for the given listview control.
        /// </summary>
        /// <param name="listView">The listview that will use the images</param>
        /// <remarks>Listviews manage two image lists, but each item can only have one image index.
        /// This means that the image for an item must occur at the same index in the two lists. 
        /// SysImageListHelper instances handle this requirement. However, if the listview already
        /// has image lists installed, they <b>must</b> be of the same length.</remarks>
        public SysImageListHelper(ObjectListView listView)
        {
            if (listView.SmallImageList == null) {
                listView.SmallImageList = new ImageList();
                listView.SmallImageList.ColorDepth = ColorDepth.Depth32Bit;
                listView.SmallImageList.ImageSize = new Size(16, 16);
            }

            if (listView.LargeImageList == null) {
                listView.LargeImageList = new ImageList();
                listView.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
                listView.LargeImageList.ImageSize = new Size(32, 32);
            }

            //if (listView.SmallImageList.Images.Count != listView.LargeImageList.Images.Count)
            //    throw new ArgumentException("Small and large image lists must have the same number of items.");

            this.listView = listView;
        }
        protected ObjectListView listView;

        /// <summary>
        /// Return the index of the image that has the Shell Icon for the given file/directory.
        /// </summary>
        /// <param name="path">The full path to the file/directory</param>
        /// <returns>The index of the image or -1 if something goes wrong.</returns>
        public int GetImageIndex(string path)
        {
            if (System.IO.Directory.Exists(path))
                path = System.Environment.SystemDirectory; // optimization! give all directories the same image
            else
                if (System.IO.Path.HasExtension(path))
                    path = System.IO.Path.GetExtension(path);

            if (this.SmallImageCollection.ContainsKey(path))
                return this.SmallImageCollection.IndexOfKey(path);

            try {
                this.AddImageToCollection(path, this.SmallImageList, ShellUtilities.GetFileIcon(path, true, true));
                this.AddImageToCollection(path, this.LargeImageList, ShellUtilities.GetFileIcon(path, false, true));
            } catch (ArgumentNullException) {
                return -1;
            }

            return this.SmallImageCollection.IndexOfKey(path);
        }

        private void AddImageToCollection(string key, ImageList imageList, Icon image) {
            if (imageList == null)
                return;

            if (imageList.ImageSize == image.Size) {
                imageList.Images.Add(key, image);
                return;
            }

            using (Bitmap imageAsBitmap = image.ToBitmap()) {
                Bitmap bm = new Bitmap(imageList.ImageSize.Width, imageList.ImageSize.Height);
                Graphics g = Graphics.FromImage(bm);
                g.Clear(imageList.TransparentColor);
                Size size = imageAsBitmap.Size;
                int x = Math.Max(0, (bm.Size.Width - size.Width) / 2);
                int y = Math.Max(0, (bm.Size.Height - size.Height) / 2);
                g.DrawImage(imageAsBitmap, x, y, size.Width, size.Height);
                imageList.Images.Add(key, bm);
            }
        }
    }
    
    /// <summary>
    /// ShellUtilities contains routines to interact with the Windows Shell.
    /// </summary>
    public static class ShellUtilities
    {
        /// <summary>
        /// Execute the default verb on the file or directory identified by the given path.
        /// For documents, this will open them with their normal application. For executables,
        /// this will cause them to run.
        /// </summary>
        /// <param name="path">The file or directory to be executed</param>
        /// <returns>Values &lt; 31 indicate some sort of error. See ShellExecute() documentation for specifics.</returns>
        /// <remarks>The same effect can be achieved by <code>System.Diagnostics.Process.Start(path)</code>.</remarks>
        public static int Execute(string path)
        {
            return ShellUtilities.Execute(path, "");
        }

        /// <summary>
        /// Execute the given operation on the file or directory identified by the given path.
        /// Example operations are "edit", "print", "explore".
        /// </summary>
        /// <param name="path">The file or directory to be operated on</param>
        /// <param name="operation">What operation should be performed</param>
        /// <returns>Values &lt; 31 indicate some sort of error. See ShellExecute() documentation for specifics.</returns>
        public static int Execute(string path, string operation)
        {
            IntPtr result = ShellUtilities.ShellExecute(0, operation, path, "", "", SW_SHOWNORMAL);
            return result.ToInt32();
        }

        /// <summary>
        /// Get the string that describes the file's type.
        /// </summary>
        /// <param name="path">The file or directory whose type is to be fetched</param>
        /// <returns>A string describing the type of the file, or an empty string if something goes wrong.</returns>
        public static String GetFileType(string path)
        {
            SHFILEINFO shfi = new SHFILEINFO();
            int flags = SHGFI_TYPENAME;
            IntPtr result = ShellUtilities.SHGetFileInfo(path, 0, out shfi, Marshal.SizeOf(shfi), flags);
            if (result.ToInt32() == 0)
                return String.Empty;
            else
                return shfi.szTypeName;
        }

        /// <summary>
        /// Return the icon for the given file/directory.
        /// </summary>
        /// <param name="path">The full path to the file whose icon is to be returned</param>
        /// <param name="isSmallImage">True if the small (16x16) icon is required, otherwise the 32x32 icon will be returned</param>
        /// <param name="useFileType">If this is true, only the file extension will be considered</param>
        /// <returns>The icon of the given file, or null if something goes wrong</returns>
        public static Icon GetFileIcon(string path, bool isSmallImage, bool useFileType)
        {
            int flags = SHGFI_ICON;
            if (isSmallImage)
                flags |= SHGFI_SMALLICON;

            int fileAttributes = 0;
            if (useFileType) {
                flags |= SHGFI_USEFILEATTRIBUTES;
                if (System.IO.Directory.Exists(path))
                    fileAttributes = FILE_ATTRIBUTE_DIRECTORY;
                else
                    fileAttributes = FILE_ATTRIBUTE_NORMAL;
            }

            SHFILEINFO shfi = new SHFILEINFO();
            IntPtr result = ShellUtilities.SHGetFileInfo(path, fileAttributes, out shfi, Marshal.SizeOf(shfi), flags);
            if (result.ToInt32() == 0)
                return null;
            else
                return Icon.FromHandle(shfi.hIcon);
        }

        /// <summary>
        /// Return the index into the system image list of the image that represents the given file.
        /// </summary>
        /// <param name="path">The full path to the file or directory whose icon is required</param>
        /// <returns>The index of the icon, or -1 if something goes wrong</returns>
        /// <remarks>This is only useful if you are using the system image lists directly. Since there is
        /// no way to do that in .NET, it isn't a very useful.</remarks>
        public static int GetSysImageIndex(string path)
        {
            SHFILEINFO shfi = new SHFILEINFO();
            int flags = SHGFI_ICON | SHGFI_SYSICONINDEX;
            IntPtr result = ShellUtilities.SHGetFileInfo(path, 0, out shfi, Marshal.SizeOf(shfi), flags);
            if (result.ToInt32() == 0)
                return -1;
            else
                return shfi.iIcon;
        }

        #region Native methods

        private const int SHGFI_ICON               = 0x00100;     // get icon
        private const int SHGFI_DISPLAYNAME        = 0x00200;     // get display name
        private const int SHGFI_TYPENAME           = 0x00400;     // get type name
        private const int SHGFI_ATTRIBUTES         = 0x00800;     // get attributes
        private const int SHGFI_ICONLOCATION       = 0x01000;     // get icon location
        private const int SHGFI_EXETYPE            = 0x02000;     // return exe type
        private const int SHGFI_SYSICONINDEX       = 0x04000;     // get system icon index
        private const int SHGFI_LINKOVERLAY        = 0x08000;     // put a link overlay on icon
        private const int SHGFI_SELECTED           = 0x10000;     // show icon in selected state
        private const int SHGFI_ATTR_SPECIFIED     = 0x20000;     // get only specified attributes
        private const int SHGFI_LARGEICON          = 0x00000;     // get large icon
        private const int SHGFI_SMALLICON          = 0x00001;     // get small icon
        private const int SHGFI_OPENICON           = 0x00002;     // get open icon
        private const int SHGFI_SHELLICONSIZE      = 0x00004;     // get shell size icon
        private const int SHGFI_PIDL               = 0x00008;     // pszPath is a pidl
        private const int SHGFI_USEFILEATTRIBUTES  = 0x00010;     // use passed dwFileAttribute
        //if (_WIN32_IE >= 0x0500)
        private const int SHGFI_ADDOVERLAYS        = 0x00020;     // apply the appropriate overlays
        private const int SHGFI_OVERLAYINDEX       = 0x00040;     // Get the index of the overlay

        private const int FILE_ATTRIBUTE_NORMAL    = 0x00080;     // Normal file
        private const int FILE_ATTRIBUTE_DIRECTORY = 0x00010;     // Directory

        private const int MAX_PATH = 260;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct SHFILEINFO
        {
            public IntPtr hIcon; 
            public int    iIcon; 
            public int    dwAttributes; 
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=MAX_PATH)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=80)]
            public string szTypeName; 
        }

        private const int SW_SHOWNORMAL = 1;

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr ShellExecute(int hwnd, string lpOperation, string lpFile, 
            string lpParameters, string lpDirectory, int nShowCmd);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SHGetFileInfo(string pszPath, int dwFileAttributes,
            out SHFILEINFO psfi, int cbFileInfo, int uFlags);

        #endregion
    }
}
