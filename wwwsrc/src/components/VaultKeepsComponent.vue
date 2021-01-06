<template>
  <div class="vault-keeps-component card container-fluid">
    <img :src="vaultKeepProp.img" class="img-fluid card-img" />
    <button v-if="vaultKeepProp.CreatorId == profile.id" @click="removeFromVault(vaultKeepProp.vaultKeepId)" class="btn border-0 bg-transparent text-danger">
      <p>Remove from vault<i class="far fa-trash-alt ml-1"></i></p>
    </button>
  </div>
</template>

<script>
import { computed } from 'vue'
import { vaultKeepsService } from '../services/VaultKeepsService'
import { AppState } from '../AppState'
export default {
  name: 'VaultKeepsComponent',
  props: ['vaultKeepProp'],
  setup(props) {
    return {
      profile: computed(() => AppState.profile),
      vaultKeep: computed(() => props.vaultKeepProp),
      removeFromVault(vaultId) {
        vaultKeepsService.removeVaultKeep(vaultId)
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
img {
    width: 300px;
    height: 300px;
}
</style>
