<template>
  <div class="about container-fluid">
    <div class="row pt-4 profArea">
      <div class="col-2">
        <img class="rounded" :src="profile.picture" alt="" />
      </div>
      <div class="col-2">
        <h1>{{ profile.name }}</h1>
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
    <div class="row main-area keeps">
      <div class="col-6">
        <h1>
          Keeps: <button class="btn border-0">
            <new-keep-component />
          </button>
        </h1>
      </div>
    </div>
    <div class="row main-area">
      <div class="card-columns">
        <keeps-component v-for="k in keeps" :key="k" :keeps-prop="k" />
      </div>
    </div>
    <div class="row main-area">
      <h1>
        Vaults: <button class="btn border-0">
          <new-vault-component />
        </button>
      </h1>
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
      vaults: computed(() => AppState.vaults.filter(v => v.creatorId === AppState.profile.id)),
      keeps: computed(() => AppState.keeps.filter(k => k.creatorId === AppState.profile.id)),
      profile: computed(() => AppState.profile),
      setActiveVault(vaultId) {
        vaultsService.setActiveVault(vaultId)
      }
    }
  }
}
</script>

<style scoped>
.main-area{
  user-select: none;
  margin-left: 5%;
  margin-right: 15%;
  /* > img{
    height: 100px;
    width: 100px;
  } */
}
.profArea {
  margin-left: 5%;
}
</style>
