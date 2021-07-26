using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel;
using System.Data.SqlClient;

namespace DAL
{
    public class UserDataOptions
    {
        #region 字段
        //用于存储服务器登录信息
        private const string serverInfo = "server=DESKTOP-JOEY\\SQLEXPRESS;uid=sa;pwd=sa;database=USERINFODB";
        private SqlConnection connection;
        #endregion
        public UserDataOptions()
        {
            //建立连接
            connection = new SqlConnection(serverInfo);
            connection.Open();
        }
        /// <summary>
        /// 以用户名查找，未找到返回0
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// 
        public bool FindUserByName(string name)
        {
            string sqlFindUserCommand = $"select count(1) from UserInfo where UserName='{name}'";
            SqlCommand command = new SqlCommand(sqlFindUserCommand , connection);
            int res = (int)command.ExecuteScalar();
            if (res == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 以账号密码查找，未找到返回0
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public bool FindUserByUserInfo(UserInfo userInfo)
        {
            string sqlFindUserCommand = $"select count(1) from UserInfo where UserName='{userInfo.UserName}' and Password='{userInfo.Password}'";
            SqlCommand command = new SqlCommand(sqlFindUserCommand, connection);
            int res = (int)command.ExecuteScalar();
            if (res == 0)
                return false;
            else
                return true;
        }
        
        /// <summary>
        /// 找到数据库中最大的用户编号，为新用户编号做准备
        /// </summary>
        /// <returns></returns>
        private int FindMaxUserID()
        {

            string sqlFindCommand = $"select max(UserInfo.UserID) from UserInfo";
            SqlCommand command = new SqlCommand(sqlFindCommand, connection);
            Object result = command.ExecuteScalar();
            //处理空数据库异常
            if (result == System.DBNull.Value)
                return 0;
            int maxUserID = (int)result;
            return maxUserID;
        }
        
        /// <summary>
        /// 保存新用户数据
        /// </summary>
        /// <param name="userInfo"></param>
        public void AddUserData(UserInfo userInfo)
        {
            //为新用户设置编号
            int userID = FindMaxUserID() + 1;
            //构建数据库命令
            string sqlAddCommand = $"insert into UserInfo(UserID,UserName,Password) values({userID},'{userInfo.UserName}','{userInfo.Password}')";
            SqlCommand command = new SqlCommand(sqlAddCommand, connection);
            //执行数据库命令
            command.ExecuteNonQuery();
        }
    }
}