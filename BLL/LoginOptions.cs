using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using DAL;

namespace BLL
{
    public class LoginOptions
    {
        #region 字段

        #endregion
        #region 方法
        public LoginOptions() { }
        /// <summary>
        /// 注册新用户，并检测数据长度以及用户名是否存在
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public LoginStatus RegisterNewUser(UserInfo userInfo)
        {
            //判断用户名及密码是否满足要求
            if (CheckRules(userInfo) != LoginStatus.NomalState)
                return CheckRules(userInfo);
            //判断用户名是否已存在
            UserDataOptions userDataOptions = new UserDataOptions();
            if (userDataOptions.FindUserByName(userInfo.UserName))
                return LoginStatus.UserNameExist;
            //保存新用户信息
            userDataOptions.AddUserData(userInfo);
            return LoginStatus.NomalState;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userInfo"></param>
        public LoginStatus UserLogin(UserInfo userInfo)
        {
            UserDataOptions userDataOptions = new UserDataOptions();
            //先检查用户名是否存在
            if (userDataOptions.FindUserByName(userInfo.UserName) == false)
                return LoginStatus.UserNameNotExist;
            //再检查用户名及密码是否匹配
            if (userDataOptions.FindUserByUserInfo(userInfo) == false)
                return LoginStatus.DataNotMatch;
            return LoginStatus.NomalState;
        }
        private LoginStatus CheckRules(UserInfo userInfo)
        {
            //先判断用户名是否满足要求
            if (userInfo.UserName.Length < (int)LoginWordSize.MinUserNameSize || userInfo.UserName.Length > (int)LoginWordSize.MaxWordSize)
                return LoginStatus.UserNameSizeError;
            //再判断密码长度是否满足要求
            if (userInfo.Password.Length < (int)LoginWordSize.MinPasswordSize || userInfo.Password.Length > (int)LoginWordSize.MaxWordSize)
                return LoginStatus.PasswordSizeError;
            return LoginStatus.NomalState;
        }
        #endregion
    }
}
