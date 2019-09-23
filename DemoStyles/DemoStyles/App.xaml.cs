using Avalonia;
using Avalonia.Markup.Xaml;

namespace DemoStyles
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
