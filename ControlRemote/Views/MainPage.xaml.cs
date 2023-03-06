using System.Net;
using System.Net.Sockets;
using ControlRemote.VIewModels;


namespace ControlRemote.Views;

public partial class MainPage : ContentPage
{
	MainViewModel viewModel;
	
	public MainPage(TcpClient client)
	{
		InitializeComponent();

      BindingContext= viewModel = new MainViewModel(client);
    }


}

