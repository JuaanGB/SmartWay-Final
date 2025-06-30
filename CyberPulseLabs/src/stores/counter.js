import { ref, computed } from 'vue'
import { defineStore } from 'pinia'
import * as api from './API_Operaciones'

export const useOperaciones = defineStore('operaciones', () => {
	const operaciones = ref([])
	async function getAllOperaciones() {
		const res = await api._getAll()
		$patchOperaciones(res)
	}
	async function createOperacion(id, nombre, estado, inicio, fin) {
		const res = await api._create(id, nombre, estado, inicio, fin)
		if (res.ok) {
			$patchOperaciones(await res.json())
			return true
		}
		return false
	}
	async function deleteOperacion(id) {
		const res = await api._delete(id)
		if (res.ok) {
			$patchOperaciones(await res.json())
			return true
		}
		return false
	}
	async function updateOperaciones(id, nombre, estado, inicio, fin) {
		const res = await api._update(id, nombre, estado, inicio, fin)
		if (res.ok) {
			$patchOperaciones(await res.json())
			return true
		}
		return false	
	}
	function $patchOperaciones(res) {
		operaciones.value = res
	}

	return {operaciones, getAllOperaciones, createOperacion, deleteOperacion, updateOperaciones}
})

export const useNotificaciones = defineStore('notificaciones', () => {
	const notificaciones = ref([])
	function addNotificacion(tipo, ms, msj) {
		notificaciones.value.push({tipo, ms, msj})
	}
	return {notificaciones, addNotificacion}
})

export const useAgentes = defineStore('agentes', () => {
  const agentes = ref([]) // Inicialmente vacío, se completa cuando se accede a la view de agentes
  function getAllAgentes() {
  }
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
  return {agentes, getAllAgentes} // Aqui se devuelve todo lo creado arribas
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
