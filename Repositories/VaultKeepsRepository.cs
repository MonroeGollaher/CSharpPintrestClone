using System;
using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    private readonly string populateCreator = "SELECT vaultKeep.*, profile.* FROM vaultKeeps vaultKeep INNER JOIN profiles profile ON vaultKeep.creatorId = profile.id";
    public VaultKeepsRepository(IDbConnection db)
    {
        _db = db;
    }
    public int CreateVaultKeep(VaultKeep newVk)
    {
      string sql = @"
      INSERT INTO vaultKeeps
      (vaultId, keepId, creatorId, id)
      VALUES
      (@VaultId, @KeepId, @CreatorId, @Id);
      SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newVk);
    }

    internal object GetAllVaultKeeps()
    {
      string sql = populateCreator;
      return _db.Query<VaultKeep, Profile, VaultKeep>(sql, (VaultKeep, profile) => { VaultKeep.Creator = profile; return VaultKeep; }, splitOn: "id");
    }
  }
}