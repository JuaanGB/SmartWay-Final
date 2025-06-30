<script setup>
import { useOperaciones } from '@/stores/counter';
import { ref } from 'vue';

    const opStore = useOperaciones()

</script>

<template>

    <div class="card card-secondary shadow-sm bg-base-300 flex-row join-item w-100">
        <div class="card-body">
            <!-- Información siempre visible -->
            <div class="flex flex-row">
                <input class="input card-title w-54" type="text" placeholder="Nombre equipo">
                <h3 class="card-title ml-auto">(#</h3>
                <input class="input card-title w-20" type="text" placeholder="ID">
                <h3 class="card-title">)</h3>
            </div>
            <input class="input w-full" type="text" placeholder="Especialidad">
            <select class="select w-full">
                <option v-for="op in opStore.operaciones">{{ op.nombre + " (#" + op.id + ")"}}</option>
            </select>
            <button class="btn btn-primary">Añadir equipo</button>
        </div>    
    </div>
    
</template>

<style scoped>

</style>