using System.Text;
using System.IO;

namespace renderer.src.Etc {

    class FileReader {

        public static string ReadFileContents(string file) {
            string source = "";

            using (StreamReader r = new StreamReader(file, Encoding.UTF8)) {
                source = r.ReadToEnd();
            }

            return source;
        }
    }
}