using Microsoft.AspNetCore.Http;

namespace homeMaintenance.Application.Ports.In
{
    public interface IImageStorageService
    {
        Task<string?> UploadImageAsync(IFormFile file, string imageName);
        string GetImageUrl(string key);
        string GetImagePath(string? avatarKey);
    }
}
