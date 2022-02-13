using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIGateway.Model;
using APIGateway.Entity;

namespace APIGateway.Shared
{
    public partial class CustomChartStatistic
    {
        [Inject]
        private IJSRuntime Js { get; set; }

        [Parameter]
        public String Title { get; set; }

        [Parameter]
        public String Subtitle { get; set; }

        [Parameter]
        public String YAxisName { get; set; }

        [Parameter]
        public String XAxisName { get; set; }

        [Parameter]
        public GameEntity[] Data { get; set; }

        private static GameEntity[] StaticData;

        private GameEntity SelectedGame = null;

        private static Func<uint, Task> MyNonStaticMethod;


        private double[] YData;
        private string[] XData;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                MyNonStaticMethod = ChangeSelectedGame;
                StaticData = Data;
                YData = new double[Data.Length];
                XData = new string[Data.Length];

                int i = 0;
                foreach (var game in Data.OrderByDescending(game => game.Date).Reverse())
                {
                    YData[i] = game.TakerPoints;
                    XData[i] = game.Date.ToString();
                    i++;
                }
                await Js.InvokeVoidAsync("computeChart", Title, Subtitle, XAxisName, YAxisName, YData, XData);
            }
        }


        [JSInvokable("OnPointSelected")]
        public static async Task AddTextToTextHistory(uint index)
        {
            await MyNonStaticMethod.Invoke(index);

        }

        private async Task ChangeSelectedGame(uint index)
        {
            SelectedGame = StaticData[index];
            Console.WriteLine(SelectedGame.Id);
            StateHasChanged();

        }
    }
}
