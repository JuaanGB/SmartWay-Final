<script setup>
import { useEquipos } from '@/stores/Equipos';
import { useNotificaciones } from '@/stores/Notificaciones';
import { useOperaciones } from '@/stores/Operaciones';
import { onMounted, ref } from 'vue';
import SelectOperacion from './SelectOperacion.vue';

    const opStore = useOperaciones()
    const equipoStore = useEquipos()
    const notiStore = useNotificaciones()

    const nuevoNombre = ref('')
    const nuevoID = ref('')
    const nuevaEspecialidad = ref('')
    const nuevaOperacionID = ref('')

    onMounted( () => {
        opStore.getAllOperaciones()
    })

    async function createEquipo() {
        let exito = await equipoStore.createEquipo(nuevoID.value, nuevoNombre.value, nuevaEspecialidad.value, nuevaOperacionID.value)
        if (exito)
            notiStore.addNotificacion("success", 3000, "Nuevo equipo añadido correctamente.")
        else
            notiStore.addNotificacion("error", 3000, "Error al crear un equipo.")

        flushInputs()
    }

    function flushInputs() {
        nuevoID.value = ''
        nuevoNombre.value = ''
        nuevaEspecialidad.value = ''
        nuevaOperacionID.value = ''
    }

</script>

<template>

    <div class="card card-secondary shadow-sm bg-base-300 flex-row join-item w-100">
        <div class="card-body">
            <!-- Información siempre visible -->
            <div class="flex flex-row">
                <input class="input card-title w-54" type="text" placeholder="Nombre equipo" v-model="nuevoNombre">
                <h3 class="card-title ml-auto">(#</h3>
                <input class="input card-title w-20" type="text" placeholder="ID" v-model="nuevoID">
                <h3 class="card-title">)</h3>
            </div>
            <input class="input w-full" type="text" placeholder="Especialidad" v-model="nuevaEspecialidad">
            <SelectOperacion v-model="nuevaOperacionID"></SelectOperacion>
            <button class="btn btn-primary" @click="createEquipo">Añadir equipo</button>
        </div>    
    </div>
    
</template>

<style scoped>

</style>