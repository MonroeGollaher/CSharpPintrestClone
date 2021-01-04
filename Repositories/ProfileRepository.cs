using System.Data;
using keepr.Models;
using Dapper;
using System;

namespace keepr.Repositories
{
    public class ProfileRepository
    {
        private readonly IDbConnection _db;

        public ProfileRepository(IDbConnection db)
        {
            _db = db;
        }

        public Profile GetByEmail(string email)
        {
            string sql = "SELECT * FROM profiles WHERE email = @Email";
            return _db.QueryFirstOrDefault<Profile>(sql, new { email });
        }

        public Profile Create(Profile userInfo)
        {
            string sql = @"
            INSERT INTO profiles
            (name, picture, email, id)
            VALUES
            (@Name, @Picture, @Email, @Id)";
            _db.Execute(sql, userInfo);
            return userInfo;
        }

    internal Profile GetProfileById(string profileId)
    {
      string sql = @"
      SELECT * FROM 
      profiles
      WHERE id = @ProfileId";
      return _db.QueryFirstOrDefault<Profile>(sql, new { profileId });
    }
  }
}