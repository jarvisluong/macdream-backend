using macdream.api.database;
using ServiceStack;

namespace macdream.api.messages
{
	[Route("/autoquery/goals")]
	public class QueryGoals : QueryDb<GoalTbl> { }
}