using APIGateway.Entity;
using APIGateway.Model;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Pages
{
    public partial class DataDetail
    {
        [Parameter]
        public long Id { get; set; }

        [Inject]
        public IDataService DataManager { get; set; }

        private PlayerFullEntity CurrentPlayer;

        protected override async Task OnParametersSetAsync()
        {
            CurrentPlayer = await DataManager.GetPlayerById(Id);
        }

        
    }
}
