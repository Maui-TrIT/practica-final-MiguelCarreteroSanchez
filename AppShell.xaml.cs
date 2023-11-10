namespace ShopApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            var uri = new Uri("http://www.google.com");
            await Browser.Default.OpenAsync(uri,BrowserLaunchMode.SystemPreferred);
        }
    }
}