<template>
  <div class="active-vault container-fluid">
    <div class="row text-start main-area pt-4">
      <div class="col-10">
        <h1>
          {{ activeVault.name }}
        </h1>
        <p>
          {{ activeVault.description }} <button v-if="profile.id == activeVault.creatorId" @click="removeVault(activeVault.id)" class="btn border-0 bg-transparent">
            <i class="far fa-trash-alt ml-1 text-danger"></i>
          </button>
        </p>
        <!-- <div class="col-2">
          <button v-if="profile.id == activeVault.creatorId" @click="removeVault(activeVault.id)" class="btn border-0 bg-transparent">
            <i class="fas fa-trash text-danger"></i>
          </button>
        </div> -->
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
import { keepsService } from '../services/KeepsService'
// import $ from 'jquery'
import Swal from 'sweetalert2'

export default {
  name: 'ActiveVault',
  components: { VaultKeepsComponent },
  setup() {
    const route = useRoute()
    onMounted(async() => {
      await vaultsService.setActiveVault(route.params.vaultId)
      await vaultsService.getVaults()
      await keepsService.getKeeps()
      await vaultKeepsService.getActiveVaultKeeps(route.params.vaultId)
      await vaultKeepsService.getAllVaultKeeps()
    })
    return {
      profile: computed(() => AppState.profile),
      activeProfile: computed(() => AppState.activeProfile),
      activeVault: computed(() => AppState.activeVault),
      activeKeeps: computed(() => AppState.activeVaultKeeps),
      removeVault(vaultId) {
        Swal.fire({
          title: 'Are you sure?',
          text: "You won't be able to revert this!",
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
          if (result.isConfirmed) {
            vaultsService.deleteVault(vaultId)
            Swal.fire(
              'Deleted!',
              'Your file has been deleted.',
              'success'
            )
          }
        })
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.main-area{
  user-select: none;
  margin-left: 5%;
  margin-right: 15%;
  /* > img{
    height: 100px;
    width: 100px;
  } */
}
</style>
