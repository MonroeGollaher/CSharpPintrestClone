<template>
  <div class="about container-fluid">
    <div class="row justify-content-start">
      <div class="col-2">
        <img class="rounded" :src="profile.picture" alt="" />
      </div>
      <div class="col-2">
        <h1>{{ profile.name }}</h1>
        <div class="row d-flex flex-column">
          <div class="col">
            <p>Vaults: </p>
          </div>
          <div class="col">
            <p>Keeps: </p>
          </div>
        </div>
      </div>
    </div>
    <div class="row justify-content-start">
      <div class="col-3">
        <new-vault-component />
      </div>
    </div>
    <div class="row justify-content-start">
      <vaults-component v-for="v in vaults" :key="v" :vaults-prop="v" />
    </div>
    <div class="row justify-content-start">
      <div class="col-3">
        <new-keep-component />
      </div>
    </div>
    <div class="row justify-content-start">
      <keeps-component v-for="k in keeps" :key="k" :keeps-prop="k" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import NewVaultComponent from '../components/NewVaultComponent'
import VaultsComponent from '../components/VaultsComponent'
import NewKeepComponent from '../components/NewKeepComponent'
export default {
  name: 'Profile',
  components: { NewVaultComponent, VaultsComponent, NewKeepComponent },
  setup() {
    onMounted(async() => {
      await keepsService.getKeeps()
      await vaultsService.getVaults()
    })
    return {
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps),
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
