namespace ReactiveCommandFilePickerDemo.Views
{
    using Avalonia.ReactiveUI;
    using Avalonia.Controls;
    using ReactiveCommandFilePickerDemo.ViewModels;

    public partial class StartWindow : ReactiveWindow<StartWindowViewModel>
    {
        public StartWindow()
        {
            InitializeComponent();
        }
    }
}
