<script setup>
    import { onMounted, ref } from 'vue';

    const props = defineProps(['ms','tipo','mensaje'])
    const activa = ref(true)
    onMounted( () => {
        console.log(props.tipo)
        setTimeout(() => {
            activa.value = !activa.value
        }, props.ms);
    })

</script>

<template>

    <div v-if="activa" role="alert" class="alert z-[100] w-80" :class="{'alert-success': props.tipo == 'success',
        'alert-error': props.tipo == 'error'}">
        <span>{{ mensaje }}</span>
    </div>

</template>