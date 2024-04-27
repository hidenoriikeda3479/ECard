using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ECard.Common
{
    /// <summary>
    /// データベース操作を行うヘルパークラス
    /// </summary>
    internal class DatabaseHelper
    {
        private readonly string _connectionString;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DatabaseHelper()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["sqlsvrconnect"].ConnectionString;
        }

        /// <summary>
        /// データベース接続を開く
        /// </summary>
        /// <returns>開いたデータベース接続</returns>
        public SqlConnection OpenConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// SQLクエリを実行し、結果をDataTableで返す
        /// </summary>
        /// <param name="connection">データベース接続</param>
        /// <param name="sql">実行するSQLクエリ</param>
        /// <returns>クエリ結果のDataTable</returns>
        public DataTable ExecuteQuery(SqlConnection connection, string sql)
        {
            using (var command = new SqlCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);
                    return dataTable;
                }
            }
        }

        /// <summary>
        /// SQL非クエリ（INSERT、UPDATE、DELETEなど）を実行
        /// </summary>
        /// <param name="connection">データベース接続</param>
        /// <param name="sql">実行するSQL非クエリ</param>
        /// <returns>影響を受けた行数</returns>
        public int ExecuteNonQuery(SqlConnection connection, string sql)
        {
            using (var command = new SqlCommand(sql, connection))
            {
                return command.ExecuteNonQuery();
            }
        }

    }
}
