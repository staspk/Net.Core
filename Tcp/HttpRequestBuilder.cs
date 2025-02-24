using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcp
{
    public enum RequestMethods
    {
        GET,
        HEAD,
        POST,
        PUT,
        DELETE,
        CONNECT,
        OPTIONS,
        TRACE,
        PATCH
    }

    /// <summary>
    /// Example Request:
    ///    "GET http://127.0.0.1/ HTTP/1.1
    ///     Host: 127.0.0.1
    ///     Connection: Close
    ///     Content-Length: 12
    ///     Content-Type: text/plain
    ///     
    ///     {Body}"
    /// 
    /// </summary>
    public class HttpRequestBuilder
    {
        IServer IServer;
        Encoding Encoding = Encoding.UTF8;

        RequestMethods RequestMethod = RequestMethods.GET;
        string Connection
        {
            get
            {
                if (IServer.KeepAlive)
                    return "Connection: keep-alive";
                else
                    return "Connection: Close";
            }
        }

        string ContentType = "text/plain";

        string Body = "Hello from the .NET world";


        public HttpRequestBuilder(IServer IServer)
        {
            this.IServer = IServer;
        }

        public byte[] Serialize()
        {
            string request = $"{RequestMethod} {IServer.Uri.AbsoluteUri} HTTP/1.1{Environment.NewLine}" +
                             $"Host: {IServer.Uri.Host}{Environment.NewLine}" +
                             $"{Connection}{Environment.NewLine}" +
                             $"Content-Length: {Body.Length}{Environment.NewLine}" +
                             $"Content-Type: {ContentType}{Environment.NewLine}" +
                             $"{Environment.NewLine}" +
                             $"{Body}";

            return Encoding.GetBytes(request);
        }

        public HttpRequestBuilder SetRequestMethod(RequestMethods requestMethod)
        {
            this.RequestMethod = requestMethod;
            return this;
        }

        public HttpRequestBuilder SetBody(string body)
        {
            this.Body = body;
            return this;
        }

        public HttpRequestBuilder SetContentType(string contentType)
        {
            this.ContentType = contentType;
            return this;
        }

        public HttpRequestBuilder SetEncoding(Encoding encoding)
        {
            this.Encoding = encoding;
            return this;
        }
    }
}
