using Presentation.UserDefineTypes;

namespace Presentation.ResponseSchema
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string? SuccessMessage { get; set; }
    }

    public class SuccessResponse : Response
    {
        public dynamic? Result { get; set; }
    }

    public class ErrorResponse : Response
    {
        public List<string>? Errors { get; set; }
    }

    public class IdenticalServiceResponse<T>
    {

        public bool Successed { get; set; } = false;
        internal string? _error = string.Empty;
        internal T? _result;
        public T? Result
        {
            get { return _result; }
            set
            {
                if (string.IsNullOrEmpty(_error))
                {
                    Successed = true;
                    _result = value;
                }
                else
                {
                    Successed = false;
                }
            }
        }
        public string? Errors
        {
            get { return _error; }
            set
            {
                Successed = false;
                _error = value;
            }
        }
    }

    public class ResponseMessage : Enumeration<ResponseMessage>
    {
        public static readonly ResponseMessage Success = new(1, "Successfully");
        public static readonly ResponseMessage Failed = new(2, "Operaion Failed");
        public static readonly ResponseMessage DataNotFound = new(3, "Data not found!");
        public static readonly ResponseMessage Created = new(4, "Successfully created");
        public static readonly ResponseMessage Unauthenticate = new(5, "Failed authenticate");
        public static readonly ResponseMessage Authenticate = new(5, "Successfully authenticate");
        public static readonly ResponseMessage InvalidData = new(6, "Invalid Operation");
        public static readonly ResponseMessage BadRequest = new(7, "Invalid Operation");
        public static readonly ResponseMessage Redundancy = new(8, "Redundancy Unaccepted!");
        public static readonly ResponseMessage DeleteSuccessfully = new(9, "Data deleted successfully");

        private ResponseMessage(Int32 value, String name) :
            base(value, name)
        {

        }
    }
}
