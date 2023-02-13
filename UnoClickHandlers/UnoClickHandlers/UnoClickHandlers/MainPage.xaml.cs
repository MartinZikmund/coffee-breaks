using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace UnoClickHandlers;

public sealed partial class MainPage : Page
{
    private MainViewModel _viewModel;

    public MainPage() => InitializeComponent();

    internal MainViewModel ViewModel => _viewModel ??= new MainViewModel(XamlRoot);

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        var contentDialog = new ContentDialog()
        {
            XamlRoot = XamlRoot,
            Title = "Button click sample",
            Content = "Button clicked",
            PrimaryButtonText = "OK",
        };

        await contentDialog.ShowAsync();
    }
}
