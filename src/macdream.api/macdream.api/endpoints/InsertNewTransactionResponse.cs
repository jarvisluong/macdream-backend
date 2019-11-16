namespace macdream.api.endpoints
{
	public class InsertNewTransactionResponse
	{
		public long NewTransactionId { get; set; }

        public decimal SavingAmount { get; set; }

        public long GoalId { get; set; }
    }
}