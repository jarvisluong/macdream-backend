using System;
using System.Collections.Generic;
using Funq;
using macdream.api.database;
using ServiceStack;
using ServiceStack.Admin;
using ServiceStack.Api.OpenApi;
using ServiceStack.Data;
using ServiceStack.Logging;
using ServiceStack.OrmLite;
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

			BuildDatabase(container);
			
		}


		private static void BuildDatabase(Container container)
		{
			var dbFactory = container.Resolve<IDbConnectionFactory>();

			using (var db = dbFactory.Open())
			{
				db.CreateTableIfNotExists<PersonTbl>();
				db.CreateTableIfNotExists<TransactionTbl>();

				{
					var person1 = new PersonTbl { Name = "The Dude 1"};
					person1.Id = db.Insert(person1, true);

					var transaction1 = new TransactionTbl
					{
						PaymentDt = DateTime.Today.AddDays(-60),
						PersonId = person1.Id,
						Description = "Beer"
					};
					db.Insert(transaction1);
				}

				

				//var result = db.SingleById<Poco>(1);
				//result.PrintDump(); //= {Id: 1, Name:Seed Data}
			}
		}
	}
}