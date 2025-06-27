<script setup>
    import { RouterLink, RouterView } from 'vue-router'
    import Navbar from './components/Navbar.vue'

    

</script>

<template>
    <header>
        <Navbar titulo="CyberPulseLabs CRUD">
            <RouterLink to="/operaciones">Operaciones</RouterLink>
            <RouterLink to="/equipos">Equipos</RouterLink>
            <RouterLink to="/agentes">Agentes</RouterLink>
        </Navbar>
    </header>
    <section>
        <RouterView />
    </section>

</template>

<style>
    @import 'tailwindcss';
    @plugin 'daisyui';
</style>