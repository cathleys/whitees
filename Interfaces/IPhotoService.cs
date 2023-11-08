using CloudinaryDotNet.Actions;

namespace Whitees.Interfaces;
public interface IPhotoService
{
    Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
    Task<DeletionResult> DeletePhotoAsync(string publicId);

}
