<script setup>
import FiltrarOperaciones from '@/components/FiltrarOperaciones.vue';
import TablaOperaciones from '@/components/TablaOperaciones.vue'
import { useSesion } from '@/stores/Sesion';

const sesion = useSesion()

</script>

<template>
  <main class="grid grid-cols-1 md:grid-cols-2 items-center gap-4 m-4 w-7/8 mx-auto">

    <div class="rounded-box p-4 w-auto p-4 bg-base-300 flex flex-col items-center gap-4 shadow-md shadow-primary h-full">
      <h2 class="text-3xl text-center font-bold">{{ 'Gestión de operaciones (' + (sesion.isAdmin ? "ADMIN)" : 'USER)') }} </h2>
      <p v-if="sesion.isAdmin" class="text-center">
        Desde esta página puedes visualizar el listado de operaciones de CyberPulseLabs, permitiendo su total gestión editándolas, eliminándolas o añadiendo nuevas operaciones de forma sencilla. 
      </p>
      <p class="text-center" v-else>
        Solamente puedes visualizar las operaciones relacionadas con el equipo al que perteneces.
      </p>
    </div>

    <div class="rounded-box w-auto p-4 bg-base-300 flex flex-col items-center gap-4 shadow-md shadow-primary">
      <h2 class="text-3xl font-bold text-center">Búsqueda de operaciones</h2>
      <FiltrarOperaciones></FiltrarOperaciones>
    </div>

    <div class="col-span-full rounded-box w-auto p-4 gap-4 bg-base-300 flex flex-col items-center shadow-md shadow-primary">
      <h2 class="text-3xl font-bold text-center">Listado de operaciones</h2>
      <TablaOperaciones></TablaOperaciones>
    </div>
  </main>
</template>