using APIGateway.Entity;
using APIGateway.Model;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace APIGateway.Pages
{
    public partial class ModifierJoueur
    {

        [Parameter]
        public long Id { get; set; }

        [Inject]
        public IDataService DataManager { get; set; }

        [Inject]
        NavigationManager NavManager { get; set; } 

        private PlayerFullEntity CurrentPlayer;

        protected override async Task OnParametersSetAsync()
        {
            if (Id != 0)
            {
                CurrentPlayer = await DataManager.GetPlayerById(Id);
            }
            else
            {
                CurrentPlayer = new PlayerFullEntity();
            }

        }

        private async Task SavePlayer()
        {
            if (CurrentPlayer != null)
            {
                if (CurrentPlayer.Id != 0)
                {
                    await DataManager.UpdatePlayer(CurrentPlayer);
                }
                else
                {
                    await DataManager.AddPlayer(CurrentPlayer);
                }
            }

            NavManager.NavigateTo("/");
        }
    }
}
