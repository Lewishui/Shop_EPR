using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.DB
{
    public class clsuserinfo
    {
        public string Order_id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string Btype { get; set; }
        public string denglushijian { get; set; }
        public string Createdate { get; set; }
        public string AdminIS { get; set; }
        public string jigoudaima { get; set; }
    }
    public class clsFAinfo
    {
        public string R_id { get; set; }//1
        public string fukuandanwei   { get; set; }//2
        public string huobizijin { get; set; }//3
        public string yinshouzhangkuan { get; set; }//4
        public string fapiaohao { get; set; }//5
        public string Input_Date { get; set; }//6
        public string shijian { get; set; }//7
        public string yingfuzhangkuan { get; set; }//8
        public string beizhu { get; set; }//9
        public string jigoudaima { get; set; }//10
        public string fendianzhangming { get; set; }//11
        public string zhifufangshi { get; set; }//12
        public string dianhuanhao { get; set; }//13
        public string jiluren { get; set; }//14
    }
    public class clsFA_FenDianinfo
    {
        public string R_id { get; set; }//1
        public string fukuandanwei { get; set; }//2
        public string shijian { get; set; }//7
        public string huokuan { get; set; }//3\
        public string fapiaohao { get; set; }//5
        public string beizhu { get; set; }//9
        public string jigoudaima { get; set; }//10
        public string fendianzhangming { get; set; }//11
        public string zhifufangshi { get; set; }//12
        public string dianhuanhao { get; set; }//13
        public string jiluren { get; set; }//14

        public string Input_Date { get; set; }//6

        //明细
        public string shangpinming { get; set; }//6
        public string shuliang { get; set; }//6
        public string jine { get; set; }//6


    }
}
