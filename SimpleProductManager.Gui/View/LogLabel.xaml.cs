using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SimpleProductManager.Gui.View;

public partial class LogLabel : UserControl
{
    public int SwitchDelayInSeconds { get; set; } = 5;
    
    public LogLabel()
    {
        InitializeComponent();
    }
    
    public static readonly DependencyProperty LogMessageProperty =
        DependencyProperty.Register(nameof(LogMessage), typeof(string), typeof(LogLabel), 
            new PropertyMetadata(string.Empty, OnLogMessageChanged));

    public string LogMessage
    {
        get => (string)GetValue(LogMessageProperty);
        set => SetValue(LogMessageProperty, value);
    }

    private static async void OnLogMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is LogLabel control && !string.IsNullOrEmpty(e.NewValue as string))
        {
            await control.ShowLogVisuals();
        }
    }

    private async Task ShowLogVisuals()
    {
        BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff4E4E"));
        LogText.Foreground = Brushes.White;
        
        await Task.Delay(SwitchDelayInSeconds * 1000);
        
        LogMessage = string.Empty;
        BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("WhiteSmoke"));
        LogText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
    }
}