using DontPanic.TumblrSharp.OAuth;
using System.Threading.Tasks;

namespace Testing
{
    public interface IMyTumblrService
    {
        Task<string> GetFollowerCount();
    }
}
