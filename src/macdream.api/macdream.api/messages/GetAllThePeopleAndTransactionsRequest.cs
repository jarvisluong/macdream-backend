using ServiceStack;

namespace macdream.api.messages
{
	[Route("/demo", 
		HttpMethods.Get
	)]
	public class GetAllThePeopleAndTransactionsRequest : IReturn<GetAllThePeopleAndTransactionsResponse>
	{
		
	}
}