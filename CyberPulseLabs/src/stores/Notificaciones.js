import { defineStore } from "pinia"
import { ref } from "vue"

export const useNotificaciones = defineStore('notificaciones', () => {
    const notificaciones = ref([])

    function addNotificacion(tipo, ms, msj) {
        const id = Date.now()
        notificaciones.value.push({id, tipo, ms, msj})

        setTimeout(() => {
            notificaciones.value = notificaciones.value.filter( n => n.id != id)
        }, ms);
    }

    return { notificaciones, addNotificacion }
})