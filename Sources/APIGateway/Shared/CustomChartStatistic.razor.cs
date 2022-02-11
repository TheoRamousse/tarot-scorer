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
        public int StartValue { get; set; }

        [Parameter]
        public String XAxisName { get; set; }

        [Parameter]
        public GameEntity[] Data { get; set; }


        public static bool IsDataSelected { get; set; } = false;


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
                await Js.InvokeVoidAsync("computeChart", Title, Subtitle, XAxisName, YAxisName, Data, StartValue);
        }


        [JSInvokable("OnPointSelected")]
        public static async Task AddTextToTextHistory(uint index)
        {
            IsDataSelected = true;
            Console.WriteLine(index);
        }
    }
}
