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

        public async Task DeletePlayer(int id)
        {

            DataManager.DeletePlayer(id);
        }

        public async Task<List<Data>> FetchDataWithNumberOfElementsAsync(int numberOfElements, int page)
        {


            List<Data> result = new List<Data>();

            foreach (var element in (await DataManager.GetPlayers(numberOfElements, page))){
                Data d = new Data();
                d.Add("Id", element.Id);
                d.Add("FirsName", element.FirstName);
                d.Add("LastName", element.LastName);
                d.Add("NickName", element.NickName);
                result.Add(d);
            }


            return result;

        }

        public async Task<int> GetTotalNumberOfData()
        {
            return await DataManager.GetNumberOfData();
        }
    }
}
