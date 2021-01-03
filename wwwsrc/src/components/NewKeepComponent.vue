<template>
  <div class="new-keep-component container-fluid">
    <button class="btn border-0 bg-transparent rounded" data-toggle="modal" data-target="#newKeepModal">
      <h5>Keeps <i class="fas fa-plus ml-2 keeprGreen"></i></h5>
    </button>
    <div class="modal" id="newKeepModal" tabindex="-1">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">
              New Keep
            </h5>
            <button type="button" class="btn-close bg-transparent border-0 text-danger" data-bs-dismiss="modal" aria-label="Close">
              <i class="far fa-times-circle"></i>
            </button>
          </div>
          <form @submit.prevent="createKeep" class="p-2">
            <div class="form-group">
              <label for="itemTitle">Title</label>
              <input type="text"
                     class="form-control"
                     id="keepTitle"
                     aria-describedby="keepTitle"
                     placeholder="Title..."
                     v-model="state.newKeep.name"
              >
            </div>
            <div class="form-group">
              <label for="itemTitle">Image Url</label>
              <input type="text"
                     class="form-control"
                     id="imageUrl"
                     aria-describedby="imageUrl"
                     placeholder="URL..."
                     v-model="state.newKeep.image"
              >
            </div>
            <div class="form-group">
              <label for="itemTitle">Description</label>
              <input type="text"
                     class="form-control"
                     id="description"
                     aria-describedby="description"
                     placeholder="Keep Description..."
                     v-model="state.newKeep.description"
              >
            </div>
            <!-- <div class="row justify-content-center align-items-center">
              <label for="image">Private?</label>
              <input class="ml-3" type="checkbox">
            </div> -->
            <div class="row justify-content-center">
              <button type="submit" class="btn keeprBtnColor mb-3 text-light">
                Create
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
import { keepsService } from '../services/KeepsService'
export default {
  name: 'NewKeepComponent',
  setup() {
    const state = reactive({
      newKeep: {

      }
    })
    return {
      state,
      createKeep() {
        keepsService.addKeep(state.newKeep)
        state.newKeep = {}
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
