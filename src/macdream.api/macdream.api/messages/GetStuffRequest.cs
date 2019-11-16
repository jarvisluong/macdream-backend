using ServiceStack;

namespace macdream.api.messages
{
	[Route("/api/demo", 
		HttpMethods.Get
	)]
	public class GetStuffRequest : IReturn<GetStuffResponse>
	{
		
	}
}