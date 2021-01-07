using System;
using System.Collections.Generic;
using System.Linq;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRespository _repo;
    private readonly ProfileRepository _profRepo;
    public VaultsService(VaultsRespository repo, ProfileRepository profRepo)
    {
        _repo = repo;
        _profRepo = profRepo;
    }
    internal Vault CreateVault(Vault newVault)
    {
      newVault.Id = _repo.CreateVault(newVault);
      return newVault;
    }

    internal IEnumerable<Vault> GetVaults(string profileId)
    {
      return _repo.GetVaults(profileId).ToList().FindAll(v => v.CreatorId == profileId || !v.IsPrivate);
    }

    internal Vault GetVaultById(int id, string userId)
    {
      Vault foundVault = _repo.GetVaultById(id);
      Profile creator = _profRepo.GetProfileById(foundVault.CreatorId);
      foundVault.Creator = creator;

      if (foundVault == null)
      {
        throw new Exception("No vault could be found with that.");
      }

      if (foundVault.CreatorId == userId && foundVault.IsPrivate == true)
      {
        return foundVault;
      }

      if (foundVault.IsPrivate == false)
      {
        return foundVault;
      }

      if (foundVault.CreatorId != userId && foundVault.IsPrivate == true)
      {
        throw new Exception("This vault is private.");
      }

      return foundVault;
    }

    internal string DeleteVault(int id, Profile userInfo)
    {
      Vault toDelete = _repo.GetVaultById(id);
      if(toDelete == null)
      {
        throw new Exception("could not be found");
      }
      if(toDelete.CreatorId != userInfo.Id)
      {
        throw new Exception("You are not the original poster of this vault, access denied");
      }
      if(_repo.DeleteVault(id))
      {
        return "Deleted";
      }
      else
      {
        return "Could not delete";
      }
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