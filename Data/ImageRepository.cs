using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;

namespace VisitorWebAPI.Data
{
    public class ImageRepository
    {
        private readonly Cloudinary cloudinary;

        public ImageRepository()
        {
            DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
            string cloudinaryUrl = Environment.GetEnvironmentVariable("CLOUDINARY_URL");

            if (string.IsNullOrEmpty(cloudinaryUrl))
            {
                throw new Exception("Cloudinary URL is not set in environment variables.");
            }

            cloudinary = new Cloudinary(cloudinaryUrl);
            cloudinary.Api.Secure = true;
        }

        public string ImageUpload(IFormFile file)
        {
            DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
            string cloudinaryUrl = Environment.GetEnvironmentVariable("CLOUDINARY_URL");

            if (string.IsNullOrEmpty(cloudinaryUrl))
            {
                throw new Exception("Cloudinary URL is not set in environment variables.");
            }

            Cloudinary cloudinary = new Cloudinary(cloudinaryUrl);
            cloudinary.Api.Secure = true;

            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    UseFilename = true,
                    UniqueFilename = false,
                    Overwrite = true
                };

                var uploadResult = cloudinary.Upload(uploadParams);

                if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception("Image upload failed: " + uploadResult.Error.Message);
                }

                return uploadResult.SecureUrl.ToString(); // Return image URL
            }
        }
    }
}
