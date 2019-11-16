using Funq;
using macdream.api.endpoints;
using ServiceStack;

namespace macdream.api.infrastructure
{
	public class AppHost : AppHostBase
	{
		public AppHost() : base("macdream thingy", typeof(MacDreamServices).Assembly)
		{
		}

		// Configure your AppHost with the necessary configuration and dependencies your App needs
		public override void Configure(Container container)
		{

			this.ConfigureApiAppHostContainer(container);

			
		}

		
	}
}