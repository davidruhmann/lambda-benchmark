using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Core;
using Amazon.S3;
using Amazon.S3.Model;

namespace Lambda;

public class Function
{
    private static async Task Main()
    {
        await LambdaBootstrapBuilder.Create(FunctionHandler)
            .Build()
            .RunAsync();
    }

    public static async Task FunctionHandler(ILambdaContext context)
    {
        var iterations = Convert.ToInt32(Environment.GetEnvironmentVariable("ITERATIONS_CODE"));
        var bucketName = Environment.GetEnvironmentVariable("BUCKET_NAME");
        var bucketKey = $"test/{context.FunctionName}/test.txt";

        var s3 = new AmazonS3Client();

        for (var i = 0; i < iterations; i++)
        {
            var request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = bucketKey,
                ContentType = "text/plain",
                ContentBody = i.ToString(),
            };

            await s3.PutObjectAsync(request).ConfigureAwait(false);
        }

        await s3.DeleteObjectAsync(new DeleteObjectRequest
        {
            BucketName = bucketName,
            Key = bucketKey
        }).ConfigureAwait(false);
    }
}
