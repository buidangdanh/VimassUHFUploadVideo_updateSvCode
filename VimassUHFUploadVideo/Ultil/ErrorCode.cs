using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Ultil
{
    internal class ErrorCode
    {
        public const int SUCCESS = 1;
        public const String MES_SUCCESS = "Thành công";

        public const int EXCEPTION = -1;
        public const String MES_EXCEPTION = "Lỗi logic";

        public const int NULL = -2;
        public const String MES_NULL = "null";

        public const int FALSE = 0;
        public const String MES_FALSE = "Lỗi";
    }
}
