using System;

namespace Nancy.WebApi.Help
{
    public class ResponseMessage<T>
    {
        private readonly Func<INancyModule, object> _constructResult;

        public ResponseMessage(Func<INancyModule, object> constructResult)
        {
            _constructResult = constructResult;
        }

        public dynamic GetReponse(INancyModule module)
        {
            return _constructResult.Invoke(module);
        }
    }
}
