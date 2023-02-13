#nullable enable
using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace UnoClickHandlers;

internal class MainViewModel
{
    private readonly XamlRoot _xamlRoot;
    private ICommand? _clickCommand = null;

    public MainViewModel(XamlRoot xamlRoot)
    {
        _xamlRoot = xamlRoot;
    }

    public ICommand ClickCommand => _clickCommand ??= new AsyncRelayCommand(ShowDialogAsync);

    public async void TwoArgClick(object sender, object e) => await ShowDialogAsync();

    public async void OneArgClick(object sender) => await ShowDialogAsync();
    
    public async Task NoArgClick() => await ShowDialogAsync();

    private async Task ShowDialogAsync()
    {
        await Task.Delay(1000);

        var dialog = new ContentDialog()
        {
            XamlRoot = _xamlRoot,
            Title = "Button click sample",
            Content = "Button clicked",
            PrimaryButtonText = "OK",
        };

        await dialog.ShowAsync();
    }
}
