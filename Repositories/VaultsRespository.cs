using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class VaultsRespository
  {
    private readonly IDbConnection _db;
    private readonly string populateCreator = "SELECT vault.*, profile.* FROM vaults vault INNER JOIN profiles profile ON vault.creatorId = profile.id ";
    public VaultsRespository(IDbConnection db)
    {
      _db = db;
    }
    internal int CreateVault(Vault newVault)
    {
      string sql = @"
      INSERT INTO vaults
      (id, creatorId, title, description, isPrivate)
      VALUES
      (@Id, @CreatorId, @Title, @Description, @IsPrivate);
      SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newVault);
    }

    internal bool DeleteVault(int id)
    {
      string sql = @"
      DELETE FROM vaults
      WHERE id = @Id
      LIMIT 1";
      int affectedRows = _db.Execute(sql, new { id });
      return affectedRows > 0;
    }

    internal object GetVaults()
    {
      string sql = populateCreator;
      return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => { vault.Creator = profile; return vault; }, splitOn: "id");
    }

    internal void EditVault(Vault editedVault)
    {
      string sql = @"
      UPDATE vaults
      SET
      title = @Title,
      description = @Description,
      isPrivate = @IsPrivate";
      _db.ExecuteScalar(sql, editedVault);
    }

    internal Vault GetVaultById(int id)
    {
      string sql = @"
      SELECT * FROM vaults
      WHERE id = @Id
      LIMIT 1";
      return _db.QueryFirstOrDefault<Vault>(sql, new { id });
    }

    internal IEnumerable<Vault> GetVaultsByProfile(string profId)
    {
      string sql = @"
      SELECT vault.*,
      p.*
      FROM vaults vault
      JOIN profiles p
      ON vault.creatorId = p.id
      WHERE vault.creatorId = @profId;";
      return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => { vault.Creator = profile; return vault; }, new { profId }, splitOn: "id");
    }
  }
}