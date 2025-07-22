using JVandSubAccount.ServicesLayer;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace JVandSubAccount.Shared
{
    public abstract class LocalizedComponentBase : ComponentBase
    {
        [Inject] protected LocalizationService Localizer { get; set; } = default!;
        [Parameter] public string Module { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(Module))
            {
                await Localizer.LoadStringsAsync(CultureInfo.CurrentCulture.Name, "common");
                await Localizer.LoadStringsAsync(CultureInfo.CurrentCulture.Name, Module);
            }

            await base.OnInitializedAsync();
        }
    }
}

