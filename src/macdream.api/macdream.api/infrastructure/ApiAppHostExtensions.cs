using System.Collections.Generic;
using Funq;
using ServiceStack;
using ServiceStack.Admin;
using ServiceStack.Api.OpenApi;
using ServiceStack.Logging;
using ServiceStack.Validation;

namespace macdream.api.infrastructure
{
	public static class ApiAppHostExtensions
	{

		public static void ConfigureApiAppHostContainer(this ServiceStackHost host, Container container)
		{
			//var debugMode = AppSettings.Get(nameof(HostConfig.DebugMode), HostingEnvironment.IsDevelopment());

			host.SetConfig(new HostConfig
			{
				DebugMode = true,

				DefaultContentType = MimeTypes.Json,

				WriteErrorsToResponse = true,

				// https://docs.servicestack.net/debugging#strictmode
				StrictMode = true
			});
            
			host.Plugins.Add(new ValidationFeature());

			host.Plugins.Add(new AutoQueryFeature { MaxLimit = 100 });

			host.Plugins.Add(new AdminFeature());

			host.Plugins.Add(new OpenApiFeature());

			host.Plugins.Add(new CorsFeature(
				allowCredentials: true,
				allowedHeaders: "Content-Type, Allow",
				allowedMethods: "GET, POST, PATCH, DELETE, OPTIONS",
				allowOriginWhitelist: new List<string>
				{
					"*"
				}
			));


			//Handle Exceptions occurring in Services:
			host.ServiceExceptionHandlers.Add((httpRequest, requestMessage, ex) =>
			{
				// we can use application logging to record details of uncaught exceptions here.
				var logger = LogManager.GetLogger(httpRequest.GetType());
				logger.Error(requestMessage, ex);

				/*
				//return null; //continue with default Error Handling

				//object customExceptionDto = null;
				//if (exception is BusinessException businessException) customExceptionDto = businessException.ConvertTo<object>();

				// or return your own custom response
				//var customResponse = DtoUtils.CreateErrorResponse(customExceptionDto, exception);
				//return customResponse;*/

				return DtoUtils.CreateErrorResponse(requestMessage, ex);
			});


			
			//// Had to comment this out so that our rate limit exceptions don't get wrapped up into 200s
			//// Handle Unhandled Exceptions occurring outside of Services
			//// E.g. Exceptions during Request binding or in filters:
			//host.UncaughtExceptionHandlers.Add((httpRequest, httpResponse, operationName, ex) =>
			//{

			//	// In addition to any RequestLogger exception logging (such as that provided by Rollbar feature),
			//	// we can use application logging to record details of uncaught exceptions here.
			//	//var logger = LogManager.GetLogger(httpRequest.GetType());
			//	//logger.Error(httpRequest, ex);

			//	//var debugMode = settings.DebugMode;
			//	//if (debugMode)
			//	//{
			//	//	httpResponse.WriteAsync("Error: {0}: {1} : {2}".Fmt(ex.GetType().Name, ex.Message, ex.StackTrace));
			//	//	httpResponse.WriteAsync("Error: {0}".Fmt(ex.StackTrace));
			//	//}
			//	//else
			//	//{
			//	//	httpResponse.WriteAsync(
			//	//		"Uncaught Exception Error occurred outside of Services: {0}: {1}".Fmt(ex.GetType().Name,
			//	//			ex.Message));
			//	//}

			//	//httpResponse.EndRequest(true);
			//});


			//if (Config.DebugMode)
			//{
			//	Plugins.Add(new HotReloadFeature());
			//}
		}

	}
}