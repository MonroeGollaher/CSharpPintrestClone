using System;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;
    public KeepsService(KeepsRepository repo)
    {
        _repo = repo;
    }
    public Keep CreateKeep(Keep newKeep)
    {
      newKeep.Id = _repo.CreateKeep(newKeep);
      return newKeep;
    }

    internal object GetKeeps()
    {
      return _repo.GetKeeps();
    }
  }
}