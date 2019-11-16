using macdream.api.database;
using ServiceStack;

namespace macdream.api.messages
{
	[Route("/autoquery/persons")]
	public class QueryPersons : QueryDb<PersonTbl> { }
}