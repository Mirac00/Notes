using ikvm.runtime;

using Microsoft.AspNetCore.Mvc.Testing;

using System.Windows;

namespace WpfAppWithApi
{
    public partial class App : Application
    {
        private WebApplicationFactory<Startup> _factory;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _factory = new WebApplicationFactory<Startup>();
            var client = _factory.CreateClient();
            // Użyj klienta do wykonywania żądań do API
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            _factory.Dispose();
        }
    }
}
