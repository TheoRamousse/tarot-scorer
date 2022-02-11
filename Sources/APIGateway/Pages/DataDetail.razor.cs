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
        private string MinimumDate;

        protected override async Task OnInitializedAsync()
        {
            CurrentPlayer = await DataManager.GetPlayerById(Id);

            MinimumDate = CurrentPlayer.Games[0].Date;
            foreach (var element in CurrentPlayer.Games)
            {
                if(String.CompareOrdinal(element.Date,MinimumDate) <0)
                {
                    MinimumDate = element.Date;
                }
            }
        }
    }
}
