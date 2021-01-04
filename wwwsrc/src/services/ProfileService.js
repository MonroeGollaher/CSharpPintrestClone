import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class ProfileService {
  async getProfile() {
    try {
      const res = await api.get('api/profile')
      AppState.profile = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getActiveProfile(id) {
    try {
      const res = await api.get('api/profile/' + id)
      // console.log(res.data)
      AppState.activeProfile = res.data
    } catch (error) {
      logger.error(error)
    }
  }
}

export const profileService = new ProfileService()
