namespace ShiftSchedulerMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell(); // AppShell indeholder nu LoginPage
        }
    }
}
