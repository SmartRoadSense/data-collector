using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Plugin.CurrentActivity;

namespace DataCollector {

    public static class FileNaming {

        private static string GenerateFilename(string addendum) {
            if(!string.IsNullOrWhiteSpace(addendum)) {
                addendum = "-" + addendum.Replace(' ', '-').ToLowerInvariant();
            }

            return string.Format("{0:yyyy}-{0:MM}-{0:dd}-{0:HH}-{0:mm}-{0:ss}{1}.csv",
                DateTime.Now, addendum);
        }

        public static (FileStream Stream, bool NewFile) OpenDumpFile(string addendum) {
            var filename = GenerateFilename(addendum);
            Debug.WriteLine("New filename: {0}", filename);

#if __ANDROID__
            string basePath = CrossCurrentActivity.Current.AppContext.GetExternalFilesDir(null).AbsolutePath;
#else
#error Unsupported platform
#endif

            var filePath = Path.Combine(basePath, filename);
            Debug.WriteLine("Opening file {0} for append", filePath);

            bool exists = File.Exists(filePath);

            return (new FileStream(filePath, FileMode.Append), !exists);
        }

    }

}
