using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Shop.Buiness;
using Shop.Common;
using Shop.DB;
using WeifenLuo.WinFormsUI.Docking;

namespace Shop_EPR
{
    public partial class frmFenDianJiZhangLuru : DockContent
    {
        private SortableBindingList<clsFA_FenDianinfo> sortablePendingOrderList;
        public log4net.ILog ProcessLogger;
        public log4net.ILog ExceptionLogger;
        int RowRemark = 0;
        int cloumn = 0;
        private string ipadress;
        string guidangren;
        List<clsFA_FenDianinfo> OrderResults;
        public frmFenDianJiZhangLuru(string username)
        {
            InitializeComponent();
            string path = AppDomain.CurrentDomain.BaseDirectory + "System\\IP.txt";
            guidangren = username;
            string[] fileText = File.ReadAllLines(path);
            ipadress = "mongodb://" + fileText[0];
        }

        private void btbew_Click(object sender, EventArgs e)
        {


            var form = new frmAddFenDianXianjinliu("create", null, guidangren);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {

                InitializeDataSource();
                //InitializeDataGridView();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];

            if (column == editColumn1)
            {
                var row = dataGridView1.Rows[e.RowIndex];

                var model = row.DataBoundItem as clsFA_FenDianinfo;

                var form = new frmAddFenDianXianjinliu("Edit", model, guidangren);
                if (form.ShowDialog() == DialogResult.Yes)
                {
                    BeginActive();
                }


            }

            else if (column == deleteColumn1)
            {

                {
                    var row = dataGridView1.Rows[e.RowIndex];
                    var model = row.DataBoundItem as clsFA_FenDianinfo;
                    string msg = string.Format("确定删除餐厅<{0}>？", model.fukuandanwei);

                    if (MessageHelper.DeleteConfirm(msg))
                    {
                        clsAllnew BusinessHelp = new clsAllnew();

                        BusinessHelp.delete_FenDianOrderServer(model);
                        BeginActive();
                    }
                }

            }
        }
        public void BeginActive()
        {
            InitializeDataSource();
            //pager1.Bind();
        }
        private void InitializeDataSource()
        {

            clsAllnew BusinessHelp = new clsAllnew();
            string start_time = clsCommHelp.objToDateTime1(dateTimePicker1.Text);
            string end_time = clsCommHelp.objToDateTime1(dateTimePicker2.Text);
            string jigoudaima = comboBox1.Text;
            string zhifufangshi = comboBox2.Text;


            OrderResults = BusinessHelp.findFenDianOrder_Server(this.keywordTextBox.Text, start_time, end_time, jigoudaima, zhifufangshi);

            var maichanglist = this.OrderResults.FindAll(o => o.R_id != null);
            var distinctProduct = maichanglist.Distinct(new FenDianIdComparer());
            OrderResults = new List<clsFA_FenDianinfo>();
            OrderResults = distinctProduct.ToList();

            //if (Result)
            this.dataGridView1.AutoGenerateColumns = false;
            sortablePendingOrderList = new SortableBindingList<clsFA_FenDianinfo>(OrderResults);
            this.bindingSource1.DataSource = sortablePendingOrderList;
            this.dataGridView1.DataSource = this.bindingSource1;
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

        private void btfind_Click(object sender, EventArgs e)
        {
            InitializeDataSource();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            InitializeDataSource();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Sorry , No Data Output !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".csv";
            saveFileDialog.Filter = "csv|*.csv";
            string strFileName = "PINGAN  FA System Info" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            saveFileDialog.FileName = strFileName;
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                strFileName = saveFileDialog.FileName.ToString();
            }
            else
            {
                return;
            }
            FileStream fa = new FileStream(strFileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(fa, Encoding.Unicode);
            string delimiter = "\t";
            string strHeader = "";
            for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
            {
                strHeader += this.dataGridView1.Columns[i].HeaderText + delimiter;
            }
            sw.WriteLine(strHeader);

            //output rows data
            for (int j = 0; j < this.dataGridView1.Rows.Count; j++)
            {
                string strRowValue = "";

                for (int k = 0; k < this.dataGridView1.Columns.Count; k++)
                {
                    if (this.dataGridView1.Rows[j].Cells[k].Value != null)
                    {
                        strRowValue += this.dataGridView1.Rows[j].Cells[k].Value.ToString().Replace("\r\n", " ").Replace("\n", "") + delimiter;
                        if (this.dataGridView1.Rows[j].Cells[k].Value.ToString() == "LIP201507-35")
                        {

                        }

                    }
                    else
                    {
                        strRowValue += this.dataGridView1.Rows[j].Cells[k].Value + delimiter;
                    }
                }
                sw.WriteLine(strRowValue);
            }
            sw.Close();
            fa.Close();
            MessageBox.Show("Dear User, Down File  Successful ！", "System", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndex = 0;
            this.comboBox2.SelectedIndex = 0;

            keywordTextBox.Text = "";
            OrderResults = new List<clsFA_FenDianinfo>();
            this.bindingSource1.DataSource = null;
            this.dataGridView1.DataSource = this.bindingSource1;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            {

                if (comboBox1.Text != null && comboBox1.Text.Length > 0)
                {
                    string msg = string.Format("确定删除分店<{0}>？", comboBox1.Text);

                    if (MessageHelper.DeleteConfirm(msg))
                    {
                        clsAllnew BusinessHelp = new clsAllnew();

                        BusinessHelp.delete_FenDianServer(comboBox1.Text);
                        BeginActive();
                    }
                }
                else
                    MessageBox.Show("请选择要删除的 机构代码选项 !");

            }
        }

    }
}
