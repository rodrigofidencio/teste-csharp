using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Aptus.Util.SqlGT
{

    public class SqlGTSession
    {
        public string Id;
    }

    public class SqlGT
    {

        private SqlConnection cnn ;
        private SqlGTSession session;
        public SqlGT(string connectionName)
        {

            if (this.InputParameters == null) this.InputParameters = new System.Collections.Specialized.ListDictionary();
            if (this.InputList == null) this.InputList = new System.Collections.Specialized.ListDictionary();
        }

        public SqlGT(string connectionName, SqlGTSession session)
        {
            if (this.InputParameters == null) this.InputParameters = new System.Collections.Specialized.ListDictionary();
            if (this.InputList == null) this.InputList = new System.Collections.Specialized.ListDictionary();
            this.session = session;
        }
        public void Dispose() {
            cnn.Close();
            cnn.Dispose();
            cnn = null;

        }



        private System.Collections.Specialized.ListDictionary InputParameters;
        private System.Collections.Specialized.ListDictionary InputList;
        private System.Collections.Specialized.ListDictionary OutputParameters;
        private System.Collections.Specialized.ListDictionary OutputLists;

        public SqlGTParameters CreateParameters()
        {
            return new SqlGTParameters();
        }
        public SqlGTResults Execute(string procedureName)
        {
            SqlGTResults results = new SqlGTResults();
             cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            if (cnn.State != ConnectionState.Open) cnn.Open();
            return this.Execute(cnn, procedureName);
        }
        public void AddParameter(string name, string value)
        {
            if (this.InputParameters == null) this.InputParameters = new System.Collections.Specialized.ListDictionary();
            this.InputParameters[name] = value;
        }
        public void AddParameter(string name, decimal value)
        {
            if (this.InputParameters == null) this.InputParameters = new System.Collections.Specialized.ListDictionary();
            this.InputParameters[name] = value;
        }
        public void AddParameter(string name, int value)
        {
            if (this.InputParameters == null) this.InputParameters = new System.Collections.Specialized.ListDictionary();
            this.InputParameters[name] = value;
        }
        public void AddParameter(string name, double value)
        {
            if (this.InputParameters == null) this.InputParameters = new System.Collections.Specialized.ListDictionary();
            this.InputParameters[name] = value;
        }

        public void AddParameter(string name, List<object> value)
        {
            if (this.InputList == null) this.InputList = new System.Collections.Specialized.ListDictionary();
            this.InputList[name] = value;
        }
        public SqlGTResults Execute(SqlConnection connection,
                                string procedureName)
        {
            SqlGTResults results = new SqlGTResults();

            // Configure the SqlCommand and SqlParameter.  
            SqlCommand insertCommand = new SqlCommand(procedureName, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            foreach (object p in this.InputParameters.Keys)
            {
                insertCommand.Parameters.AddWithValue("@" + p, this.InputParameters[p]);
            }
            foreach (object p in this.InputList.Keys)
            {
                SqlParameter tvpParam = insertCommand.Parameters.AddWithValue("@" + p,
                    SqlGTUtil.ConvertToDataTable((List<object>)InputList[p]));
                tvpParam.SqlDbType = SqlDbType.Structured;
            }
            //caso tenha sessao aqui 
            //e não tenha informado forçar
            if (!insertCommand.Parameters.Contains("SESSION_ID")
                && this.session!=null) {
                insertCommand.Parameters.AddWithValue("@SESSION_ID", this.session.Id);
            }


            //executar / pegar resultados
            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(insertCommand);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            //pegar as tabelas de retorno
            foreach (DataTable dt in ds.Tables)
            {
                if (dt.Rows.Count == 0) continue;
                //table name
                dt.TableName = (string)dt.Rows[0]["OPTION_OUTPUT_TABLE"];

                //tabela especial de resultados
                if ((string)dt.Rows[0]["OPTION_OUTPUT_TABLE"] == "RESULT")
                {
                    results.ResultCode = (string)dt.Rows[0]["CODIGO"];
                    results.ResultType = (string)dt.Rows[0]["TIPO"];
                    results.ResultMessage  = (string)dt.Rows[0]["MENSAGEM"];
                    results.ResultException = (string)dt.Rows[0]["EXCEPTION"];

                }
                //para cada datable remove coluna de apoio
                dt.Columns.Remove("OPTION_OUTPUT_TABLE");
                //adiciona
                results.Tables.Add(dt);
            }
            if (results.ResultCode == null)
            {
                throw new Exception("SqlGT: resposta invalida de procedure não contém dados de controle.");
            }
            if (!ds.Tables.Contains("DEFAULT")) {
                throw new Exception("SqlGT: resposta invalida de procedure não contém dados padrao de retorno.");
            }

            return results;

        }
    }

    public class SqlGTResultType
    {
        public static string RESULT_SUCCESS = "RESULT_SUCCESS";
        public static string RUNTIME_ERROR = "RUNTIME_ERROR";
        public static string EXCEPTION = "EXCEPTION";
    }
    public class SqlGTResults
    {

        public string ResultCode;
        public string ResultMessage;
        public string ResultType;
        public string ResultException;

        private List<DataTable> tables;

        public List<DataTable> Tables {
            get {
                if (tables == null) tables = new List<DataTable>();
                return tables;
            }
        }


        public T GetValue<T>(string name)
        {
            //pegar do result padrao
            DataTable dt = tables.Where(c => c.TableName.ToLower() == "default").FirstOrDefault();

            Type conversionType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

            return (T)Convert.ChangeType(dt.Rows[0][name], conversionType);

        }

        public T GetResultValue<T>(string name)
        {
            //pegar do result 
            DataTable dt = tables.Where(c => c.TableName.ToLower() == "result").FirstOrDefault();

            Type conversionType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

            return (T)Convert.ChangeType(dt.Rows[0][name], conversionType);
            
        }
        public T GetListValue<T>(string listName, string name)
        {
            //pegar do result informado
            DataTable dt = tables.Where(c => c.TableName.ToLower() == listName.ToLower()).FirstOrDefault();

            Type conversionType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

            return (T)Convert.ChangeType(dt.Rows[0][name], conversionType);

        }
        public List<T> GetList<T>()
        {            
            return GetList<T>("default");
        }
        public List<T> GetList<T>(string name)
        {
            DataTable dt = tables.Where(c => c.TableName.ToLower() == name.ToLower()).FirstOrDefault();
            return SqlGTUtil.ConvertToList<T>(dt);
        }

    }


    public class SqlGTParameters
    {
        public void Add(string name, object value) { }
        public void Add(string name, List<object> value) { }


    }

    public class SqlGTUtil
    {

        public static DataTable ConvertToDataTable(List<object> lista)
        {

            if (lista.Count == 0) return null;
            DataTable dt = new DataTable();
            var properties = lista[0].GetType().GetFields();
            foreach (var pro in properties)
            {
                dt.Columns.Add(pro.Name, pro.FieldType);                
            }
            foreach (var item in lista)
            {
                DataRow row = dt.NewRow();
                foreach (var pro in properties)
                {
                    //TODO: valores nulos, identificar o tipo e colocar valor padrão
                    //antes de enviar para o banco de dados
                    row[pro.Name] = (pro.GetValue(item) ==  null ? DBNull.Value : pro.GetValue(item));
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        public static List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>()
                    .Select(c => c.ColumnName)
                    .ToList();
            var properties = typeof(T).GetFields();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name))
                    {
                        
                        FieldInfo pI = objT.GetType().GetField(pro.Name);
                        pro.SetValue(objT, row[pro.Name] == DBNull.Value ? null : Convert.ChangeType(row[pro.Name], pI.FieldType));
                    }
                }
                return objT;
            }).ToList();
        }
    }
}
