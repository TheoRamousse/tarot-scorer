using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using APIGateway.Entity;
using APIGateway.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace APIGateway.Pages
{
    public partial class Index
    {
        [Inject]
        public IDataService DataManager { get; set; }

        public async Task<List<Data>> FetchDataWithNumberOfElementsAsync(int numberOfElements, int page)
        {


            /*List<Data> result = new List<Data>();
            
            var jsonDeserializePlayer = await Http.GetFromJsonAsync<PlayerEntity[]>("sample-data/player.json");
            var jsonDeserializeGame = await Http.GetFromJsonAsync<GameEntity[]>("sample-data/game.json");
            client.InvokeVoidAsync("console.log", jsonDeserializePlayer);
            foreach (var element in jsonDeserializePlayer)
            {
                List<GameEntity> lesGames = new List<GameEntity>();
                Data d = new Data();
                d.Add("Id", element.Id);
                d.Add("FirsName", element.FirstName);
                d.Add("LastName", element.LastName);
                d.Add("NickName", element.NickName);
                foreach (var e in element.ListeDesParties)
                {
                    foreach (var elem in jsonDeserializeGame)
                    {
                        if(e.Id == elem.Id)
                            lesGames.Add(elem);
                    }
                }
                d.Add("_ignoreThisPartOfDataOrConsequences", lesGames);
                result.Add(d);
            }
            return result.Skip(page*numberOfElements).Take(numberOfElements).ToList();*/

            List<Data> result = new List<Data>();

            foreach (var element in (await DataManager.GetPlayers(numberOfElements, page))){
                Data d = new Data();
                d.Add("Id", element.Id);
                d.Add("FirsName", element.FirstName);
                d.Add("LastName", element.LastName);
                d.Add("NickName", element.NickName);
            }


            return result;



            /*List<Data> result = new List<Data>();
            Data data1 = new Data();
            data1.Add("nan", true);
            data1.Add("ok", "Toto");
            data1.Add("Id", 2);
            Data data2 = new Data();
            data2.Add("nan", false);
            data2.Add("ok", "Titi");
            data2.Add("Id", 69);
            Data data3 = new Data();
            data3.Add("nan", true);
            data3.Add("ok", "Tutu");
            data3.Add("Id", 96);
            Data data4 = new Data();
            data4.Add("nan", true);
            data4.Add("ok", "dijdij");
            data4.Add("Id", 966);

            Data data5 = new Data();
            data5.Add("nan", false);
            data5.Add("ok", "diiuhhujdij");
            data5.Add("Id", 66);

            Data data6 = new Data();
            data6.Add("nan", true);
            data6.Add("ok", "dijdij");
            data6.Add("Id", 922222);

            Data data7 = new Data();
            data7.Add("nan", true);
            data7.Add("ok", "dijdij");
            data7.Add("Id", 911111);

            Data data8 = new Data();
            data8.Add("nan", true);
            data8.Add("ok", "dijdij");
            data8.Add("Id", 9856846);

            Data data9 = new Data();
            data9.Add("nan", true);
            data9.Add("ok", "dijdij");
            data9.Add("Id", 9662479);

            Data data10 = new Data();
            data10.Add("nan", true);
            data10.Add("ok", "dijdij");
            data10.Add("Id", 69785);

            Data data11 = new Data();
            data11.Add("nan", true);
            data11.Add("ok", "dijdij");
            data11.Add("Id", 12114);

            Data data12 = new Data();
            data12.Add("nan", true);
            data12.Add("ok", "dijdij");
            data12.Add("Id", 96789);

            Data data13 = new Data();
            data13.Add("nan", true);
            data13.Add("ok", "dijdij");
            data13.Add("Id", 642);

            Data data14 = new Data();
            data14.Add("nan", true);
            data14.Add("ok", "dijdij");
            data14.Add("Id", 966478);

            Data data15 = new Data();
            data15.Add("nan", true);
            data15.Add("ok", "dijdij");
            data15.Add("Id", 916);

            Data data16 = new Data();
            data16.Add("nan", true);
            data16.Add("ok", "dijdij");
            data16.Add("Id", 9645456);

            Data data17 = new Data();
            data17.Add("nan", true);
            data17.Add("ok", "dijdij");
            data17.Add("Id", 946);

            result.Add(data1);
            result.Add(data2);
            result.Add(data3);
            result.Add(data4);
            result.Add(data5);
            result.Add(data6);
            result.Add(data7);
            result.Add(data8);
            result.Add(data9);
            result.Add(data10);
            result.Add(data11);
            result.Add(data12);
            result.Add(data13);
            result.Add(data14);
            result.Add(data15);
            result.Add(data16);
            result.Add(data17);*/
        }

        public async Task<int> GetTotalNumberOfData()
        {
            return 17;
        }
    }
}
