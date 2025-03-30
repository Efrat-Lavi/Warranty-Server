﻿using Microsoft.AspNetCore.Mvc;
using Recipes.Core.Interfaces.IServices;


namespace Warranty.API.Controllers
{
   
    [ApiController]
    [Route("api/upload")]
    public class UploadController : ControllerBase
    {
        private readonly IS3Service _s3Service;


         public UploadController(IS3Service s3Server)
         {
            _s3Service = s3Server;
         }
       
        // ⬆️ שלב 1: קבלת URL להעלאת קובץ ל-S3
        
        [HttpGet("upload-url")]
        public async Task<IActionResult> GetUploadUrl([FromQuery] string fileName, [FromQuery] string contentType)
        {
            if (string.IsNullOrEmpty(fileName))
                return BadRequest("Missing userId or fileName");

            var url = await _s3Service.GeneratePresignedUrlAsync(fileName, contentType);
            return Ok(new { url });
        }

        // ⬇️ שלב 2: קבלת URL להורדת קובץ מה-S3
        [HttpGet("download-url")]
        public async Task<IActionResult> GetDownloadUrl([FromQuery] string fileName)
        {
            var url = await _s3Service.GetDownloadUrlAsync(fileName);
            return Ok(new { downloadUrl = url });
        }
            
          
    }
}
