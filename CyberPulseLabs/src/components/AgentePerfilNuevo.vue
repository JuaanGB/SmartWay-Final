<script setup>
import { computed, ref } from 'vue';
import ActividadAgente from './ActividadAgente.vue';

    const nombre = ref('')
    const activo = ref(false)
    const equipoId = ref('')
    const rango = ref('')
    const inicial = computed(() => {
        return nombre.value[0]
    })


</script>

<template>

    <div class="card bg-base-200 border border-width-2 border-primary shadow-md w-50">
        <div class="card-body items-center">
            <!-- Avatar del nombre -->
            <div class="avatar avatar-placeholder">
                <div class="bg-neutral text-neutral-content w-16 rounded-full">
                    <span class="text-xl font-bold">{{ inicial }}</span>
                </div>
            </div>

            <!-- Nombre -->
            <input class="input input-sm w-full" type="text" v-model="nombre" placeholder="Nombre">

            <!-- Activo / inactivo -->
            <div class="flex flex-row gap-2">
                <input class="checkbox" type="checkbox" v-model="activo">
                <ActividadAgente :estado="activo"></ActividadAgente>
            </div>
            

            <div class="flex flex-col items-center gap-2">
                <!-- Equipo -->
                <select class="select select-sm w-full" v-memo="equipoId" placeholder="Equipo">

                </select>

                <!-- Rango -->
                <input class="input input-sm w-full" type="text" v-model="rango" placeholder="Rango">
            </div>

            <button class="btn btn-primary btn-circle btn-sm absolute top-2 right-2">
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24"><!-- Icon from Material Design Icons by Pictogrammers - https://github.com/Templarian/MaterialDesign/blob/master/LICENSE --><path fill="currentColor" d="M15 9H5V5h10m-3 14a3 3 0 0 1-3-3a3 3 0 0 1 3-3a3 3 0 0 1 3 3a3 3 0 0 1-3 3m5-16H5a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2V7z"/></svg>
            </button>

        </div>
    </div>

</template>