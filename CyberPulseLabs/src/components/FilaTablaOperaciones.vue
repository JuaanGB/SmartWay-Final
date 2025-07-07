<script setup>
    import { useOperaciones } from '@/stores/Operaciones';
    import { useNotificaciones } from '@/stores/Notificaciones';
    import { ref, watch } from 'vue';
import MedallaEstado from './MedallaEstado.vue';
import { useEditMode } from '@/composables/editMode';

    const props = defineProps(['id', 'nombre', 'estado', 'inicio', 'fin'])
    let nuevoNombre = props.nombre
    let nuevoEstado = props.estado
    let nuevoInicio = props.inicio
    let nuevoFin = props.fin

    const opStore = useOperaciones()
    const notiStore = useNotificaciones()

    const {editModeActive, toggleEditMode} = useEditMode(() => {
        nuevoNombre = props.nombre
        nuevoEstado = props.estado
        nuevoInicio = props.inicio
        nuevoFin = props.fin
    })

    async function saveChanges() {
        if (!nuevoNombre || !nuevoEstado || !nuevoInicio || !nuevoFin) {
            notiStore.addNotificacion("error", 3000, "Todos los campos deben estar completos.")
            return
        } else if (nuevoFin <= nuevoInicio) {
            notiStore.addNotificacion("error", 3000, "La fecha de fin debe ser posterior a la de inicio.")
            return
        }
        
        let exito = await opStore.updateOperaciones(props.id, nuevoNombre, nuevoEstado, nuevoInicio, nuevoFin)
        if (exito) {
            notiStore.addNotificacion("success", 3000, "Operación actualizada correctamente.")
        }
        else 
            notiStore.addNotificacion("error", 3000, "Error al actualizar la operación.")
        toggleEditMode()
    }

    async function deleteOperacion() {
        let exito = await opStore.deleteOperacion(props.id)
        if (exito)
            notiStore.addNotificacion("success", 3000, "Operación eliminada correctamente.")
        else
            notiStore.addNotificacion("error", 3000, "Error al eliminar la operación.")
    }

</script>

<template>

    <tr :class="{'bg-success': editModeActive}" class="bg-base-100">
        <th>
            <!-- Botones edición no activa -->
            <div v-if="!editModeActive">
                <button class="btn btn-primary btn-outline rounded-md" @click="toggleEditMode">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24"><!-- Icon from HeroIcons by Refactoring UI Inc - https://github.com/tailwindlabs/heroicons/blob/master/LICENSE --><path fill="currentColor" d="M21.731 2.269a2.625 2.625 0 0 0-3.712 0l-1.157 1.157l3.712 3.712l1.157-1.157a2.625 2.625 0 0 0 0-3.712m-2.218 5.93l-3.712-3.712l-12.15 12.15a5.25 5.25 0 0 0-1.32 2.214l-.8 2.685a.75.75 0 0 0 .933.933l2.685-.8a5.25 5.25 0 0 0 2.214-1.32z"/></svg>
                </button>
                <button class="btn btn-primary btn-outline rounded-md" @click="deleteOperacion">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24"><!-- Icon from HeroIcons by Refactoring UI Inc - https://github.com/tailwindlabs/heroicons/blob/master/LICENSE --><path fill="currentColor" fill-rule="evenodd" d="M16.5 4.478v.227a49 49 0 0 1 3.878.512a.75.75 0 1 1-.256 1.478l-.209-.035l-1.005 13.07a3 3 0 0 1-2.991 2.77H8.084a3 3 0 0 1-2.991-2.77L4.087 6.66l-.209.035a.75.75 0 0 1-.256-1.478A49 49 0 0 1 7.5 4.705v-.227c0-1.564 1.213-2.9 2.816-2.951a53 53 0 0 1 3.369 0c1.603.051 2.815 1.387 2.815 2.951m-6.136-1.452a51 51 0 0 1 3.273 0C14.39 3.05 15 3.684 15 4.478v.113a50 50 0 0 0-6 0v-.113c0-.794.609-1.428 1.364-1.452m-.355 5.945a.75.75 0 1 0-1.5.058l.347 9a.75.75 0 1 0 1.499-.058zm5.48.058a.75.75 0 1 0-1.498-.058l-.347 9a.75.75 0 0 0 1.5.058z" clip-rule="evenodd"/></svg>
                </button>
            </div>
            <!-- Botones edición activa -->
            <div v-else>
                <button class="btn btn-primary btn-outline rounded-md" @click="saveChanges">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24"><!-- Icon from Material Design Icons by Pictogrammers - https://github.com/Templarian/MaterialDesign/blob/master/LICENSE --><path fill="currentColor" d="M15 9H5V5h10m-3 14a3 3 0 0 1-3-3a3 3 0 0 1 3-3a3 3 0 0 1 3 3a3 3 0 0 1-3 3m5-16H5a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2V7z"/></svg>
                </button>
                <button class="btn btn-primary btn-outline rounded-md" @click="toggleEditMode">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24"><!-- Icon from Material Design Icons by Pictogrammers - https://github.com/Templarian/MaterialDesign/blob/master/LICENSE --><path fill="currentColor" d="M19 6.41L17.59 5L12 10.59L6.41 5L5 6.41L10.59 12L5 17.59L6.41 19L12 13.41L17.59 19L19 17.59L13.41 12z"/></svg>
                </button>
            </div>
        </th>
        <!-- ID -->
        <td>
            <p>{{ props.id }}</p>
        </td>
        <!-- Nombre -->
        <td>
            <input class="input color-base-content" type="text" :disabled="!editModeActive" v-model="nuevoNombre">
        </td>
        <!-- Estado -->
        <td>
            <select v-if="editModeActive" class="select">
                <option :selected="nuevoEstado == 0" @click="nuevoEstado = 0">Planificada</option>
                <option :selected="nuevoEstado == 1" @click="nuevoEstado = 1">Activa</option>
                <option :selected="nuevoEstado == 2" @click="nuevoEstado = 2">Completada</option>
            </select>
            <div v-else class="w-full">
                <MedallaEstado :estado="props.estado"></MedallaEstado>
            </div>
        </td>
        <!-- Fecha de inicio -->
        <td>
            <input :disabled="!editModeActive" class="input" type="date" v-model="nuevoInicio">
        </td>
        <!-- Fecha de fin -->
        <td>
            <input :disabled="!editModeActive" class="input" type="date" v-model="nuevoFin">
        </td>    
    </tr>

</template>

<style scoped>
.btn {
    padding: 4px;
    margin-left: 4px;
    margin-right: 4px;
}
</style>