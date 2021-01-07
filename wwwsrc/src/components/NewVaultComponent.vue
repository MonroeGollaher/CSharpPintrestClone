<template>
  <div class="new-vault-component container-fluid">
    <button class="btn border-0 bg-transparent rounded" data-toggle="modal" data-target="#newVaultModal">
      <i class="fas fa-plus ml-2 keeprGreen"></i>
    </button>
    <div class="modal" id="newVaultModal" tabindex="-1">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">
              New Vault
            </h5>
            <button type="button" class="btn-close bg-transparent border-0 text-danger" data-bs-dismiss="modal" aria-label="Close">
              <i class="far fa-times-circle"></i>
            </button>
          </div>
          <form @submit.prevent="createVault(state.newVault)" class="p-2">
            <div class="form-group">
              <label for="itemTitle">Title</label>
              <input type="text"
                     class="form-control"
                     id="listTitle"
                     aria-describedby="listTitle"
                     placeholder="What's your vault called?"
                     v-model="state.newVault.name"
              >
            </div>
            <!-- <div class="form-group">
              <label for="itemTitle">Image URL</label>
              <input type="text"
                     class="form-control"
                     id="listTitle"
                     aria-describedby="listTitle"
                     v-model="state.newVault.image"
              >
            </div> -->
            <div class="form-group">
              <label for="itemTitle">Description</label>
              <input type="text"
                     class="form-control"
                     id="listTitle"
                     aria-describedby="listTitle"
                     v-model="state.newVault.description"
              >
            </div>
            <div class="row justify-content-center align-items-center">
              <label for="image">Private?</label>
              <input class="ml-3" type="checkbox" v-model="state.newVault.isPrivate">
            </div>
            <div class="row justify-content-center">
              <button type="submit" class="btn keeprBtnColor mb-3 text-light">
                Add vault
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive } from 'vue'
import { vaultsService } from '../services/VaultsService'
import $ from 'jquery'
export default {
  name: 'NewVaultComponent',
  setup() {
    const state = reactive({
      newVault: {
        name: null,
        description: null,
        isPrivate: false
      }
    })
    return {
      state,
      createVault(newVault) {
        debugger
        vaultsService.createVault(newVault)
        state.newVault = {}
        $('#newVaultModal').modal('hide')
        vaultsService.getVaults()
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
.keeprBtnColor{
  background-color: rgb(8, 175, 137);
}
</style>
