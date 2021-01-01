using System;
using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;
    private readonly string populateCreator = "SELECT keep.*, profile.* FROM keeps keep INNER JOIN profiles profile ON keep.creatorId = profile.id ";
    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }
    internal int CreateKeep(Keep newKeep)
    {
      string sql = @"
      INSERT INTO keeps
      (id, creatorId, name, description, image, views, shares, keeps)
      VALUES
      (@Id, @CreatorId, @Name, @Description, @Image, @Views, @Shares, @Keeps);
      SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newKeep);
    }

    internal object GetKeeps()
    {
      string sql = populateCreator;
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, splitOn: "id");
    }
  }
}