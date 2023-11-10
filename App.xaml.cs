using ShopApp.DataAccess;
using ShopApp.Views;

namespace ShopApp
{
    public partial class App : Application
    {
        public App(LoginPage loginPage, ShopOutDbContext context)
        {
            InitializeComponent();

            MainPage = new AppShell();

            //esto es más correcto tenerlo en el AppShell. Es quien se encarga de la navegación
            //context.Database.EnsureCreated();

            //var accessToken = Preferences.Get("accesstoken", string.Empty);
            //if (string.IsNullOrEmpty(accessToken))
            //{
            //    MainPage = loginPage;    
            //}
            //else 
            //{
            //    MainPage = new AppShell();
            //}
        }
    }
}