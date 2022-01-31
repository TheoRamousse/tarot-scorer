using System.Collections.Generic;
using System.Threading.Tasks;
using APIGateway.Model;

namespace APIGateway.Pages
{
    public partial class TablePage
    {
        public async Task<List<Data>> FetchDataWithNumberOfElementsAsync(int numberOfElements, int page)
        {
            List<Data> result = new List<Data>();
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
            result.Add(data1);
            result.Add(data2);
            result.Add(data3);

            //await Task.Delay(2000);

            return result;
        }
    }
}
