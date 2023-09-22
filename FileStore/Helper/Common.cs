namespace FileStore.Helper
{
    public static class Common
    {
        public static string GetCurrentDirectory()
        {
            var result = Directory.GetCurrentDirectory();
            return result;
        }
        public static string GetStatisContentDirectory() {
            var result = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\StatisContent\\");
            if (!Directory.Exists(result)) { 
                Directory.CreateDirectory(result);
            }
            return result;
        }
        public static string GetFilePath(string FileName)
        {
            var _GetStaticContentDirectory = GetStatisContentDirectory();
            var result = Path.Combine(_GetStaticContentDirectory, FileName);
            return result;
        }

    }
}
