using ServiceStack;

namespace macdream.api.endpoints
{
	[Route("/transactions/new", HttpMethods.Post)]
	public class InsertNewTransactionRequest : IReturn<InsertNewTransactionResponse>
	{
		public long PersonId { get; set; }
	}
}