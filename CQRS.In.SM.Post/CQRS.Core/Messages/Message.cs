using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CQRS.Core.Messages
{
    public abstract class Message
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
    }
}