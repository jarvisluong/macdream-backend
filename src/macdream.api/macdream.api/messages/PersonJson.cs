using System.Collections.Generic;

namespace macdream.api.messages
{
	public class PersonJson
	{
		public long Id { get; set; }

		public string Name { get; set; }

		public List<TransactionDto> Transactions { get; set; }



	}
}