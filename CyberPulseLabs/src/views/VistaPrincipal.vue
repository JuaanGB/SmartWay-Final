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
                <h2 class="card-title">Operaciones</h2>
                <p>Organiza las misiones activas, planificadas o completadas.</p>
            </div>
            </div>
            <div class="card bg-base-200 shadow-md cursor-pointer hover:bg-base-300 transition" @click="$router.push('/agentes')">
            <div class="card-body">
                <h2 class="card-title">Equipos</h2>
                <p>Gestiona equipos de campo con especialidades únicas.</p>
            </div>
            </div>
            <div class="card bg-base-200 shadow-md cursor-pointer hover:bg-base-300 transition" @click="$router.push('/equipos')">
            <div class="card-body">
                <h2 class="card-title">Agentes</h2>
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