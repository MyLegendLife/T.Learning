using System.Threading.Tasks;

namespace T.DI.Services
{
    public interface ITwoService
    {
        Task<string> GetOne();

        Task<string> GetTwo();
    }
}