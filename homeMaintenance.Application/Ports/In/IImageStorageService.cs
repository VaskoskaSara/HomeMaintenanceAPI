using Microsoft.AspNetCore.Http;

namespace homeMaintenance.Application.Interfaces
{
    public interface IImageStorageService
    {
        Task<string?> UploadImageAsync(IFormFile file);
        string GetImageUrl(string key);
        string GetImagePath(string? avatarKey);
    }
}
