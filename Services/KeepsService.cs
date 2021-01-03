using System;
using System.Linq;
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

    internal Keep GetKeepById(int id)
    {
      return _repo.GetKeepById(id);
    }

    internal string DeleteKeep(int id, Profile userInfo)
    {
      Keep toDelete = _repo.GetKeepById(id);
      if(toDelete.CreatorId == userInfo.Id)
      {
        _repo.DeleteKeep(id);
        return "The Keep has been deleted";
      }
      return "The Keep could not be deleted";
    }

    internal Keep EditKeep(Profile userInfo, Keep editedKeep)
    {
      Keep oldKeep = _repo.GetKeepById(editedKeep.Id);
      if(oldKeep == null)
      {
          throw new Exception("Bad Id");
      }
      if(oldKeep.CreatorId != userInfo.Id)
      {
          throw new Exception("You are not the original poster of the Keep");
      }
      _repo.EditKeep(editedKeep);
      return _repo.GetKeepById(editedKeep.Id);

    }

    internal object GetKeepsByProfile(string profId, string userId)
    {
      return _repo.GetKeepsByProfile(profId).ToList().FindAll(k => k.CreatorId == userId);
    }
  }
}