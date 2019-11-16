using macdream.api.database;
using ServiceStack;

namespace macdream.api.messages
{
	[Route("/autoquery/visamccs")]
	public class QueryVisaMccs : QueryDb<VisaMccTbl> { }
}