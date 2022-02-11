using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorClient.Pages
{
    public partial class EditData
    {
        [Parameter]
        public long Id { get; set; }
    }
}
