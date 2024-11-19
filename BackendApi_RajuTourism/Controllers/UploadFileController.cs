using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace BackendApi_RajuTourism.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : Controller
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient Client;

        public UploadFileController(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
            Client = _blobServiceClient.GetBlobContainerClient("rajutourism");
        }

        [Route("saveimage")]
        [HttpPost]    
        public IActionResult SaveFile()
        {
            var httprequest = Request.Form;
            var postedfile = httprequest.Files[0];
            string filename = postedfile.FileName;
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            var blobClient = Client.GetBlobClient(GuidString + filename);

            Stream stream = postedfile.OpenReadStream();
            blobClient.Upload(stream);

            return Ok(blobClient.Uri.AbsoluteUri);
        }

        [Route("listimages")]
        [HttpGet]
        public async Task<IActionResult> ListBlobs()
        {
            List<string> lst = new();
            await foreach(var blobItem in Client.GetBlobsAsync())
            {
                var blobClient = Client.GetBlobClient(blobItem.Name);
                lst.Add(blobClient.Uri.AbsoluteUri);
            }
            return Ok(lst);
            
        }
    }
}
