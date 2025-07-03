<script setup>

    const props = defineProps(['estado'])

</script>

<template>

    <div class="badge" :class="{'badge-error' : props.estado == false, 'badge-success' : props.estado == true}">
        {{ props.estado == true ? 'Activo' : 'Inactivo' }}
    </div>

</template>