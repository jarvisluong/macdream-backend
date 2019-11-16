namespace macdream.api.endpoints
{
	public class UpdateGoalResponse
	{
        public long GoalId { get; set; }
        public decimal GoalSaving { get; set; }
        public decimal GoalPrice { get; set; }
        public bool GoalAchieved { get; set; }
	}
}