<script setup>
import { useEquipos } from '@/stores/Equipos';
import { useOperaciones } from '@/stores/Operaciones';
import { onMounted, ref } from 'vue';


const opStore = useOperaciones()
const totalOp = ref(null)
const eqStore = useEquipos()
const totalEq = ref(null)
const agStore = null // useAgentes
const totalAg = ref(null)

onMounted(async () => {
    totalOp.value = await opStore.getCount()
    totalEq.value = await eqStore.getCount()
    totalAg.value = await agStore.getCount()
})

</script>

<template>

    <main class="flex flex-col items-center gap-6 m-6">

        <!-- Hero principal -->
        <div class="hero bg-base-300 rounded-lg p-8 w-full shadow">
            <div class="hero-content flex-col text-center">
                <h2 class="text-5xl font-bold">Cyberpulse Labs</h2>
                <h3 class="text-2xl mt-2">Herramienta de gestión</h3>
                <p class="mt-4 max-w-xl">
                    Gestiona fácilmente la base de datos de la organización a través de una página web sencilla
                    que te permitirá añadir, eliminar, actualizar y consultar sus equipos, operaciones y agentes.
                </p>
                <p class="text-sm italic text-gray-400 mt-2">
                    Iniciando protocolo de restauración de red interna tras ciberataque...
                </p>
                <p class="mt-4 font-medium">¡Empieza ya a gestionar la organización!</p>
            </div>
        </div>

        <!-- Funcionalidades destacadas -->
        <div class="grid grid-cols-1 md:grid-cols-3 gap-4 mt-6 w-full max-w-5xl">
            <div class="card bg-base-200 shadow-md cursor-pointer hover:bg-base-300 transition" @click="$router.push('/operaciones')">
                <div class="card-body">
                    <div class="flex gap-2">
                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24"><!-- Icon from Material Design Icons by Pictogrammers - https://github.com/Templarian/MaterialDesign/blob/master/LICENSE --><path fill="currentColor" d="m13.78 15.3l6 6l2.11-2.16l-6-6zm3.72-5.2c-.39 0-.81-.05-1.14-.19L4.97 21.25l-2.11-2.11l7.41-7.4L8.5 9.96l-.72.7l-1.45-1.41v2.86l-.7.7l-3.52-3.56l.7-.7h2.81l-1.4-1.41l3.56-3.56a2.976 2.976 0 0 1 4.22 0L9.89 5.74l1.41 1.4l-.71.71l1.79 1.78l1.82-1.88c-.14-.33-.2-.75-.2-1.12a3.49 3.49 0 0 1 3.5-3.52c.59 0 1.11.14 1.58.42L16.41 6.2l1.5 1.5l2.67-2.67c.28.47.42.97.42 1.6c0 1.92-1.55 3.47-3.5 3.47"/></svg>
                        <h2 class="card-title">Operaciones</h2>
                    </div>
                    <p>Organiza las misiones activas, planificadas o completadas.</p>
                </div>
            </div>
            <div class="card bg-base-200 shadow-md cursor-pointer hover:bg-base-300 transition" @click="$router.push('/equipos')">
                <div class="card-body">
                    <div class="flex gap-2">
                        <svg class="inline" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24"><!-- Icon from Material Design Icons by Pictogrammers - https://github.com/Templarian/MaterialDesign/blob/master/LICENSE --><path fill="currentColor" d="M12 5.5A3.5 3.5 0 0 1 15.5 9a3.5 3.5 0 0 1-3.5 3.5A3.5 3.5 0 0 1 8.5 9A3.5 3.5 0 0 1 12 5.5M5 8c.56 0 1.08.15 1.53.42c-.15 1.43.27 2.85 1.13 3.96C7.16 13.34 6.16 14 5 14a3 3 0 0 1-3-3a3 3 0 0 1 3-3m14 0a3 3 0 0 1 3 3a3 3 0 0 1-3 3c-1.16 0-2.16-.66-2.66-1.62a5.54 5.54 0 0 0 1.13-3.96c.45-.27.97-.42 1.53-.42M5.5 18.25c0-2.07 2.91-3.75 6.5-3.75s6.5 1.68 6.5 3.75V20h-13zM0 20v-1.5c0-1.39 1.89-2.56 4.45-2.9c-.59.68-.95 1.62-.95 2.65V20zm24 0h-3.5v-1.75c0-1.03-.36-1.97-.95-2.65c2.56.34 4.45 1.51 4.45 2.9z"/></svg>
                        <h2 class="card-title">Equipos</h2>
                    </div>
                    <p>Gestiona equipos de campo con especialidades únicas.</p>
                </div>
            </div>
            <div class="card bg-base-200 shadow-md cursor-pointer hover:bg-base-300 transition" @click="$router.push('/agentes')">
                <div class="card-body">
                    <div class="flex gap-2">
                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24"><!-- Icon from Material Design Icons by Pictogrammers - https://github.com/Templarian/MaterialDesign/blob/master/LICENSE --><path fill="currentColor" d="M18.72 14.76c.35-.85.54-1.76.54-2.76c0-.72-.11-1.41-.3-2.05c-.65.15-1.33.23-2.04.23A9.07 9.07 0 0 1 9.5 6.34a9.2 9.2 0 0 1-4.73 4.88c-.04.25-.04.52-.04.78A7.27 7.27 0 0 0 12 19.27c1.05 0 2.06-.23 2.97-.64c.57 1.09.83 1.63.81 1.63c-1.64.55-2.91.82-3.78.82c-2.42 0-4.73-.95-6.43-2.66a9 9 0 0 1-2.24-3.69H2v-4.55h1.09a9.09 9.09 0 0 1 15.33-4.6a9 9 0 0 1 2.47 4.6H22v4.55h-.06L18.38 18l-5.3-.6v-1.67h4.83zm-9.45-2.99c.3 0 .59.12.8.34a1.136 1.136 0 0 1 0 1.6c-.21.21-.5.33-.8.33c-.63 0-1.14-.5-1.14-1.13s.51-1.14 1.14-1.14m5.45 0c.63 0 1.13.51 1.13 1.14s-.5 1.13-1.13 1.13s-1.14-.5-1.14-1.13a1.14 1.14 0 0 1 1.14-1.14"/></svg>
                        <h2 class="card-title">Agentes</h2>
                    </div>
                    <p>Supervisa los rangos, estados y asignaciones de agentes.</p>
                </div>
            </div>
        </div>

        <!-- Métricas del sistema -->
        <h2 class="text-3xl font-bold mt-8">Estado actual del sistema</h2>
        <div class="stats max-sm:stats-vertical shadow bg-base-200 w-full max-w-4xl">
            <div class="stat">
                <div class="stat-title">Operaciones</div>
                <div class="stat-value">{{ totalOp !== null ? totalOp : 'Cargando...' }}</div>
            </div>
            <div class="stat">
                <div class="stat-title">Equipos desplegados</div>
                <div class="stat-value">{{ totalEq !== null ? totalEq : 'Cargando...' }}</div>
            </div>
            <div class="stat">
                <div class="stat-title">Agentes</div>
                <div class="stat-value">{{ totalAg !== null ? totalAg : 'Cargando...' }}</div>
            </div>
        </div>

        <!-- Consola de estado -->
        <div class="mockup-code bg-base-200 mt-8 w-full max-w-4xl">
            <pre data-prefix="1"><code class="text-base-content">Conectando a CyberPulse DB...</code></pre>
            <pre data-prefix="2"><code class="text-base-content">Autenticación completada.</code></pre>
            <pre data-prefix="3" class="bg-warning text-warning-content"><code>Estado de red: FRAGMENTADO</code></pre>
        </div>

    </main>


</template>

<style scoped>

.btn {
    padding-left: 20px;
    padding-right: 20px;
    width: 160px;
}

.p {
    text-align: center;
}

code {
    padding-left: 18px;
}

</style>