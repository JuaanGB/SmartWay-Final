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

            <!-- Botón de editar -->
            <button class="inline-flex lg:hidden btn btn-primary btn-circle btn-sm absolute top-2 right-2">
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24"><!-- Icon from HeroIcons by Refactoring UI Inc - https://github.com/tailwindlabs/heroicons/blob/master/LICENSE --><path fill="currentColor" d="M21.731 2.269a2.625 2.625 0 0 0-3.712 0l-1.157 1.157l3.712 3.712l1.157-1.157a2.625 2.625 0 0 0 0-3.712m-2.218 5.93l-3.712-3.712l-12.15 12.15a5.25 5.25 0 0 0-1.32 2.214l-.8 2.685a.75.75 0 0 0 .933.933l2.685-.8a5.25 5.25 0 0 0 2.214-1.32z"/></svg>
            </button>

            <!-- Botón de eliminar -->
            <button class="inline-flex lg:hidden btn btn-primary btn-circle btn-sm absolute top-2 left-2">
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24"><!-- Icon from HeroIcons by Refactoring UI Inc - https://github.com/tailwindlabs/heroicons/blob/master/LICENSE --><path fill="currentColor" fill-rule="evenodd" d="M16.5 4.478v.227a49 49 0 0 1 3.878.512a.75.75 0 1 1-.256 1.478l-.209-.035l-1.005 13.07a3 3 0 0 1-2.991 2.77H8.084a3 3 0 0 1-2.991-2.77L4.087 6.66l-.209.035a.75.75 0 0 1-.256-1.478A49 49 0 0 1 7.5 4.705v-.227c0-1.564 1.213-2.9 2.816-2.951a53 53 0 0 1 3.369 0c1.603.051 2.815 1.387 2.815 2.951m-6.136-1.452a51 51 0 0 1 3.273 0C14.39 3.05 15 3.684 15 4.478v.113a50 50 0 0 0-6 0v-.113c0-.794.609-1.428 1.364-1.452m-.355 5.945a.75.75 0 1 0-1.5.058l.347 9a.75.75 0 1 0 1.499-.058zm5.48.058a.75.75 0 1 0-1.498-.058l-.347 9a.75.75 0 0 0 1.5.058z" clip-rule="evenodd"/></svg>
            </button>

        </div>
    </div>

</template>

<style scoped>

.card-body:hover button {
    display: inline-flex;
}

</style>