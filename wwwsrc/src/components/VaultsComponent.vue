<template>
  <div class="vaults-component col-2 shadow card container-fluid">
    <h5>{{ vault.title }}</h5>
  </div>
</template>

<script>
import { computed } from 'vue'
import { vaultsService } from '../services/VaultsService'
export default {
  name: 'VaultsComponent',
  props: ['vaultsProp'],
  setup(props) {
    return {
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
