using System;
using ServiceStack.DataAnnotations;

namespace macdream.api.database
{
	/// <summary>
	/// To keep track of a financial / savings goal, something that you want to have money for at a certain
	/// date in the future
	/// </summary>
	public class GoalTbl
	{
		[AutoIncrement]
		[PrimaryKey]
		public long Id { get; set; }


		[References(typeof(PersonTbl))]
		public long PersonId { get; set; }

		[Required]
		public DateTime TargetDt { get; set; }

		[Required]
		public GoalTypeEnum GoalType { get; set; }

        public decimal Saving { get; set; } = 0;
        public decimal Price { get; set; }

		[StringLength(1, 100)]
		public string Name { get; set; } = "macbook pro";

		[StringLength(1, 255)] 
		public string Description { get; set; } = "I want a new macbook so I can work faster";


	}
}