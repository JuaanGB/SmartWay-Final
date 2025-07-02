<script setup>

    const props = defineProps(['estado'])

</script>

<template>

    <div v-if="props.estado == 0" class="badge badge-primary">Planificada</div>
    <div v-else-if="props.estado == 1" class="badge badge-warning">Activa</div>
    <div v-else class="badge badge-success">Completada</div>

</template>