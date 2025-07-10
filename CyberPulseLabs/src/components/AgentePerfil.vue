<script setup>
    import { computed, onMounted, ref, watch } from 'vue';
    import ActividadAgente from './ActividadAgente.vue';
    import { useEditMode } from '@/composables/editMode';
    import { useEquipos } from '@/stores/Equipos';
    import { useAgentes } from '@/stores/Agentes';
    import { useNotificaciones } from '@/stores/Notificaciones';
    import SelectEquipo from './SelectEquipo.vue';

    const eqStore = useEquipos()
    const agStore = useAgentes()
    const notiStore = useNotificaciones()

    const props = defineProps(['id','nombre','rango','activo','equipoId','editable'])
    const { editModeActive, toggleEditMode } = useEditMode(() => {
        nuevoNombre.value = props.nombre
        nuevoRango.value = props.rango
        nuevoEstado.value = props.activo
        nuevoEquipoId.value = props.equipoId
    })

    const nuevoNombre = ref(props.nombre)
    const nuevoRango = ref(props.rango)
    const nuevoEstado = ref(props.activo)
    const nuevoEquipoId = ref(props.equipoId)
    const inicial = computed(() => nuevoNombre.value?.charAt(0)?.toUpperCase() || '?')

    const nuevoEquipoNombre = ref('Equipo')

    async function deleteAgente() {
        let exito = await agStore.deleteAgente(props.id)
        if (exito)
            notiStore.addNotificacion("success", 3000, "Agente eliminado correctamente.")
        else
            notiStore.addNotificacion("error", 3000, "Error al eliminar el agente.")
    }

    async function saveChanges() {
        if (!nuevoNombre.value || !nuevoRango.value) {
            notiStore.addNotificacion("error", 3000, "Debes rellenar el nombre y el rango.")
            return
        }

        let exito = await agStore.updateAgente(props.id, nuevoNombre.value, nuevoEstado.value,
            nuevoEquipoId.value, nuevoRango.value)

        if (nuevoEquipoNombre.value)
            nuevoEquipoNombre.value = (await eqStore.getEquipo(nuevoEquipoId.value)).nombre
        
        if (exito)
            notiStore.addNotificacion("success", 3000, "Agente actualizado correctamente.")
        else 
            notiStore.addNotificacion("error", 3000, "Error al actualizar la operación.")

        toggleEditMode()
    }

    // Necesario para q se muestre el nombre cuando se añade un nuevo agente
    onMounted( async () => {
        const res = await eqStore.getEquipo(props.equipoId)
        nuevoEquipoNombre.value = res?.nombre
    })

    watch(() => eqStore.loadingAllEquipos, async () => {
        const res = await eqStore.getEquipo(props.equipoId)
        nuevoEquipoNombre.value = res?.nombre
    })

    function updateNuevoEquipo(id) {
        nuevoEquipoId.value = id
    }

</script>

<template>

    <div class="card bg-base-200 border border-primary shadow-md w-50 h-70 content-center shadow-md">
        <div class="card-body items-center justify-center">
            <!-- Avatar del nombre -->
            <div class="avatar avatar-placeholder mb-4">
                <div class="bg-neutral text-neutral-content w-16 rounded-full">
                    <span class="text-xl font-bold">{{ inicial }}</span>
                </div>
            </div>

            <!-- Vista normal (no edición) -->
            <div v-if="!editModeActive" class="flex flex-col items-center gap-2">
                <!-- Nombre -->
                <p class="card-title text-center">{{ props.nombre }}</p>

                <!-- Activo / inactivo -->
                <ActividadAgente :estado="props.activo"></ActividadAgente>

                <div class="flex flex-col items-center mt-4 text-center">
                    <!-- Equipo -->
                    <p>{{ nuevoEquipoNombre }}</p>

                    <!-- Rango -->
                    <p class="text-gray-400 text-center">{{ props.rango }}</p>
                </div>

                <!-- Botón de editar -->
                <button @click="toggleEditMode" class="inline-flex lg:hidden btn btn-primary btn-circle btn-sm absolute top-2 right-2">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24"><!-- Icon from HeroIcons by Refactoring UI Inc - https://github.com/tailwindlabs/heroicons/blob/master/LICENSE --><path fill="currentColor" d="M21.731 2.269a2.625 2.625 0 0 0-3.712 0l-1.157 1.157l3.712 3.712l1.157-1.157a2.625 2.625 0 0 0 0-3.712m-2.218 5.93l-3.712-3.712l-12.15 12.15a5.25 5.25 0 0 0-1.32 2.214l-.8 2.685a.75.75 0 0 0 .933.933l2.685-.8a5.25 5.25 0 0 0 2.214-1.32z"/></svg>
                </button>

                <!-- Botón de eliminar -->
                <button v-if="props.editable" @click="deleteAgente" class="inline-flex lg:hidden btn btn-primary btn-circle btn-sm absolute top-2 left-2">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24"><!-- Icon from HeroIcons by Refactoring UI Inc - https://github.com/tailwindlabs/heroicons/blob/master/LICENSE --><path fill="currentColor" fill-rule="evenodd" d="M16.5 4.478v.227a49 49 0 0 1 3.878.512a.75.75 0 1 1-.256 1.478l-.209-.035l-1.005 13.07a3 3 0 0 1-2.991 2.77H8.084a3 3 0 0 1-2.991-2.77L4.087 6.66l-.209.035a.75.75 0 0 1-.256-1.478A49 49 0 0 1 7.5 4.705v-.227c0-1.564 1.213-2.9 2.816-2.951a53 53 0 0 1 3.369 0c1.603.051 2.815 1.387 2.815 2.951m-6.136-1.452a51 51 0 0 1 3.273 0C14.39 3.05 15 3.684 15 4.478v.113a50 50 0 0 0-6 0v-.113c0-.794.609-1.428 1.364-1.452m-.355 5.945a.75.75 0 1 0-1.5.058l.347 9a.75.75 0 1 0 1.499-.058zm5.48.058a.75.75 0 1 0-1.498-.058l-.347 9a.75.75 0 0 0 1.5.058z" clip-rule="evenodd"/></svg>
                </button>

            </div>

            <!-- Vista en modo edición -->
            <div v-else class="flex flex-col items-center gap-2">
                <!-- Nombre -->
                <input class="input input-sm w-full" type="text" v-model="nuevoNombre" placeholder="Nombre">

                <!-- Activo / inactivo -->
                <div class="flex flex-row gap-2">
                    <input class="checkbox" type="checkbox" v-model="nuevoEstado">
                    <ActividadAgente :estado="nuevoEstado"></ActividadAgente>
                </div>
                

                <div class="flex flex-col items-center gap-2">
                    <!-- Equipo -->
                    <SelectEquipo :disabled="!props.editable" @update-nuevo-equipo="(id) => updateNuevoEquipo(id)" class="select-sm" :default="nuevoEquipoNombre"></SelectEquipo>

                    <!-- Rango -->
                    <input :disabled="!props.editable" class="input input-sm w-full" type="text" v-model="nuevoRango" placeholder="Rango">
                </div>

                <!-- Botón de guardar cambios -->
                <button @click="saveChanges" class="btn btn-primary btn-circle btn-sm absolute top-2 left-2">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24"><!-- Icon from Material Design Icons by Pictogrammers - https://github.com/Templarian/MaterialDesign/blob/master/LICENSE --><path fill="currentColor" d="M15 9H5V5h10m-3 14a3 3 0 0 1-3-3a3 3 0 0 1 3-3a3 3 0 0 1 3 3a3 3 0 0 1-3 3m5-16H5a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2V7z"/></svg>
                </button>
                <!-- Botón de salir de la edición -->
                <button class="btn btn-primary btn-circle btn-sm absolute top-2 right-2" @click="toggleEditMode">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24"><!-- Icon from Material Design Icons by Pictogrammers - https://github.com/Templarian/MaterialDesign/blob/master/LICENSE --><path fill="currentColor" d="M19 6.41L17.59 5L12 10.59L6.41 5L5 6.41L10.59 12L5 17.59L6.41 19L12 13.41L17.59 19L19 17.59L13.41 12z"/></svg>
                </button>

            </div>
            
        </div>
    </div>

</template>

<style scoped>

.card-body:hover button {
    display: inline-flex;
}

</style>