using Microsoft.AspNetCore.Mvc;

namespace webapi._Base
{
    public class BasedController : ControllerBase
    {
        #region COMMON_ROUTES
        public const string CREATE_ROUTE = "add";
        public const string UPDATE_ROUTE = "update/{id}";
        public const string DELETE_ROUTE = "delete/{id}";
        public const string GET_ALL_ROUTE = "getall";
        public const string GET_BY_ID_ROUTE = "getById/{id}";
        #endregion
    }
}
