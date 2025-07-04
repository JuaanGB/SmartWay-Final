import { defineStore } from "pinia";
import { ref } from "vue";
import { API } from "./API";


export const useAgentes = defineStore('agentes', () => {

    const agentes = ref([])
    const api = new API('http://localhost:5152/api/Agentes')

    // Operaciones con la API
    async function getAllAgentes() {
        const res = await api._getAll()
        if (res != false)
            setAgentes(res)
        return res
    }

    async function getCount() {
        return await api._getCount()
    }

    async function createAgente(nombre, estado, equipoId, rango) {
        const res = await api._create(
            attributesToItem(undefined,nombre, estado, equipoId, rango))
        if (res.ok) {
            setAgentes(await res.json())
            return true
        }
        return false
    }

    async function deleteAgente(id) {
        const res = await api._delete(id)
        if (res.ok) {
            setAgentes(await res.json())
            return true
        }
        return false
    }

    async function updateAgente(id, nombre, estado, equipoId, rango) {
        const res = await api._update(
            attributesToItem(id, nombre, estado, equipoId, rango))
        if (res.ok) {
            setAgentes(await res.json())
            return true
        }
        return false	
    }

    function setAgentes(res) {
        agentes.value = res
    }

    function attributesToItem(id, nombre, estado, equipoId, rango) {
        return {
            id: id,
            nombre: nombre,
            activo: estado,
            equipoId: equipoId,
            rango: rango
        }
    }

    return {agentes, getAllAgentes, getCount, createAgente,
        deleteAgente, updateAgente
    }
})