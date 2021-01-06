using System;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    private readonly VaultsRespository _vaultRepo;
    public VaultKeepsService(VaultKeepsRepository repo, VaultsRespository vaultRepo)
    {
        _repo = repo;
        _vaultRepo = vaultRepo;
    }
    public VaultKeep CreateVaultKeep(VaultKeep newVk)
    {
      Vault found = _vaultRepo.GetVaultById(newVk.VaultId);
      if(found.CreatorId != newVk.CreatorId)
      {
        throw new Exception("Cannot add Keeps to vaults that are not yours");
      }

      newVk.Id = _repo.CreateVaultKeep(newVk);
      return newVk;
    }

    internal object GetAllVaultKeeps()
    {
      return _repo.GetAllVaultKeeps();
    }

    internal VaultKeep GetKeepById(int id)
    {
      return _repo.GetKeepById(id);
    }

    internal object GetKeepsByVaultId(int id)
    {
      return _repo.GetKeepsByVaultId(id);
    }
    internal string DeleteVaultKeep(int id, Profile userInfo)
    {
      VaultKeep toDelete = _repo.GetKeepById(id);
      if (toDelete.CreatorId == userInfo.Id)
      {
        _repo.DeleteVaultKeep(id);
        return "Vault Keep deleted";
      }
      return "Vault Keep could not be deleted";
    }

    internal object EditVaultKeep(VaultKeep editedVaultKeep, Profile userInfo)
    {
      VaultKeep oldVaultKeep = _repo.GetKeepById(editedVaultKeep.Id);
      if(oldVaultKeep == null)
      {
        throw new Exception("Bad Id");
      }
      if(oldVaultKeep.CreatorId != userInfo.Id)
      {
        throw new Exception("You are not the original poster of the Vault Keep");
      }
      _repo.EditVaultKeep(editedVaultKeep);
      return _repo.GetKeepById(editedVaultKeep.Id);
    }
  }
}