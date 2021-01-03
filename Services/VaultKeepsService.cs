using System;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    public VaultKeepsService(VaultKeepsRepository repo)
    {
        _repo = repo;
    }
    public VaultKeep CreateVaultKeep(VaultKeep newVk)
    {
      newVk.Id = _repo.CreateVaultKeep(newVk);
      return newVk;
    }

    internal object GetAllVaultKeeps()
    {
      return _repo.GetAllVaultKeeps();
    }
  }
}