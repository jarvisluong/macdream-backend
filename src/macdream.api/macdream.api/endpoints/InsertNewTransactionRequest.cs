using ServiceStack;
using System;
using macdream.api.database;

namespace macdream.api.endpoints
{
	[Route("/transactions/new", HttpMethods.Post)]
	public class InsertNewTransactionRequest : IReturn<InsertNewTransactionResponse>
	{
		public long PersonId { get; set; }
        public DateTime PaymentDt { get; set; }


        public decimal Price { get; set; }

        public VisaMccEnum VisaMcc { get; set; } = VisaMccEnum.Missing;

        public string Description { get; set; }
    }
}