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
import { keepsService } from '../services/KeepsService'
// import $ from 'jquery'
import Swal from 'sweetalert2'

export default {
  name: 'ActiveVault',
  components: { VaultKeepsComponent },
  setup() {
    const route = useRoute()
    onMounted(async() => {
      await vaultsService.getVaults()
      keepsService.getKeeps()
      vaultKeepsService.getActiveVaultKeeps(route.params.vaultId)
      vaultKeepsService.getAllVaultKeeps()
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
            vaultsService.deleteKeep(vaultId)
            // eslint-disable-next-line no-undef
            Swal.fire(
              'Deleted!',
              'Your file has been deleted.',
              'success'
            )
          }
        })
        // $('#activeKeepModal').modal('hide')
      }
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
