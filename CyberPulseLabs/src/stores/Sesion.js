import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import { jwtDecode } from 'jwt-decode'
import { API } from './API'
import * as apiAuth from '@/stores/Auth'
import { useOperaciones } from './Operaciones'
import { useEquipos } from './Equipos'
import { useAgentes } from './Agentes'

export const useSesion = defineStore('sesion', () => {
    
    const token = ref(localStorage.getItem('token'))
    const agenteAct = ref({})

    const opStore = useOperaciones()
    const eqStore = useEquipos()
    const agStore = useAgentes()

    const apiAg = new API('http://localhost:5152/api/Agentes')

    // Computado: decodifica el token
    const decoded = computed(() => {
        if (!token.value) return null
        try {
            return jwtDecode(token.value)
        } catch (e) {
            localStorage.removeItem('token')
            token.value = null
            return null
        }
    })

    // Computado: si el token es válido
    const isValidToken = computed(() => {
        const now = Date.now() / 1000
        return decoded.value?.exp && decoded.value.exp > now
    })

    const agenteRol = computed(() => { decoded.value.role })

    const userId = computed(() => decoded.value?.nameid || null)
    const userFirstLetter = computed(() => {
        if (isValidToken.value)
            return decoded.value?.unique_name?.charAt(0).toUpperCase()
        else return '?'
    })

    const agenteId = computed(() => {
        if (isValidToken.value)
        return agenteAct.value?.id || 0}
    )
    const agenteNombre = computed(() => agenteAct.value?.nombre || '')
    const agenteRango = computed(() => agenteAct.value?.rango || '')
    const agenteEquipoId = computed(() => agenteAct.value?.equipoId || '')
    const agenteActivo = computed(() => agenteAct.value?.activo ?? false)
    const isAdmin = computed(() => agenteAct.value?.rol == "ADMIN" ?? false)

    // Login
    async function login(email, contraseña) {
        const res = await apiAuth._login(email, contraseña)
        token.value = localStorage.getItem('token')

        if (res && isValidToken.value) {
            const id = userId.value
            agenteAct.value = await apiAg._get(id)
        }

        return res
    }

    // Set agente actual (por si recarga el usuario en la vista del perfil)
    async function setAgenteAct() {
        if (isValidToken.value) {
            agenteAct.value = await apiAg._get(userId.value)
        }
    }

    // Registro
    async function register(nombre, email, contraseña) {
        const res = await apiAuth._register(nombre, email, contraseña)
        token.value = localStorage.getItem('token')

        if (res && isValidToken.value) {
            const id = userId.value
            agenteAct.value = await apiAg._get(id)
        }

        return res
    }

    // Cambiar contraseña
    async function changePassword(antigua, nueva) {
        return await apiAuth._changePassword(userId.value, antigua, nueva)
    }

    function logout() {
        localStorage.removeItem('token')
        token.value = null
        agenteAct.value = {}

        // Vaciar los stores
        opStore.flushOperaciones()
        eqStore.flushEquipos()
        agStore.flushAgentes()
    }

    return {
        // estado
        token,
        agenteAct,

        // acciones
        login,
        register,
        changePassword,
        logout,
        setAgenteAct,
        

        // getters
        isValidToken,
        userId,
        userFirstLetter,
        agenteRol,


        // agente computed
        agenteId,
        agenteNombre,
        agenteRango,
        agenteEquipoId,
        agenteActivo,
        isAdmin
    }
})
