using System;
using System.Collections.Generic;
using Funq;
using ServiceStack;
using ServiceStack.Admin;
using ServiceStack.Api.OpenApi;
using ServiceStack.Data;
using ServiceStack.Logging;
using ServiceStack.OrmLite;
using ServiceStack.Text;
using ServiceStack.Text.Common;
using ServiceStack.Validation;

namespace macdream.api.infrastructure
{
	public static class ApiAppHostExtensions
	{

		public static void ConfigureApiAppHostContainer(this ServiceStackHost host, Container container)
		{
			host.SetConfig(new HostConfig
			{
				DebugMode = true,

				DefaultContentType = MimeTypes.Json,

				WriteErrorsToResponse = true,

				// https://docs.servicestack.net/debugging#strictmode
				StrictMode = true
			});
            
			JsConfig<DateTime>.SerializeFn = time => new DateTime(time.Ticks, DateTimeKind.Utc).ToString(DateTimeSerializer.XsdDateTimeFormatSeconds);
			JsConfig<DateTime?>.SerializeFn =
				time => time != null
					? new DateTime(time.Value.Ticks, DateTimeKind.Utc).ToString(DateTimeSerializer.XsdDateTimeFormatSeconds)
					: null;

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

				return DtoUtils.CreateErrorResponse(requestMessage, ex);
			});



			container.Register<IDbConnectionFactory>(c => 
				new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider)); //InMemory Sqlite DB

			InitialiseAndSeedDatabase.BuildDatabase(container);
			
		}
	}
}