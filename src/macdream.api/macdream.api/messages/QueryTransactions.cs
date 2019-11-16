using macdream.api.database;
using ServiceStack;

namespace macdream.api.messages
{
	[Route("/autoquery/transactions")]
	public class QueryTransactions : QueryDb<TransactionTbl> { }
}