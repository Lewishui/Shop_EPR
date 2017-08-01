using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Shop.DB;

namespace Shop.Buiness
{
    public class clsAllnew
    {
        string connectionString = "mongodb://127.0.0.1";
        string DB_NAME = "FA_shop_PT";


        public clsAllnew()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "System\\IP.txt";

            string[] fileText = File.ReadAllLines(path);
            connectionString = "mongodb://" + fileText[0];



        }

        public void createUser_Server(List<clsuserinfo> AddMAPResult)
        {
            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase db1 = server.GetDatabase(DB_NAME);
            MongoCollection collection1 = db1.GetCollection("FA_shop_User");
            MongoCollection<BsonDocument> employees1 = db1.GetCollection<BsonDocument>("FA_shop_User");

            //  collection1.RemoveAll();
            if (AddMAPResult == null)
            {
                MessageBox.Show("No Data  input Sever", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (clsuserinfo item in AddMAPResult)
            {

                QueryDocument query = new QueryDocument("name", item.name);
                collection1.Remove(query);
                BsonDocument fruit_1 = new BsonDocument
                 { 
                 { "name", item.name },
                 { "password", item.password },
                 { "Createdate", DateTime.Now.ToString("yyyy/MM/dd/HH")}, 
                 { "Btype", item.Btype} ,
                  { "denglushijian", item.denglushijian} ,
                   { "jigoudaima", item.jigoudaima} ,
                 { "AdminIS", item.AdminIS} 
                 };
                collection1.Insert(fruit_1);
            }
        }


        public void updateLoginTime_Server(List<clsuserinfo> AddMAPResult)
        {

            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase db1 = server.GetDatabase(DB_NAME);
            MongoCollection collection1 = db1.GetCollection("FA_shop_User");
            MongoCollection<BsonDocument> employees1 = db1.GetCollection<BsonDocument>("FA_shop_User");

            //  collection1.RemoveAll();
            if (AddMAPResult == null)
            {
                MessageBox.Show("No Data  input Sever", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (clsuserinfo item in AddMAPResult)
            {
                QueryDocument query = new QueryDocument("name", item.name);
                var update = Update.Set("denglushijian", item.denglushijian.Trim());
                collection1.Update(query, update);
            }
        }
        public void lock_Userpassword_Server(List<clsuserinfo> AddMAPResult)
        {

            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase db1 = server.GetDatabase(DB_NAME);
            MongoCollection collection1 = db1.GetCollection("FA_shop_User");
            MongoCollection<BsonDocument> employees1 = db1.GetCollection<BsonDocument>("FA_shop_User");

            if (AddMAPResult == null)
            {
                MessageBox.Show("No Data  input Sever", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (clsuserinfo item in AddMAPResult)
            {
                QueryDocument query = new QueryDocument("name", item.name);
                var update = Update.Set("Btype", item.Btype.Trim());
                collection1.Update(query, update);
            }
        }
        public List<clsuserinfo> ReadUserlistfromServer()
        {

            #region Read  database info server
            try
            {
                List<clsuserinfo> ClaimReport_Server = new List<clsuserinfo>();

                MongoServer server = MongoServer.Create(connectionString);
                MongoDatabase db1 = server.GetDatabase(DB_NAME);
                MongoCollection collection1 = db1.GetCollection("FA_shop_User");
                MongoCollection<BsonDocument> employees = db1.GetCollection<BsonDocument>("FA_shop_User");

                foreach (BsonDocument emp in employees.FindAll())
                {
                    clsuserinfo item = new clsuserinfo();

                    #region 数据
                    if (emp.Contains("_id"))
                        item.Order_id = (emp["_id"].ToString());
                    if (emp.Contains("name"))
                        item.name = (emp["name"].AsString);
                    if (emp.Contains("password"))
                        item.password = (emp["password"].ToString());
                    if (emp.Contains("Btype"))
                        item.Btype = (emp["Btype"].AsString);
                    if (emp.Contains("denglushijian"))
                        item.denglushijian = (emp["denglushijian"].AsString);
                    if (emp.Contains("Createdate"))
                        item.Createdate = (emp["Createdate"].AsString);
                    if (emp.Contains("AdminIS"))
                        item.AdminIS = (emp["AdminIS"].AsString);

                    if (emp.Contains("jigoudaima"))
                        item.jigoudaima = (emp["jigoudaima"].AsString);

                    #endregion
                    ClaimReport_Server.Add(item);
                }
                return ClaimReport_Server;

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
                return null;
                throw ex;
            }
            #endregion
        }
        public void deleteUSER(string name)
        {

            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase db1 = server.GetDatabase(DB_NAME);
            MongoCollection collection1 = db1.GetCollection("FA_shop_User");
            MongoCollection<BsonDocument> employees = db1.GetCollection<BsonDocument>("FA_shop_User");

            if (name == null)
            {
                MessageBox.Show("No Data  input Sever", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            QueryDocument query = new QueryDocument("name", name);

            collection1.Remove(query);
        }

        public void changeUserpassword_Server(List<clsuserinfo> AddMAPResult)
        {

            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase db1 = server.GetDatabase(DB_NAME);
            MongoCollection collection1 = db1.GetCollection("FA_shop_User");
            MongoCollection<BsonDocument> employees1 = db1.GetCollection<BsonDocument>("FA_shop_User");

            if (AddMAPResult == null)
            {
                MessageBox.Show("No Data  input Sever", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (clsuserinfo item in AddMAPResult)
            {
                QueryDocument query = new QueryDocument("name", item.name);
                var update = Update.Set("password", item.password.Trim());
                collection1.Update(query, update);
            }
        }
        public List<clsuserinfo> findUser(string findtext)
        {

            #region Read  database info server
            try
            {
                List<clsuserinfo> ClaimReport_Server = new List<clsuserinfo>();

                MongoServer server = MongoServer.Create(connectionString);
                MongoDatabase db1 = server.GetDatabase(DB_NAME);
                MongoCollection collection1 = db1.GetCollection("FA_shop_User");
                MongoCollection<BsonDocument> employees = db1.GetCollection<BsonDocument>("FA_shop_User");

                var query = new QueryDocument("name", findtext);

                foreach (BsonDocument emp in employees.Find(query))
                {
                    clsuserinfo item = new clsuserinfo();

                    #region 数据
                    if (emp.Contains("_id"))
                        item.Order_id = (emp["_id"].ToString());
                    if (emp.Contains("name"))
                        item.name = (emp["name"].AsString);
                    if (emp.Contains("password"))
                        item.password = (emp["password"].ToString());
                    if (emp.Contains("Btype"))
                        item.Btype = (emp["Btype"].AsString);
                    if (emp.Contains("denglushijian"))
                        item.denglushijian = (emp["denglushijian"].AsString);
                    if (emp.Contains("Createdate"))
                        item.Createdate = (emp["Createdate"].AsString);
                    if (emp.Contains("AdminIS"))
                        item.AdminIS = (emp["AdminIS"].AsString);

                    if (emp.Contains("jigoudaima"))
                        item.jigoudaima = (emp["jigoudaima"].AsString);
                    #endregion
                    ClaimReport_Server.Add(item);
                }
                return ClaimReport_Server;

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
                return null;

                throw ex;
            }
            #endregion
        }
        public void create_OrderServer(List<clsFAinfo> AddMAPResult)
        {

            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase db1 = server.GetDatabase(DB_NAME);
            MongoCollection collection1 = db1.GetCollection("FA_shop_PT_Order");
            MongoCollection<BsonDocument> employees1 = db1.GetCollection<BsonDocument>("FA_shop_PT_Order");

            if (AddMAPResult == null)
            {
                MessageBox.Show("No Data  input Sever", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (clsFAinfo item in AddMAPResult)
            {
                //QueryDocument query = new QueryDocument("name", item.name);

                //var dd = Query.And(Query.EQ("fukuandanwei", item.fukuandanwei), Query.EQ("weituoren", item.weituoren), Query.EQ("shouji2", item.shouji2));//同时满足多个条件

                //collection1.Remove(dd);
                #region 集合
                BsonDocument fruit_1 = new BsonDocument
                 { 
                 { "fukuandanwei", item.fukuandanwei },
                 { "huobizijin", item.huobizijin },                 
                 { "yinshouzhangkuan", item.yinshouzhangkuan} ,
                 { "fapiaohao", item.fapiaohao} ,
                 { "shijian", item.shijian} ,
                 { "yingfuzhangkuan", item.yingfuzhangkuan} ,
                 { "beizhu", item.beizhu }, 
                 { "jigoudaima", item.jigoudaima }, 
                 { "fendianzhangming", item.fendianzhangming }, 
                 { "zhifufangshi", item.zhifufangshi }, 
                 { "dianhuanhao", item.dianhuanhao }, 
                 { "jiluren", item.jiluren }, 
                 { "Input_Date", item.Input_Date}
                 };
                #endregion

                collection1.Insert(fruit_1);
            }
        }
        public void update_OrderServer(List<clsFAinfo> AddMAPResult)
        {

            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase db1 = server.GetDatabase(DB_NAME);
            MongoCollection collection1 = db1.GetCollection("FA_shop_PT_Order");
            MongoCollection<BsonDocument> employees = db1.GetCollection<BsonDocument>("FA_shop_PT_Order");

            //  collection1.RemoveAll();
            if (AddMAPResult == null)
            {
                MessageBox.Show("No Data  input Sever", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (clsFAinfo item in AddMAPResult)
            {

                IMongoQuery query = Query.EQ("_id", new ObjectId(item.R_id));
                //collection.Remove(query);

                //QueryDocument query = new QueryDocument("name", item.name);
                //var query = Query.And(Query.EQ("fapiaohao", item.fapiaohao), Query.EQ("danganhao", item.danganhao), Query.EQ("jigoudaima", item.jigoudaima));//同时满足多个条件

                #region 集合

                var update = Update.Set("fukuandanwei", item.fukuandanwei.Trim());
                collection1.Update(query, update);

                if (item.huobizijin != null)
                {
                    update = Update.Set("huobizijin", item.huobizijin);
                    collection1.Update(query, update);
                }
                if (item.yinshouzhangkuan != null)
                {
                    update = Update.Set("yinshouzhangkuan", item.yinshouzhangkuan);
                    collection1.Update(query, update);
                }
                if (item.fapiaohao != null)
                {
                    update = Update.Set("fapiaohao", item.fapiaohao);
                    collection1.Update(query, update);
                }
                update = Update.Set("Input_Date", item.Input_Date);
                collection1.Update(query, update);
                if (item.shijian != null)
                {
                    update = Update.Set("shijian", item.shijian);
                    collection1.Update(query, update);
                }
                if (item.yingfuzhangkuan != null)
                {
                    update = Update.Set("yingfuzhangkuan", item.yingfuzhangkuan);
                    collection1.Update(query, update);
                }
                if (item.beizhu != null)
                {
                    update = Update.Set("beizhu", item.beizhu);
                    collection1.Update(query, update);
                }
                if (item.jigoudaima != null)
                {
                    update = Update.Set("jigoudaima", item.jigoudaima);
                    collection1.Update(query, update);
                }
                if (item.fendianzhangming != null)
                {
                    update = Update.Set("fendianzhangming", item.fendianzhangming);
                    collection1.Update(query, update);
                }
                if (item.zhifufangshi != null)
                {
                    update = Update.Set("zhifufangshi", item.zhifufangshi);
                    collection1.Update(query, update);
                }
                if (item.dianhuanhao != null)
                {
                    update = Update.Set("dianhuanhao", item.dianhuanhao);
                    collection1.Update(query, update);
                }
                if (item.jiluren != null)
                {
                    update = Update.Set("jiluren", item.jiluren);
                    collection1.Update(query, update);
                }
                #endregion

            }
        }
        public void delete_OrderServer(clsFAinfo AddMAPResult)
        {

            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase db1 = server.GetDatabase(DB_NAME);
            MongoCollection collection1 = db1.GetCollection("FA_shop_PT_Order");
            MongoCollection<BsonDocument> employees = db1.GetCollection<BsonDocument>("FA_shop_PT_Order");

            if (AddMAPResult.R_id != null && AddMAPResult.R_id.Length > 0)
            {
                IMongoQuery query = Query.EQ("_id", new ObjectId(AddMAPResult.R_id));
                collection1.Remove(query);
            }
        }

        public List<clsFAinfo> findOrder_Server(string kettext, string start_time, string end_time, string jigoudaima, string zhifufangshi)
        {

            #region Read  database info server
            try
            {
                List<clsFAinfo> ClaimReport_Server = new List<clsFAinfo>();

                MongoServer server = MongoServer.Create(connectionString);
                MongoDatabase db1 = server.GetDatabase(DB_NAME);
                MongoCollection collection1 = db1.GetCollection("FA_shop_PT_Order");
                MongoCollection<BsonDocument> employees = db1.GetCollection<BsonDocument>("FA_shop_PT_Order");

                var query = new QueryDocument("jigoudaima", kettext);
                //    var dd = Query.And(Query.EQ("jigoudaima", jigoudaima), Query.EQ("fapiaoleixing", fapiaoleixing));//同时满足多个条件
                if (kettext != "")
                {
                    foreach (BsonDocument emp in employees.Find(query))
                    {
                        clsFAinfo item = new clsFAinfo();

                        #region 数据
                        if (emp.Contains("_id"))
                            item.R_id = (emp["_id"].ToString());
                        if (emp.Contains("fukuandanwei"))
                            item.fukuandanwei = (emp["fukuandanwei"].ToString());
                        if (emp.Contains("huobizijin"))
                            item.huobizijin = (emp["huobizijin"].ToString());
                        if (emp.Contains("yinshouzhangkuan"))
                            item.yinshouzhangkuan = (emp["yinshouzhangkuan"].ToString());
                        if (emp.Contains("fapiaohao"))
                            item.fapiaohao = (emp["fapiaohao"].AsString);
                        if (emp.Contains("Input_Date"))
                            item.Input_Date = (emp["Input_Date"].AsString);
                        if (emp.Contains("shijian"))
                            item.shijian = (emp["shijian"].AsString);
                        if (emp.Contains("yingfuzhangkuan"))
                            item.yingfuzhangkuan = (emp["yingfuzhangkuan"].AsString);

                        if (emp.Contains("beizhu"))
                            item.beizhu = (emp["beizhu"].ToString());
                        if (emp.Contains("jigoudaima"))
                            item.jigoudaima = (emp["jigoudaima"].ToString());
                        if (emp.Contains("fendianzhangming"))
                            item.fendianzhangming = (emp["fendianzhangming"].ToString());
                        if (emp.Contains("zhifufangshi"))
                            item.zhifufangshi = (emp["zhifufangshi"].ToString());
                        if (emp.Contains("dianhuanhao"))
                            item.dianhuanhao = (emp["dianhuanhao"].AsString);
                        if (emp.Contains("jiluren"))
                            item.jiluren = (emp["jiluren"].AsString);


                        #endregion

                        ClaimReport_Server.Add(item);
                    }
                    query = new QueryDocument("beizhu", kettext);
                    foreach (BsonDocument emp in employees.Find(query))
                    {
                        clsFAinfo item = new clsFAinfo();

                        #region 数据
                        if (emp.Contains("_id"))
                            item.R_id = (emp["_id"].ToString());
                        if (emp.Contains("fukuandanwei"))
                            item.fukuandanwei = (emp["fukuandanwei"].ToString());
                        if (emp.Contains("huobizijin"))
                            item.huobizijin = (emp["huobizijin"].ToString());
                        if (emp.Contains("yinshouzhangkuan"))
                            item.yinshouzhangkuan = (emp["yinshouzhangkuan"].ToString());
                        if (emp.Contains("fapiaohao"))
                            item.fapiaohao = (emp["fapiaohao"].AsString);
                        if (emp.Contains("Input_Date"))
                            item.Input_Date = (emp["Input_Date"].AsString);
                        if (emp.Contains("shijian"))
                            item.shijian = (emp["shijian"].AsString);
                        if (emp.Contains("yingfuzhangkuan"))
                            item.yingfuzhangkuan = (emp["yingfuzhangkuan"].AsString);

                        if (emp.Contains("beizhu"))
                            item.beizhu = (emp["beizhu"].ToString());
                        if (emp.Contains("jigoudaima"))
                            item.jigoudaima = (emp["jigoudaima"].ToString());
                        if (emp.Contains("fendianzhangming"))
                            item.fendianzhangming = (emp["fendianzhangming"].ToString());
                        if (emp.Contains("zhifufangshi"))
                            item.zhifufangshi = (emp["zhifufangshi"].ToString());
                        if (emp.Contains("dianhuanhao"))
                            item.dianhuanhao = (emp["dianhuanhao"].AsString);
                        if (emp.Contains("jiluren"))
                            item.jiluren = (emp["jiluren"].AsString);


                        #endregion


                        ClaimReport_Server.Add(item);
                    }
                    query = new QueryDocument("fendianzhangming", kettext);
                    foreach (BsonDocument emp in employees.Find(query))
                    {
                        clsFAinfo item = new clsFAinfo();
                        #region 数据
                        if (emp.Contains("_id"))
                            item.R_id = (emp["_id"].ToString());
                        if (emp.Contains("fukuandanwei"))
                            item.fukuandanwei = (emp["fukuandanwei"].ToString());
                        if (emp.Contains("huobizijin"))
                            item.huobizijin = (emp["huobizijin"].ToString());
                        if (emp.Contains("yinshouzhangkuan"))
                            item.yinshouzhangkuan = (emp["yinshouzhangkuan"].ToString());
                        if (emp.Contains("fapiaohao"))
                            item.fapiaohao = (emp["fapiaohao"].AsString);
                        if (emp.Contains("Input_Date"))
                            item.Input_Date = (emp["Input_Date"].AsString);
                        if (emp.Contains("shijian"))
                            item.shijian = (emp["shijian"].AsString);
                        if (emp.Contains("yingfuzhangkuan"))
                            item.yingfuzhangkuan = (emp["yingfuzhangkuan"].AsString);

                        if (emp.Contains("beizhu"))
                            item.beizhu = (emp["beizhu"].ToString());
                        if (emp.Contains("jigoudaima"))
                            item.jigoudaima = (emp["jigoudaima"].ToString());
                        if (emp.Contains("fendianzhangming"))
                            item.fendianzhangming = (emp["fendianzhangming"].ToString());
                        if (emp.Contains("zhifufangshi"))
                            item.zhifufangshi = (emp["zhifufangshi"].ToString());
                        if (emp.Contains("dianhuanhao"))
                            item.dianhuanhao = (emp["dianhuanhao"].AsString);
                        if (emp.Contains("jiluren"))
                            item.jiluren = (emp["jiluren"].AsString);


                        #endregion


                        ClaimReport_Server.Add(item);
                    }
                    query = new QueryDocument("zhifufangshi", kettext);
                    foreach (BsonDocument emp in employees.Find(query))
                    {
                        clsFAinfo item = new clsFAinfo();

                        #region 数据
                        if (emp.Contains("_id"))
                            item.R_id = (emp["_id"].ToString());
                        if (emp.Contains("fukuandanwei"))
                            item.fukuandanwei = (emp["fukuandanwei"].ToString());
                        if (emp.Contains("huobizijin"))
                            item.huobizijin = (emp["huobizijin"].ToString());
                        if (emp.Contains("yinshouzhangkuan"))
                            item.yinshouzhangkuan = (emp["yinshouzhangkuan"].ToString());
                        if (emp.Contains("fapiaohao"))
                            item.fapiaohao = (emp["fapiaohao"].AsString);
                        if (emp.Contains("Input_Date"))
                            item.Input_Date = (emp["Input_Date"].AsString);
                        if (emp.Contains("shijian"))
                            item.shijian = (emp["shijian"].AsString);
                        if (emp.Contains("yingfuzhangkuan"))
                            item.yingfuzhangkuan = (emp["yingfuzhangkuan"].AsString);

                        if (emp.Contains("beizhu"))
                            item.beizhu = (emp["beizhu"].ToString());
                        if (emp.Contains("jigoudaima"))
                            item.jigoudaima = (emp["jigoudaima"].ToString());
                        if (emp.Contains("fendianzhangming"))
                            item.fendianzhangming = (emp["fendianzhangming"].ToString());
                        if (emp.Contains("zhifufangshi"))
                            item.zhifufangshi = (emp["zhifufangshi"].ToString());
                        if (emp.Contains("dianhuanhao"))
                            item.dianhuanhao = (emp["dianhuanhao"].AsString);
                        if (emp.Contains("jiluren"))
                            item.jiluren = (emp["jiluren"].AsString);


                        #endregion


                        ClaimReport_Server.Add(item);
                    }
                    query = new QueryDocument("dianhuanhao", kettext);
                    foreach (BsonDocument emp in employees.Find(query))
                    {
                        clsFAinfo item = new clsFAinfo();
                        #region 数据
                        if (emp.Contains("_id"))
                            item.R_id = (emp["_id"].ToString());
                        if (emp.Contains("fukuandanwei"))
                            item.fukuandanwei = (emp["fukuandanwei"].ToString());
                        if (emp.Contains("huobizijin"))
                            item.huobizijin = (emp["huobizijin"].ToString());
                        if (emp.Contains("yinshouzhangkuan"))
                            item.yinshouzhangkuan = (emp["yinshouzhangkuan"].ToString());
                        if (emp.Contains("fapiaohao"))
                            item.fapiaohao = (emp["fapiaohao"].AsString);
                        if (emp.Contains("Input_Date"))
                            item.Input_Date = (emp["Input_Date"].AsString);
                        if (emp.Contains("shijian"))
                            item.shijian = (emp["shijian"].AsString);
                        if (emp.Contains("yingfuzhangkuan"))
                            item.yingfuzhangkuan = (emp["yingfuzhangkuan"].AsString);

                        if (emp.Contains("beizhu"))
                            item.beizhu = (emp["beizhu"].ToString());
                        if (emp.Contains("jigoudaima"))
                            item.jigoudaima = (emp["jigoudaima"].ToString());
                        if (emp.Contains("fendianzhangming"))
                            item.fendianzhangming = (emp["fendianzhangming"].ToString());
                        if (emp.Contains("zhifufangshi"))
                            item.zhifufangshi = (emp["zhifufangshi"].ToString());
                        if (emp.Contains("dianhuanhao"))
                            item.dianhuanhao = (emp["dianhuanhao"].AsString);
                        if (emp.Contains("jiluren"))
                            item.jiluren = (emp["jiluren"].AsString);


                        #endregion


                        ClaimReport_Server.Add(item);
                    }
                }
                //if (jigoudaima != "")
                //{
                //    query = new QueryDocument("jigoudaima", jigoudaima);
                //    foreach (BsonDocument emp in employees.Find(query))
                //    {
                //        clsFAinfo item = new clsFAinfo();

                //        #region 数据
                //        if (emp.Contains("_id"))
                //            item.R_id = (emp["_id"].ToString());
                //        if (emp.Contains("fukuandanwei"))
                //            item.fukuandanwei = (emp["fukuandanwei"].ToString());
                //        if (emp.Contains("huobizijin"))
                //            item.huobizijin = (emp["huobizijin"].ToString());
                //        if (emp.Contains("yinshouzhangkuan"))
                //            item.yinshouzhangkuan = (emp["yinshouzhangkuan"].ToString());
                //        if (emp.Contains("fapiaohao"))
                //            item.fapiaohao = (emp["fapiaohao"].AsString);
                //        if (emp.Contains("Input_Date"))
                //            item.Input_Date = (emp["Input_Date"].AsString);
                //        if (emp.Contains("shijian"))
                //            item.shijian = (emp["shijian"].AsString);
                //        if (emp.Contains("yingfuzhangkuan"))
                //            item.yingfuzhangkuan = (emp["yingfuzhangkuan"].AsString);

                //        if (emp.Contains("beizhu"))
                //            item.beizhu = (emp["beizhu"].ToString());
                //        if (emp.Contains("jigoudaima"))
                //            item.jigoudaima = (emp["jigoudaima"].ToString());
                //        if (emp.Contains("fendianzhangming"))
                //            item.fendianzhangming = (emp["fendianzhangming"].ToString());
                //        if (emp.Contains("zhifufangshi"))
                //            item.zhifufangshi = (emp["zhifufangshi"].ToString());
                //        if (emp.Contains("dianhuanhao"))
                //            item.dianhuanhao = (emp["dianhuanhao"].AsString);
                //        if (emp.Contains("jiluren"))
                //            item.jiluren = (emp["jiluren"].AsString);


                //        #endregion


                //        ClaimReport_Server.Add(item);
                //    }


                //}
                //if (zhifufangshi != "")
                //{
                //    query = new QueryDocument("zhifufangshi", zhifufangshi);
                //    foreach (BsonDocument emp in employees.Find(query))
                //    {
                //        clsFAinfo item = new clsFAinfo();

                //        #region 数据
                //        if (emp.Contains("_id"))
                //            item.R_id = (emp["_id"].ToString());
                //        if (emp.Contains("fukuandanwei"))
                //            item.fukuandanwei = (emp["fukuandanwei"].ToString());
                //        if (emp.Contains("huobizijin"))
                //            item.huobizijin = (emp["huobizijin"].ToString());
                //        if (emp.Contains("yinshouzhangkuan"))
                //            item.yinshouzhangkuan = (emp["yinshouzhangkuan"].ToString());
                //        if (emp.Contains("fapiaohao"))
                //            item.fapiaohao = (emp["fapiaohao"].AsString);
                //        if (emp.Contains("Input_Date"))
                //            item.Input_Date = (emp["Input_Date"].AsString);
                //        if (emp.Contains("shijian"))
                //            item.shijian = (emp["shijian"].AsString);
                //        if (emp.Contains("yingfuzhangkuan"))
                //            item.yingfuzhangkuan = (emp["yingfuzhangkuan"].AsString);

                //        if (emp.Contains("beizhu"))
                //            item.beizhu = (emp["beizhu"].ToString());
                //        if (emp.Contains("jigoudaima"))
                //            item.jigoudaima = (emp["jigoudaima"].ToString());
                //        if (emp.Contains("fendianzhangming"))
                //            item.fendianzhangming = (emp["fendianzhangming"].ToString());
                //        if (emp.Contains("zhifufangshi"))
                //            item.zhifufangshi = (emp["zhifufangshi"].ToString());
                //        if (emp.Contains("dianhuanhao"))
                //            item.dianhuanhao = (emp["dianhuanhao"].AsString);
                //        if (emp.Contains("jiluren"))
                //            item.jiluren = (emp["jiluren"].AsString);


                //        #endregion
                //        ClaimReport_Server.Add(item);
                //    }


                //}
                //  query = new QueryDocument("guidangrenzhanghao", guidangren);

                var query1 = Query.And(Query.GTE("shijian", start_time.Replace("/", "")), Query.LTE("shijian", end_time.Replace("/", "")));
                foreach (BsonDocument emp in employees.Find(query1))
                {
                    clsFAinfo item = new clsFAinfo();

                    #region 数据
                    if (emp.Contains("_id"))
                        item.R_id = (emp["_id"].ToString());
                    if (emp.Contains("fukuandanwei"))
                        item.fukuandanwei = (emp["fukuandanwei"].ToString());
                    if (emp.Contains("huobizijin"))
                        item.huobizijin = (emp["huobizijin"].ToString());
                    if (emp.Contains("yinshouzhangkuan"))
                        item.yinshouzhangkuan = (emp["yinshouzhangkuan"].ToString());
                    if (emp.Contains("fapiaohao"))
                        item.fapiaohao = (emp["fapiaohao"].AsString);
                    if (emp.Contains("Input_Date"))
                        item.Input_Date = (emp["Input_Date"].AsString);
                    if (emp.Contains("shijian"))
                        item.shijian = (emp["shijian"].AsString);
                    if (emp.Contains("yingfuzhangkuan"))
                        item.yingfuzhangkuan = (emp["yingfuzhangkuan"].AsString);

                    if (emp.Contains("beizhu"))
                        item.beizhu = (emp["beizhu"].ToString());
                    if (emp.Contains("jigoudaima"))
                        item.jigoudaima = (emp["jigoudaima"].ToString());
                    if (emp.Contains("fendianzhangming"))
                        item.fendianzhangming = (emp["fendianzhangming"].ToString());
                    if (emp.Contains("zhifufangshi"))
                        item.zhifufangshi = (emp["zhifufangshi"].ToString());
                    if (emp.Contains("dianhuanhao"))
                        item.dianhuanhao = (emp["dianhuanhao"].AsString);
                    if (emp.Contains("jiluren"))
                        item.jiluren = (emp["jiluren"].AsString);


                    #endregion

                    if (zhifufangshi != "")
                    {
                        if (!item.zhifufangshi.Contains(zhifufangshi))
                            continue;

                    }
                    if (jigoudaima != "")
                    {
                        if (!item.jigoudaima.Contains(jigoudaima))
                            continue;

                    }


                    ClaimReport_Server.Add(item);
                }

                return ClaimReport_Server;

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
                return null;

                throw ex;
            }
            #endregion
        }
        public void create_FenDianOrderServer(List<clsFA_FenDianinfo> AddMAPResult)
        {

            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase db1 = server.GetDatabase(DB_NAME);
            MongoCollection collection1 = db1.GetCollection("FA_shop_PT_FenDianOrder");
            MongoCollection<BsonDocument> employees1 = db1.GetCollection<BsonDocument>("FA_shop_PT_FenDianOrder");

            if (AddMAPResult == null)
            {
                MessageBox.Show("No Data  input Sever", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (clsFA_FenDianinfo item in AddMAPResult)
            {
                //QueryDocument query = new QueryDocument("name", item.name);

                //var dd = Query.And(Query.EQ("fukuandanwei", item.fukuandanwei), Query.EQ("weituoren", item.weituoren), Query.EQ("shouji2", item.shouji2));//同时满足多个条件

                //collection1.Remove(dd);
                #region 集合
                BsonDocument fruit_1 = new BsonDocument
                 { 
                 { "fukuandanwei", item.fukuandanwei },
                 { "huokuan", item.huokuan },                 
                 { "shangpinming", item.shangpinming} ,
                 { "fapiaohao", item.fapiaohao} ,
                 { "shijian", item.shijian} ,
                 { "shuliang", item.shuliang} ,
                 { "beizhu", item.beizhu }, 
                 { "jigoudaima", item.jigoudaima }, 
                 { "fendianzhangming", item.fendianzhangming }, 
                 { "zhifufangshi", item.zhifufangshi }, 
                 { "dianhuanhao", item.dianhuanhao }, 
                 { "jiluren", item.jiluren }, 
                  { "jine", item.jine }, 
                 { "Input_Date", item.Input_Date}
                 };
                #endregion

                collection1.Insert(fruit_1);
            }
        }
        public void update_FenDianOrderServer(List<clsFA_FenDianinfo> AddMAPResult)
        {

            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase db1 = server.GetDatabase(DB_NAME);
            MongoCollection collection1 = db1.GetCollection("FA_shop_PT_FenDianOrder");
            MongoCollection<BsonDocument> employees = db1.GetCollection<BsonDocument>("FA_shop_PT_FenDianOrder");

            //  collection1.RemoveAll();
            if (AddMAPResult == null)
            {
                MessageBox.Show("No Data  input Sever", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (clsFA_FenDianinfo item in AddMAPResult)
            {

                IMongoQuery query = Query.EQ("_id", new ObjectId(item.R_id));
                //collection.Remove(query);

                //QueryDocument query = new QueryDocument("name", item.name);
                //var query = Query.And(Query.EQ("fapiaohao", item.fapiaohao), Query.EQ("danganhao", item.danganhao), Query.EQ("jigoudaima", item.jigoudaima));//同时满足多个条件

                #region 集合

                var update = Update.Set("fukuandanwei", item.fukuandanwei.Trim());
                collection1.Update(query, update);

                if (item.huokuan != null)
                {
                    update = Update.Set("huokuan", item.huokuan);
                    collection1.Update(query, update);
                }
                if (item.shangpinming != null)
                {
                    update = Update.Set("shangpinming", item.shangpinming);
                    collection1.Update(query, update);
                }
                if (item.fapiaohao != null)
                {
                    update = Update.Set("fapiaohao", item.fapiaohao);
                    collection1.Update(query, update);
                }
                update = Update.Set("Input_Date", item.Input_Date);
                collection1.Update(query, update);
                if (item.shijian != null)
                {
                    update = Update.Set("shijian", item.shijian);
                    collection1.Update(query, update);
                }
                if (item.shuliang != null)
                {
                    update = Update.Set("shuliang", item.shuliang);
                    collection1.Update(query, update);
                }
                if (item.beizhu != null)
                {
                    update = Update.Set("beizhu", item.beizhu);
                    collection1.Update(query, update);
                }
                if (item.jigoudaima != null)
                {
                    update = Update.Set("jigoudaima", item.jigoudaima);
                    collection1.Update(query, update);
                }
                if (item.fendianzhangming != null)
                {
                    update = Update.Set("fendianzhangming", item.fendianzhangming);
                    collection1.Update(query, update);
                }
                if (item.zhifufangshi != null)
                {
                    update = Update.Set("zhifufangshi", item.zhifufangshi);
                    collection1.Update(query, update);
                }
                if (item.dianhuanhao != null)
                {
                    update = Update.Set("dianhuanhao", item.dianhuanhao);
                    collection1.Update(query, update);
                }
                if (item.jiluren != null)
                {
                    update = Update.Set("jiluren", item.jiluren);
                    collection1.Update(query, update);
                }
                if (item.jine != null)
                {
                    update = Update.Set("jine", item.jine);
                    collection1.Update(query, update);
                }
                #endregion

            }
        }
        public List<clsFA_FenDianinfo> findFenDianOrder_Server(string kettext, string start_time, string end_time, string jigoudaima, string zhifufangshi)
        {

            #region Read  database info server
            try
            {
                List<clsFA_FenDianinfo> ClaimReport_Server = new List<clsFA_FenDianinfo>();

                MongoServer server = MongoServer.Create(connectionString);
                MongoDatabase db1 = server.GetDatabase(DB_NAME);
                MongoCollection collection1 = db1.GetCollection("FA_shop_PT_FenDianOrder");
                MongoCollection<BsonDocument> employees = db1.GetCollection<BsonDocument>("FA_shop_PT_FenDianOrder");

                var query = new QueryDocument("jigoudaima", kettext);
                //    var dd = Query.And(Query.EQ("jigoudaima", jigoudaima), Query.EQ("fapiaoleixing", fapiaoleixing));//同时满足多个条件
                if (kettext != "")
                {
                    foreach (BsonDocument emp in employees.Find(query))
                    {
                        clsFA_FenDianinfo item = new clsFA_FenDianinfo();

                        #region 数据
                        if (emp.Contains("_id"))
                            item.R_id = (emp["_id"].ToString());
                        if (emp.Contains("fukuandanwei"))
                            item.fukuandanwei = (emp["fukuandanwei"].ToString());
                        if (emp.Contains("huokuan"))
                            item.huokuan = (emp["huokuan"].ToString());
                        if (emp.Contains("shangpinming"))
                            item.shangpinming = (emp["shangpinming"].ToString());
                        if (emp.Contains("fapiaohao"))
                            item.fapiaohao = (emp["fapiaohao"].AsString);
                        if (emp.Contains("Input_Date"))
                            item.Input_Date = (emp["Input_Date"].AsString);
                        if (emp.Contains("shijian"))
                            item.shijian = (emp["shijian"].AsString);
                        if (emp.Contains("shuliang"))
                            item.shuliang = (emp["shuliang"].AsString);

                        if (emp.Contains("beizhu"))
                            item.beizhu = (emp["beizhu"].ToString());
                        if (emp.Contains("jigoudaima"))
                            item.jigoudaima = (emp["jigoudaima"].ToString());
                        if (emp.Contains("fendianzhangming"))
                            item.fendianzhangming = (emp["fendianzhangming"].ToString());
                        if (emp.Contains("zhifufangshi"))
                            item.zhifufangshi = (emp["zhifufangshi"].ToString());
                        if (emp.Contains("dianhuanhao"))
                            item.dianhuanhao = (emp["dianhuanhao"].AsString);
                        if (emp.Contains("jiluren"))
                            item.jiluren = (emp["jiluren"].AsString);

                        if (emp.Contains("jine"))
                            item.jine = (emp["jine"].AsString);

                        #endregion

                        ClaimReport_Server.Add(item);
                    }
                    query = new QueryDocument("beizhu", kettext);
                    foreach (BsonDocument emp in employees.Find(query))
                    {
                        clsFA_FenDianinfo item = new clsFA_FenDianinfo();

                        #region 数据
                        if (emp.Contains("_id"))
                            item.R_id = (emp["_id"].ToString());
                        if (emp.Contains("fukuandanwei"))
                            item.fukuandanwei = (emp["fukuandanwei"].ToString());
                        if (emp.Contains("huokuan"))
                            item.huokuan = (emp["huokuan"].ToString());
                        if (emp.Contains("shangpinming"))
                            item.shangpinming = (emp["shangpinming"].ToString());
                        if (emp.Contains("fapiaohao"))
                            item.fapiaohao = (emp["fapiaohao"].AsString);
                        if (emp.Contains("Input_Date"))
                            item.Input_Date = (emp["Input_Date"].AsString);
                        if (emp.Contains("shijian"))
                            item.shijian = (emp["shijian"].AsString);
                        if (emp.Contains("shuliang"))
                            item.shuliang = (emp["shuliang"].AsString);

                        if (emp.Contains("beizhu"))
                            item.beizhu = (emp["beizhu"].ToString());
                        if (emp.Contains("jigoudaima"))
                            item.jigoudaima = (emp["jigoudaima"].ToString());
                        if (emp.Contains("fendianzhangming"))
                            item.fendianzhangming = (emp["fendianzhangming"].ToString());
                        if (emp.Contains("zhifufangshi"))
                            item.zhifufangshi = (emp["zhifufangshi"].ToString());
                        if (emp.Contains("dianhuanhao"))
                            item.dianhuanhao = (emp["dianhuanhao"].AsString);
                        if (emp.Contains("jiluren"))
                            item.jiluren = (emp["jiluren"].AsString);

                        if (emp.Contains("jine"))
                            item.jine = (emp["jine"].AsString);

                        #endregion

                        ClaimReport_Server.Add(item);
                    }
                    query = new QueryDocument("fendianzhangming", kettext);
                    foreach (BsonDocument emp in employees.Find(query))
                    {
                        clsFA_FenDianinfo item = new clsFA_FenDianinfo();
                        #region 数据
                        if (emp.Contains("_id"))
                            item.R_id = (emp["_id"].ToString());
                        if (emp.Contains("fukuandanwei"))
                            item.fukuandanwei = (emp["fukuandanwei"].ToString());
                        if (emp.Contains("huokuan"))
                            item.huokuan = (emp["huokuan"].ToString());
                        if (emp.Contains("shangpinming"))
                            item.shangpinming = (emp["shangpinming"].ToString());
                        if (emp.Contains("fapiaohao"))
                            item.fapiaohao = (emp["fapiaohao"].AsString);
                        if (emp.Contains("Input_Date"))
                            item.Input_Date = (emp["Input_Date"].AsString);
                        if (emp.Contains("shijian"))
                            item.shijian = (emp["shijian"].AsString);
                        if (emp.Contains("shuliang"))
                            item.shuliang = (emp["shuliang"].AsString);

                        if (emp.Contains("beizhu"))
                            item.beizhu = (emp["beizhu"].ToString());
                        if (emp.Contains("jigoudaima"))
                            item.jigoudaima = (emp["jigoudaima"].ToString());
                        if (emp.Contains("fendianzhangming"))
                            item.fendianzhangming = (emp["fendianzhangming"].ToString());
                        if (emp.Contains("zhifufangshi"))
                            item.zhifufangshi = (emp["zhifufangshi"].ToString());
                        if (emp.Contains("dianhuanhao"))
                            item.dianhuanhao = (emp["dianhuanhao"].AsString);
                        if (emp.Contains("jiluren"))
                            item.jiluren = (emp["jiluren"].AsString);

                        if (emp.Contains("jine"))
                            item.jine = (emp["jine"].AsString);

                        #endregion

                        ClaimReport_Server.Add(item);
                    }
                    query = new QueryDocument("zhifufangshi", kettext);
                    foreach (BsonDocument emp in employees.Find(query))
                    {
                        clsFA_FenDianinfo item = new clsFA_FenDianinfo();

                        #region 数据
                        if (emp.Contains("_id"))
                            item.R_id = (emp["_id"].ToString());
                        if (emp.Contains("fukuandanwei"))
                            item.fukuandanwei = (emp["fukuandanwei"].ToString());
                        if (emp.Contains("huokuan"))
                            item.huokuan = (emp["huokuan"].ToString());
                        if (emp.Contains("shangpinming"))
                            item.shangpinming = (emp["shangpinming"].ToString());
                        if (emp.Contains("fapiaohao"))
                            item.fapiaohao = (emp["fapiaohao"].AsString);
                        if (emp.Contains("Input_Date"))
                            item.Input_Date = (emp["Input_Date"].AsString);
                        if (emp.Contains("shijian"))
                            item.shijian = (emp["shijian"].AsString);
                        if (emp.Contains("shuliang"))
                            item.shuliang = (emp["shuliang"].AsString);

                        if (emp.Contains("beizhu"))
                            item.beizhu = (emp["beizhu"].ToString());
                        if (emp.Contains("jigoudaima"))
                            item.jigoudaima = (emp["jigoudaima"].ToString());
                        if (emp.Contains("fendianzhangming"))
                            item.fendianzhangming = (emp["fendianzhangming"].ToString());
                        if (emp.Contains("zhifufangshi"))
                            item.zhifufangshi = (emp["zhifufangshi"].ToString());
                        if (emp.Contains("dianhuanhao"))
                            item.dianhuanhao = (emp["dianhuanhao"].AsString);
                        if (emp.Contains("jiluren"))
                            item.jiluren = (emp["jiluren"].AsString);

                        if (emp.Contains("jine"))
                            item.jine = (emp["jine"].AsString);

                        #endregion

                        ClaimReport_Server.Add(item);
                    }
                    query = new QueryDocument("dianhuanhao", kettext);
                    foreach (BsonDocument emp in employees.Find(query))
                    {
                        clsFA_FenDianinfo item = new clsFA_FenDianinfo();
                        #region 数据
                        if (emp.Contains("_id"))
                            item.R_id = (emp["_id"].ToString());
                        if (emp.Contains("fukuandanwei"))
                            item.fukuandanwei = (emp["fukuandanwei"].ToString());
                        if (emp.Contains("huokuan"))
                            item.huokuan = (emp["huokuan"].ToString());
                        if (emp.Contains("shangpinming"))
                            item.shangpinming = (emp["shangpinming"].ToString());
                        if (emp.Contains("fapiaohao"))
                            item.fapiaohao = (emp["fapiaohao"].AsString);
                        if (emp.Contains("Input_Date"))
                            item.Input_Date = (emp["Input_Date"].AsString);
                        if (emp.Contains("shijian"))
                            item.shijian = (emp["shijian"].AsString);
                        if (emp.Contains("shuliang"))
                            item.shuliang = (emp["shuliang"].AsString);

                        if (emp.Contains("beizhu"))
                            item.beizhu = (emp["beizhu"].ToString());
                        if (emp.Contains("jigoudaima"))
                            item.jigoudaima = (emp["jigoudaima"].ToString());
                        if (emp.Contains("fendianzhangming"))
                            item.fendianzhangming = (emp["fendianzhangming"].ToString());
                        if (emp.Contains("zhifufangshi"))
                            item.zhifufangshi = (emp["zhifufangshi"].ToString());
                        if (emp.Contains("dianhuanhao"))
                            item.dianhuanhao = (emp["dianhuanhao"].AsString);
                        if (emp.Contains("jiluren"))
                            item.jiluren = (emp["jiluren"].AsString);

                        if (emp.Contains("jine"))
                            item.jine = (emp["jine"].AsString);

                        #endregion

                        ClaimReport_Server.Add(item);
                    }
                }

                var query1 = Query.And(Query.GTE("shijian", start_time.Replace("/", "")), Query.LTE("shijian", end_time.Replace("/", "")));
                foreach (BsonDocument emp in employees.Find(query1))
                {
                    clsFA_FenDianinfo item = new clsFA_FenDianinfo();

                    #region 数据
                    if (emp.Contains("_id"))
                        item.R_id = (emp["_id"].ToString());
                    if (emp.Contains("fukuandanwei"))
                        item.fukuandanwei = (emp["fukuandanwei"].ToString());
                    if (emp.Contains("huokuan"))
                        item.huokuan = (emp["huokuan"].ToString());
                    if (emp.Contains("shangpinming"))
                        item.shangpinming = (emp["shangpinming"].ToString());
                    if (emp.Contains("fapiaohao"))
                        item.fapiaohao = (emp["fapiaohao"].AsString);
                    if (emp.Contains("Input_Date"))
                        item.Input_Date = (emp["Input_Date"].AsString);
                    if (emp.Contains("shijian"))
                        item.shijian = (emp["shijian"].AsString);
                    if (emp.Contains("shuliang"))
                        item.shuliang = (emp["shuliang"].AsString);

                    if (emp.Contains("beizhu"))
                        item.beizhu = (emp["beizhu"].ToString());
                    if (emp.Contains("jigoudaima"))
                        item.jigoudaima = (emp["jigoudaima"].ToString());
                    if (emp.Contains("fendianzhangming"))
                        item.fendianzhangming = (emp["fendianzhangming"].ToString());
                    if (emp.Contains("zhifufangshi"))
                        item.zhifufangshi = (emp["zhifufangshi"].ToString());
                    if (emp.Contains("dianhuanhao"))
                        item.dianhuanhao = (emp["dianhuanhao"].AsString);
                    if (emp.Contains("jiluren"))
                        item.jiluren = (emp["jiluren"].AsString);

                    if (emp.Contains("jine"))
                        item.jine = (emp["jine"].AsString);

                    #endregion
                    if (zhifufangshi != "")
                    {
                        if (!item.zhifufangshi.Contains(zhifufangshi))
                            continue;

                    }
                    if (jigoudaima != "")
                    {
                        if (!item.jigoudaima.Contains(jigoudaima))
                            continue;

                    }


                    ClaimReport_Server.Add(item);
                }

                return ClaimReport_Server;

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
                return null;

                throw ex;
            }
            #endregion
        }
        public void delete_FenDianOrderServer(clsFA_FenDianinfo AddMAPResult)
        {

            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase db1 = server.GetDatabase(DB_NAME);
            MongoCollection collection1 = db1.GetCollection("FA_shop_PT_FenDianOrder");
            MongoCollection<BsonDocument> employees = db1.GetCollection<BsonDocument>("FA_shop_PT_FenDianOrder");

            if (AddMAPResult.R_id != null && AddMAPResult.R_id.Length > 0)
            {
                IMongoQuery query = Query.EQ("_id", new ObjectId(AddMAPResult.R_id));
                collection1.Remove(query);
            }
        }
        public void delete_FenDianServer(string AddMAPResult)
        {

            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase db1 = server.GetDatabase(DB_NAME);
            MongoCollection collection1 = db1.GetCollection("FA_shop_PT_FenDianOrder");
            MongoCollection<BsonDocument> employees = db1.GetCollection<BsonDocument>("FA_shop_PT_FenDianOrder");

            if (AddMAPResult != null && AddMAPResult.Length > 0)
            {
                var dd = Query.And(Query.EQ("jigoudaima", AddMAPResult));//同时满足多个条件

                collection1.Remove(dd);
            }
        }
        public void delete_ZongDianFenDianServer(string AddMAPResult)
        {

            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase db1 = server.GetDatabase(DB_NAME);
            MongoCollection collection1 = db1.GetCollection("FA_shop_PT_Order");
            MongoCollection<BsonDocument> employees = db1.GetCollection<BsonDocument>("FA_shop_PT_Order");

            if (AddMAPResult != null && AddMAPResult.Length > 0)
            {
                var dd = Query.And(Query.EQ("jigoudaima", AddMAPResult));//同时满足多个条件

                collection1.Remove(dd);
            }
        }
    }
}
