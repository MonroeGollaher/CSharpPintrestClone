<template>
  <div class="home container-fluid">
    <div class="row pt-3">
      <div class="card-columns keeps">
        <keeps-component v-for="k in keeps" :key="k" :keeps-prop="k" />
      </div>
    </div>
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
  margin-left: 10%;
  margin-right: 10%;
  > img{
    height: 200px;
    width: 200px;
  }
}

</style>
