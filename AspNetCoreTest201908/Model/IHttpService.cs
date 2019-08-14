using System.Threading.Tasks;

namespace AspNetCoreTest201908.Model
{
    public interface IHttpService
    {
        Task<bool> IsAuthAsync();
    }
}