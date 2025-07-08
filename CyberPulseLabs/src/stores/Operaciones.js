import { defineStore } from "pinia"
import { ref } from "vue"
import { API } from "./API"

export const useOperaciones = defineStore('operaciones', () => {
    
    const operaciones = ref([])
    const operacionAct = ref({})
    const api = new API('http://localhost:5152/api/Operaciones')

    const countStatus = ref(false)

    // Filtros
    function filterOperaciones(nombre, estado, inicio, fin) {
        operaciones.value = operaciones.value.filter( op => {
            let res = true
            if (nombre)
                res = res && op.nombre.toLowerCase().includes(nombre)
            if (estado)
                res = res && (op.estado == estado)
            if (inicio && fin) // Si tengo inicio y fin, filtro operaciones dentro del rango [inicio, fin]
                res = res && (op.fechaInicio >= inicio && op.fechaFin <= fin);
            else if (inicio) // Si solo inicio, filtro operaciones con fechaInicio >= inicio
                res = res && (op.fechaInicio >= inicio);
            else if (fin) // Si solo fin, filtro operaciones con fechaFin <= fin
                res = res && (op.fechaFin <= fin);

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
    async function getCount() {
        countStatus.value = true
        let res = await api._getCount()
        if (res === false)
            countStatus.value = false
        return res
    }
    async function getOperacion(id) {
        let op = operaciones.value.filter( o => o.id == id)[0]
        if (!op) {
            op = await api._get(id)
            operaciones.value.push(op)
        }
        operacionAct.value = op
        
        return operacionAct
    }
    async function createOperacion(id, nombre, estado, inicio, fin) {
        const res = await api._create(attributesToItem(id, nombre, estado, inicio, fin))
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
        const res = await api._update(attributesToItem(id, nombre, estado, inicio, fin))
        if (res.ok) {
            $patchOperaciones(await res.json())
            return true
        }
        return false	
    }
    function $patchOperaciones(res) {
        operaciones.value = res
    }
    function attributesToItem(id, nombre, estado, inicio, fin) {
        return {
            id: id,
            nombre: nombre,
            estado: estado,
            fechaInicio: inicio,
            fechaFin: fin
        }
    }

    return {operaciones, operacionAct, filterOperaciones, getOperacion, getCount, countStatus,
            getAllOperaciones, createOperacion, deleteOperacion, updateOperaciones}
})