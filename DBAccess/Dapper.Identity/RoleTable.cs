using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using System.Linq;
using CompanyDemo.Domain.DTOs;

namespace DBAccess.Dapper.Identity
{
    /// <summary>
    /// Class that represents the Role table in the Database
    /// </summary>
    public class RoleTable<TRole> where TRole : IdentityRole
    {
        private DbManager db;
        /// <summary>
        /// Constructor that takes a DbManager instance 
        /// </summary>
        /// <param name="database"></param>
        public RoleTable(DbManager database)
        {
            db = database;
        }

        /// <summary>
        /// Deltes a role from the Roles table
        /// </summary>
        /// <param name="roleId">The role Id</param>
        /// <returns></returns>
        public void Delete(int roleId)
        {
            db.Connection.Execute(@"DELETE FROM Role WHERE Id = @id", new { id = roleId });
        }

        /// <summary>
        /// Inserts a new Role in the Roles table
        /// </summary>
        /// <param name="roleName">The role's name</param>
        /// <returns></returns>
        public void Insert(IdentityRole role)
        {
            db.Connection.Execute(@"INSERT INTO Role (Name) VALUES (@name)",
                new { name = role.Name });
        }

        public OneToManyMap<TRole> FindAllRoles(int currentPage, int itemsPerPages, bool isDesc = false)
        {
            var sortType = isDesc ? "DESC" : "ASC";
            var sqlString = @"
                    DECLARE @Start int = (@CurrentPage - 1) * @ItemsPerPages
                    SELECT COUNT(*) FROM Role WITH(NOLOCK)
                    SELECT * FROM  Role
                        ORDER BY  Id " + sortType + @"
                        OFFSET @Start ROWS
                        FETCH NEXT @ItemsPerPages ROWS ONLY";

            var sqlResult = db.Connection.QueryMultiple(sqlString,
              new
              {
                  ItemsPerPages = itemsPerPages,
                  CurrentPage = currentPage,
                  SortType = sortType,
              });
            var result = new OneToManyMap<TRole>
            {
                TotalCount = sqlResult.ReadSingle<int>(),
                List = sqlResult.Read<TRole>()
            };
            return result;
        }

        /// <summary>
        /// Returns a role name given the roleId
        /// </summary>
        /// <param name="roleId">The role Id</param>
        /// <returns>Role name</returns>
        public string GetRoleName(int roleId)
        {
            return db.Connection.ExecuteScalar<string>("SELECT Name FROM Role WHERE Id=@id", new { id = roleId });
        }

        /// <summary>
        /// Returns the role Id given a role name
        /// </summary>
        /// <param name="roleName">Role's name</param>
        /// <returns>Role's Id</returns>
        public int GetRoleId(string roleName)
        {
            return db.Connection.ExecuteScalar<int>("SELECT Id FROM Role WHERE Name=@name", new { name = roleName });
        }

        /// <summary>
        /// Gets the IdentityRole given the role Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IdentityRole GetRoleById(int roleId)
        {
            var roleName = GetRoleName(roleId);
            IdentityRole role = null;

            if (roleName != null)
            {
                role = new IdentityRole(roleName, roleId);
            }

            return role;
        }

        /// <summary>
        /// Gets the IdentityRole given the role name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public IdentityRole GetRoleByName(string roleName)
        {
            var roleId = GetRoleId(roleName);
            IdentityRole role = null;

            if (roleId > 0)
            {
                role = new IdentityRole(roleName, roleId);
            }

            return role;
        }

        public void Update(IdentityRole role)
        {
            db.Connection
            .Execute(@"
                    UPDATE Role
                    SET
                        Name = @name
                    WHERE
                        Id = @id",
                    new
                    {
                        name = role.Name,
                        id = role.Id
                    });
        }
    }
}
