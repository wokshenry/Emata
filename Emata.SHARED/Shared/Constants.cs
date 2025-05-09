using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emata.Shared.Shared
{
    public static partial class Constants
    {
        public static class StringMaxLength
        {
            public const int DEFAULT_NAME_LENGTH = 100;
            public const int TYPE_NAME_LENGTH = 30;

            public const int PERSON_NAME_LENGTH = 30;
            public const int PERSON_GENDER_LENGTH = 10;


            public const int COMPANY_SHORT_NAME_LENGTH = 10;

            public const int IDENTIFICATION_NUMBER_LENGTH = 30;

            public const int SHORT_DESCRIPTION_LENGTH = 50;

            //phones
            public const int PHONE_COUNTRY_CODE_LENGTH = 5;
            public const int PHONE_NUMBER_LENGTH = 15;

            public const int DEFAULT_DESCRIPTION_LENGTH = 150;

            public const int MEDIUM_DESCRIPTION_LENGTH = 300;

            public const int LONG_DESCRIPTION_LENGTH = 500;
        }
        public static class PaginationHeaders
        {
            public const string TotalCount = "X-Total-Count";
            public const string TotalPages = "X-Total-Pages";
            public const string CurrentPage = "X-Current-Page";
            public const string PageSize = "X-Page-Size";
        }
    }
}
