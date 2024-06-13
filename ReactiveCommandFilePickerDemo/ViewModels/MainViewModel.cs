namespace ReactiveCommandFilePickerDemo.ViewModels;

using ReactiveUI.Fody.Helpers;

public class MainViewModel : ViewModelBase
{
    [Reactive]
    public string FilePath { get; set; }

    public MainViewModel(string filePath)
    {
        this.FilePath = filePath;
    }
}
