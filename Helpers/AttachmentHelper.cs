using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MNBugTracker.Helpers
{
    public class AttachmentHelper
    {
        public static bool IsWebFriendlyAttachment(HttpPostedFileBase file)
        {
            if (file == null) return false;

            if (file.ContentLength > 2 * 1024 * 1024 || file.ContentLength < 1024) return false;

            try
            {
                var fileExt = VirtualPathUtility.GetExtension(file.FileName);
                return fileExt.Contains(".bmp") ||
                       fileExt.Contains(".png") ||
                       fileExt.Contains(".jpg") ||
                       fileExt.Contains(".jpeg") ||
                       fileExt.Contains(".tiff") ||
                       fileExt.Contains(".pdf") ||
                       fileExt.Contains(".txt") ||
                       fileExt.Contains(".doc") ||
                       fileExt.Contains(".docx") ||
                       fileExt.Contains(".xls") ||
                       fileExt.Contains(".xlsx");
            }
            catch
            {
                return false;
            }
        }

        public static string DisplayImage(string filePath)
        {
            var fileName = filePath;

            switch (Path.GetExtension(filePath))
            {
                case ".doc":
                    fileName = "/Images/DefaultDoc.ico";
                    break;
                case ".docx":
                    fileName = "/Images/DefaultDocx.ico";
                    break;
                case ".pdf":
                    fileName = "/Images/DefaultPdf.ico";
                    break;
                case ".xls":
                    fileName = "/Images/DefaultXls.ico";
                    break;
                case ".xlsx":
                    fileName = "/Images/DefaultXlsx.ico";
                    break;
                case ".txt":
                    fileName = "/Images/DefaultTxt.ico";
                    break;
                default:
                    break;
            }
            return fileName;
        }
    }
}