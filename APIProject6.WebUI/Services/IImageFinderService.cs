namespace APIProject6.WebUI.Services
{
    public interface IImageFinderService
    {
        Task<string> GetImageUrlAsync(string query);
    }
}
