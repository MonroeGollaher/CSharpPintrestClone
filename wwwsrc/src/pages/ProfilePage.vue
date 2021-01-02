<template>
  <div class="about text-center container-fluid">
    <h1>Welcome {{ profile.name }}</h1>
    <img class="rounded" :src="profile.picture" alt="" />
    <p>{{ profile.email }}</p>
    <div class="row">
      <new-vault-component />
      <vaults-component v-for="v in vaults" :key="v" :vaults-prop="v" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import NewVaultComponent from '../components/NewVaultComponent'
import VaultsComponent from '../components/VaultsComponent'
import { vaultsService } from '../services/VaultsService'
export default {
  name: 'Profile',
  components: { NewVaultComponent, VaultsComponent },
  setup() {
    onMounted(async() => {
      await keepsService.getKeeps()
      await vaultsService.getVaults()
    })
    return {
      vaults: computed(() => AppState.vaults),
      profile: computed(() => AppState.profile)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
