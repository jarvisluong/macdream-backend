using System;
using ServiceStack.DataAnnotations;

namespace macdream.api.database
{
	public class TransactionTbl
	{
		[AutoIncrement]
		[PrimaryKey]
		public long Id { get; set; }


		[References(typeof(PersonTbl))]
		public long PersonId { get; set; }

		[Required]
		public DateTime PaymentDt { get; set; }


		public decimal Price { get; set; }

		[Required]
		//[StringLength(4, 4)]
		public VisaMccEnum VisaMcc { get; set; } = VisaMccEnum.Missing;


		[StringLength(1, 255)]
		public string Description { get; set; }


	}
}