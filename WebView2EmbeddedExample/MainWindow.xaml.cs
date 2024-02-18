using Microsoft.Web.WebView2.Core;
using System.IO;
using System.Windows;

namespace WebView2EmbeddedExample;

public partial class MainWindow : Window
{
    private const string webroot = "Frontend/build";
    private readonly WebViewRequestHandler requestHandler = new(webroot);

    public MainWindow()
    {
        InitializeComponent();

        Webview.CoreWebView2InitializationCompleted += HandleInit;
        Webview.Source = new Uri($"http://localhost");
    }

    private void HandleInit(object? sender, CoreWebView2InitializationCompletedEventArgs e)
    {
        Webview.CoreWebView2.AddWebResourceRequestedFilter("*", CoreWebView2WebResourceContext.All);
        Webview.CoreWebView2.WebResourceRequested += HandleWebSourceRequest;
    }

    private void HandleWebSourceRequest(object? sender, CoreWebView2WebResourceRequestedEventArgs e)
    {
        e.Response = requestHandler.HandleRequest(Webview, e.Request);
    }
}