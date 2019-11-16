using System.Linq;
using macdream.api.database;
using macdream.api.messages;
using ServiceStack;
using ServiceStack.OrmLite;

namespace macdream.api.endpoints
{
	public class MacDreamController : Service
	{
		public GetAllThePeopleAndTransactionsResponse Get(GetAllThePeopleAndTransactionsRequest request)
		{

			var persons = Db.Select<PersonTbl>();
			var transactions = Db.Select<TransactionTbl>();
			persons.Merge(transactions);


			return new GetAllThePeopleAndTransactionsResponse
			{
				People = persons.Map(p => new PersonJson
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

		public InsertNewTransactionResponse Post(InsertNewTransactionRequest request)
		{

			// step 1 : try to load a user matching the requested user, they will own the new transaction
			var person = Db.Select<PersonTbl>(p => p.Id == request.PersonId);

			if (person == null) throw HttpError.BadRequest("User wasnt found in db");


			
			return new InsertNewTransactionResponse
			{
				// TODO use hte ~Request json to build up a db record, insert the new transaction and return the id to the UI
			};
		}

	}
}