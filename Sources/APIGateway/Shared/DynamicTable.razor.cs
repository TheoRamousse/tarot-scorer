using APIGateway.Model;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIGateway.Shared
{

    public partial class DynamicTable
    {
        [Parameter]
        public int NumberElementsPerPage { get; set; }
        [Parameter]
        public bool IsEditable { get; set; }
        [Parameter]
        public bool IsViewable { get; set; }
        [Parameter]
        public String OnView { get; set; }
        [Parameter]
        public String OnEdit { get; set; }
        [Parameter]
        public bool IsDeletable { get; set; }
        public List<Data> Elements { get; set; } = new List<Data>();
        [Parameter]
        public Func<int, int, Task<List<Data>>> FetchDataWithNumberOfElementsAsync { get; set; }

        [Parameter]
        public Func<Task<int>> GetTotalNumberOfData { get; set; }

        [Parameter]
        public Func<int, Task> Delete { get; set; }



        public decimal NumberOfData { get; set; }
        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; } = 1;

        protected override async Task OnInitializedAsync()
        {
            NumberOfData = await GetTotalNumberOfData();
            NumberOfPages = Decimal.ToInt32(Math.Ceiling(NumberOfData / NumberElementsPerPage))-1;

            await setData();
                   
        }

        private async Task setData()
        {
            var response = await FetchDataWithNumberOfElementsAsync(NumberElementsPerPage, CurrentPage);
            Elements = new List<Data>(response);
        }

        public async Task ChangeCurrentPage(int numPage)
        {
            CurrentPage = numPage;
            await setData();
        }

        public async Task DecrementCurrentPage()
        {
            if(CurrentPage > 1)
            {
                CurrentPage--;
                await setData();
            }
        }

        public async Task IncrementCurrentPage()
        {
            if (CurrentPage < NumberOfPages)
            {
                CurrentPage++;
                await setData();
            }
        }

        public void DeleteLocal(int id)
        {
            Elements.RemoveAll(el => (int)el["Id"] == id);
        }


    }
}
