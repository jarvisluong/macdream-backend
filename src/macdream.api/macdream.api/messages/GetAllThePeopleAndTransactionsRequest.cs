using macdream.api.database;
using ServiceStack;

namespace macdream.api.messages
{
	[Route("/demo", 
		HttpMethods.Get
	)]
	public class GetAllThePeopleAndTransactionsRequest : IReturn<GetAllThePeopleAndTransactionsResponse>
	{
		
	}


	[Route("/autoquery/persons")]
	public class QueryPersons : QueryDb<PersonTbl> { }

	[Route("/autoquery/transactions")]
	public class QueryTransactions : QueryDb<TransactionTbl> { }
}