using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Model
{
    public class Manager
    {
        IDataManager DataManager { get; set; }

        public Manager(IDataManager dataManager)
        {
            DataManager = dataManager;
        }

        public async Task<int> GetNbPlayers() => await DataManager?.GetNbPlayers();

        public async Task<IEnumerable<Player>> GetPlayers(int index, int count)
        {
            return await DataManager?.GetPlayers(index, count);
        }

        public async Task<IEnumerable<Player>> FindPlayerByName(string substring, int index, int count)
        {
            return await DataManager?.GetPlayersByName(substring, index, count);
        }

        public async Task<bool> CreatePlayer(string firstname, string lastname, string nickname, string imagename)
        {
            return await DataManager?.AddPlayer(new Player(firstname, lastname, nickname, imagename));
        }

        public async Task<Player> GetPlayerById(long id)
        {
            return await DataManager?.GetPlayerById(id);
        }

        public async Task<bool> DeletePlayer(long id)
            => await DataManager?.DeletePlayer(id);

        public async Task<bool> DeletePlayer(Player player)
            => await DataManager?.DeletePlayer(player);

        public async Task<int> GetNbSessions() => await DataManager?.GetNbSessions();

        public async Task<IEnumerable<Session>> GetSessions(int index, int count)
            => await DataManager?.GetSessions(index, count);

        public async Task<IEnumerable<Session>> FindSession(string substring, int index, int count)
            => await DataManager?.GetSessionsByName(substring, index, count);

        public async Task<IEnumerable<Session>> FindSessionWithPlayer(Player player, int index, int count)
            => await DataManager?.GetSessionsByPlayer(player, index, count);

        public async Task<bool> CreateSession(string name, DateTime? startTime, DateTime? endTime, params Player[] players)
            => await DataManager?.AddSession(new Session(name, startTime, endTime, players));

        public async Task<bool> CreateSession(string name, params Player[] players)
            => await DataManager?.AddSession(new Session(name, DateTime.Now, null, players));

        public async Task<bool> CreateSession(string name)
            => await DataManager?.AddSession(new Session(name, DateTime.Now, null));

        public async Task<bool> CreateSession(string name, DateTime? startTime, DateTime? endTime)
            => await DataManager?.AddSession(new Session(name, startTime, endTime));

        public async Task<Session> GetSessionById(long id)
            => await DataManager?.GetSessionById(id);
    }
}
