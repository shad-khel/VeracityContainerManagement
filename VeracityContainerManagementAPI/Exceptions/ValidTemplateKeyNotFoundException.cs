using System;
using System.Runtime.Serialization;

namespace VeracityContainerManagementAPI.Exceptions
{
    [Serializable]
    internal class ValidTemplateKeyNotFoundException : Exception
    {
        public ValidTemplateKeyNotFoundException()
        {
        }

        public ValidTemplateKeyNotFoundException(string message) : base(message)
        {
        }

        public ValidTemplateKeyNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ValidTemplateKeyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}