namespace ShiftSchedulerMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        // Navigationsmetode
        private async void OnNavigateButtonClicked(object sender, EventArgs e)
        {
            // Naviger til ShiftPage
            await Navigation.PushAsync(new ShiftPage());
        }
    }
}
