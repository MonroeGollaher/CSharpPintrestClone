<template>
  <div class="active-vault container-fluid">
    <h2>{{ activeVault.title }}</h2>
    <div class="row">
      <vault-keeps-component v-for="ak in activeKeeps" :key="ak.id" :vault-keep-prop="ak" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { vaultKeepsService } from '../services/VaultKeepsService'
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
    })
    return {
      activeVault: computed(() => AppState.activeVault),
      activeKeeps: computed(() => AppState.activeVaultKeeps)
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
