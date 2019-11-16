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
			// by wrapping all the db operations in a transaction, we can be sure that BOTH the Person+Transaction
			// tables will be updated correctly, or NONE of them will... db transaction keeps our database state
			// consistent
			using (var dbTransaction = Db.OpenTransaction())
			{
				// step 1 : try to load a user matching the requested user, they will own the new transaction
				var person = Db.SingleById<PersonTbl>(request.PersonId);

				if (person == null) throw HttpError.BadRequest("User wasnt found in db");

				if (person.Balance < request.Price) throw HttpError.BadRequest("User doesn't have enough money");

				var newTransaction = new TransactionTbl
				{
					PersonId = request.PersonId,
					Price = request.Price,
					PaymentDt = request.PaymentDt,
					VisaMcc = request.VisaMcc,
					Description = request.Description
				};

				Db.UpdateOnly(() => new PersonTbl { Balance = person.Balance - request.Price }, p => p.Id == person.Id);
				var newTransactionId = Db.Insert(newTransaction, true);

				dbTransaction.Commit();

				return new InsertNewTransactionResponse
				{
					// return the id to the UI
					NewTransactionId = newTransactionId
				};
			}

			
		}

	}
}