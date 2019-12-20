﻿using CoreHelper.Ioc;
using Loogn.OrmLite;
using project.model.entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace project.dal
{
    [AppService]
    public class SystemUser_RoleDao : CommonServiceBaseDao<SystemUser_Role>
    {

        public List<long> GetEmployeeIds(long roleId)
        {
            using (var db = Open())
            {
                return db.Column<long>("select SystemUserId from SystemUser_Role where SystemRoleId=" + roleId);
            }
        }

        public List<long> GetRoleIds(long employeeId)
        {
            using (var db = Open())
            {
                return db.Column<long>("select SystemRoleId from SystemUser_Role where SystemUserId=" + employeeId);
            }
        }

        public int DeleteByRoleId(long roleId)
        {
            return DeleteWhere("SystemRoleId", roleId);

        }

        public int DeleteByUserId(long userId)
        {
            return DeleteWhere("SystemUserId", userId);
        }

    }
}