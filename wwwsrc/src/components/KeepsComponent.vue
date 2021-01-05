<template>
  <div class="keeps-component container-fluid card" @click="viewCount(keep.id, keep)" data-toggle="modal" :data-target="'#activeKeepModal' + keep.id">
    <img class="card-img" :src="keep.image">
    <div class="card-img-overlay d-flex justify-content-between flex-column">
      <h4 class="card-title text-light text-center">
        {{ keep.name }}
      </h4>
      <div class="d-flex justify-content-end">
        <img :src="keep.creator.picture" class="rounded-circle creator-pic" height="40" width="40">
      </div>
    </div>
    <!-- <div class="keeps keepImage" :style="'background-image: url('+keep.image+')'" data-toggle="modal" :data-target="'#activeKeepModal' + keep.id">
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
    <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
      <div class="modal-content">
        <div class="row">
          <div class="col-6">
            <img :src="keep.image" class="img-fluid" />
          </div>
          <div class="col-6 text-center">
            <div class="row">
              <div class="col-4">
                <p>View Count: {{ keep.views }}</p>
              </div>
              <div class="col-4">
                <p>Keeps: {{ keep.keeps }}</p>
              </div>
              <div class="col-4">
                <p>Shares: {{ keep.shares }}</p>
              </div>
              <div class="col-12">
                <h1>{{ keep.name }}</h1>
              </div>
              <div class="col-12">
                <p>{{ keep.description }}</p>
                <!-- <router-link :to="{ name: 'ActiveProfile', params: { profileId: keep.creator.id }}">
                  <h5>{{ keep.creator.name }}</h5>
                </router-link> -->
              </div>
            </div>

            <div class="row justify-content-center">
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
                        {{ v.title }}
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
                  <h5>{{ keep.creator.name }}</h5>
                </router-link>
              </div>
            </div>
          </div>
        </div>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
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
      },
      deleteKeep(keepId) {
        keepsService.deleteKeep(keepId)
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
</style>
