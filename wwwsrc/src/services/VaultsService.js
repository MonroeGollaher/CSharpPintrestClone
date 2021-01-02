import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { AppState } from '../AppState'
class VaultsService {
  async getVaults() {
    try {
      const res = await api.get('api/vaults')
      // console.log(res.data)
      AppState.vaults = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async createVault(vaultData) {
    try {
      debugger
      await api.post('api/vaults', vaultData)
      console.log(vaultData)
      this.getVaults()
    } catch (error) {
      logger.error(error)
    }
  }

  async deleteVailt(id) {
    try {
      await api.delete('api/vaults/' + id)
      this.getVaults()
    } catch (error) {
      logger.error(error)
    }
  }

  async editVault(editData, id) {
    try {
      await api.put('api/vaults' + id, editData)
      this.getVaults()
    } catch (error) {
      logger.error(error)
    }
  }

  async setActiveVault(vaultId) {
    try {
      const res = await api.get('api/vaults', vaultId)
      AppState.activeVault = res.data
    } catch (error) {
      logger.error(error)
    }
  }
}
export const vaultsService = new VaultsService()
