using System;
using System.Data;
using Funq;
using macdream.api.database;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace macdream.api.infrastructure
{
	public static class InitialiseAndSeedDatabase
	{
		public static void BuildDatabase(Container container)
		{
			var dbFactory = container.Resolve<IDbConnectionFactory>();

			using (var db = dbFactory.Open())
			{
				db.CreateTableIfNotExists<PersonTbl>();
				db.CreateTableIfNotExists<TransactionTbl>();
				db.CreateTableIfNotExists<GoalTbl>();
                db.CreateTableIfNotExists<VisaMccTbl>();

				{
                    var visaMcc1 = new VisaMccTbl { VisaMcc = VisaMccEnum.Alcohol, isSaving = false };
                    var visaMcc2 = new VisaMccTbl { VisaMcc = VisaMccEnum.Coffee, isSaving = false };
                    var visaMcc3 = new VisaMccTbl { VisaMcc = VisaMccEnum.Deposit, isSaving = false };
                    var visaMcc4 = new VisaMccTbl { VisaMcc = VisaMccEnum.Electronics, isSaving = false };
                    var visaMcc5 = new VisaMccTbl { VisaMcc = VisaMccEnum.Food, isSaving = false };
                    var visaMcc6 = new VisaMccTbl { VisaMcc = VisaMccEnum.Missing, isSaving = false };
                    var visaMcc7 = new VisaMccTbl { VisaMcc = VisaMccEnum.Rent, isSaving = false };
                    var visaMcc8 = new VisaMccTbl { VisaMcc = VisaMccEnum.Withdrawal, isSaving = false };
                    db.Insert(visaMcc1);
                    db.Insert(visaMcc2);
                    db.Insert(visaMcc3);
                    db.Insert(visaMcc4);
                    db.Insert(visaMcc5);
                    db.Insert(visaMcc6);
                    db.Insert(visaMcc7);
                    db.Insert(visaMcc8);

                    var person1 = new PersonTbl { Name = "Charlie Chapman", Balance = 10000m};
					person1.Id = db.Insert(person1, true);

					var goal1 = new GoalTbl
					{
						GoalType = GoalTypeEnum.Purchase,
						Name = "Macbook Pro",
						PersonId = person1.Id,
						Price = 2500.0m,
						TargetDt = DateTime.Today.AddDays(6 * 30),
						Description = "I love the new macbook and want one"
					};
					db.Insert(goal1);

					var goal2 = new GoalTbl
					{
						GoalType = GoalTypeEnum.CashSaving,
						Name = "Rainy Day Reserves",
						PersonId = person1.Id,
						Price = 5000.0m,
						TargetDt = DateTime.Today.AddDays(12 * 30),
						Description = "I want to always have enough cash saved to be " +
						              "unemployed for a year and not stress out"
					};
					db.Insert(goal2);

					var goal3 = new GoalTbl
					{
						GoalType = GoalTypeEnum.Investing,
						Name = "S&P500 ETF",
						PersonId = person1.Id,
						TargetDt = DateTime.Today.AddDays(12 * 30),
						Description = "I want to invest in a shares index fund"
					};
					db.Insert(goal3);

                    InjectSampleTransaction(db, person1.Id, 155, VisaMccEnum.Rent, "Rent", 300);
                    InjectSampleTransaction(db, person1.Id, 154, VisaMccEnum.Alcohol, "Beer", 8);
                    InjectSampleTransaction(db, person1.Id, 154, VisaMccEnum.Food, "Skittles", 3.5m);
                    InjectSampleTransaction(db, person1.Id, 152, VisaMccEnum.Coffee, "Latte", 2.5m);
                    InjectSampleTransaction(db, person1.Id, 144, VisaMccEnum.Food, "Skittles", 3.5m);
					InjectSampleTransaction(db, person1.Id, 143, VisaMccEnum.Electronics, "Gamer mouse", 22.5m);
					InjectSampleTransaction(db, person1.Id, 142, VisaMccEnum.Food, "Skittles", 3.5m);
                    InjectSampleTransaction(db, person1.Id, 141, VisaMccEnum.Electronics, "Mobile charger cable", 12);
                    InjectSampleTransaction(db, person1.Id, 137, VisaMccEnum.Food, "Skittles", 3.5m);
                    InjectSampleTransaction(db, person1.Id, 134, VisaMccEnum.Alcohol, "Beer", 8);
                    InjectSampleTransaction(db, person1.Id, 134, VisaMccEnum.Coffee, "Latte", 2.5m);
                    InjectSampleTransaction(db, person1.Id, 133, VisaMccEnum.Food, "Skittles", 3.5m);
                    InjectSampleTransaction(db, person1.Id, 132, VisaMccEnum.Alcohol, "Beer", 8);
                    InjectSampleTransaction(db, person1.Id, 120, VisaMccEnum.Food, "Skittles", 3.5m);
                    InjectSampleTransaction(db, person1.Id, 119, VisaMccEnum.Coffee, "Latte", 2.5m);
                    InjectSampleTransaction(db, person1.Id, 118, VisaMccEnum.Food, "Skittles", 3.5m);
                    InjectSampleTransaction(db, person1.Id, 117, VisaMccEnum.Deposit, "Salary", -3000m);
                    InjectSampleTransaction(db, person1.Id, 116, VisaMccEnum.Alcohol, "Beer", 8);
                    InjectSampleTransaction(db, person1.Id, 115, VisaMccEnum.Coffee, "Latte", 2.5m);
                    InjectSampleTransaction(db, person1.Id, 95, VisaMccEnum.Alcohol, "Beer", 8);
					InjectSampleTransaction(db, person1.Id, 95, VisaMccEnum.Coffee, "Latte", 2.5m);
                    InjectSampleTransaction(db, person1.Id, 95, VisaMccEnum.Electronics, "Steam subscription", 15);
                    InjectSampleTransaction(db, person1.Id, 94, VisaMccEnum.Coffee, "Latte", 2.5m);
                    InjectSampleTransaction(db, person1.Id, 92, VisaMccEnum.Alcohol, "Beer", 8);
                    InjectSampleTransaction(db, person1.Id, 84, VisaMccEnum.Coffee, "Latte", 2.5m);
                    InjectSampleTransaction(db, person1.Id, 74, VisaMccEnum.Withdrawal, "Cash withdrawal", 200m);
                    InjectSampleTransaction(db, person1.Id, 64, VisaMccEnum.Coffee, "Latte", 2.5m);
                    InjectSampleTransaction(db, person1.Id, 57, VisaMccEnum.Coffee, "Latte", 2.5m);
                    InjectSampleTransaction(db, person1.Id, 56, VisaMccEnum.Alcohol, "Beer", 8);
                    InjectSampleTransaction(db, person1.Id, 55, VisaMccEnum.Coffee, "Latte", 2.5m);
					InjectSampleTransaction(db, person1.Id, 53, VisaMccEnum.Rent, "Rent", 300);
                    InjectSampleTransaction(db, person1.Id, 49, VisaMccEnum.Coffee, "Latte", 2.5m);
                    InjectSampleTransaction(db, person1.Id, 48, VisaMccEnum.Alcohol, "Beer", 8);
                    InjectSampleTransaction(db, person1.Id, 46, VisaMccEnum.Coffee, "Latte", 2.5m);
                    InjectSampleTransaction(db, person1.Id, 44, VisaMccEnum.Rent, "Rent", 300);
					InjectSampleTransaction(db, person1.Id, 34, VisaMccEnum.Electronics, "Call of Duty game subscription", 18);
                    InjectSampleTransaction(db, person1.Id, 14, VisaMccEnum.Alcohol, "Beer", 8);
                }

                //var result = db.SingleById<Poco>(1);
                //result.PrintDump(); //= {Id: 1, Name:Seed Data}
            }
		}

		private static void InjectSampleTransaction(
			IDbConnection db, long personId, int daysAgo, VisaMccEnum visaMccEnum, string description, decimal price)
		{
            var person = db.SingleById<PersonTbl>(personId);
            if (person == null || person.Balance < price) return;

            var visaMcc = db.Single<VisaMccTbl>(v => v.VisaMcc == visaMccEnum);
            if (visaMcc == null) return;

            var transaction1 = new TransactionTbl
			{
				PaymentDt = DateTime.Today.AddDays(-daysAgo),
				PersonId = personId,
				Description = description,
                Price = price,
                VisaMccId = visaMcc.Id
			};

            db.UpdateOnly(() => new PersonTbl { Balance = person.Balance - price }, p => p.Id == person.Id);
            db.Insert(transaction1);
		}

	}
}