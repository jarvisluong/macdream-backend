using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;

namespace macdream.api.database
{
	public class PersonTbl
	{
		[AutoIncrement]
		[PrimaryKey]
		public long Id { get; set; }


		[StringLength(1,255)]
		public string Name { get; set; }

		[Reference]
		public List<TransactionTbl> Transactions { get; set; } = new List<TransactionTbl>();
	}

	public class TransactionTbl
	{
		[AutoIncrement]
		[PrimaryKey]
		public long Id { get; set; }



		[References(typeof(PersonTbl))]
		public long PersonId { get; set; }

		[Required]
		public DateTime PaymentDt { get; set; }


		[StringLength(1, 255)]
		public string Description { get; set; }


	}
}
