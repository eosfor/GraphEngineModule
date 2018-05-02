using System;
using System.Collections.Generic;
using System.Text;

namespace GraphEngineModule.Exceptions
{
    class GEIsNotInitializedException : Exception
    {
        public GEIsNotInitializedException()
        {
        }

        public GEIsNotInitializedException(string message)
            : base(message)
        {
        }

        public GEIsNotInitializedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
