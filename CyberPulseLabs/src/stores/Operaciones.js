import { defineStore } from "pinia"
import { ref } from "vue"
import * as api from './API_Operaciones'

export const useOperaciones = defineStore('operaciones', () => {
    
    const operaciones = ref([])

    // Filtros
    function filterOperaciones(nombre, estado, inicio, fin) {
        operaciones.value = operaciones.value.filter( op => {
            let res = true
            if (nombre)
                res = res && op.nombre.toLowerCase().includes(nombre)
            if (estado)
                res = res && (op.estado == estado)
            if (inicio != '')
                res = res && (op.fechaInicio >= inicio)
            if (fin != '')
                res = res && (op.fechaFin <= fin)

            return res
        })
    }

    // Operaciones con la API
    async function getAllOperaciones() {
        const res = await api._getAll()
        if (res != false)
            $patchOperaciones(res)
        return res
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

    return {operaciones, filterOperaciones, getAllOperaciones, createOperacion, deleteOperacion, updateOperaciones}
})