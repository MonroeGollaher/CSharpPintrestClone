<template>
  <div class="active-profile-page container-fluid">
    <div class="row pt-4 profArea">
      <div class="col-2 ">
        <img :src="activeProfile.picture" class="img-fluid rounded-circle" />
      </div>
      <div class="col-2">
        <h1>{{ activeProfile.name }}</h1>
        <div class="row d-flex flex-column">
          <div class="col">
            <p class="lead">
              Vaults: {{ vaults.length }}
            </p>
          </div>
          <div class="col">
            <p class="lead">
              Keeps: {{ keeps.length }}
            </p>
          </div>
        </div>
      </div>
    </div>
    <div class="row main-area">
      <h1>
        Keeps: <button v-if="profile.id == activeProfile.id" class="btn border-0">
          <new-keep-component />
        </button>
      </h1>
    </div>
    <div class="row main-area">
      <div class="card-columns">
        <keeps-component v-for="k in keeps" :key="k" :keeps-prop="k" />
      </div>
    </div>
    <div class="row main-area">
      <h1>Vaults: </h1>
      <div v-if="profile.id == activeProfile.id">
        <new-vault-component />
      </div>
    </div>
    <div class="row main-area d-flex">
      <div class="card-columns">
        <vaults-component v-for="v in vaults" :key="v" :vaults-prop="v" />
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
import VaultsComponent from '../components/VaultsComponent'
export default {
  name: 'ActiveProfilePage',
  components: { KeepsComponent, NewVaultComponent, NewKeepComponent, VaultsComponent },
  setup() {
    const route = useRoute()
    onMounted(async() => {
      await keepsService.getKeeps()
      await vaultsService.getVaults()
      await profileService.getActiveProfile(route.params.profileId)
      await keepsService.getKeepsByProfile(route.params.profileId)
      await vaultsService.getVaultsByProfile(route.params.profileId)
    })
    return {
      profile: computed(() => AppState.profile),
      activeProfile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.activeProfileKeeps),
      // keeps: computed(() => AppState.keeps.filter(k => k.creatorId === AppState.activeProfile.id || k.creatorId === AppState.profile.id)),
      // keeps: computed(() => AppState.keeps.filter(k => k.creatorId === AppState.profile.id)),
      // vaults: computed(() => AppState.activeProfileVaults)
      vaults: computed(() => AppState.activeProfileVaults)
      // vaults: computed(() => AppState.activeProfileVaults)
    }
  }
}
</script>

<style lang="scss" scoped>
.main-area{
  user-select: none;
  margin-left: 5%;
  margin-right: 15%;
  > img{
    height: 100px;
    width: 100px;
  }
}

.profArea {
  margin-left: 5%;
}
</style>
