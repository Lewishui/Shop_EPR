using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shop.Buiness;
using Shop.Common;
using Shop.DB;

namespace Shop_EPR
{
    public partial class frmAddZongDianXianjinliu : Form
    {
        private string ModelId { get; set; }
        public bool istrue;
        string username;
        List<clsFAinfo> Result;
        clsFAinfo item;

        public frmAddZongDianXianjinliu(string maintype, clsFAinfo obj, string user)
        {
            InitializeComponent();
            username = user;

            Set_NewMethod(maintype, obj);
        }

        private void Set_NewMethod(string maintype, clsFAinfo obj)
        {
            item = new clsFAinfo();
            item = obj;

            if (maintype == "Edit")
            {

                this.Text = "编辑";
                ModelId = obj.R_id;

                this.titleTextBox.Text = item.fukuandanwei;
                textBox1.Text = item.huobizijin;
                textBox2.Text = item.yinshouzhangkuan;
                textBox5.Text = item.fapiaohao;
                dateTimePicker1.Value = Convert.ToDateTime(clsCommHelp.objToDateTime((item.shijian)));
                textBox19.Text = item.yingfuzhangkuan;
                //2
                textBox29.Text = item.beizhu;
                comboBox1.Text = item.jigoudaima;
                textBox26.Text = item.fendianzhangming;
                comboBox2.Text = item.zhifufangshi;
                textBox24.Text = item.dianhuanhao;
                textBox30.Text = item.jiluren;
                
            }
            else
            {
                textBox30.Text = username;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            item = new clsFAinfo();

            #region 读取信息
            NewMethod(item);
            #endregion
            save();
        }
        private void NewMethod(clsFAinfo item)
        {

            //1
            item.fukuandanwei = this.titleTextBox.Text;
            item.huobizijin = textBox1.Text;
            item.yinshouzhangkuan = textBox2.Text;
            item.fapiaohao = textBox5.Text;
            item.shijian = clsCommHelp.objToDateTime1(dateTimePicker1.Text).Replace("/", "");
            item.yingfuzhangkuan = textBox19.Text;
            //2
            item.beizhu = textBox29.Text;

            item.jigoudaima = comboBox1.Text;
            item.fendianzhangming = textBox26.Text;
            item.zhifufangshi = comboBox2.Text;
            item.dianhuanhao = textBox24.Text;
            item.jiluren = textBox30.Text;

            item.Input_Date = DateTime.Now.ToString("yyyyMMdd");


        }
        private void save()
        {
            Result = new List<clsFAinfo>();

            if (ModelId != null && ModelId != "")
                item.R_id = ModelId;

            Result.Add(item);

            clsAllnew BusinessHelp = new clsAllnew();
            if (ModelId == null || ModelId == "")
                BusinessHelp.create_OrderServer(Result);
            else
                BusinessHelp.update_OrderServer(Result);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            item = new clsFAinfo();

            Result = new List<clsFAinfo>();

            #region 读取信息
            NewMethod(item);
            clear_NewMethod();
            #endregion
            Result.Add(item);
            clsAllnew BusinessHelp = new clsAllnew();
            BusinessHelp.create_OrderServer(Result);
        }
        private void clear_NewMethod()
        {
            //1

            textBox1.Text = "";
            textBox2.Text = "";
            textBox5.Text = "";
            textBox19.Text = "";
            //5
            textBox29.Text = "";

            textBox26.Text = "";

            textBox24.Text = "";
            textBox30.Text = "";
            comboBox2.SelectedIndex = 1;
            comboBox1.SelectedIndex = 1;
            titleTextBox.SelectedIndex = 1;

        }
    }
}
