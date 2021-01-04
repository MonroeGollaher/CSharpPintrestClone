using System;
using System.Linq;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRespository _repo;
    public VaultsService(VaultsRespository repo)
    {
        _repo = repo;
    }
    internal Vault CreateVault(Vault newVault)
    {
      newVault.Id = _repo.CreateVault(newVault);
      return newVault;
    }

    internal object GetVaults()
    {
      return _repo.GetVaults();
    }

    internal Vault GetVaultById(int id)
    {
      return _repo.GetVaultById(id);
    }

    internal string DeleteVault(int id, Profile userInfo)
    {
      Vault toDelete = _repo.GetVaultById(id);
      if(toDelete.CreatorId == userInfo.Id)
      {
        _repo.DeleteVault(id);
        return "The Vault has been deleted";
      }
      return "The Vault could not be deleted";
    }

    internal Vault EditVault(Profile userInfo, Vault editedVault)
    {
      Vault oldVault = _repo.GetVaultById(editedVault.Id);
      if(oldVault == null)
      {
        throw new Exception("Bad Id");
      }
      if(oldVault.CreatorId != userInfo.Id)
      {
        throw new Exception("You are not the original poster of the Vault");
      }
      _repo.EditVault(editedVault);
      return _repo.GetVaultById(editedVault.Id);
    }

    internal object GetVaultsByProfile(string profId, string userId)
    {
      return _repo.GetVaultsByProfile(profId).ToList().FindAll(v => v.CreatorId == userId || v.CreatorId == profId && v.IsPrivate == false);
    }
  }
}