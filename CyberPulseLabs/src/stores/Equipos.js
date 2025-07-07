import { defineStore } from "pinia"
import { ref } from "vue"
import { API } from '@/stores/API'

export const useEquipos = defineStore('equipos', () => {
    
    const equipos = ref([])
    const api = new API('http://localhost:5152/api/Equipos')      
    const loadingAllEquipos = ref(false)  

    const countStatus = ref(false)

    // Operaciones con la API
    async function getAllEquipos() {
        loadingAllEquipos.value = true
        const res = await api._getAll()
        setEquipos(res)
        loadingAllEquipos.value = false
    }
    async function getEquipo(id) {
        let eq = null
        if (loadingAllEquipos.value == false) {
            eq = equipos.value.find( e => e.id == id)
            if (!eq) {
                eq = await api._get(id)
                equipos.value.push(eq)
            }
        }
        
        return eq
    }
    async function getCount() {
        countStatus.value = true
        let res = await api._getCount()
        if (!res)
            countStatus.value = false
        return res
    }
    async function createEquipo(nombre, especialidad, operacionID) {
        const res = await api._create(attributesToItem(undefined, nombre, especialidad, operacionID))
        if (res.ok) {
            setEquipos(await res.json())
            return true
        }
        return false
    }
    async function deleteEquipo(id) {
        const res = await api._delete(id)
        if (res.ok) {
            setEquipos(await res.json())
            return true
        }
        return false
    }
    async function updateEquipo(id, nombre, especialidad, operacionID) {
        const res = await api._update(attributesToItem(id, nombre, especialidad, operacionID))
        if (res.ok) {
            setEquipos(await res.json())
            return true
        }
        return false	
    }
    function setEquipos(res) {
        equipos.value = res
    }
    function attributesToItem(id, nombre, especialidad, operacionID) {
        const equipo = {
            id: id,
            nombre: nombre,
            especialidad: especialidad,
            operacionID: operacionID
        }
        return equipo
    }

    return {equipos, loadingAllEquipos, getAllEquipos, getEquipo, getCount, countStatus,
        createEquipo, deleteEquipo, updateEquipo}
})