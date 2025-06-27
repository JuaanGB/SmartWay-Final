<script setup>
    const props = defineProps(['titulo'])
</script>

<template>

    <nav class="navbar bg-primary shadow-sm">
        <h1 class="navbar-start text-white">{{ titulo }}</h1>
        <div class="navbar-center gap-4 text-white hover:bg-primary-100">
            <slot></slot>
        </div>
        <div class="navbar-end">

        </div>
    </nav>

</template>

<style scoped>
</style>