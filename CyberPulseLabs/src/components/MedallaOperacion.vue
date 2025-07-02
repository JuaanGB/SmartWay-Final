<script setup>
import { useOperaciones } from '@/stores/Operaciones';


    const props = defineProps(['id', 'nombre'])
    const opStore = useOperaciones()

    function showModal() {
        opStore.getOperacion(props.id)
        document.getElementById("modalOp").showModal()
    }

</script>

<template>

    <button class="btn badge badge-outline badge-secondary" @click="showModal">{{ props.id }}</button>

</template>