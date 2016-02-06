using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MvcStreamingFile.Controllers
{
    public class UploadingNormalController : ApiController
    {
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

                        return new FileDesc(fileName, projectId, i.Headers.ContentLength.Value / 1024);
                    });

                return fileInfo;
            });
        }
    }
}
