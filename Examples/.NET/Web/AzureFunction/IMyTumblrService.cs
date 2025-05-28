using System.Threading.Tasks;

namespace AzureTest
{
    public interface IMyTumblrService
    {
        Task<string> GetUser();

        Task<string> GetBlog(string blogName);
    }
}