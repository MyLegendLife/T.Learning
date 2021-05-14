using System.Threading.Tasks;

namespace T.DI.Services
{
    public interface IOneService
    {
        Task<string> GetOne();

        Task<string> GetTwo();
    }
}