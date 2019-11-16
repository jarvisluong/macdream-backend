using System.Linq;
using macdream.api.database;
using macdream.api.messages;
using ServiceStack;
using ServiceStack.OrmLite;

namespace macdream.api.endpoints
{
	public class MacDreamServices : Service
	{
		public GetAllThePeopleAndTransactionsResponse Get(GetAllThePeopleAndTransactionsRequest request)
		{

			var persons = Db.Select<PersonTbl>();
			var transactions = Db.Select<TransactionTbl>();
			persons.Merge(transactions);


			return new GetAllThePeopleAndTransactionsResponse
			{
				People = persons.Map(p => new PersonDto
				{
					Id = p.Id,
					Name = p.Name,
					Transactions = p.Transactions.Map(t => new TransactionDto
					{
						Description = t.Description,
						PaymentDt = t.PaymentDt
					})
				})
			};
		}

	}
}