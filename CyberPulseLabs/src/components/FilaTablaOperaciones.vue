<script setup>
    import { ref, toRefs, watch } from 'vue';

    const props = defineProps(['id', 'nombre', 'estado', 'inicio', 'fin'])
    const {nuevoID, nuevoNombre, nuevoEstado, nuevoInicio, nuevoFin} = toRefs(props)
    const editModeActive = ref(false)

    function toggleEditMode() {
        editModeActive.value = !editModeActive.value
    }

    watch(editModeActive, () => {
        //nuevoID, nuevoNombre, nuevoEstado, nuevoInicio, nuevoFin = toRefs(props) // Reiniciamos todo
    })

</script>

<template>

    <tr>
        <th>
            <!-- Botones edición no activa -->
            <div v-if="!editModeActive">
                <button class="btn rounded-md" @click="toggleEditMode">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24"><!-- Icon from HeroIcons by Refactoring UI Inc - https://github.com/tailwindlabs/heroicons/blob/master/LICENSE --><path fill="currentColor" d="M21.731 2.269a2.625 2.625 0 0 0-3.712 0l-1.157 1.157l3.712 3.712l1.157-1.157a2.625 2.625 0 0 0 0-3.712m-2.218 5.93l-3.712-3.712l-12.15 12.15a5.25 5.25 0 0 0-1.32 2.214l-.8 2.685a.75.75 0 0 0 .933.933l2.685-.8a5.25 5.25 0 0 0 2.214-1.32z"/></svg>
                </button>
                <button class="btn rounded-md">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24"><!-- Icon from HeroIcons by Refactoring UI Inc - https://github.com/tailwindlabs/heroicons/blob/master/LICENSE --><path fill="currentColor" fill-rule="evenodd" d="M16.5 4.478v.227a49 49 0 0 1 3.878.512a.75.75 0 1 1-.256 1.478l-.209-.035l-1.005 13.07a3 3 0 0 1-2.991 2.77H8.084a3 3 0 0 1-2.991-2.77L4.087 6.66l-.209.035a.75.75 0 0 1-.256-1.478A49 49 0 0 1 7.5 4.705v-.227c0-1.564 1.213-2.9 2.816-2.951a53 53 0 0 1 3.369 0c1.603.051 2.815 1.387 2.815 2.951m-6.136-1.452a51 51 0 0 1 3.273 0C14.39 3.05 15 3.684 15 4.478v.113a50 50 0 0 0-6 0v-.113c0-.794.609-1.428 1.364-1.452m-.355 5.945a.75.75 0 1 0-1.5.058l.347 9a.75.75 0 1 0 1.499-.058zm5.48.058a.75.75 0 1 0-1.498-.058l-.347 9a.75.75 0 0 0 1.5.058z" clip-rule="evenodd"/></svg>
                </button>
            </div>
            <!-- Botones edición activa -->
            <div v-else>
                <button class="btn rounded-md">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24"><!-- Icon from Material Design Icons by Pictogrammers - https://github.com/Templarian/MaterialDesign/blob/master/LICENSE --><path fill="currentColor" d="M15 9H5V5h10m-3 14a3 3 0 0 1-3-3a3 3 0 0 1 3-3a3 3 0 0 1 3 3a3 3 0 0 1-3 3m5-16H5a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2V7z"/></svg>
                </button>
                <button class="btn rounded-md" @click="toggleEditMode">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24"><!-- Icon from Material Design Icons by Pictogrammers - https://github.com/Templarian/MaterialDesign/blob/master/LICENSE --><path fill="currentColor" d="M19 6.41L17.59 5L12 10.59L6.41 5L5 6.41L10.59 12L5 17.59L6.41 19L12 13.41L17.59 19L19 17.59L13.41 12z"/></svg>
                </button>
            </div>
        </th>
        <!-- ID -->
        <td>
            <input class="input" type="text" :disabled="!editModeActive" value="ID">
        </td>
        <!-- Nombre -->
        <td>
            <input class="input" type="text" :disabled="!editModeActive" value="nombre">
        </td>
        <!-- Estado -->
        <td>
            <select class="select">
                <option>Planificada</option>
                <option>Activa</option>
            </select>
        </td>
        <!-- Fecha de inicio -->
        <td>
            <input :disabled="!editModeActive" class="input" type="date">
        </td>
        <!-- Fecha de fin -->
        <td>
            <input :disabled="!editModeActive" class="input" type="date">
        </td>    
    </tr>

</template>

<style>
.btn {
    padding: 4px;
    margin-left: 4px;
    margin-right: 4px;
}
</style>