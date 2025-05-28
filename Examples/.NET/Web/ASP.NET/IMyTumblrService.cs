using System.Threading.Tasks;

namespace ASPNet
{
    public interface IMyTumblrService
    {
        Task<string> GetFollowerCount();
    }
}
