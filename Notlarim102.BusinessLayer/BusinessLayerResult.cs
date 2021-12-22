using Notlarim102.Entity.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.BusinessLayer
{
    public class BusinessLayerResult<T> where T:class
    {
        //public List<KeyValuePair<ErrorMessageCode,string>> Errors { get; set; }
        public List<ErrorMessageObject> Errors { get; set; }
        public T Result { get; set; }

        public BusinessLayerResult()
        {
            Errors =new List<ErrorMessageObject>();
        }

        public void AddError(ErrorMessageCode code,string message)
        {
            Errors.Add(new ErrorMessageObject { Code = code, Message = message });
        }
    }
}
