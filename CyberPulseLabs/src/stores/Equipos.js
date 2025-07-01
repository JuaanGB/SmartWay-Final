import { defineStore } from "pinia"
import { ref } from "vue"
import * as api from './API_Equipos'

export const useEquipos = defineStore('equipos', () => {
    
    const equipos = ref([])

    // Filtros
        

    // Operaciones con la API
    async function getAllEquipos() {
        const res = await api._getAll()
        $patchEquipos(res)
    }
    async function createEquipo(id, nombre, especialidad, operacionID) {
        const res = await api._create(id, nombre, especialidad, operacionID)
        if (res.ok) {
            $patchEquipos(await res.json())
            return true
        }
        return false
    }
    async function deleteEquipo(id) {
        const res = await api._delete(id)
        if (res.ok) {
            $patchEquipos(await res.json())
            return true
        }
        return false
    }
    async function updateEquipo(id, nombre, especialidad, operacionID) {
        const res = await api._update(id, nombre, especialidad, operacionID)
        if (res.ok) {
            $patchEquipos(await res.json())
            return true
        }
        return false	
    }
    function $patchEquipos(res) {
        equipos.value = res
    }

    return {equipos, getAllEquipos, createEquipo, deleteEquipo, updateEquipo}
})