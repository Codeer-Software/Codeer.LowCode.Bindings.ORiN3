using Codeer.LowCode.Blazor.RequestInterfaces;
using Microsoft.AspNetCore.Components;
using ORiN3App.Client.Shared.Services;

namespace ORiN3App.Client
{
    public class NavigationService : NavigationServiceBase
    {
        public NavigationService(NavigationManager nav, IAppInfoService app) : base(nav, app) { }
        public override bool CanLogout => false;
        public override async Task Logout() => await Task.CompletedTask;
    }
}
