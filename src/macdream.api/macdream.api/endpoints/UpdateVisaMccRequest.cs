using macdream.api.database;
using ServiceStack;

namespace macdream.api.endpoints
{
	[Route("/visamcc/update", HttpMethods.Put)]
	public class UpdateVisaMccRequest : IReturn<UpdateVisaMccResponse>
	{
        public long VisaMccId { get; set; }
        public bool isSaving { get; set; }
    }
}