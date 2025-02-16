namespace VisitorWebAPI.Helpers
{
    public class ImageHelper
    {
        public static string SaveImageToFile(IFormFile imageFile, string directoryPath)
        {
            if (imageFile == null || imageFile.Length == 0)
                return null;
            // Ensure the directory exists
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            // Get the file extension and validate
            string fileExtension = Path.GetExtension(imageFile.FileName);
            if (string.IsNullOrWhiteSpace(fileExtension))
            {
                fileExtension = ".jpeg"; // Default extension
            }
            // Generate a unique file name
            string fileName = $"{Guid.NewGuid()}{fileExtension}";
            string fullPath = Path.Combine(directoryPath, fileName);
            // Save the file to the specified path
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }
            return fullPath;
        }
        public static byte[] ReadFileBytes(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
                return null;
            return File.ReadAllBytes(filePath);
        }
    }
}
