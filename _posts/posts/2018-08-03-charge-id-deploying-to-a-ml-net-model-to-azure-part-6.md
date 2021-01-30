---
layout: post
category: posts
title: "Charge Id - Deploying a ML.Net Model to Azure"
date: "2018-08-03"
categories: 
  - "net"
  - "chargeid"
  - "data-science"
  - "machine-learning"
  - "ml-net"
  - "software"
  - "startup"
---

In the [previous post](/blog/?p=668) we built a machine learning model using [ML.Net](http://dot.net/ml), in this post we will deploy the model to an Azure app and allow it to be used via a HTTP API

Using the output model in zip format ‘vita-model-1.zip’ we can include this in our web application as an embedded resource or simply include the file for deployment.

To use the file from a HTTP endpoint:

1. Include the zip file in your deployment – embedded resource/content/read from blob storage etc..
2. Initialise the model as a singleton during application start up by using a file path or stream
3. Call the model using the function PredictionModel.Predict(‘my data from which to predict’)

Sample below that logs to [Logz.io](https://logz.io/)

 \[Produces("application/json")\]
 \[Route("\[controller\]")\]
    public class PredictionController : Controller
    {
        private readonly IPredict \_predictor;

        public PredictionController(IPredict predictor)
        {
            \_predictor = predictor;
        }

 \[HttpPost("predict/")\]
 \[SwaggerResponse(HttpStatusCode.OK, typeof(string))\]
        public async Task<IActionResult> Search(PredictionRequest request)
        {
            Guard.AgainstNull(request);
            var requestId = Guid.NewGuid();
            using (LogContext.PushProperty("request", request.ToJson()))
            using (LogContext.PushProperty("requestId", requestId))
            {
                try
                {
                    var result = await \_predictor.PredictAsync(request);
                    return Ok(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Log.Warning(e, "PredictionController error {request}", request.ToJson());
                    return NoContent();
                }
            }
        }
    }

[Hosting our endpoint](https://chargeid-api-test.azurewebsites.net/swagger/index.html?url=/swagger/v1/swagger.json#!/Prediction/Prediction_Search) with Swagger on Azure allows us to test the inputs and see the results below:

T[![image](https://raw.githubusercontent.com/chrismckelt/chrismckelt.github.io/master/_posts/posts/images//image_thumb-3.png "image")]https://raw.githubusercontent.com/chrismckelt/chrismckelt.github.io/master/_posts/posts/images//2018/08/image-3.png)

* * *

### Conclusion

Here we hosted our model in Azure using an App Service and managed to test it via Swagger.

Hoping to make this a Function App when this issue is resolved –> [https://github.com/dotnet/machinelearning/issues/569](https://github.com/dotnet/machinelearning/issues/569 "https://github.com/dotnet/machinelearning/issues/569")

 

 

* * *

 

## Posts in this series

[Charge Id – scratching the tech itch \[ part 1 \]](/blog/?p=460) [Charge Id – lean canvas \[ part 2 \]](/blog/?p=485) [Charge Id – solution overview \[ part 3 \]](/blog/?p=505) [Charge Id – analysing the data \[ part 4 \]](/blog/?p=507) [Charge Id – the prediction model \[ part 5 \]](/blog/?p=668) [Charge Id – deploying a ML.Net Model to Azure \[ part 6 \]](/blog/?p=705)

 

## Code

[https://github.com/chrismckelt/vita](https://github.com/chrismckelt/vita "https://github.com/chrismckelt/vita")
