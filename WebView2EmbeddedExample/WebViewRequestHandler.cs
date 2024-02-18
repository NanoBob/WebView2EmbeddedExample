using HeyRed.Mime;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace WebView2EmbeddedExample;

public class WebViewRequestHandler(string staticFilesootDirectory)
{
    private readonly EmbeddedResourceHelper resourceHelper = new(Assembly.GetExecutingAssembly(), staticFilesootDirectory);

    public CoreWebView2WebResourceResponse HandleRequest(WebView2 webview, CoreWebView2WebResourceRequest request)
    {
        int statusCode;
        string statusReason;
        Stream content;
        string headers;

        try
        {
            var path = request.Uri.Replace("http://localhost", "");
            if (path == "/")
                path = "/index.html";

            var mimeType = MimeTypesMap.GetMimeType(path);

            statusCode = 200;
            statusReason = "OK";
            content = resourceHelper.GetFile(path);
            headers = $"Content-Length: {content.Length}\nContent-Type: {mimeType}";
        } catch (FileNotFoundException)
        {
            statusCode = 404;
            statusReason = "Not found";
            content = Stream.Null;
            headers = "";
        }
        catch (Exception e)
        {
            statusCode = 500;
            statusReason = "Internal server error";
            content = Stream.Null;
            headers = "";
        }

        return webview.CoreWebView2.Environment.CreateWebResourceResponse(content, statusCode, statusReason, headers);
    }
}
