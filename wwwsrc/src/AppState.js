import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  profile: {},
  activeProfile: {},
  activeProfileKeeps: [],
  activeProfileVaults: [],
  keeps: [],
  vaults: [],
  vaultKeeps: [],
  userVaults: [],
  activeKeep: {},
  activeVault: [],
  activeVaultKeeps: []
})
