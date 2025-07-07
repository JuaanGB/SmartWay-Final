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
    const nuevaEspecialidad = ref('')
    const nuevaOperacionID = ref('')

    onMounted( () => {
        opStore.getAllOperaciones()
    })

    async function createEquipo() {
        let exito = await equipoStore.createEquipo(nuevoNombre.value, nuevaEspecialidad.value, nuevaOperacionID.value)
        if (exito)
            notiStore.addNotificacion("success", 3000, "Nuevo equipo añadido correctamente.")
        else
            notiStore.addNotificacion("error", 3000, "Error al crear un equipo.")

        flushInputs()
    }

    function flushInputs() {
        nuevoNombre.value = ''
        nuevaEspecialidad.value = ''
        nuevaOperacionID.value = ''
    }

</script>

<template>

    <div class="card card-secondary shadow-md bg-base-200 flex-row join-item w-auto">
        <div class="card-body">
            <!-- Información siempre visible -->
            <div class="flex flex-row">
                <input class="input card-title w-full" type="text" placeholder="Nombre equipo" v-model="nuevoNombre">
            </div>
            <input class="input w-full" type="text" placeholder="Especialidad" v-model="nuevaEspecialidad">
            <SelectOperacion v-model="nuevaOperacionID"></SelectOperacion>
            <button class="btn btn-primary" @click="createEquipo">
                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 16 16"><!-- Icon from HeroIcons by Refactoring UI Inc - https://github.com/tailwindlabs/heroicons/blob/master/LICENSE --><path fill="currentColor" d="M8.75 3.75a.75.75 0 0 0-1.5 0v3.5h-3.5a.75.75 0 0 0 0 1.5h3.5v3.5a.75.75 0 0 0 1.5 0v-3.5h3.5a.75.75 0 0 0 0-1.5h-3.5z"/></svg>
                Añadir equipo
            </button>
        </div>    
    </div>
    
</template>

<style scoped>

</style>