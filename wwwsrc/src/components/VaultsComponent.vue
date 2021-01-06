<template>
  <div class="vaults-component col-2 shadow card container-fluid">
    <router-link :to="{name: 'ActiveVault', params: {vaultId: vault.id}}" @click="setActiveVault(vault.id)">
      <h5>{{ vault.name }}</h5>
    </router-link>
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

</style>
