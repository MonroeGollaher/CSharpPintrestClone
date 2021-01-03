<template>
  <div class="keeps-component col-2 shadow card container-fluid">
    <div class="keeps keepImage" :style="'background-image: url('+keep.image+')'" data-toggle="modal" :data-target="'#activeKeepModal' + keep.id">
      <h4>{{ keep.name }}</h4>
    </div>
  </div>
  <div class="modal fade keepModal"
       :id="'activeKeepModal' + keep.id"
       tabindex="-1"
       role="dialog"
       aria-labelledby="exampleModalLabel"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered" role="document">
      <div class="modal-content">
        <div class="row">
          <div class="col-6">
            <img :src="keep.image" class="img-fluid" />
          </div>
          <div class="col-6">
            <h5>{{ keep.name }}</h5>
            <div class="row">
              <div class="col-12">
                <p>{{ keep.description }}</p>
              </div>
            </div>

            <div class="row justify-content-center">
              <div class="col-12">
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
                      <option v-for="v in vaults" :key="v.id" :value="v.id">
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
                <i class="fas fa-trash"></i>
              </div>
              <div class="col-3">
                <i class="far fa-user"></i>
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
      vaults: computed(() => AppState.vaults),
      keep: computed(() => props.keepsProp),
      setActiveKeep(keepId) {
        keepsService.setActiveKeep(keepId)
      },
      addToVault(vaultId, keepId) {
        const newKeep = {
          vaultId: vaultId,
          keepId: keepId
        }
        vaultKeepsService.addKeepToVault(newKeep)
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
