using System.Threading.Tasks;

namespace AspNetCoreTest201908.Api.Lab05_Service
{
    public interface IHttpService
    {
        Task<bool> IsAuthAsync();
    }
}