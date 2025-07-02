<script setup>
import { useOperaciones } from '@/stores/Operaciones';
import { ref } from 'vue';


    const opStore = useOperaciones()
    const nombre = ref('')
    const estado = ref(0)
    const comienzo = ref('')
    const fin = ref('')

    function filterOperaciones() {
        opStore.filterOperaciones(nombre.value, estado.value, comienzo.value, fin.value)
    }

    function flushFilters() {
        opStore.getAllOperaciones()
        nombre.value = ''
        estado.value = 0
        comienzo.value = ''
        fin.value = ''
    }

</script>

<template>

    <fieldset class="bg-base-200 border-base-300 shadow-md rounded-box border p-4 grid grid-cols-1 md:grid-cols-2 gap-2">
        <!-- Filtrar por nombre -->
        <div>
            <label class="label text-sm block">Nombre</label>
            <input class="input" placeholder="Nombre" type="text" v-model="nombre">
        </div>
        <!-- Filtrar por estado -->
        <div>
            <label class="label text-sm block max-md:mt-4">Estado</label>
            <select class="select" v-model="estado">
                <option :selected="estado == 0" @click="estado = 0">Planificada</option>
                <option :selected="estado == 1" @click="estado = 1">Activa</option>
                <option :selected="estado == 2" @click="estado = 2">Completada</option>
            </select>
        </div>

        <!-- Filtrar por fechas -->
        <div class="col-span-full">
            <label class="label text-sm block max-md:mt-4">Fecha (comienzo - fin)</label>
            <div class="flex flex-row items-center">
                <input type="date" class="input w-full" v-model="comienzo">
                <span class="mx-2 inline">-</span>
                <input type="date" class="input w-full" v-model="fin">
            </div>  
        </div>

        <!-- Botones de búsqueda -->
        <div class="col-span-full flex flex-row gap-1 mt-4">
            <button class="btn btn-primary w-1/2" @click="filterOperaciones">
                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24"><!-- Icon from HeroIcons by Refactoring UI Inc - https://github.com/tailwindlabs/heroicons/blob/master/LICENSE --><path fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="1.5" d="m21 21l-5.197-5.197m0 0A7.5 7.5 0 1 0 5.196 5.196a7.5 7.5 0 0 0 10.607 10.607"/></svg>
                Buscar
            </button>
            <button class="btn btn-outline btn-primary w-1/2">Eliminar filtros</button>
        </div>
    </fieldset>

</template>