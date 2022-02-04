using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;
using TarotDB;
using Utils;

[assembly:InternalsVisibleTo("DataManager_UT")]
namespace TarotDB2Model
{
    public class TarotDBManager : IDataManager, IDisposable
    {
        private readonly DbContext _dbContext;

        private UnitOfWork UnitOfWork { get; set; }

        public TarotDBManager() : this(new TarotContext())
        {
        }

        internal TarotDBManager(TarotContext context)
        {
            _dbContext = context;
            UnitOfWork = new UnitOfWork(_dbContext);
            Mapper.Reset();
        }

        public async Task<bool> AddPlayer(Player player)
        {
            if(player.Id != 0) return false;

            PlayerEntity pe = player.ToEntity();
            PlayerEntity found = await UnitOfWork.Repository<PlayerEntity>().FindById(pe.Id);
            if(found != null) return false;

            PlayerEntity result = await UnitOfWork.Repository<PlayerEntity>().Insert(pe);
            if(result == null)
            {
                await UnitOfWork.RejectChangesAsync();
                return false;
            }
            await UnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePlayer(Player player)
        {
            return await DeletePlayer(player.Id);
        }

        public async Task<bool> DeletePlayer(long id)
        {
            var janeDoe = await UnitOfWork.Repository<PlayerEntity>().FindById((long)-1);

            foreach(PlayerSessionEntity pse in UnitOfWork.Repository<PlayerSessionEntity>().Set
                                                .Include(pse => pse.Player)
                                                .Include(pse => pse.Session)
                                                .Where(pse => pse.Player.Id == id))
            {
                var temp = new PlayerSessionEntity();
                _dbContext.Entry(temp).Property("PlayerId").CurrentValue = (long)-1;
                _dbContext.Entry(temp).Property("SessionId").CurrentValue = pse.Session.Id;
                await UnitOfWork.Repository<PlayerSessionEntity>().Insert(temp);
            }
            foreach(PlayerBiddingEntity pbe in UnitOfWork.Repository<PlayerBiddingEntity>().Set
                                                .Include(pbe => pbe.Player)
                                                .Include(pbe => pbe.Game)
                                                .Where(pbe => pbe.Player.Id == id))
            {
                var temp = new PlayerBiddingEntity() { Bidding = pbe.Bidding };
                _dbContext.Entry(temp).Property("PlayerId").CurrentValue = (long)-1;
                _dbContext.Entry(temp).Property("GameId").CurrentValue = pbe.Game.Id;
                await UnitOfWork.Repository<PlayerBiddingEntity>().Insert(temp);
            }
            bool result = await UnitOfWork.Repository<PlayerEntity>().Delete(id);
            
            if(!result)
            {
                await UnitOfWork.RejectChangesAsync();
                return false;
            }
            await UnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetNbPlayers()
            => await UnitOfWork.Repository<PlayerEntity>().Count() - 1;

        public async Task<Player> GetPlayerById(long id)
            => (await UnitOfWork.Repository<PlayerEntity>().FindById(id))?.ToModel();

        //public async Task<IEnumerable<Player>> GetPlayers(int index, int count)
        //    => (await UnitOfWork.Repository<PlayerEntity>().GetItems(index, count)).ToModels();

        public async Task<IEnumerable<Player>> GetPlayers(int index, int count)
        {
            return await Task.Run(() => _dbContext.Set<PlayerEntity>()
                                            .OrderBy(p => p.Id)
                                            .Skip(1 + index*count)
                                            .Take(count)
                                            .ToModels());
        }

        public async Task<IEnumerable<Player>> GetPlayersByName(string substring, int index, int count)
            => await Task.Run(() => UnitOfWork.Repository<PlayerEntity>().Set
                .Where(pe => pe.Id != -1 && ((pe.FirstName != null && pe.FirstName.ToUpper().Contains(substring.ToUpper()))
                                || pe.LastName != null && pe.LastName.ToUpper().Contains(substring.ToUpper())
                                || pe.NickName != null && pe.NickName.ToUpper().Contains(substring.ToUpper()))
                )
                .Skip(index*count).Take(count)
                .ToModels());

        public async Task<bool> UpdatePlayer(long id, Player player)
        {
            PlayerEntity pe = await UnitOfWork.Repository<PlayerEntity>().FindById(id);
            if(pe == null || player == null)
                return false;
            PlayerEntity pe2 = player.ToEntity();
            foreach(var property in typeof(PlayerEntity).GetProperties()
                                                        .Where(pi => pi.CanWrite
                                                                && pi.Name != nameof(PlayerEntity.Id)))
            {
                property.SetValue(pe, property.GetValue(pe2));
            }
                                                        
            if(await UnitOfWork.Repository<PlayerEntity>().Update(pe) == null)
            {
                await UnitOfWork.RejectChangesAsync();
                return false;
            }
            await UnitOfWork.SaveChangesAsync();
            return true;
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
            _dbContext?.Dispose();
        }

        public async Task<int> GetNbSessions()
            => await UnitOfWork.Repository<SessionEntity>().Count();

        public async Task<IEnumerable<Session>> GetSessions(int index, int count)
            => await Task.Run(() => UnitOfWork.Repository<SessionEntity>()
            .Set.Include(se => se.Players).ThenInclude(p => p.Player)
            .Skip(index*count).Take(count).ToModels());

        public async Task<Session> GetSessionById(long id)
            => (await UnitOfWork.Repository<SessionEntity>().Set.Include(se => se.Players).ThenInclude(p => p.Player)
                                    .SingleOrDefaultAsync(se => se.Id == id))?.ToModel();


        public async Task<IEnumerable<Session>> GetSessionsByName(string substring, int index, int count)
            => await Task.Run(() => UnitOfWork.Repository<SessionEntity>().Set.Include(se => se.Players).ThenInclude(pse => pse.Player)
                .Where(se => se.Name.Contains(substring)
                            || se.Players.Where(pse => (pse.Player.LastName != null && pse.Player.LastName.Contains(substring))
                                                        || (pse.Player.FirstName != null && pse.Player.FirstName.Contains(substring))
                                                        || (pse.Player.NickName != null && pse.Player.NickName.Contains(substring)))
                                         .Count() > 0)
                .Skip(index*count).Take(count)
                .ToModels());

        public async Task<IEnumerable<Session>> GetSessionsByPlayer(Player player, int index, int count)
        {
            if (player == null) return new Session[0];
            return await Task.Run(() =>
                    UnitOfWork.Repository<SessionEntity>().Set.Include(se => se.Players).ThenInclude(pse => pse.Player)
                        .Where(se => se.Players.Select(pse => pse.Player.Id).Contains(player.Id))
                        .Skip(index*count).Take(count)
                        .ToModels());
        }

        public async Task<bool> AddSession(Session session)
        {
            if(session.Id != 0) return false;
            SessionEntity se = session.ToEntity();
            SessionEntity found = await UnitOfWork.Repository<SessionEntity>().FindById(se.Id);
            if(found != null) return false;

            SessionEntity sessionEntity = session.ToEntity();
            var result = await UnitOfWork.Repository<SessionEntity>().Insert(sessionEntity);

            foreach(var p in sessionEntity.Players.Select(pse => pse.Player))
            {
                if(await UnitOfWork.Repository<PlayerEntity>().FindById(p.Id) != null)
                {
                    _dbContext.Entry(p).State = EntityState.Unchanged;
                }
            }

            if(result == null)
            {
                await UnitOfWork.RejectChangesAsync();
                return false;
            }

            await UnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSession(Session session)
            => await DeleteSession(session.Id);

        public async Task<bool> DeleteSession(long id)
        {
            bool result = await UnitOfWork.Repository<SessionEntity>().Delete(id);
            
            if(!result)
            {
                await UnitOfWork.RejectChangesAsync();
                return false;
            }
            await UnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateSession(long id, Session session)
        {

            SessionEntity se = await UnitOfWork.Repository<SessionEntity>().Set.Include(s => s.Players)
                                                                               .ThenInclude(ps => ps.Player)
                                                                               .SingleOrDefaultAsync(s => s.Id == id);
            if(se == null)
                return false;
            SessionEntity se2 = session.ToEntity();
            se2.Id = id;

            var temp = se.Players.Except(se2.Players, PlayerSessionEntity.EqualityComparer).Reverse().ToList();
            var temp2 = se2.Players.Except(se.Players, PlayerSessionEntity.EqualityComparer).ToList();

            foreach(var pse in se.Players)
            {
                _dbContext.Entry<PlayerSessionEntity>(pse).State = EntityState.Detached;
            }

            foreach(var pse in temp)
            {
                _dbContext.Entry<PlayerSessionEntity>(pse).State = EntityState.Deleted;
            }
            foreach(var pse in temp2)
            {
                _dbContext.Entry<PlayerSessionEntity>(pse).State = EntityState.Added;
            }

            foreach (var property in typeof(SessionEntity).GetProperties()
                                                        .Where(pi => pi.CanWrite
                                                                && pi.Name != nameof(SessionEntity.Id)))
            {
                property.SetValue(se, property.GetValue(se2));
            }

            _dbContext.Entry<SessionEntity>(se).State = EntityState.Modified;

            await UnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetNbGames()
            => await UnitOfWork.Repository<GameEntity>().Count();

        public async Task<IEnumerable<Game>> GetGames(int index, int count)
            => await Task.Run(() => UnitOfWork.Repository<GameEntity>()
                                        .Set.Include(g => g.Biddings).ThenInclude(b => b.Player)
                                        .Skip(index*count).Take(count)
                                        .ToModels());

        public async Task<Game> GetGameById(long id)
            => (await UnitOfWork.Repository<GameEntity>()
                            .Set.Include(g => g.Biddings).ThenInclude(b => b.Player)
                            .SingleOrDefaultAsync(ge => ge.Id == id))?.ToModel();

        public async Task<IEnumerable<Game>> GetGamesByDate(DateTime? startTime, DateTime? endTime, int index, int count)
        {
            DateTime start = startTime.HasValue ? startTime.Value : DateTime.MinValue;
            DateTime end = endTime.HasValue ? endTime.Value : DateTime.MaxValue;

            return await Task.Run(() => UnitOfWork.Repository<GameEntity>().Set
                                            .Include(g => g.Biddings).ThenInclude(b => b.Player)
                                            .Where(ge => ge.DateTime >= start && ge.DateTime <= end)
                .Skip(index*count).Take(count)
                .ToModels());
        }

        public async Task<IEnumerable<Game>> GetGamesBySession(Session session, int index, int count)
        {
            DateTime start = session.StartingTime.HasValue ? session.StartingTime.Value : DateTime.MinValue;
            DateTime end = session.EndingTime.HasValue ? session.EndingTime.Value : DateTime.MaxValue;
            var sessionPlayersIds = session.Players.Select(p => p.Id).OrderBy(i => i).ToList();
            return await Task.Run(() => UnitOfWork.Repository<GameEntity>().Set
                                            .Include(g => g.Biddings).ThenInclude(b => b.Player)
                                            .Where(ge => ge.DateTime >= start && ge.DateTime <= end
                                                && ge.Biddings.Count == sessionPlayersIds.Count
                                                && ge.Biddings.Select(b => b.Player.Id).All(id => sessionPlayersIds.Contains(id)))
                .Skip(index*count).Take(count)
                .ToModels());
        }

        public async Task<IEnumerable<Game>> GetGamesByPlayer(Player player, int index, int count)
            => await Task.Run(() => UnitOfWork.Repository<GameEntity>().Set.Include(ge => ge.Biddings).ThenInclude(b => b.Player)
                        .Where(ge => ge.Biddings.Select(b => b.Player.Id).Contains(player.Id))
                        .Skip(index*count).Take(count)
                        .ToModels());

        public async Task<IEnumerable<Game>> GetGamesByPlayers(IEnumerable<Player> players, int index, int count)
        {
            var listPlayers = players.Select(p => p.Id).ToList();
            return await Task.Run(() => UnitOfWork.Repository<GameEntity>().Set.Include(ge => ge.Biddings).ThenInclude(b => b.Player)
                        .Where(ge => ge.Biddings.Count >= listPlayers.Count
                                       && ge.Biddings.Select(b => b.Player.Id).Count(id => listPlayers.Contains(id)) == listPlayers.Count)
                        .Skip(index*count).Take(count)
                        .ToModels());
        }

        public async Task<bool> AddGame(Game game)
        {
            if(game.Id != 0) return false;
            GameEntity ge = game.ToEntity();
            GameEntity found = await UnitOfWork.Repository<GameEntity>().FindById(ge.Id);
            if(found != null) return false;

            GameEntity gameEntity = game.ToEntity();
            var result = await UnitOfWork.Repository<GameEntity>().Insert(gameEntity);

            foreach(var p in gameEntity.Biddings.Select(kvp => kvp.Player))
            {
                if(await UnitOfWork.Repository<PlayerEntity>().FindById(p.Id) != null)
                {
                    _dbContext.Entry(p).State = EntityState.Unchanged;
                }
            }

            if(result == null)
            {
                await UnitOfWork.RejectChangesAsync();
                return false;
            }

            await UnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteGame(Game game)
            => await DeleteGame(game.Id);

        public async Task<bool> DeleteGame(long id)
        {
            bool result = await UnitOfWork.Repository<GameEntity>().Delete(id);
            
            if(!result)
            {
                await UnitOfWork.RejectChangesAsync();
                return false;
            }
            await UnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<Game> UpdateGame(long id, Game game)
        {
            GameEntity ge = await UnitOfWork.Repository<GameEntity>().Set.Include(g => g.Biddings)
                                                                               .ThenInclude(b => b.Player)
                                                                               .SingleOrDefaultAsync(g => g.Id == id);
            if (ge == null)
                return null;
            GameEntity ge2 = game.ToEntity();

            var temp = ge.Biddings.Except(ge2.Biddings, PlayerBiddingEntity.Comparer).ToList();
            var temp2 = ge2.Biddings.Except(ge.Biddings, PlayerBiddingEntity.Comparer).ToList();

            foreach(var pbe in ge.Biddings)
            {
                _dbContext.Entry<PlayerBiddingEntity>(pbe).State = EntityState.Detached;
            }

            foreach(var pbe in temp)
            {
                _dbContext.Entry<PlayerBiddingEntity>(pbe).State = EntityState.Deleted;
            }
            foreach(var pbe in temp2)
            {
                _dbContext.Entry<PlayerBiddingEntity>(pbe).State = EntityState.Added;
            }

            foreach(var property in typeof(GameEntity).GetProperties()
                                                        .Where(pi => pi.CanWrite
                                                                && pi.Name != nameof(GameEntity.Id)))
            {
                property.SetValue(ge, property.GetValue(ge2));
            }

            var result = await UnitOfWork.Repository<GameEntity>().Update(ge);

            if (result == null)
            {
                await UnitOfWork.RejectChangesAsync();
                return null;
            }
            await UnitOfWork.SaveChangesAsync();
            return result.ToModel();
        }
    }
}
