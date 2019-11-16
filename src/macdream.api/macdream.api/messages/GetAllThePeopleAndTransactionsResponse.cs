using System;
using System.Collections.Generic;

namespace macdream.api.messages
{
	public class GetAllThePeopleAndTransactionsResponse 
	{
		public List<PersonDto> People { get; set; }
	}

	public class PersonDto
	{
		public long Id { get; set; }

		public string Name { get; set; }

		public List<TransactionDto> Transactions { get; set; }



	}

	public class TransactionDto
	{

		public DateTime PaymentDt { get; set; }

		public string Description { get; set; }
	}
}