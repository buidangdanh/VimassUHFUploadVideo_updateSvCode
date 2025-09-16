using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VimassUHFUploadVideo.Vpass.Object.ObjectVanTay;
using VimassUHFUploadVideo.Vpass.Object;
using VimassUHFUploadVideo.Ultil;
using VimassUHFUploadVideo.Vpass.Object.ObjectTestDichVu;

namespace VimassUHFUploadVideo.Vpass.GiaoDien
{
    public partial class GoiDichVuTest : Form
    {
        public GoiDichVuTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ObjectGoiDichVuMini o = new ObjectGoiDichVuMini();
                o.funcId = 12801;
                o.device = 2;
                o.currentime = FunCGeneral.timeNow();

                ObjectVTRWE themVanTayRIQ = new ObjectVTRWE();
                themVanTayRIQ.other = 1;

                List<InfomationVID> listD = new List<InfomationVID>();

                InfomationVID infomationVID = new InfomationVID();
                infomationVID.id = "";
                infomationVID.idVid = "3050114";
                infomationVID.uID = "3050003";
                infomationVID.maSoThue = "3050003";
                infomationVID.diaChi = "3050003";
                infomationVID.dienThoai = "3050003";
                infomationVID.email = "3050003";
                infomationVID.gioiTinh = "3050003";
                infomationVID.hoTen = "3050003";
                infomationVID.ngayCapCCCD = "3050003";
                infomationVID.ngaySinh = "3050003";
                infomationVID.quocTich = "3050003";
                infomationVID.soCanCuoc = "3050003";
                infomationVID.soTheBHYT = "3050003";
                infomationVID.tk = "3050003";
                infomationVID.anhDaiDien = "3050003";
                infomationVID.anhCMNDMatTruoc = "3050003";
                infomationVID.anhCMNDMatSau = "3050003";
                infomationVID.faceData = "3050003";
                infomationVID.personName = "3050003";
                infomationVID.chucDanh = "3050003";
                infomationVID.personPosition = 1;
                infomationVID.mcID = "3050003";

                List<FingerData> fingerDatad = new List<FingerData>();
                FingerData fingerData = new FingerData();
                fingerData.name = "1";
                fingerData.emptyID = "0001";
                fingerData.idThietBiFP = "";
                fingerData.data = "02342029A65B8A6A05BE3E6E4D9D4272AB7B0C76BB77147A97EF1C7EB7E12482AFC75909E99BEB1BE2C6D7088068F63CB79AAE36B5AC1C725D480E77AFEEC44BCF10BFBE75290ECCEF439DD4C663B8B407885048F29C7A4607205C30C76E91C51B583C9B03E93BE3D9C379835963690A97B0523E79D0FA065CA0E516C27FE22A727AC434CEA39F8FE9518EB77C066856A4C872DD84049265FB31CED6A9CD828A63D23923B39968414C0178C10000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000004142056A64B8A6A05BE9E6E4D9D4272AA7B8C76BB77147A97EF1C7EB5E1A482ABC5D901E8EBE719E2C54F0CBD6A433DA49EFF1CD6B41C745D48777983DE2D150B79986A55EBF9A95FC54FCE15652B44898094109288BF38F108972EFF2E9243E67B254A55ED6EDE92CA67276173710B41A2637A293462434E616522827C63368A9CCBEC5061CDCC0B5AAC74D4536B1EA71AD436DA69BA56A5D3CE4E9DC6BC4A470B39F4670C68412C0978C100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000BDA5";
                
                fingerDatad.Add(fingerData);
                FingerData fingerData2 = new FingerData();
                fingerData2.name = "2";
                fingerData2.emptyID = "0001";
                fingerData2.idThietBiFP = "";
                fingerData2.data = "03342029A65B8A6A05BE3E6E4D9D4272AB7B0C76BB77147A97EF1C7EB7E12482AFC75909E99BEB1BE2C6D7088068F63CB79AAE36B5AC1C725D480E77AFEEC44BCF10BFBE75290ECCEF439DD4C663B8B407885048F29C7A4607205C30C76E91C51B583C9B03E93BE3D9C379835963690A97B0523E79D0FA065CA0E516C27FE22A727AC434CEA39F8FE9518EB77C066856A4C872DD84049265FB31CED6A9CD828A63D23923B39968414C0178C10000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000004142056A64B8A6A05BE9E6E4D9D4272AA7B8C76BB77147A97EF1C7EB5E1A482ABC5D901E8EBE719E2C54F0CBD6A433DA49EFF1CD6B41C745D48777983DE2D150B79986A55EBF9A95FC54FCE15652B44898094109288BF38F108972EFF2E9243E67B254A55ED6EDE92CA67276173710B41A2637A293462434E616522827C63368A9CCBEC5061CDCC0B5AAC74D4536B1EA71AD436DA69BA56A5D3CE4E9DC6BC4A470B39F4670C68412C0978C100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000BDA5";
                fingerDatad.Add(fingerData2);
                FingerData fingerData3= new FingerData();
                fingerData3.name = "3";
                fingerData3.emptyID = "0001";
                fingerData3.idThietBiFP = "";
                fingerData3.data = "04342029A65B8A6A05BE3E6E4D9D4272AB7B0C76BB77147A97EF1C7EB7E12482AFC75909E99BEB1BE2C6D7088068F63CB79AAE36B5AC1C725D480E77AFEEC44BCF10BFBE75290ECCEF439DD4C663B8B407885048F29C7A4607205C30C76E91C51B583C9B03E93BE3D9C379835963690A97B0523E79D0FA065CA0E516C27FE22A727AC434CEA39F8FE9518EB77C066856A4C872DD84049265FB31CED6A9CD828A63D23923B39968414C0178C10000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000004142056A64B8A6A05BE9E6E4D9D4272AA7B8C76BB77147A97EF1C7EB5E1A482ABC5D901E8EBE719E2C54F0CBD6A433DA49EFF1CD6B41C745D48777983DE2D150B79986A55EBF9A95FC54FCE15652B44898094109288BF38F108972EFF2E9243E67B254A55ED6EDE92CA67276173710B41A2637A293462434E616522827C63368A9CCBEC5061CDCC0B5AAC74D4536B1EA71AD436DA69BA56A5D3CE4E9DC6BC4A470B39F4670C68412C0978C100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000BDA5";


                fingerDatad.Add(fingerData3);
                infomationVID.fingerData = fingerDatad;
                listD.Add(infomationVID);
                themVanTayRIQ.listPeople = listD;
                o.data = JsonConvert.SerializeObject(themVanTayRIQ);
                textBox8.Text = "http://192.168.1.39:58080/autobank/services/vimassTool/dieuPhoi";
                String url = textBox8.Text.Trim();
                var json = JsonConvert.SerializeObject(o);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);
                System.Diagnostics.Debug.WriteLine("V reques" + json.ToString());
                if (response != null && response.msgCode == 1)
                {




                }
                else
                {



                }

            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ObjectGoiDichVuMini o = new ObjectGoiDichVuMini();
                o.funcId = 129;
                o.device = 2;
                o.currentime = FunCGeneral.timeNow();

                ObjXacThucTuDienThoai objXacThucTuDienThoai = new ObjXacThucTuDienThoai();
                objXacThucTuDienThoai.type= 1;
                objXacThucTuDienThoai.idFP = "F1730964221797QV3X";


                ObjectFPRequest objectFPRequest = new ObjectFPRequest();
                objectFPRequest.idVid = "3089783";
                objectFPRequest.personName = "";



                FingerData fingerData2 = new FingerData();
                fingerData2.name = "1";
                fingerData2.emptyID = "0001";
                fingerData2.idThietBiFP = "";
                fingerData2.data = "04142059A65B8A6A05BEBE6E4D9D4272AB7B0C76BB77147A97EF1C7EB7E12482AFC75909E99BEB1BE2C6D7088068F63CB79AAE36B5AC1C725D480E77AFEEC44BCF10BFBE75290ECCEF439DD4C663B8B407885048F29C7A4607205C30C76E91C51B583C9B03E93BE3D9C379835963690A97B0523E79D0FA065CA0E516C27FE22A727AC434CEA39F8FE9518EB77C066856A4C872DD84049265FB31CED6A9CD828A63D23923B39968414C0178C10000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000004142056A64B8A6A05BE9E6E4D9D4272AA7B8C76BB77147A97EF1C7EB5E1A482ABC5D901E8EBE719E2C54F0CBD6A433DA49EFF1CD6B41C745D48777983DE2D150B79986A55EBF9A95FC54FCE15652B44898094109288BF38F108972EFF2E9243E67B254A55ED6EDE92CA67276173710B41A2637A293462434E616522827C63368A9CCBEC5061CDCC0B5AAC74D4536B1EA71AD436DA69BA56A5D3CE4E9DC6BC4A470B39F4670C68412C0978C100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000BDA5";

             
                objXacThucTuDienThoai.fingerData = fingerData2;
                objXacThucTuDienThoai.thongTinNguoi = objectFPRequest;
            
                o.data = JsonConvert.SerializeObject(objXacThucTuDienThoai);
          
                String url = textBox8.Text.Trim();
                var json = JsonConvert.SerializeObject(o);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);
                System.Diagnostics.Debug.WriteLine("V reques" + json.ToString());
                if (response != null && response.msgCode == 1)
                {




                }
                else
                {



                }

            }
            catch
            {

            }

        }

        private void GoiDichVuTest_Load(object sender, EventArgs e)
        {
            textBox8.Text = "http://192.168.1.45:58080/autobank/services/vimassTool/dieuPhoi";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                ObjectGoiDichVuMini o = new ObjectGoiDichVuMini();
                o.funcId = 1;
                o.device = 2;
                o.currentime = FunCGeneral.timeNow();

                ObjRequestVimassAI objXacThucTuDienThoai = new ObjRequestVimassAI();
            
                InformationAI informationAI = new InformationAI();
                informationAI.typeAI = 0;
                informationAI.textToText = "Thông tin THCS Đại Đồng ở Thạch Thất";
                informationAI.urlImages = "";

                objXacThucTuDienThoai.cauHoi = informationAI;

                InformationAccount informationAccount = new InformationAccount();
                informationAccount.vID = "";
                informationAccount.phone = "";
                objXacThucTuDienThoai.nguoiHoi = informationAccount;

                o.data = JsonConvert.SerializeObject(objXacThucTuDienThoai);

                String url = textBox8.Text.Trim();
                var json = JsonConvert.SerializeObject(o);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);
                System.Diagnostics.Debug.WriteLine("V reques" + json.ToString());
                if (response != null && response.msgCode == 1)
                {




                }
                else
                {



                }

            }
            catch
            {

            }
        }
    }
}
