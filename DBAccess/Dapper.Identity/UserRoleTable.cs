using CompanyDemo.Domain.DTOs;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DBAccess.Dapper.Identity
{
    /// <summary>
    /// Class that represents the UserRoles table in the Database
    /// </summary>
    public class UserRolesTable
    {
        private DbManager db;

        /// <summary>
        /// Constructor that takes a DbManager instance 
        /// </summary>
        /// <param name="database"></param>
        public UserRolesTable(DbManager database)
        {
            db = database;
        }



        /// <summary>
        /// Returns a list of user's roles By id
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public OneToManyMap<IdentityRole> FindRolesByUser(int memberId, int currentPage, int itemsPerPage, bool isDesc = false)
        {
            var sortType = isDesc ? "DESC" : "ASC";
            var sqlString = @"
                            DECLARE @Start int = (@CurrentPage - 1) * @ItemsPerPage
                            
                            Select COUNT(Role.Name) from MemberRole, Role WITH(NOLOCK)
                                WHERE MemberRole.MemberId=@MemberId AND MemberRole.RoleId = Role.Id 

                            Select Role.Id, Role.Name from MemberRole, Role 
                                WHERE MemberRole.MemberId=@MemberId AND MemberRole.RoleId = Role.Id
                                ORDER BY Role.Id
                                OFFSET @Start ROWS
                                FETCH NEXT @ItemsPerPage ROWS ONLY ";

            var sqlResult = db.Connection.QueryMultiple(sqlString,
                new
                {
                    MemberId = memberId,
                    CurrentPage = currentPage,
                    ItemsPerPage = itemsPerPage,
                });

            var result = new OneToManyMap<IdentityRole>
            {
                TotalCount = sqlResult.ReadSingle<int>(),
                List = sqlResult.Read<IdentityRole>()
            };
            return result;
        }


        /// <summary>
        /// Returns a list of user's roles By id
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public OneToManyMap<IdentityRole> FindNotInRolesByUser(int memberId, int currentPage, int itemsPerPage, bool isDesc = false)
        {
            var sortType = isDesc ? "DESC" : "ASC";
            var sqlString = @"
                            DECLARE @Start int = (@CurrentPage - 1) * @ItemsPerPage
                            
                            SELECT COUNT(*) FROM [Role]
                                    WHERE Id NOT IN(
                                    SELECT [RoleId]
                                      FROM [CompanyDB].[dbo].[MemberRole]
                                      WHERE MemberId =@MemberId)

                            SELECT * FROM [Role]
                                    WHERE Id NOT IN(
                                    SELECT [RoleId]
                                      FROM [CompanyDB].[dbo].[MemberRole]
                                      WHERE MemberId =@MemberId)
                                ORDER BY Role.Id
                                OFFSET @Start ROWS
                                FETCH NEXT @ItemsPerPage ROWS ONLY ";

            var sqlResult = db.Connection.QueryMultiple(sqlString,
                new
                {
                    MemberId = memberId,
                    CurrentPage = currentPage,
                    ItemsPerPage = itemsPerPage,
                });

            var result = new OneToManyMap<IdentityRole>
            {
                TotalCount = sqlResult.ReadSingle<int>(),
                List = sqlResult.Read<IdentityRole>()
            };
            return result;
        }

        public void RemoveRoleFromUser(int memberId, int roleId)
        {
            db.Connection.Execute(@"dELETE FROM MemberRole WHERE MemberId = @MemberId AND RoleId = @RoleId", new { MemberId = memberId, RoleId = roleId });
        }



        /// <summary>
        /// Returns a list of user's roles
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public List<string> FindByUserId(int memberId)
        {
            return db.Connection.Query<string>("Select Role.Name from MemberRole, Role where MemberRole.MemberId=@MemberId and MemberRole.RoleId = Role.Id", new { MemberId = memberId })
                .ToList();
        }

        /// <summary>
        /// Deletes all roles from a user in the UserRoles table
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public void Delete(int memberId)
        {
            db.Connection.Execute(@"Delete from MemberRole where Id = @MemberId", new { MemberId = memberId });
        }

        /// <summary>
        /// Inserts a new role for a user in the UserRoles table
        /// </summary>
        /// <param name="user">The User</param>
        /// <param name="roleId">The Role's id</param>
        /// <returns></returns>
        public void Insert(IdentityMember member, int roleId)
        {
            db.Connection.Execute(@"INSERT INTO MemberRole (MemberId, RoleId) VALUES (@userId, @roleId)",
                new { userId = member.Id, roleId = roleId });
        }


        /// <summary>
        /// Inserts a new role for a user in the UserRoles table
        /// </summary>
        /// <param name="user">The User</param>
        /// <param name="roleId">The Role's id</param>
        /// <returns></returns>
        public void InsertMultiple(IdentityMember member, List<int> roleIds)
        {
            foreach (var Id in roleIds)
            {
                db.Connection.Execute(@"INSERT INTO MemberRole (MemberId, RoleId) VALUES (@userId, @roleId)",
                                new { userId = member.Id, roleId = Id });

            }
        }

    }
}
