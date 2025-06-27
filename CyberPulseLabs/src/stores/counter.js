import { ref, computed } from 'vue'
import { defineStore } from 'pinia'

export const useAgentes = defineStore('agentes', () => {
  const agentes = ref([]) // Inicialmente vacío, se completa cuando se accede a la view de agentes
  function getAllAgentes() {}
  function createAgente() {
    // llamada a api
    // mutación con this.$createAgente() para actualizar estado ?
  }
  function deleteAgente() {}
  function updateAgente() {}
  // Crear método $reset si necesita resetear valores en algún momento
  /* Mutaciones:
  store.$patch((state) => {
    state.items.push({ name: 'shoes', quantity: 1 })
    state.hasChanged = true
  })
  
  */
  return {agentes} // Aqui se devuelve todo lo creado arribas
})

/* --------------------------- EJEMPLO DE STORE ------------------------  */


export const useCounterStore = defineStore('counter', () => {
  const count = ref(0)
  const doubleCount = computed(() => count.value * 2)
  function increment() {
    count.value++
  }

  return { count, doubleCount, increment }
})
