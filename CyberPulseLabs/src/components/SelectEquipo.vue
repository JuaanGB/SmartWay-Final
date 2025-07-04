<script setup>
import { useEquipos } from '@/stores/Equipos';
import { onMounted, ref, watch } from 'vue';


    const emit = defineEmits(['updateNuevoEquipo'])
    const eqStore = useEquipos()
    const idAct = ref(0)

    watch(idAct, () => emit('updateNuevoEquipo', idAct.value))

</script>

<template>

    <select class="select w-full">
        <option v-for="eq in eqStore.equipos" @click="idAct = eq.id" :key="eq.id">{{ eq.nombre }}</option>
    </select>

</template>