import { logger } from '../utils/Logger'
import { AppState } from '../AppState'
import { api } from '../services/AxiosService'
class KeepsService {
  async getKeeps() {
    try {
      const res = await api.get('api/keeps')
      AppState.keeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async getUserKeeps() {
    try {
      // @ts-ignore
      const res = await api.get(`api/profile/${AppState.profile.id}/keeps`)
      AppState.keeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async getKeepsByProfile(id) {
    try {
      const res = await api.get('api/profile/' + id + '/keeps')
      AppState.activeProfileKeeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async addKeep(keepData) {
    try {
      await api.post('api/keeps', keepData)
      this.getKeeps()
    } catch (error) {
      logger.error(error)
    }
  }

  async deleteKeep(id) {
    try {
      await api.delete('api/keeps/' + id)
      this.getKeeps()
    } catch (error) {
      logger.error(error)
    }
  }

  async editKeep(editData, id) {
    try {
      await api.put('api/keeps/' + id, editData)
      this.getKeeps()
    } catch (error) {
      logger.error(error)
    }
  }

  async setActiveKeep(keepId) {
    try {
      const res = await api.get('api/keeps/' + keepId)
      AppState.activeKeep = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async viewCount(keepId, keep) {
    try {
      await api.get('api/keeps/' + keepId)
      keep.views++
    } catch (error) {
      logger.error(error)
    }
  }

  async keptCount(keep, keepId) {
    try {
      await api.get('api/vaultkeeps/' + keepId)
      keep.keeps++
    } catch (error) {
      logger.error(error)
    }
  }
}
export const keepsService = new KeepsService()
