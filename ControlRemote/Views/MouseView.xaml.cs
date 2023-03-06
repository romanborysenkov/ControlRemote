using ControlRemote.VIewModels;
using System.IO;
using System.Net.Sockets;
//using ThreadNetwork;

namespace ControlRemote.Views;

public partial class MouseView : ContentView
{
    MouseViewModel _viewModel;
    protected TcpClient client;
    NetworkStream stream;

    public MouseView()
	{
		InitializeComponent();

        client = new TcpClient();
        client.Connect(Preferences.Get("DefaultIP", ""), 5555);

       
        BindingContext = _viewModel = new MouseViewModel(client);
        stream = client.GetStream();
    }


    void OnButtonReleased(object sender, EventArgs args)
    {
        _viewModel.stopwatch.Stop();
        _viewModel.animationInProgress = false;

    }

    [Obsolete]
    private void Button_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        switch (btn.Text)
        {
            case "Up":
                _viewModel.SendCommand("{UP}");
                break;
            case "Down":
                _viewModel.SendCommand("{DOWN}");
                break;
            case "Left":
                _viewModel.SendCommand("{LEFT}");
                break;
            case "Right":
                _viewModel.SendCommand("{RIGHT}");
                break;
            case "Enter":
                _viewModel.SendCommand("{ENTER}");
                break;
        }

    }

}