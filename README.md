# WebView2EmbeddedExample

This repo serves as an example and/or template on setting op a desktop app using [Edge WebView2](https://developer.microsoft.com/en-us/microsoft-edge/webview2). This allows you to combine a native app and a web-based application, much like how [Electron](https://www.electronjs.org/) works.

This example uses: 
- WPF to host the WebView2 control
- React for the actual frontend itself.
  - This is easily swapped out for the front-end library / framework of your choice.


## How it works:
- The WPF app only has one control, the Edge WebView2 itself.
- The react frontend is included in the WPF app as embedded resources.
- When the WebView makes a web request it's intercepted by the WPF app, and served content from the embedded resources.
  - This means no web requests will actually be made over the internet, but it is all contained within the application

## How to expand
- Web requests are handled in [`WebViewRequestHandler`](https://github.com/NanoBob/WebView2EmbeddedExample/blob/6eb245a3e26da4adb3507b8f55d8bc591eee5330/WebView2EmbeddedExample/WebViewRequestHandler.cs#L14), where it returns the files with appropriate mime type. If you wanted to support other requests / make an API accesible, you would do that here.
- The frontend can be replaced with anything you wish, currently the project is setup to serve anything from the `Frontend/build` directory, but this can be modified in [`MainWindow.xaml.cs`](https://github.com/NanoBob/WebView2EmbeddedExample/blob/6eb245a3e26da4adb3507b8f55d8bc591eee5330/WebView2EmbeddedExample/MainWindow.xaml.cs#L9)