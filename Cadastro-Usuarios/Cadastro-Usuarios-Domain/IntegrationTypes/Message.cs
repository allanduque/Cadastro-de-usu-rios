using System.Text.Json.Serialization;

namespace Cadastro_Usuarios_Domain.IntegrationTypes
{
    public abstract class Message
    {
        [JsonInclude]
        public string MessageType { get; protected set; }

        [JsonInclude]
        public Guid AggregateId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
