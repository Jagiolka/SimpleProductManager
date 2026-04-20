using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SimpleProductManager.Gui.View;

public partial class LogControl : UserControl
{
    public int SwitchDelayInSeconds { get; set; } = 8;

    public LogControl()
    {
        InitializeComponent();
        Visibility = Visibility.Collapsed;
    }

    public static readonly DependencyProperty LogMessageProperty =
        DependencyProperty.Register(nameof(LogMessage), typeof(string), typeof(LogControl), 
            new PropertyMetadata(string.Empty, OnLogMessageChanged));

    public string LogMessage
    {
        get => (string)GetValue(LogMessageProperty);
        set => SetValue(LogMessageProperty, value);
    }

    private static async void OnLogMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not LogControl ctrl)
        {
            return;
        }

        var newMessage = e.NewValue as string;

        if (string.IsNullOrWhiteSpace(newMessage))
        {
            ctrl.Visibility = Visibility.Collapsed;
            return;
        }

        await ctrl.ShowLogVisualsAsync();
    }

    private async Task ShowLogVisualsAsync()
    {
        Visibility = Visibility.Visible;
        BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff4E4E"));
        LogText.Foreground = Brushes.White;

        await Task.Delay(SwitchDelayInSeconds * 1000);
        Visibility = Visibility.Collapsed;
    }
}