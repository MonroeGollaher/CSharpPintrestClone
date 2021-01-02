<template>
  <div class="home flex-grow-1 d-flex flex-column align-items-center justify-content-center">
    <keeps-component v-for="k in keeps" :key="k" :keeps-prop="k" />
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import KeepsComponent from '../components/KeepsComponent'
import { keepsService } from '../services/KeepsService'
import { AppState } from '../AppState'
export default {
  name: 'Home',
  components: { KeepsComponent },
  setup() {
    onMounted(async() => {
      await keepsService.getKeeps()
    })
    return {
      keeps: computed(() => AppState.keeps),
      profile: computed(() => AppState.profile)
    }
  }
}
</script>

<style scoped lang="scss">
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}
</style>
