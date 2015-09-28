using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace YHCSheng.Utils {
    /// <summary>
    ///     SqlHelper 的摘要说明
    /// </summary>
    public class SqlHelper {
        private static string _connString = "";
        private static readonly Hashtable ParmCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// </summary>
        public static string ConnStr {
            set { _connString = value; }
        }


        /// <summary>
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) {
            var cmd = new SqlCommand();

            using (var conn = new SqlConnection(_connString)) {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                var val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(SqlConnection conn, CommandType cmdType, string cmdText,
            params SqlParameter[] cmdParms) {
            var cmd = new SqlCommand();

            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
            var val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }


        /// <summary>
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText,
            params SqlParameter[] cmdParms) {
            var cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            var val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }


        /// <summary>
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) {
            var cmd = new SqlCommand();
            var conn = new SqlConnection(_connString);

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                var rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch {
                conn.Close();
                throw;
            }
        }


        /// <summary>
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) {
            var cmd = new SqlCommand();

            using (var conn = new SqlConnection(_connString)) {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                var val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static object ExecuteScalar(SqlConnection conn, CommandType cmdType, string cmdText,
            params SqlParameter[] cmdParms) {
            var cmd = new SqlCommand();

            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
            var val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }


        /// <summary>
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="cmdParms"></param>
        public static void CacheParameters(string cacheKey, params SqlParameter[] cmdParms) {
            ParmCache[cacheKey] = cmdParms;
        }


        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <summary>
        ///     Prepare a command for execution
        /// </summary>
        /// <param name="cmd">SqlCommand object</param>
        /// <param name="conn">SqlConnection object</param>
        /// <param name="trans">SqlTransaction object</param>
        /// <param name="cmdType">Cmd type e.g. stored procedure or text</param>
        /// <param name="cmdText">Command text, e.g. Select * from Products</param>
        /// <param name="cmdParms">SqlParameters to use in the command</param>
        public static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType,
            string cmdText, SqlParameter[] cmdParms) {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null) {
                foreach (var parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
    }
}