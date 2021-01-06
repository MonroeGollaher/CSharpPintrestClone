<template>
  <div class="active-profile-page container-fluid">
    <div class="row pt-4 pl-4">
      <div class="col-2 ">
        <img :src="activeProfile.picture" class="img-fluid" />
      </div>
      <div class="col-2">
        <h1>{{ activeProfile.name }}</h1>
        <div class="row text-start d-flex flex-column">
          <div class="col pt-4 pl-4">
            <h5>Keeps: {{ keeps.length }} </h5>
          </div>
          <div class="col">
            <h5>Vaults: {{ vaults.length }} </h5>
          </div>
        </div>
      </div>
    </div>
    <div class="row">
      <h1>Keeps: </h1>
      <div class="card-columns">
        <keeps-component v-for="k in keeps" :key="k" :keeps-prop="k" />
      </div>
      <div v-if="profile.id == activeProfile.id">
        <new-keep-component />
      </div>
    </div>
    <div class="row ml-1">
      <h1>Vaults: </h1>
      <vaults-component v-for="v in vaults" :key="v" :vaults-prop="v" />
      <div v-if="profile.id == activeProfile.id">
        <new-vault-component />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import { profileService } from '../services/ProfileService'
import { keepsService } from '../services/KeepsService'
import KeepsComponent from '../components/KeepsComponent'
import { vaultsService } from '../services/VaultsService'
import NewVaultComponent from '../components/NewVaultComponent'
import NewKeepComponent from '../components/NewKeepComponent'
export default {
  name: 'ActiveProfilePage',
  components: { KeepsComponent, NewVaultComponent, NewKeepComponent },
  setup() {
    const route = useRoute()
    onMounted(() => {
      profileService.getActiveProfile(route.params.profileId)
      keepsService.getKeepsByProfile(route.params.profileId)
      vaultsService.getVaultsByProfile(route.params.profileId)
    })
    return {
      profile: computed(() => AppState.profile),
      activeProfile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.activeProfileKeeps),
      vaults: computed(() => AppState.activeProfileVaults)
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
