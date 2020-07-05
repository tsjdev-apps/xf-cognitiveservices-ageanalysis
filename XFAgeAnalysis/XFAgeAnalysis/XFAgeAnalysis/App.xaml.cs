using Xamarin.Forms;
using XFAgeAnalysis.Init;

namespace XFAgeAnalysis
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Bootstrapper.RegisterDependencies();

            MainPage = new NavigationPage(new MainPage()) { BarBackgroundColor = Color.FromHex("#CC0000"), BarTextColor = Color.FromHex("#FFFFFF") };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
