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

				{
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
						Price = 2000.0m,
						TargetDt = DateTime.Today.AddDays(12 * 30),
						Description = "I want to invest in a shares index fund"
					};
					db.Insert(goal3);

					InjectSampleTransaction(db, person1.Id, 60, VisaMccEnum.Alcohol, "Beer", 10);
					InjectSampleTransaction(db, person1.Id, 60, VisaMccEnum.Alcohol, "Beer", 9);
					InjectSampleTransaction(db, person1.Id, 60, VisaMccEnum.Alcohol, "Beer", 8);
					InjectSampleTransaction(db, person1.Id, 60, VisaMccEnum.Alcohol, "Beer", 11);
					InjectSampleTransaction(db, person1.Id, 60, VisaMccEnum.Alcohol, "Beer", 12);
					InjectSampleTransaction(db, person1.Id, 60, VisaMccEnum.Alcohol, "Beer", 9);
					InjectSampleTransaction(db, person1.Id, 60, VisaMccEnum.Food, "Skittles", 3.5m);
					InjectSampleTransaction(db, person1.Id, 60, VisaMccEnum.Food, "Skittles", 3.5m);
					InjectSampleTransaction(db, person1.Id, 60, VisaMccEnum.Food, "Skittles", 3.5m);
					InjectSampleTransaction(db, person1.Id, 60, VisaMccEnum.Food, "Skittles", 3.5m);
                    InjectSampleTransaction(db, person1.Id, 60, VisaMccEnum.Food, "Skittles", 3.5m);
                    InjectSampleTransaction(db, person1.Id, 60, VisaMccEnum.Food, "Skittles", 3.5m);
					InjectSampleTransaction(db, person1.Id, 60, VisaMccEnum.Coffee, "Latte", 5);
					InjectSampleTransaction(db, person1.Id, 60, VisaMccEnum.Coffee, "Latte", 4.5m);
					InjectSampleTransaction(db, person1.Id, 60, VisaMccEnum.Coffee, "Latte", 5.5m);
					InjectSampleTransaction(db, person1.Id, 60, VisaMccEnum.Coffee, "Latte", 4);
					InjectSampleTransaction(db, person1.Id, 60, VisaMccEnum.Coffee, "Latte", 5);
					InjectSampleTransaction(db, person1.Id, 60, VisaMccEnum.Coffee, "Latte", 4);
					InjectSampleTransaction(db, person1.Id, 60, VisaMccEnum.Coffee, "Latte", 5.5m);
					InjectSampleTransaction(db, person1.Id, 60, VisaMccEnum.Rent, "Rent", 200);
					InjectSampleTransaction(db, person1.Id, 30, VisaMccEnum.Rent, "Rent", 250);
					InjectSampleTransaction(db, person1.Id, 0, VisaMccEnum.Rent, "Rent", 300);
					InjectSampleTransaction(db, person1.Id, 27, VisaMccEnum.Electronics, "Mobile charger cable", 12);
					InjectSampleTransaction(db, person1.Id, 13, VisaMccEnum.Electronics, "Gamer mouse", 22.5m);
					InjectSampleTransaction(db, person1.Id, 56, VisaMccEnum.Electronics, "Steam subscription", 15);
					InjectSampleTransaction(db, person1.Id, 56, VisaMccEnum.Electronics, "Call of Duty game subscription", 18);
				}

				

				//var result = db.SingleById<Poco>(1);
				//result.PrintDump(); //= {Id: 1, Name:Seed Data}
			}
		}

		private static void InjectSampleTransaction(
			IDbConnection db, long personId, int daysAgo, VisaMccEnum visaMcc, string description, decimal price)
		{
			var transaction1 = new TransactionTbl
			{
				PaymentDt = DateTime.Today.AddDays(-daysAgo),
				PersonId = personId,
				VisaMcc = visaMcc,
				Description = description,
                Price = price
			};

            var person = db.SingleById<PersonTbl>(personId);
            if (person == null || person.Balance < price) return;

            db.UpdateOnly(() => new PersonTbl { Balance = person.Balance - price }, p => p.Id == person.Id);
            db.Insert(transaction1);
		}

	}
}