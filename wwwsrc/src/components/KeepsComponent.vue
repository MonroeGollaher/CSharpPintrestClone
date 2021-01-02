<template>
  <div class="keeps-component col-2 shadow card">
    <div class="keeps keepImage" :style="'background-image: url('+keep.image+')'">
      <h4>{{ keep.name }}</h4>
    </div>
  </div>
</template>

<script>
import { computed } from 'vue'
export default {
  name: 'KeepsComponent',
  props: ['keepsProp'],
  setup(props) {
    return {
      keep: computed(() => props.keepsProp)
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
