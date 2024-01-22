using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Ultil
{
    public class SendNotification
    {
        public String idThietBi;
        public String uCodeID;
        public String mcID;

        public String cks;          // MD5("SafFPMPKCjauZ%Ma" + mcID + timeInit + idThietBi + uCodeID)
        public long timeInit;
    }
}
