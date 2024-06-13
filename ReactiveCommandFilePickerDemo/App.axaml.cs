namespace ReactiveCommandFilePickerDemo;

using System;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using ReactiveCommandFilePickerDemo.ViewModels;
using ReactiveCommandFilePickerDemo.Views;

using ReactiveUI;


public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var startWindowVM = new StartWindowViewModel();
            var startWindow = new StartWindow
            {
                DataContext = startWindowVM
            };

            startWindow.ViewModel!.ReactiveOpenFileCommand
                .Subscribe(result =>
                {
                    desktop.MainWindow = new MainWindow
                    {
                        DataContext = new MainViewModel(result),
                    };
                    desktop.MainWindow.Show();
                    startWindow.Close();
                });

            startWindowVM.ObservableForProperty(x => x.FilePath)
                    .Subscribe(result =>
                    {
                        desktop.MainWindow = new MainWindow
                        {
                            DataContext = new MainViewModel(startWindowVM.FilePath),
                        };
                        desktop.MainWindow.Show();
                        startWindow.Close();
                    });

            desktop.MainWindow = startWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }
}
