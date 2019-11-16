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

        public UpdateVisaMccResponse Put(UpdateVisaMccRequest request)
        {
            using (var dbTransaction = Db.OpenTransaction())
            {
                var visaMcc = Db.SingleById<VisaMccTbl>(request.VisaMccId);
                if (visaMcc == null) throw HttpError.BadRequest("VisaMcc not found");

                Db.UpdateOnly(() => new VisaMccTbl { isSaving = request.isSaving}, v => v.Id == request.VisaMccId);

                dbTransaction.Commit();

                return new UpdateVisaMccResponse
                {
                    VisaMccId = visaMcc.Id,
                    isSaving = request.isSaving,
                    VisaMcc = visaMcc.VisaMcc
                };
            }
        }

        public UpdateGoalResponse Put(UpdateGoalRequest request)
        {
            using (var dbTransaction = Db.OpenTransaction())
            {
                var person = Db.SingleById<PersonTbl>(request.PersonId);
                if (person == null) throw HttpError.BadRequest("User wasnt found in db");

                var goal = Db.SingleById<GoalTbl>(request.GoalId);
                if (goal == null) throw HttpError.BadRequest("Goal wasn't found in db");

                if (person.Balance < request.Amount) throw HttpError.BadRequest("User doesn't have enought money");

                Db.UpdateOnly(() => new GoalTbl { Saving = goal.Saving + request.Amount }, g => g.Id == request.GoalId);
                Db.UpdateOnly(() => new PersonTbl { Balance = person.Balance - request.Amount }, p => p.Id == request.PersonId);

                dbTransaction.Commit();

                return new UpdateGoalResponse
                {
                    // return the goal saving response
                    GoalId = goal.Id,
                    GoalSaving = goal.Saving + request.Amount,
                    GoalPrice = goal.Price,
                    GoalAchieved = goal.Price != 0 && goal.Saving + request.Amount >= goal.Price
                };
            }
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

                var visaMcc = Db.SingleById<VisaMccTbl>(request.VisaMccId);
                if (visaMcc == null) throw HttpError.BadRequest("VisaMcc not found");

				var newTransaction = new TransactionTbl
				{
					PersonId = request.PersonId,
					Price = request.Price,
					PaymentDt = request.PaymentDt,
					Description = request.Description,
                    VisaMccId = visaMcc.Id
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