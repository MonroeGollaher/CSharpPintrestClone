<template>
  <div class="active-profile-page container-fluid">
    <h5>This is the Active Profile Page</h5>
    <h5>User: {{ activeProfile.name }} </h5>
    <div class="row ml-1">
      <h5>Keeps: </h5>
      <keeps-component v-for="k in keeps" :key="k" :keeps-prop="k" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import { profileService } from '../services/ProfileService'
import { keepsService } from '../services/KeepsService'
import KeepsComponent from '../components/KeepsComponent'
export default {
  name: 'ActiveProfilePage',
  components: { KeepsComponent },
  setup() {
    const route = useRoute()
    onMounted(() => {
      keepsService.getKeepsByProfile(route.params.profileId)
      profileService.getActiveProfile(route.params.profileId)
    })
    return {
      profile: computed(() => AppState.profile),
      activeProfile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.activeProfileKeeps)
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
