using System.Threading.Tasks;

namespace BorgunPayment.Model
{
    public interface ITokenMultiAPI
    {
        Task<TokenMultiResponse> CreateAsync(TokenMultiRequest req);

        Task<TokenMultiResponse> DeleteAsync(string token);

        Task<TokenMultiResponse> GetAsync(string token);

    }
}