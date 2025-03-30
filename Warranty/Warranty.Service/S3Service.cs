﻿using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recipes.Core.Interfaces.IServices;

namespace Recipes.Service.Services
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _s3Client;
        //private readonly AwsSettings _awsSettings;
        public S3Service(IConfiguration configuration)
        {
            var awsOptions = configuration.GetSection("AWS");
            var name = awsOptions["BucketName"];
            var accessKey = Environment.GetEnvironmentVariable("AccessKey");
            var secretKey = Environment.GetEnvironmentVariable("SecretKey");
            var region = Environment.GetEnvironmentVariable("Region");
            _s3Client = new AmazonS3Client(accessKey, secretKey, Amazon.RegionEndpoint.GetBySystemName(region));
        }
      

        public async Task<string> GeneratePresignedUrlAsync(string fileName, string contentType)
        {
            var key = $"users/{fileName}"; // נתיב כולל תיקיה

            var request = new GetPreSignedUrlRequest
            {
                BucketName = "files-warranty",
                Key = key,
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddMinutes(60),
                ContentType = contentType
            };

            return _s3Client.GetPreSignedURL(request);
        }

        public async Task<string> GetDownloadUrlAsync(string fileName)
        {
            var key = $"users/{fileName}"; // נתיב כולל תיקיה

            var request = new GetPreSignedUrlRequest
            {
                BucketName = "files-warranty",
                Key = key,
                Verb = HttpVerb.GET,
                Expires = DateTime.UtcNow.AddMinutes(60) // תוקף של שעה
            };

            return _s3Client.GetPreSignedURL(request);
        }

    }
}



