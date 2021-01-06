using System;
using System.Collections.Generic;
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
      (id, creatorId, name, description, img, views, shares, keeps)
      VALUES
      (@Id, @CreatorId, @Name, @Description, @Img, @Views, @Shares, @Keeps);
      SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newKeep);
    }


    internal Keep GetKeepById(int id)
    {
      string sql = @"
      UPDATE keeps 
      SET views = views+1 
      WHERE id = @Id;
      SELECT * FROM keeps
      WHERE id = @Id
      LIMIT 1";
      return _db.QueryFirstOrDefault<Keep>(sql, new { id });
    }

    internal object GetKeeps()
    {
      string sql = populateCreator;
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, splitOn: "id");
    }

    internal void EditKeep(Keep editedKeep)
    {
      string sql = @"
      UPDATE keeps
      SET
      name = @Name,
      description = @Description,
      img = @Img,
      views = @Views,
      shares = @Shares,
      keeps = @Keeps
      WHERE id = @Id;";
      _db.ExecuteScalar(sql, editedKeep);
    }

    internal IEnumerable<Keep> GetKeepsByProfile(string profileId)
    {
      string sql = @"
      SELECT
      keep.*,
      p.*
      FROM keeps keep
      JOIN profiles p ON keep.creatorId = p.id
      WHERE keep.creatorId = @profileId;";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { profileId }, splitOn: "id");
    }

    internal bool DeleteKeep(int id)
    {
      string sql = @"
      DELETE FROM keeps
      WHERE id = @Id
      LIMIT 1";
      int affectedRows = _db.Execute(sql, new { id });
      return affectedRows > 0;
    }
  }
}