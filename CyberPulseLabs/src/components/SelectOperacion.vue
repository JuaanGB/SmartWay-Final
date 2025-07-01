<script setup>
import { useOperaciones } from '@/stores/Operaciones';
import { onMounted } from 'vue';


    const model = defineModel()
    const opStore = useOperaciones()

    onMounted( () => {
        opStore.getAllOperaciones()
    })

</script>

<template>

    <select class="select w-full" v-model="model">
        <option v-for="op in opStore.operaciones" :key="op.id">{{ op.id }}</option>
    </select>

</template>