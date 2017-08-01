using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Shop.Buiness;
using Shop.Common;
using Shop.DB;

namespace Shop_EPR
{
    public partial class frmAddFenDianXianjinliu : Form
    {
        private SortableBindingList<clsFA_FenDianinfo> sortablePendingOrderList;
        private string ModelId { get; set; }
        public bool istrue;
        string username;
        List<clsFA_FenDianinfo> Result;
        clsFA_FenDianinfo item;
        int RowRemark = 0;
        int cloumn = 0;
        public frmAddFenDianXianjinliu(string maintype, clsFA_FenDianinfo obj, string user)
        {
            InitializeComponent();
            username = user;

            Set_NewMethod(maintype, obj);
        }
        public class SortableBindingList<T> : BindingList<T>
        {
            private bool isSortedCore = true;
            private ListSortDirection sortDirectionCore = ListSortDirection.Ascending;
            private PropertyDescriptor sortPropertyCore = null;
            private string defaultSortItem;

            public SortableBindingList() : base() { }

            public SortableBindingList(IList<T> list) : base(list) { }

            protected override bool SupportsSortingCore
            {
                get { return true; }
            }

            protected override bool SupportsSearchingCore
            {
                get { return true; }
            }

            protected override bool IsSortedCore
            {
                get { return isSortedCore; }
            }

            protected override ListSortDirection SortDirectionCore
            {
                get { return sortDirectionCore; }
            }

            protected override PropertyDescriptor SortPropertyCore
            {
                get { return sortPropertyCore; }
            }

            protected override int FindCore(PropertyDescriptor prop, object key)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (Equals(prop.GetValue(this[i]), key)) return i;
                }
                return -1;
            }

            protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
            {
                isSortedCore = true;
                sortPropertyCore = prop;
                sortDirectionCore = direction;
                Sort();
            }

            protected override void RemoveSortCore()
            {
                if (isSortedCore)
                {
                    isSortedCore = false;
                    sortPropertyCore = null;
                    sortDirectionCore = ListSortDirection.Ascending;
                    Sort();
                }
            }

            public string DefaultSortItem
            {
                get { return defaultSortItem; }
                set
                {
                    if (defaultSortItem != value)
                    {
                        defaultSortItem = value;
                        Sort();
                    }
                }
            }

            private void Sort()
            {
                List<T> list = (this.Items as List<T>);
                list.Sort(CompareCore);
                ResetBindings();
            }

            private int CompareCore(T o1, T o2)
            {
                int ret = 0;
                if (SortPropertyCore != null)
                {
                    ret = CompareValue(SortPropertyCore.GetValue(o1), SortPropertyCore.GetValue(o2), SortPropertyCore.PropertyType);
                }
                if (ret == 0 && DefaultSortItem != null)
                {
                    PropertyInfo property = typeof(T).GetProperty(DefaultSortItem, BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.IgnoreCase, null, null, new Type[0], null);
                    if (property != null)
                    {
                        ret = CompareValue(property.GetValue(o1, null), property.GetValue(o2, null), property.PropertyType);
                    }
                }
                if (SortDirectionCore == ListSortDirection.Descending) ret = -ret;
                return ret;
            }

            private static int CompareValue(object o1, object o2, Type type)
            {
                if (o1 == null) return o2 == null ? 0 : -1;
                else if (o2 == null) return 1;
                else if (type.IsPrimitive || type.IsEnum) return Convert.ToDouble(o1).CompareTo(Convert.ToDouble(o2));
                else if (type == typeof(DateTime)) return Convert.ToDateTime(o1).CompareTo(o2);
                else return String.Compare(o1.ToString().Trim(), o2.ToString().Trim());
            }
        }

        private void Set_NewMethod(string maintype, clsFA_FenDianinfo obj)
        {
            item = new clsFA_FenDianinfo();
            item = obj;

            if (maintype == "Edit")
            {

                this.Text = "编辑";
                ModelId = obj.R_id;
                this.titleTextBox.Text = item.fukuandanwei;
                textBox5.Text = item.fapiaohao;
                dateTimePicker1.Value = Convert.ToDateTime(clsCommHelp.objToDateTime((item.shijian)));
                //2
                textBox29.Text = item.beizhu;
                comboBox1.Text = item.jigoudaima;
                textBox26.Text = item.fendianzhangming;
                comboBox2.Text = item.zhifufangshi;
                textBox24.Text = item.dianhuanhao;
                textBox30.Text = item.jiluren;
                textBox2.Text = item.huokuan;
                clsAllnew BusinessHelp = new clsAllnew();
                Result = new List<clsFA_FenDianinfo>();
                string start_time = clsCommHelp.objToDateTime1(dateTimePicker1.Value);
                string end_time = clsCommHelp.objToDateTime1(dateTimePicker1.Value);
                Result = BusinessHelp.findFenDianOrder_Server("", start_time, end_time, item.jigoudaima, item.zhifufangshi);

                NewMethod12();

            }
            else
            {
                textBox30.Text = username;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            item = new clsFA_FenDianinfo();

            #region 读取信息
            NewMethod(item);
            #endregion
            save();
        }
        private void NewMethod(clsFA_FenDianinfo item)
        {
            //1
            item.fukuandanwei = this.titleTextBox.Text;

            item.huokuan = textBox2.Text;
            item.fapiaohao = textBox5.Text;
            item.shijian = clsCommHelp.objToDateTime1(dateTimePicker1.Text).Replace("/", "");

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
            if (Result == null)
            {
                if (ModelId != null && ModelId != "")
                    item.R_id = ModelId;
                Result = new List<clsFA_FenDianinfo>();
                Result.Add(item);
            }
            else
            {
                foreach (clsFA_FenDianinfo temp in Result)
                {
               
                    temp.fukuandanwei = item.fukuandanwei;
                    temp.huokuan = item.huokuan;
                    temp.fapiaohao = item.fapiaohao;
                    temp.shijian = item.shijian;
                    temp.beizhu = item.beizhu;
                    temp.jigoudaima = item.jigoudaima;
                    temp.fendianzhangming = item.fendianzhangming;
                    temp.zhifufangshi = item.zhifufangshi;
                    temp.dianhuanhao = item.dianhuanhao;
                    temp.jiluren = item.jiluren;
                    temp.Input_Date = item.Input_Date;
                }
            }
        

           // 

            clsAllnew BusinessHelp = new clsAllnew();
            if (ModelId == null || ModelId == "")
                BusinessHelp.create_FenDianOrderServer(Result);
            else
                BusinessHelp.update_FenDianOrderServer(Result);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            item = new clsFA_FenDianinfo();

            Result = new List<clsFA_FenDianinfo>();

            #region 读取信息
            NewMethod(item);
            clear_NewMethod();
            #endregion
            #region MyRegion
            if (Result == null)
            {
                if (ModelId != null && ModelId != "")
                    item.R_id = ModelId;
                Result = new List<clsFA_FenDianinfo>();
                Result.Add(item);
            }
            else
            {
                foreach (clsFA_FenDianinfo temp in Result)
                {

                    temp.fukuandanwei = item.fukuandanwei;
                    temp.huokuan = item.huokuan;
                    temp.fapiaohao = item.fapiaohao;
                    temp.shijian = item.shijian;
                    temp.beizhu = item.beizhu;
                    temp.jigoudaima = item.jigoudaima;
                    temp.fendianzhangming = item.fendianzhangming;
                    temp.zhifufangshi = item.zhifufangshi;
                    temp.dianhuanhao = item.dianhuanhao;
                    temp.jiluren = item.jiluren;
                    temp.Input_Date = item.Input_Date;
                }


            }
            #endregion
            //Result.Add(item);
            clsAllnew BusinessHelp = new clsAllnew();
            BusinessHelp.create_FenDianOrderServer(Result);
        }
        private void clear_NewMethod()
        {
            //1     
            textBox2.Text = "";
            textBox5.Text = "";
            //5
            textBox29.Text = "";
            textBox26.Text = "";
            textBox24.Text = "";
            textBox30.Text = "";
            comboBox2.SelectedIndex = 1;
            comboBox1.SelectedIndex = 1;
            titleTextBox.SelectedIndex = 1;
        }

        private void addFlightButton1_Click(object sender, EventArgs e)
        {
            if (Result == null)
                Result = new List<clsFA_FenDianinfo>();

            clsFA_FenDianinfo item = new clsFA_FenDianinfo();
            item.Input_Date = DateTime.Now.ToString("yyyyMMdd");
            item.jigoudaima = comboBox1.Text;
            Result.Add(item);

            NewMethod12();
        }

        private void NewMethod12()
        {
            this.dataGridView1.AutoGenerateColumns = false;
            sortablePendingOrderList = new SortableBindingList<clsFA_FenDianinfo>(Result);
            this.bindingSource1.DataSource = sortablePendingOrderList;
            this.dataGridView1.DataSource = this.bindingSource1;
        }



        private void deleteFlightButton_Click(object sender, EventArgs e)
        {
            var row = dataGridView1.Rows[RowRemark];

            var model = row.DataBoundItem as clsFA_FenDianinfo;
            Result.RemoveAt(RowRemark);
            NewMethod12();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RowRemark = e.RowIndex;
            cloumn = e.ColumnIndex;
        }
    }
}
