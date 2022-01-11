using System;
using System.Threading.Tasks;
using Model;
using TarotDB2Model;

namespace ManagerDBTests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using(TarotDBManager dbManager = new TarotDBManager())
            {
                Manager manager = new Manager(dbManager);
                int count = 3;
                int nbPlayers = await manager.GetNbPlayers();
                int nbPages = nbPlayers/count + (nbPlayers%count > 0 ? 1 : 0);
                for(int numPage = 0; numPage<nbPages; numPage++)
                {
                    Console.WriteLine($"page {numPage+1}/{nbPages}");
                    foreach(var player in await manager.GetPlayers(numPage, count))
                    {
                        Console.WriteLine(player);
                    }
                }
            }

            //using(TarotDBManager dbManager = new TarotDBManager())
            //{
            //    Manager manager = new Manager(dbManager);
            //    //await manager.CreatePlayer("Charlie", "Parker", "Bird", null);
            //    Player p = await manager.GetPlayerById(1);
            //    if(p != null)
            //        await manager.DeletePlayer(p);
            //}

            //using(TarotDBManager dbManager = new TarotDBManager())
            //{
            //    Manager manager = new Manager(dbManager);
            //    int count = 3;
            //    int nbPlayers = await manager.GetNbPlayers();
            //    int nbPages = nbPlayers/count + (nbPlayers%count > 0 ? 1 : 0);
            //    for(int numPage = 0; numPage<nbPages; numPage++)
            //    {
            //        Console.WriteLine($"page {numPage+1}/{nbPages}");
            //        foreach(var player in await manager.GetPlayers(numPage, count))
            //        {
            //            Console.WriteLine(player);
            //        }
            //    }
            //}

            using(TarotDBManager dbManager = new TarotDBManager())
            {
                Manager manager = new Manager(dbManager);
                foreach(var session in await dbManager.GetSessionsByName("or", 0, 3))
                {
                    Console.WriteLine(session);
                }
            }
        }
    }
}
