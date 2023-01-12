using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;

namespace UnoConnectivityCheck;

public sealed partial class MainPage : Page
{
    private readonly ApiClient _apiClient = new();

    public MainPage()
    {
        this.InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;
        UpdateState();
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
        base.OnNavigatedFrom(e);
        NetworkInformation.NetworkStatusChanged -= NetworkInformation_NetworkStatusChanged;
    }

    private void NetworkInformation_NetworkStatusChanged(object sender)
    {
        UpdateState();
    }

    private void UpdateState()
    {
        DispatcherQueue.TryEnqueue(() =>
        {
            var profile = NetworkInformation.GetInternetConnectionProfile();
            var level = profile?.GetNetworkConnectivityLevel() ?? NetworkConnectivityLevel.None;
            RefreshButton.IsEnabled =
                level == NetworkConnectivityLevel.InternetAccess ||
                level == NetworkConnectivityLevel.ConstrainedInternetAccess;
        });
    }

    public async void Refresh()
    {
        TimeTextBlock.Text = await _apiClient.GetDataAsync();
    }
}
