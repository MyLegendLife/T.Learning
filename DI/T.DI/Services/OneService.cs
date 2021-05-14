using System.Threading.Tasks;

namespace T.DI.Services
{
    public class OneService : IOneService
    {
        public async Task<string> GetOne()
        {
            return await Task.FromResult("1");
        }

        public async Task<string> GetTwo()
        {
            return await Task.FromResult("2");
        }
    }
}