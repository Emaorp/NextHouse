using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.Contracts.Security
{
    public static class PermissionCodesCatalog
    {
        public const string SHOW_PROPERTIES = "showProperties";
        public const string CREATE_PROPERTIES = "createProperties";
        public const string EDIT_PROPERTIES = "editProperties";
        public const string DELETE_PROPERTIES = "deleteProperties";

        public const string SHOW_REQUESTS = "showRequests";
        public const string CREATE_REQUESTS = "createRequests";
        public const string EDIT_REQUESTS = "editRequests";
        public const string DELETE_REQUESTS = "deleteRequests";

      //public const string SHOW_USERS = "showUsers";
        public const string CREATE_USERS = "createUsers";
        //public const string EDIT_USERS = "editUsers";
        //public const string DELETE_USERS = "deleteUsers";

        public readonly record struct PermissionSeed(string Code, string Description, string Module);

    }
}
