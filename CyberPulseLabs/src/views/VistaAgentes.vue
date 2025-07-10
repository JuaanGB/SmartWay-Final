<script setup>
import AgentePerfil from '@/components/AgentePerfil.vue';
import AgentePerfilNuevo from '@/components/AgentePerfilNuevo.vue';
import { useAgentes } from '@/stores/Agentes';
import { useEquipos } from '@/stores/Equipos';
import { useSesion } from '@/stores/Sesion';
import { onMounted, watch } from 'vue';

const eqStore = useEquipos()
const agStore = useAgentes()
const sesion = useSesion()

onMounted(async () => {
  agStore.getAllAgentes()
  eqStore.getAllEquipos()
})

</script>

<template>
  <main class="flex flex-col items-center gap-4 m-4">

    <div class="rounded-box p-4 lg:w-3/4 w-full bg-base-300 flex flex-col items-center gap-2 shadow-md shadow-primary">
      <h2 class="text-3xl font-bold">Gestión de agentes</h2>
      <p class="text-center">Desde esta página puedes visualizar el listado de agentes de CyberPulseLabs, permitiendo su total gestión 
        editándolos, eliminándolos o añadiendo nuevos agentes de forma sencilla.
      </p>
    </div>

    <div class="rounded-box p-4 lg:w-3/4 w-full bg-base-300 flex flex-col items-center gap-2 shadow-md shadow-primary">
      <h2 class="text-3xl font-bold">{{ "Rol: " + (sesion.isAdmin ? "ADMIN" : 'USER') }}</h2>
      <p v-if="sesion.isAdmin" class="text-center">
        Puedes crear nuevos agentes, eliminar agentes existentes y editar a todos los agentes.
      </p>
      <p v-else>
        Solamente puedes editarte a ti mismo y visualizar a los agentes que son compañeros (compartes equipo con ellos).
      </p>
    </div>

    <div class="rounded-box lg:w-3/4 w-full relative px-10 pb-10 bg-base-300 flex flex-col items-center gap-10 shadow-md shadow-primary">
      <h2 class="text-3xl font-bold mt-8">Listado de agentes</h2>

      <label class="swap swap-flip absolute top-4 right-4">
        <input type="checkbox" @click="agStore.toggleOrden">
        <svg class="swap-off" xmlns="http://www.w3.org/2000/svg" width="32" height="32" viewBox="0 0 24 24"><!-- Icon from Material Design Icons by Pictogrammers - https://github.com/Templarian/MaterialDesign/blob/master/LICENSE --><path fill="currentColor" d="M19 17h3l-4 4l-4-4h3V3h2m-8 10v2l-3.33 4H11v2H5v-2l3.33-4H5v-2M9 3H7c-1.1 0-2 .9-2 2v6h2V9h2v2h2V5a2 2 0 0 0-2-2m0 4H7V5h2Z"/></svg>
        <svg class="swap-on" xmlns="http://www.w3.org/2000/svg" width="32" height="32" viewBox="0 0 24 24"><!-- Icon from Material Design Icons by Pictogrammers - https://github.com/Templarian/MaterialDesign/blob/master/LICENSE --><path fill="currentColor" d="M19 7h3l-4-4l-4 4h3v14h2m-8-8v2l-3.33 4H11v2H5v-2l3.33-4H5v-2M9 3H7c-1.1 0-2 .9-2 2v6h2V9h2v2h2V5a2 2 0 0 0-2-2m0 4H7V5h2Z"/></svg>
      </label>
      
      <div class="overflow-x-auto overflow-y-auto w-full h-80">
        <div class="grid grid-cols-1 md:grid-cols-3 xl:grid-cols-4 gap-10">
          <AgentePerfilNuevo v-if="sesion.isAdmin" class="mx-auto"></AgentePerfilNuevo>
          <AgentePerfil class="mx-auto" v-for="ag in agStore.agentesOrdenados" :key="ag.id" :id="ag.id" :activo="ag.activo" 
            :nombre="ag.nombre" :rango="ag.rango" :equipo-id="ag.equipoId" :editable="true"></AgentePerfil>
        </div>
      </div>
      
    </div>
  </main>
</template>
