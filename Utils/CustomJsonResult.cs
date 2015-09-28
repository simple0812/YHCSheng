using System.Collections;

namespace YHCSheng.Utils {
    public sealed class CustomJsonResult {
        public static readonly CustomJsonResult Instance = new CustomJsonResult();
        public string Code { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }

        private CustomJsonResult() {}

        public string GetSuccess(object result) {
            Code = "success";
            Message = "";
            Result = JsonHelper.Instance.ConvertToDictionary(result);

            return JsonHelper.Instance.SerializeObject(this);
        }

        public string GetSuccess(string result) {
            Code = "success";
            Message = "";
            Result = result;

            return JsonHelper.Instance.SerializeObject(this);
        }

        public string GetSuccess(int result) {
            Code = "success";
            Message = "";
            Result = result;

            return JsonHelper.Instance.SerializeObject(this);
        }

        public string GetSuccess(bool result) {
            Code = "success";
            Message = "";
            Result = result;

            return JsonHelper.Instance.SerializeObject(this);
        }

        public string GetSuccess(ICollection collection) {
            Code = "success";
            Message = "";
            Result = JsonHelper.Instance.ConvertToDictionary(collection);

            return JsonHelper.Instance.SerializeObject(this);
        }

        public string GetError(string message) {
            Code = "error";
            Message = message;
            Result = "";

            return JsonHelper.Instance.SerializeObject(this);
        }


        public string PageError(string message) {
            Code = "error";
            Message = message;
            Result = new {
                entities = "",
                total = 0
            };

            return JsonHelper.Instance.SerializeObject(this);
        }

        public string PageSuccess(ICollection collection, int total = 0) {
            if (total == 0) total = collection.Count;
            Code = "success";
            Message = "";
            Result = new {
                entities = JsonHelper.Instance.ConvertToDictionary(collection),
                total
            };

            return JsonHelper.Instance.SerializeObject(this);
        }
    }
}