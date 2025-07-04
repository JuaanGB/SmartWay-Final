<script setup>
import AgentePerfil from '@/components/AgentePerfil.vue';
import AgentePerfilNuevo from '@/components/AgentePerfilNuevo.vue';
import { useAgentes } from '@/stores/Agentes';
import { useEquipos } from '@/stores/Equipos';
import { onMounted } from 'vue';

const eqStore = useEquipos()
const agStore = useAgentes()

onMounted(async () => {
  agStore.getAllAgentes()
  eqStore.getAllEquipos()
})

</script>

<template>
  <main class="flex flex-col items-center gap-4 m-4">
    <h2 class="text-3xl font-bold">Gestión de agentes</h2>
    <p>Desde esta página puedes visualizar el listado de agentes de CyberPulseLabs, permitiendo su total gestión 
      editándolos, eliminándolos o añadiendo nuevos agentes de forma sencilla.
    </p>
    <h2 class="text-3xl font-bold mt-8">Listado de agentes</h2>

    <div class="grid grid-cols-1 md:grid-cols-3 lg:grid-cols-4 gap-10">
      <AgentePerfilNuevo></AgentePerfilNuevo>
      <AgentePerfil v-for="ag in agStore.agentes" :key="ag.id" :id="ag.id" :activo="ag.activo" 
        :nombre="ag.nombre" :rango="ag.rango" :equipo-id="ag.equipoId"></AgentePerfil>
    </div>
  </main>
</template>
