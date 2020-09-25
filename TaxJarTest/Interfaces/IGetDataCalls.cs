using System.Threading.Tasks;

namespace TaxJarTest.Interfaces
{
    public interface IGetDataCalls
    {
        Task<string> GetDataFromApi();
    }
}