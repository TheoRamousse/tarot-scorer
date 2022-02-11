using APIGateway.Entity;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace APIGateway.Model.Persistence
{
    public class StubService : IDataService
    {

        [Inject]
        public HttpClient Http { get; set; }

        private PlayerEntity[] jsonDeserializePlayer;
        private GameEntity[] jsonDeserializeGame;

        public StubService()
        {
        }

        private async Task initJson()
        {
            if (jsonDeserializeGame == null || jsonDeserializePlayer == null)
            {
                jsonDeserializePlayer = await Http.GetFromJsonAsync<PlayerEntity[]>("sample-data/player.json");
                jsonDeserializeGame = await Http.GetFromJsonAsync<GameEntity[]>("sample-data/game.json");
            }
        }

        public async Task<PlayerFullEntity> GetPlayerById(long id)
        {
            await initJson();

            foreach (var element in jsonDeserializePlayer)
            {
                if (element.Id == id)
                {
                    List<GameEntity> lesGames = new List<GameEntity>();
                    PlayerFullEntity playerFullEntity = new PlayerFullEntity()
                    {
                        Id = element.Id,
                        FirstName = element.FirstName,
                        LastName = element.LastName,
                        NickName = element.NickName,
                    };
                    foreach (var e in element.ListeDesParties)
                    {
                        foreach (var elem in jsonDeserializeGame)
                        {
                            if (e.Id == elem.Id)
                                lesGames.Add(elem);
                        }
                    }
                    playerFullEntity.Games = lesGames.ToArray();
                    return playerFullEntity;
                }
            }

            return null;
        }

        public async Task<PlayerFullEntity[]> GetPlayers(int numberOfElements, int page)
        {
            PlayerFullEntity[] result = new PlayerFullEntity[numberOfElements];
            await initJson();

            int i = 0;
            foreach (var element in jsonDeserializePlayer.Skip(page * numberOfElements).Take(numberOfElements))
            {
                List<GameEntity> lesGames = new List<GameEntity>();
                PlayerFullEntity playerFullEntity = new PlayerFullEntity()
                {
                    Id=element.Id,
                    FirstName = element.FirstName,
                    LastName = element.LastName,
                    NickName = element.NickName,
                };
                foreach (var e in element.ListeDesParties)
                {
                    foreach (var elem in jsonDeserializeGame)
                    {
                        if (e.Id == elem.Id)
                            lesGames.Add(elem);
                    }
                }
                playerFullEntity.Games = lesGames.ToArray();
                result[i] = playerFullEntity;
                i++;
            }

            return result;
        }

        public Task UpdatePlayer(PlayerFullEntity p)
        {
            throw new NotImplementedException();
        }
    }
}
