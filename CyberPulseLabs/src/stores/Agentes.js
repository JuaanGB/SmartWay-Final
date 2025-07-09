import { defineStore } from "pinia";
import { computed, ref } from "vue";
import { API } from "./API";


export const useAgentes = defineStore('agentes', () => {

    const agentes = ref([])
    const api = new API('http://localhost:5152/api/Agentes')
    const agenteAct = ref({})

    const ordenInverso = ref(false)
    const agentesOrdenados = computed( () => {
        if (ordenInverso.value) {
            return [...agentes.value].reverse()
        }
        return agentes.value
    })
    function toggleOrden() {
        ordenInverso.value = !ordenInverso.value
    }

    const countStatus = ref(false)

    // Operaciones con la API
    async function getAllAgentes() {
        const res = await api._getAll()
        if (res != false)
            setAgentes(res)
        return res
    }

    async function setAgenteAct(id) {
        if (id == null) return
        let ag = agentes.value.find( a => a.id == id)
        console.log(ag)
        if (!ag) {
            ag = await api._get(id)
        }
        agenteAct.value = ag
    }

    async function getCount() {
        countStatus.value = true
        let res = await api._getCount()
        if (res === false)
            countStatus.value = false
        return res
    }

    async function createAgente(nombre, estado, equipoId, rango) {
        equipoId = !equipoId ? null : equipoId // Necesario porque si no la API busca un id con clave ""
        const res = await api._create(
            attributesToItem(undefined, nombre, estado, equipoId, rango))
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

    return {agentes, agentesOrdenados, agenteAct, setAgenteAct, getAllAgentes, getCount, createAgente, countStatus, toggleOrden,
        deleteAgente, updateAgente
    }
})