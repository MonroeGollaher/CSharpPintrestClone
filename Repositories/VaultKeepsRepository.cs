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
      string sql = @"
      SELECT * FROM vaultkeeps";
      return _db.Query<VaultKeep>(sql);
      // string sql = populateCreator;
      // return _db.Query<VaultKeep, Profile, VaultKeep>(sql, (VaultKeep, profile) => { VaultKeep.Creator = profile; return VaultKeep; }, splitOn: "id");
    }


    internal VaultKeep GetKeepById(int id)
    {
      string sql = @"
      SELECT * 
      FROM vaultKeeps
      WHERE id = @Id";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }
    internal object GetKeepsByVaultId(int vaultId)
    {
      string sql = @"
      SELECT keep.*,
      vaultkeep.id as VaultKeepId,
      profile.*
      FROM vaultkeeps vaultkeep
      JOIN keeps keep
      ON keep.id = vaultkeep.keepId
      JOIN profiles profile
      ON profile.id = keep.creatorId
      WHERE vaultId = @VaultId;";
      return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (keep, profile) => {keep.Creator = profile; return keep; }, new { vaultId }, splitOn: "id");
    }

    internal void EditVaultKeep(VaultKeep editedVaultKeep)
    {
      string sql = @"
      UPDATE vaultkeeps
      SET
      vaultId = @VaultId,
      keepId = @KeepId
      WHERE id = @Id;";
      _db.Execute(sql, editedVaultKeep);
    }

    internal bool DeleteVaultKeep(int id)
    {
      string sql = @"DELETE FROM vaultKeeps WHERE id = @Id LIMIT 1";
      int affectedRows = _db.Execute(sql, new { id });
      return affectedRows > 0;
    }
  }
}