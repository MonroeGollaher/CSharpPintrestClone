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

  async addKeepToVault(vaultKeep) {
    try {
      await api.post('api/vaultkeeps', vaultKeep)
      console.log(vaultKeep)
      this.getAllVaultKeeps()
    } catch (error) {
      logger.error(error)
    }
  }
}

export const vaultKeepsService = new VaultKeepsService()
