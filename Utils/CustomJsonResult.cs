using System.Collections;

namespace YHCSheng.Utils
{
    public sealed class CustomJsonResult {
        public string Code {get;set;}
        public string Message {get;set;}
        public object Result {get;set;}

        private CustomJsonResult() {}
        public static readonly CustomJsonResult Instance = new CustomJsonResult();
        
        public string GetSuccess(object result) {
            this.Code = "success";
            this.Message = "";
            this.Result = JsonHelper.Instance.ConvertToDictionary(result );

            return JsonHelper.Instance.SerializeObject(this);

        }

        public string GetSuccess(ICollection collection) {
            this.Code = "success";
            this.Message = "";
            this.Result = JsonHelper.Instance.ConvertToDictionary(collection);

            return JsonHelper.Instance.SerializeObject(this);

        }
        
        public string GetError(string message) {
            this.Code = "error";
            this.Message = message;
            this.Result = "";

            return JsonHelper.Instance.SerializeObject(this);
        }


        public string PageError(string message) {
            this.Code = "error";
            this.Message = message;
            this.Result = new {
                entities = "",
                total = 0
            };

            return JsonHelper.Instance.SerializeObject(this);
        }

        public string PageSuccess(ICollection collection, int total = 0) {

            if(total == 0) total = collection.Count;
            this.Code = "error";
            this.Message = "";
            this.Result = new {
                entities = JsonHelper.Instance.ConvertToDictionary(collection),
                total = total
            };

            return JsonHelper.Instance.SerializeObject(this);
        }
    }
}
