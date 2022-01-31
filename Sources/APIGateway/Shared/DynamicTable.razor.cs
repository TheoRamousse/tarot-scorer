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
        public string NumberElementsPerPage { get; set; }
        [Parameter]
        public bool IsEditable { get; set; }
        [Parameter]
        public bool IsViewable { get; set; }
        [Parameter]
        public bool IsDeletable { get; set; }
        public List<Data> Elements { get; set; } = new List<Data>();
        [Parameter]
        public Func<int, int, Task<List<Data>>> FetchDataWithNumberOfElementsAsync { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var response = await FetchDataWithNumberOfElementsAsync(2, 3);
            Elements = new List<Data>(response);       
        }

    }
}
