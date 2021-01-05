<template>
  <div class="active-vault container-fluid">
    <div class="row">
      <div class="col-4">
        <h2>{{ activeVault.title }}</h2>
        <button v-if="profile.id == activeVault.creatorId" @click="removeVault(activeVault.id)">
          <h5>Delete test</h5>
        </button>
        <div class="col-4">
        </div>
      </div>
    </div>
    <div class="row">
      <vault-keeps-component v-for="ak in activeKeeps" :key="ak.id" :vault-keep-prop="ak" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { vaultKeepsService } from '../services/VaultKeepsService'
import { vaultsService } from '../services/VaultsService'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import VaultKeepsComponent from '../components/VaultKeepsComponent'
export default {
  name: 'ActiveVault',
  components: { VaultKeepsComponent },
  setup() {
    const route = useRoute()
    onMounted(() => {
      vaultKeepsService.getActiveVaultKeeps(route.params.vaultId)
      vaultKeepsService.getAllVaultKeeps()
    })
    return {
      profile: computed(() => AppState.profile),
      activeProfile: computed(() => AppState.activeProfile),
      activeVault: computed(() => AppState.activeVault),
      activeKeeps: computed(() => AppState.activeVaultKeeps),
      removeVault(vaultId) {
        vaultsService.deleteVault(vaultId)
      }
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
