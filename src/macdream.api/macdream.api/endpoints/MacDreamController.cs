using System.Linq;
using macdream.api.database;
using macdream.api.messages;
using ServiceStack;
using ServiceStack.OrmLite;
using System;

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
            var person = Db.SingleById<PersonTbl>(request.PersonId);

            if (person == null) throw HttpError.BadRequest("User wasnt found in db");

            var newTransaction = new TransactionTbl
            {
                PaymentDt = request.PaymentDt,
                PersonId = request.PersonId,
                VisaMcc = request.VisaMcc,
                Description = request.Description
            };

            var newTransactionId = Db.Insert(newTransaction, true);

            return new InsertNewTransactionResponse
			{
				// return the id to the UI
                   NewTransactionId = newTransactionId
            };
		}

	}
}