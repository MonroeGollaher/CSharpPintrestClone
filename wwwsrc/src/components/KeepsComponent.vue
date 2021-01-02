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
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <h5 class="modal-title" id="exampleModalLabel">
          {{ keep.name }}
        </h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from 'vue'
import { keepsService } from '../services/KeepsService'
export default {
  name: 'KeepsComponent',
  props: ['keepsProp'],
  setup(props) {
    return {
      keep: computed(() => props.keepsProp),
      setActiveKeep(keepId) {
        keepsService.setActiveKeep(keepId)
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
