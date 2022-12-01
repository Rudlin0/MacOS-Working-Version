namespace UWOsh_InteractiveMap
{
    public partial class MainPage : ContentPage
    {
     

        public MainPage()
        {
            InitializeComponent();
        }

        async void OnTextChanged(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("ListAllPlants"); // on text changed it 
        }

    }
}