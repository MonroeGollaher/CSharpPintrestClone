<template>
  <div class="vaults-component shadow card container-fluid">
    <div class="row">
      <div class="col-12 vault">
        <div class="text-center">
          <router-link :to="{name: 'ActiveVault', params: {vaultId: vault.id}}" @click="setActiveVault(vault.id)">
            <h5>{{ vault.name }}</h5>
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from 'vue'
import { vaultsService } from '../services/VaultsService'
import { AppState } from '../AppState'
export default {
  name: 'VaultsComponent',
  props: ['vaultsProp'],
  setup(props) {
    return {
      profile: computed(() => AppState.profile),
      vault: computed(() => props.vaultsProp),
      setActiveVault(vaultId) {
        vaultsService.setActiveVault(vaultId)
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
.vault {
  height: 15vh;
  width: 25vh;
}
</style>
