using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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

		[IgnoreDataMember]  // prevents properties being queried when viewing in AutoQuery /admin UI
		[Reference]
		public List<TransactionTbl> Transactions { get; set; } = new List<TransactionTbl>();

		[IgnoreDataMember]  // prevents properties being queried when viewing in AutoQuery /admin UI
		[Reference]
		public List<GoalTbl> Goals { get; set; } = new List<GoalTbl>();

        [Reference]
        public decimal Balance { get; set; }
    }
}
