using APIGateway.Entity;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
        private HttpClient Http { get; set; }

        private List<PlayerFullEntity> PlayerFullEntities { get; set; } = new List<PlayerFullEntity>();
        private bool IsDataInitialized = false;

        public StubService(HttpClient Http)
        {
            this.Http = Http;
        }

        private async Task initData()
        {
            if (!IsDataInitialized)
            {
                IsDataInitialized = true;
                var jsonDeserializePlayer = await Http.GetFromJsonAsync<PlayerEntity[]>("sample-data/player.json");
                var jsonDeserializeGame = await Http.GetFromJsonAsync<GameEntity[]>("sample-data/game.json");


                int i = 0;
                foreach (var element in jsonDeserializePlayer)
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
                    PlayerFullEntities.Add(playerFullEntity);
                    i++;
                }


            }
        }

        public async Task<PlayerFullEntity> GetPlayerById(long id)
        {
            await initData();

            foreach (var element in PlayerFullEntities)
            {
                if(element.Id == id)
                {
                    return element;
                }
            }

            return null;
        }

        public async Task<PlayerFullEntity[]> GetPlayers(int numberOfElements, int page)
        {
            await initData();

            
            return PlayerFullEntities.Skip(page * numberOfElements).Take(numberOfElements).ToArray();
        }

        public async Task UpdatePlayer(PlayerFullEntity p)
        {
            PlayerFullEntity playerToModify = PlayerFullEntities.Find(player => player.Id == p.Id);
            playerToModify.FirstName = p.FirstName;
            playerToModify.LastName = p.LastName;
            playerToModify.NickName = p.NickName;
        }

        public async Task DeletePlayer(int id)
        {
            await initData();

            PlayerFullEntities.RemoveAll(player => player.Id == id);
        }

        public async Task AddPlayer(PlayerFullEntity p)
        {
            await initData();

            p.Id = GetNewId();
            PlayerFullEntities.Add(p);
        }

        private int GetNewId()
        {

            int maxId = 0;

            PlayerFullEntities.ForEach(player =>
            {
                if (player.Id > maxId)
                    maxId = player.Id;
            });

            return maxId+1;
        }

        public async Task<int> GetNumberOfData()
        {
            return PlayerFullEntities.Count();
        }
    }
}
