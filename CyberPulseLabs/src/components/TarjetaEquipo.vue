<script setup>
import { ref } from 'vue';
import SelectOperacion from './SelectOperacion.vue';
import { useEquipos } from '@/stores/Equipos';
import { useNotificaciones } from '@/stores/Notificaciones';
import MedallaOperacion from './MedallaOperacion.vue';
import { useEditMode } from '@/composables/editMode';

    const props = defineProps(['id', 'nombre', 'especialidad', 'operacionid'])

    const visibleMembers = ref(false)

    const equipoStore = useEquipos()
    const notiStore = useNotificaciones()

    const nuevoNombre = ref(props.nombre)
    const nuevaEspecialidad = ref(props.especialidad)
    const nuevaOperacionid = ref(props.operacionid)

    const { editModeActive, toggleEditMode } = useEditMode( () => {
        nuevoNombre.value = props.nombre
        nuevaEspecialidad.value = props.especialidad
        nuevaOperacionid.value = props.operacionid
    })

    function toggleMembers() {
        visibleMembers.value = !visibleMembers.value
    }

    function addMember() {
        // Obtener texto del input con sus comprobaciones (no es necesario v-model)
        // Llamada a pinia para que gestione todo (búsqueda del miembro, adición y actualización)
    }

    async function updateEquipo() {
        const exito = await equipoStore.updateEquipo(props.id, nuevoNombre.value, nuevaEspecialidad.value, nuevaOperacionid.value)
        if (exito)
            notiStore.addNotificacion("success", 3000, "Nueva operación añadida correctamente.")
        else
            notiStore.addNotificacion("error", 3000, "Error al crear una operación.")

        toggleEditMode()
    }

    async function deleteOperacion() {
        let exito = await equipoStore.deleteEquipo(props.id)
        if (exito)
            notiStore.addNotificacion("success", 3000, "Equipo eliminado correctamente.")
        else
            notiStore.addNotificacion("error", 3000, "Error al eliminar el equipo.")
    }


</script>

<template>

    <div class="join max-lg:join-vertical">
        <div class="card card-secondary shadow-md bg-base-300 flex-row join-item">
            <div class="card-body w-full">
                <!-- Información siempre visible -->
                <div class="flex flex-row">

                    <!-- Nombre del equipo -->
                    <h3 v-if="!editModeActive" class="card-title">{{ props.nombre }}</h3>
                    <input v-else class="input input-sm" type="text" v-model="nuevoNombre">

                    <!-- Botones de edición y miembros -->
                    <button class="btn btn-circle btn-secondary btn-sm ml-auto mx-1" @click="toggleEditMode">
                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 16 16"><!-- Icon from HeroIcons by Refactoring UI Inc - https://github.com/tailwindlabs/heroicons/blob/master/LICENSE --><path fill="currentColor" fill-rule="evenodd" d="M11.013 2.513a1.75 1.75 0 0 1 2.475 2.474L6.226 12.25a2.8 2.8 0 0 1-.892.596l-2.047.848a.75.75 0 0 1-.98-.98l.848-2.047a2.8 2.8 0 0 1 .596-.892z" clip-rule="evenodd"/></svg>
                    </button>
                    <button class="btn btn-circle btn-secondary btn-sm mr-1" @click="deleteOperacion">
                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24"><!-- Icon from Material Design Icons by Pictogrammers - https://github.com/Templarian/MaterialDesign/blob/master/LICENSE --><path fill="currentColor" d="M19 4h-3.5l-1-1h-5l-1 1H5v2h14M6 19a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2V7H6z"/></svg>
                    </button>
                    <button class="btn btn-circle btn-secondary btn-sm" @click="toggleMembers">
                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 16 16"><!-- Icon from HeroIcons by Refactoring UI Inc - https://github.com/tailwindlabs/heroicons/blob/master/LICENSE --><path fill="currentColor" d="M8 8a2.5 2.5 0 1 0 0-5a2.5 2.5 0 0 0 0 5m-4.844 3.763c.16-.629.44-1.21.813-1.72a2.5 2.5 0 0 0-2.725 1.377c-.136.287.102.58.418.58h1.449q.015-.116.045-.237m9.691 0q.03.12.046.237h1.446c.316 0 .554-.293.417-.579a2.5 2.5 0 0 0-2.722-1.378c.374.51.653 1.09.813 1.72M14 7.5a1.5 1.5 0 1 1-3 0a1.5 1.5 0 0 1 3 0M3.5 9a1.5 1.5 0 1 0 0-3a1.5 1.5 0 0 0 0 3M5 13c-.552 0-1.013-.455-.876-.99a4.002 4.002 0 0 1 7.753 0c.136.535-.324.99-.877.99z"/></svg>
                    </button>
                </div>

                <!-- Especialidad -->
                <div v-if="!editModeActive" class="badge badge-primary">{{ props.especialidad }}</div>
                <input v-else class="input input-primary input-sm" type="text" v-model="nuevaEspecialidad">

                <!-- Operación -->
                <MedallaOperacion v-if="!editModeActive" :id="props.operacionid" :nombre="props.operacionid"></MedallaOperacion>
                <SelectOperacion v-else v-model="nuevaOperacionid" class="select-secondary select-sm"></SelectOperacion>

                <!-- Boton de guardar cambios-->
                <button v-if="editModeActive" class="btn btn-primary btn-sm" @click="updateEquipo">Guardar cambios</button>
            </div>    
        </div>
        <!-- Información visible si clica botón de miembros -->
        <div v-if="visibleMembers" class="w-full lg:w-50 bg-base-200 rounded-box shadow-sm relative join-item h-40">
            <!-- Lista scrolleable -->
            <ul class="list overflow-y-auto pr-1 max-h-full pb-16">
                <li v-for="i in 10" class="list-row h-10">Miembro {{ i }}</li>
            </ul>

            <!-- Input fijo abajo -->
            <div class="absolute bottom-0 left-0 w-full bg-base-200 p-2">
                <div class="join w-full">
                <input class="input join-item w-full" type="text" placeholder="nuevo miembro">
                <button class="btn btn-primary join-item" @click="addMember">Ok</button>
                </div>
            </div>
        </div>
    </div>

</template>