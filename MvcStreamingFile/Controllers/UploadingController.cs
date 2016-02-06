using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace MvcStreamingFile.Controllers
{
    [ControllerStreamingConfig]
    public class UploadingController : ApiController
    {
        //ref: http://www.asp.net/web-api/overview/extensibility/configuring-aspnet-web-api

        public async Task<IEnumerable<FileDesc>> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                    "This request is not properly formatted"));
            }

            var streamProvider = new MultipartFormDataMemoryStreamProvider();
            return await Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith(t =>
            {
                if (t.IsFaulted || t.IsCanceled)
                {
                    throw new HttpResponseException(HttpStatusCode.InternalServerError);
                }
                MultipartFormDataMemoryStreamProvider resultado = t.Result;
                string projectId = resultado.FormData.GetValues("hidProjectId").Single();
                IEnumerable<FileDesc> fileInfo =
                    streamProvider.Contents.Where((content, idx) => resultado.IsStream(idx)).Select(i =>
                    {
                        string fileName = i.Headers.ContentDisposition.FileName.Replace("\"", string.Empty);
                        byte[] fileContent = i.ReadAsByteArrayAsync().Result;

                        return new FileDesc(fileName, projectId, i.Headers.ContentLength.Value/1024);
                    });

                return fileInfo;
            });
        }

        public async Task<HttpResponseMessage> Get(int id)
        {
            return await Task.Factory.StartNew(() =>
            {
                if (id <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                string fileName = null;
                string localFilePath = null;

                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read))
                };
                response.Content.Headers.ContentDisposition =
                    new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
                response.Content.Headers.ContentDisposition.FileName = fileName;

                return response;
            });
        }
    }
}