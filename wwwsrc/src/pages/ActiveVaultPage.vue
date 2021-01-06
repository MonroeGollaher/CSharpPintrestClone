<template>
  <div class="active-vault container-fluid">
    <div class="row text-start p-4">
      <div class="col-12">
        <h2>
          {{ activeVault.name }} <button v-if="profile.id == activeVault.creatorId" @click="removeVault(activeVault.id)" class="btn border-0 bg-transparent">
            <i class="fas fa-trash text-danger"></i>
          </button>
        </h2>
        <p>{{ activeVault.description }}</p>
      </div>
      <div class="row pt-3">
        <div class="card-columns">
          <vault-keeps-component v-for="ak in activeKeeps" :key="ak.id" :vault-keep-prop="ak" />
        </div>
      </div>
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
