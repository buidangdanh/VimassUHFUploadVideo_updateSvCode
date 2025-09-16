using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Vpass.Object.ObjectTestDichVu
{
    public class InformationAI
    {
        public int typeAI { get; set; }//0: deepseek 1: chatgpt 2: gemini 3: copilot
        public String textToText { get; set; } //noi dung can hoi
        public String urlImages { get; set; } //truyen '' neu cau hoi khong co anh
    }
}
