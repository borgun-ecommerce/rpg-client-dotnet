using System.Threading.Tasks;

namespace BorgunPayment.Model
{
    public interface ITokenMultiAPI
    {
        Task<TokenMultiResponse> CreateAsync(TokenMultiRequest req);

        Task<TokenMultiResponse> DisableAsync(string token);

        Task<TokenMultiResponse> GetAsync(string token);

    }
}