using macdream.api.database;

namespace macdream.api.endpoints
{
	public class UpdateVisaMccResponse
	{
        public long VisaMccId { get; set; }
        public VisaMccEnum VisaMcc { get; set; }
        public bool isSaving { get; set; }
	}
}