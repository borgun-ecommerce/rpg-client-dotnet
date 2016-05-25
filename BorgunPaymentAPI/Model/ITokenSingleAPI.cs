using System.Threading.Tasks;

namespace BorgunPayment.Model
{
    public interface ITokenSingleAPI
    {
        Task<TokenSingleResponse> CreateAsync(TokenSingleRequest req);

        Task<TokenSingleResponse> DeleteAsync(string token);

        Task<TokenSingleResponse> GetAsync(string token);
    }
}