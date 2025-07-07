<script setup>
import { computed, ref } from 'vue';
import ActividadAgente from './ActividadAgente.vue';
import { useAgentes } from '@/stores/Agentes';
import { useNotificaciones } from '@/stores/Notificaciones';
import SelectEquipo from './SelectEquipo.vue';

    const agStore = useAgentes()
    const notiStore = useNotificaciones()

    const nombre = ref('')
    const activo = ref(false)
    const equipoId = ref(0)
    const rango = ref('')

    async function addAgente() {
        if (!nombre.value || !rango.value) {
            notiStore.addNotificacion("error", 3000, "Debes completar todos los campos.")
            return
        }

        const exito = await agStore.createAgente(nombre.value, activo.value, equipoId.value, rango.value)
        if (exito) {
            notiStore.addNotificacion("success", 3000, "Agente añadido correctamente.")
            flushInputs()
        }
        else 
            notiStore.addNotificacion("error", 3000, "Error al añadir un agente.")
    }

    function updateNuevoEquipo(id) {
        equipoId.value = id
    }

    function flushInputs() {
        nombre.value = ''
        activo.value = false
        equipoId.value = 0
        rango.value = ''
    }


</script>

<template>

    <div class="card bg-base-200 border border-2 border-primary shadow-md w-50 h-70">
        <div class="card-body items-center">
            <!-- Avatar del nombre -->
            <div class="avatar avatar-placeholder">
                <div class="bg-neutral text-neutral-content w-16 rounded-full">
                    <span class="text-4xl">+</span>
                </div>
            </div>

            <!-- Nombre -->
            <input class="input input-sm w-full" type="text" v-model="nombre" placeholder="Nombre">

            <!-- Activo / inactivo -->
            <div class="flex flex-row gap-2">
                <input class="checkbox" type="checkbox" v-model="activo">
                <ActividadAgente :estado="activo" value=""></ActividadAgente>
            </div>
            

            <div class="flex flex-col items-center gap-2">
                <!-- Equipo -->
                <SelectEquipo class="select-sm" @update-nuevo-equipo="(id) => updateNuevoEquipo(id)"></SelectEquipo>

                <!-- Rango -->
                <input class="input input-sm w-full" type="text" v-model="rango" placeholder="Rango">
            </div>

            <button @click="addAgente" class="btn btn-primary btn-circle btn-sm absolute top-2 right-2">
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24"><!-- Icon from Material Design Icons by Pictogrammers - https://github.com/Templarian/MaterialDesign/blob/master/LICENSE --><path fill="currentColor" d="M15 9H5V5h10m-3 14a3 3 0 0 1-3-3a3 3 0 0 1 3-3a3 3 0 0 1 3 3a3 3 0 0 1-3 3m5-16H5a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2V7z"/></svg>
            </button>

        </div>
    </div>

</template>