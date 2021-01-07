<template>
  <div class="vault-keeps-component card container-fluid" data-toggle="modal">
    <img :src="vaultKeepProp.img" class="img-fluid card-img" :data-target="'#activeKeepModal' + vaultKeepProp.id" />
    <div class="card-img-overlay justify-content-start">
      <button v-if="vaultKeepProp.creatorId == profile.id" @click="removeFromVault(vaultKeepProp.vaultKeepId)" class="btn border-0 bg-transparent text-danger">
        <i class="far fa-trash-alt ml-1"></i>
      </button>
    </div>
  </div>
  <!-- <div class="modal fade keepModal"
       :id="'activeKeepModal' + vaultKeepProp.id"
       tabindex="-1"
       role="dialog"
       aria-labelledby="exampleModalLabel"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered modal-lg h-100" role="document">
      <div class="modal-content row flex-row">
        <div class="row justify-content-center w-100">
          <div class="col-6">
            <img :src="keep.img" class="img-fluid p-2" />
          </div>
          <div class="col-6 text-center">
            <div class="row justify-content-end">
              <div class="col">
                <i class="far fa-times-circle close mt-3 text-danger" data-dismiss="modal" aria-label="Close" type="button"></i>
              </div>
            </div>
            <div class="row justify-content-center mb-1">
              <div class="col-3">
                <h4 class="keeprGreen">
                  <i class="far fa-eye"> {{ keep.views }}</i>
                </h4>
              </div>
              <div class="col-3">
                <h4 class="keeprGreen">
                  <i class="fab fa-kaggle"> {{ keep.keeps }}</i>
                </h4>
              </div>
              <div class="col-3">
                <h4 class="keeprGreen">
                  <i class="fas fa-share-alt"> {{ keep.shares }}</i>
                </h4>
              </div>
            </div>
            <div class="row justify-content-between">
              <div class="col-12">
                <h1>{{ keep.name }}</h1>
              </div>
              <div class="col-12">
                <p>{{ keep.description }}</p>
              </div>
            </div>

            <div class="row justify-content-center p-5">
              <div class="col-3">
                <form @submit.prevent="addToVault(state.vaultId, keep.id)">
                  <div class="row justify-content-center">
                    <select v-model="state.vaultId"
                            name=""
                            id=""
                            class=""
                            data-option-label="Select a Vault"
                            required
                    >
                      <option disabled value="">
                        Select a vault
                      </option>
                      <option @click="keptCount(keep, keep.id)" v-for="v in vaults" :key="v.id" :value="v.id">
                        {{ v.name }}
                      </option>
                    </select>
                  </div>
                  <div class="row justify-content-center">
                    <button type="submit" class="btn btn-success" v-if="profile.id">
                      Add to Vault
                    </button>
                  </div>
                </form>
              </div>

              <div class="col-3">
                <button @click="deleteKeep(keep.id)" v-if="profile.id == keep.creatorId" class="bg-transparent border-0 text-danger">
                  <i class="fas fa-trash"></i>
                </button>
              </div>
              <div class="col-3">
                <router-link :to="{ name: 'ActiveProfile', params: { profileId: keep.creator.id }}">
                  <img :src="keep.creator.picture" class="rounded-circle img-fluid" />
                </router-link>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div> -->
</template>

<script>
import { computed } from 'vue'
import { vaultKeepsService } from '../services/VaultKeepsService'
import { AppState } from '../AppState'
import Swal from 'sweetalert2'
export default {
  name: 'VaultKeepsComponent',
  props: ['vaultKeepProp'],
  setup(props) {
    return {
      profile: computed(() => AppState.profile),
      vaultKeep: computed(() => props.vaultKeepProp),
      removeFromVault(vaultId) {
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
            vaultKeepsService.removeVaultKeep(vaultId)
            // eslint-disable-next-line no-undef
            Swal.fire(
              'Deleted!',
              'Your file has been deleted.',
              'success'
            )
          }
        })
        vaultKeepsService.removeVaultKeep(vaultId)
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
img {
    width: 300px;
    height: 300px;
    object-fit: cover;
}
</style>
