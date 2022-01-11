using System;
using System.Threading.Tasks;
using Model;
using StubLib;

namespace ManagerStubTests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Manager manager = new Manager(new Stub());
            foreach(var player in await manager.FindPlayerByName("ro", 0, 10))
            {
                Console.WriteLine(player);
            }
            await manager.CreatePlayer("Sonny", "Rollins", null, null);

            foreach(var player in await manager.FindPlayerByName("ro", 0, 10))
            {
                Console.WriteLine(player);
            }

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

            int countSessions = 2;
            int nbSessions = await manager.GetNbSessions();
            int nbPagesofSessions = nbSessions/countSessions + (nbSessions%countSessions > 0 ? 1 : 0);
            for(int numPage = 0; numPage<nbPagesofSessions; numPage++)
            {
                Console.WriteLine($"page {numPage+1}/{nbPages}");
                foreach(var session in await manager.GetSessions(numPage, count))
                {
                    Console.WriteLine(session);
                }
            }
        }
    }
}
