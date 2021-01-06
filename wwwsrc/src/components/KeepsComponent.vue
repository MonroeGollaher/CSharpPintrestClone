<template>
  <div class="keeps-component container-fluid card" @click="viewCount(keep.id, keep)" data-toggle="modal" :data-target="'#activeKeepModal' + keep.id">
    <img class="card-img" :src="keep.img">
    <div class="card-img-overlay d-flex justify-content-between flex-column">
      <h4 class="card-title text-light text-center">
        {{ keep.name }}
      </h4>
      <div class="d-flex justify-content-end">
        <img :src="keep.creator.picture" class="rounded-circle creator-pic" height="40" width="40">
      </div>
    </div>
    <!-- <div class="keeps keepImage" :style="'background-image: url('+keep.img+')'" data-toggle="modal" :data-target="'#activeKeepModal' + keep.id">
      <img :src="keep.creator.picture" class="rounded-circle shadow" height="40">
      <h4 class="text-light">
        {{ keep.name }}
      </h4>
      <div class="div card-img-overlay">
      </div>
    </div> -->
  </div>

  <div class="modal fade keepModal"
       :id="'activeKeepModal' + keep.id"
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
                <router-link :to="{ name: 'ActiveProfile', params: { profileId: keep.creator.id }}" @click="setActiveKeep(keep.id)">
                  <img :src="keep.creator.picture" class="rounded-circle img-fluid" />
                </router-link>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { vaultKeepsService } from '../services/VaultKeepsService'
import { AppState } from '../AppState'
import $ from 'jquery'
import Swal from 'sweetalert2'

export default {
  name: 'KeepsComponent',
  props: ['keepsProp'],
  setup(props) {
    const state = reactive({
      vaultId: null
    })
    onMounted(() => {
      vaultsService.getVaults()
    })
    return {
      state,
      profile: computed(() => AppState.profile),
      vaults: computed(() => AppState.vaults.filter(v => v.creatorId === AppState.profile.id)),
      keep: computed(() => props.keepsProp),
      setActiveKeep(keepId) {
        keepsService.setActiveKeep(keepId)
        $('#activeKeepModal').modal('hide')
      },
      addToVault(vaultId, keepId) {
        const newKeep = {
          vaultId: vaultId,
          keepId: keepId
        }
        vaultKeepsService.addKeepToVault(newKeep)
        $('#activeKeepModal').modal('hide')
      },
      deleteKeep(keepId) {
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
            keepsService.deleteKeep(keepId)
            // eslint-disable-next-line no-undef
            Swal.fire(
              'Deleted!',
              'Your file has been deleted.',
              'success'
            )
          }
        })
        // keepsService.deleteKeep(keepId)
        $('#activeKeepModal').modal('hide')
      },
      viewCount(keepId, keep) {
        keepsService.viewCount(keepId, keep)
      },
      keptCount(keep, keepId) {
        keepsService.keptCount(keep, keepId)
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
.keepImage {
  height: 300px;
  background-size: cover;
  background-position: center;
}
img:hover {
  opacity: 0.5;
  transition:all 1s ease;
}
</style>
