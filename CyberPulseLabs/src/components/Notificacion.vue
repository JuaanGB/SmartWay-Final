<script setup>
    import { onMounted, ref } from 'vue';

    const props = defineProps(['ms','tipo','mensaje'])
    const activa = ref(true)
    onMounted( () => {
        setTimeout(() => {
            activa.value = !activa.value
        }, props.ms);
    })

</script>

<template>

    <div v-if="activa" class="alert z-100 w-80" :class="'alert-'+props.tipo">
        <span>{{ mensaje }}</span>
    </div>

</template>