using NextHouse.Domain.Entities.Request;

namespace NextHouse.Application.UseCases.PropertyRequest.Queries.GetRequestByAgentId
{
    public class GetPropertyRequestByAgentIdResponseDTO
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public string PropertyTitle { get; set; } = string.Empty;
        public string ApplicationsName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public RequestStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}