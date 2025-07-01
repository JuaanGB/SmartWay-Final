<script setup>
    import { RouterLink, RouterView } from 'vue-router'
    import Navbar from './components/Navbar.vue'
import Notificacion from './components/Notificacion.vue';
import { useNotificaciones } from './stores/Notificaciones';
    
    const notifStore = useNotificaciones()

</script>

<template>
    <header>
        <div id="notificaciones" class="toast toast-top toast-start z-10">
            <Notificacion v-for="noti in notifStore.notificaciones" :mensaje="noti.msj" :ms="noti.ms" :tipo="noti.tipo">
            </Notificacion>
        </div>
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