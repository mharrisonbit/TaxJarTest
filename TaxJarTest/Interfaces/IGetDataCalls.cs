using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TaxJarTest.Interfaces
{
    public interface IGetDataCalls
    {
        Task<string> GetDataFromApi(JObject callData);
        Task<string> GetTaxForOrderFromApi(JObject orderInfo);
    }
}