using System;
using System.Runtime.Serialization;

namespace eShopSolution.Utilities.Exceptions
{
    [Serializable]
    public class EShopSolutionException : Exception
    {
        public EShopSolutionException()
        {
        }

        public EShopSolutionException(string message) : base(message)
        {
        }

        public EShopSolutionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EShopSolutionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}