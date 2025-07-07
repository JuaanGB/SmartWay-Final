<script setup>
    import { useOperaciones } from '@/stores/Operaciones';
    import { useNotificaciones } from '@/stores/Notificaciones';
    import { computed, ref } from 'vue';

    const nuevoNombre = ref('')
    const nuevoEstado = ref(0)
    const nuevoInicio = ref('')
    const nuevoFin = ref('')
    const nuevoID = computed( () => {
        return (nuevoNombre.value + '-' + nuevoInicio.value + '-' + nuevoFin.value).replace('/', '-')
    })

    const opStore = useOperaciones()
    const notiStore = useNotificaciones()

    async function createOperacion() {
        let exito = await opStore.createOperacion(nuevoID.value, nuevoNombre.value, 
            nuevoEstado.value, nuevoInicio.value, nuevoFin.value)
        if (exito)
            notiStore.addNotificacion("success", 3000, "Nueva operación añadida correctamente.")
        else
            notiStore.addNotificacion("error", 3000, "Error al crear una operación.")

        flushInputs()
    }

    function flushInputs() {
        nuevoID.value = ''
        nuevoNombre.value = ''
        nuevoEstado.value = 0
        nuevoInicio.value = ''
        nuevoFin.value = ''
    }

</script>

<template>

    <tr class="bg-base-100">
        <th>
            <!-- Botón de guardar -->
            <button class="btn btn-primary btn-outline rounded-md" @click="createOperacion">
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24"><!-- Icon from Material Design Icons by Pictogrammers - https://github.com/Templarian/MaterialDesign/blob/master/LICENSE --><path fill="currentColor" d="M15 9H5V5h10m-3 14a3 3 0 0 1-3-3a3 3 0 0 1 3-3a3 3 0 0 1 3 3a3 3 0 0 1-3 3m5-16H5a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2V7z"/></svg>
            </button>  
        </th>
        <!-- ID -->
        <td>
            <input class="input" type="text" placeholder="ID" v-model="nuevoID">
        </td>
        <!-- Nombre -->
        <td>
            <input class="input" type="text" placeholder="Nombre" v-model="nuevoNombre">
        </td>
        <!-- Estado -->
        <td>
            <select class="select">
                <option @click="nuevoEstado = 0">Planificada</option>
                <option @click="nuevoEstado = 1">Activa</option>
                <option @click="nuevoEstado = 2">Completada</option>
            </select>
        </td>
        <!-- Fecha de inicio -->
        <td>
            <input class="input" type="date" v-model="nuevoInicio">
        </td>
        <!-- Fecha de fin -->
        <td>
            <input class="input" type="date" v-model="nuevoFin">
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