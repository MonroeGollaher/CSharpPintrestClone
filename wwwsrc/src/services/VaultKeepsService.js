import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { AppState } from '../AppState'

class VaultKeepsService {
  async getAllVaultKeeps() {
    try {
      const res = await api.get('api/vaultkeeps')
      AppState.vaultKeeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async getActiveVaultKeeps(vaultId) {
    try {
      const res = await api.get('api/vaults/' + vaultId + '/keeps')
      AppState.activeVaultKeeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async addKeepToVault(vaultKeep) {
    try {
      await api.post('api/vaultkeeps', vaultKeep)
      this.getAllVaultKeeps()
    } catch (error) {
      logger.error(error)
    }
  }

  async removeVaultKeep(keepId, vaultId) {
    try {
      await api.delete('api/vaultkeeps/' + vaultId)
      const keep = AppState.activeVaultKeeps.findIndex(k => k.id === keepId)
      AppState.activeVaultKeeps.splice(keep, 1)
      this.getAllVaultKeeps()
    } catch (error) {
      logger.error(error)
    }
  }
}

export const vaultKeepsService = new VaultKeepsService()
