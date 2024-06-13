namespace ReactiveCommandFilePickerDemo.ViewModels
{
    using Avalonia;
    using Avalonia.Controls.ApplicationLifetimes;
    using Avalonia.Platform.Storage;

    using ReactiveUI;
    using ReactiveUI.Fody.Helpers;

    using System.Linq;
    using System.Reactive;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class StartWindowViewModel : ReactiveObject
    {
        public ReactiveCommand<Unit, string> ReactiveOpenFileCommand { get; }

        public ICommand ICommandOpenFileCommand { get; }

        [Reactive]
        public string FilePath { get; set; }

        public StartWindowViewModel()
        {
            ReactiveOpenFileCommand = ReactiveCommand.Create(() => this.ShowFileDialogAsyncWithResult().Result);

            ICommandOpenFileCommand = ReactiveCommand.Create(() => this.ShowFileDialogAsyncVoid());
        }

        private async Task<string> ShowFileDialogAsyncWithResult()
        {
            if (Application.Current?.ApplicationLifetime is ClassicDesktopStyleApplicationLifetime lifetime)
            {
                var storageProvider = lifetime.MainWindow?.StorageProvider;
                if (storageProvider != null)
                {
                    var options = new FilePickerOpenOptions() { AllowMultiple = false };
                    var picker = await storageProvider.OpenFilePickerAsync(options); // OpenFilePickerAsync never returns
                    var selectedfile = picker.FirstOrDefault();
                    if (selectedfile != null)
                    {
                        return selectedfile.TryGetLocalPath();
                    }
                }
            }
            return string.Empty;
        }

        private async void ShowFileDialogAsyncVoid()
        {
            if (Application.Current?.ApplicationLifetime is ClassicDesktopStyleApplicationLifetime lifetime)
            {
                var storageProvider = lifetime.MainWindow?.StorageProvider;
                if (storageProvider != null)
                {
                    var options = new FilePickerOpenOptions() { AllowMultiple = false };
                    var picker = await storageProvider.OpenFilePickerAsync(options);
                    var selectedfile = picker.FirstOrDefault();
                    if (selectedfile != null)
                    {
                        this.FilePath = selectedfile.TryGetLocalPath();
                    }
                }
            }
        }
    }
}
