<script setup>
    import { RouterLink, RouterView } from 'vue-router'
    import Navbar from './components/Navbar.vue'
import Notificacion from './components/Notificacion.vue';
import { useNotificaciones } from './stores/Notificaciones';
    
    const notifStore = useNotificaciones()

</script>

<template>
    <header class="sticky top-0 z-10">
        <div id="notificaciones" class="toast toast-top toast-start z-10">
            <Notificacion v-for="noti in notifStore.notificaciones" :key="noti.id" :mensaje="noti.msj" :ms="noti.ms" :tipo="noti.tipo">
            </Notificacion>
        </div>
        <Navbar titulo="CyberPulseLabs CRUD">
            <RouterLink class="btn btn-primary btn-ghost rounded-xl" to="/operaciones">Operaciones</RouterLink>
            <RouterLink class="btn btn-primary btn-ghost rounded-xl" to="/equipos">Equipos</RouterLink>
            <RouterLink class="btn btn-primary btn-ghost rounded-xl" to="/agentes">Agentes</RouterLink>
        </Navbar>
    </header>
    <section>
        <RouterView />
    </section>

    <footer class="footer sm:footer-horizontal footer-center bg-neutral text-neutral-content p-4">
        <aside>
            <p>Copyright © - CyberPulseLabs</p>
        </aside>
    </footer>
    
</template>

<style>
    @import 'tailwindcss';
    @plugin 'daisyui';
</style>