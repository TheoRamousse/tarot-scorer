using APIGateway.Entity;
using System.Threading.Tasks;

namespace APIGateway.Model
{
    public interface IDataService
    {
        public Task<PlayerFullEntity> GetPlayerById(long id);
        public Task<PlayerFullEntity[]> GetPlayers(int numberOfElements, int page);

        public Task UpdatePlayer(PlayerFullEntity p);

        public Task DeletePlayer(int id);

        public Task AddPlayer(PlayerFullEntity p);

        public Task<int> GetNumberOfData();
    }
}
