<script setup>
import ActividadAgente from './ActividadAgente.vue';


    const props = defineProps(['id','nombre','rango','activo','equipoId'])

</script>

<template>

    <div class="card bg-base-200 border border-width-2 border-primary shadow-md w-50">
        <div class="card-body items-center">
            <!-- Avatar del nombre -->
            <div class="avatar avatar-placeholder">
                <div class="bg-neutral text-neutral-content w-16 rounded-full">
                    <span class="text-xl font-bold">{{ props.nombre[0] }}</span>
                </div>
            </div>

            <!-- Nombre -->
            <p class="card-title">{{ props.nombre }}</p>

            <!-- Activo / inactivo -->
            <ActividadAgente :estado="props.activo"></ActividadAgente>

            <div class="flex flex-col items-center">
                <!-- Equipo -->
                <p>{{ props.equipoId }}</p>

                <!-- Rango -->
                <p class="text-gray-400">{{ props.rango }}</p>
            </div>

        </div>
    </div>

</template>