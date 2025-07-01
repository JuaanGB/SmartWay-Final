import { defineStore } from "pinia"
import { ref } from "vue"

export const useNotificaciones = defineStore('notificaciones', () => {
    const notificaciones = ref([])
    function addNotificacion(tipo, ms, msj) {
        notificaciones.value.push({tipo, ms, msj})
    }
    return {notificaciones, addNotificacion}
})