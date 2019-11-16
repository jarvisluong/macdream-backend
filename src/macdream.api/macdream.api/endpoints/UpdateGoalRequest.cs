using ServiceStack;
using System;
using macdream.api.database;

namespace macdream.api.endpoints
{
	[Route("/goals/update", HttpMethods.Put)]
	public class UpdateGoalRequest : IReturn<UpdateGoalResponse>
	{
		public long PersonId { get; set; }
        public long GoalId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}