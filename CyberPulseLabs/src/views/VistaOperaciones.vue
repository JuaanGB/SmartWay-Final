<script setup>
import FiltrarOperaciones from '@/components/FiltrarOperaciones.vue';
import TablaOperaciones from '@/components/TablaOperaciones.vue'

</script>

<template>
  <main class="grid grid-cols-1 md:grid-cols-2 items-center gap-4 m-4">

    <div class="rounded-box p-4 w-auto p-4 bg-base-300 flex flex-col items-center gap-4 shadow-md shadow-primary">
      <h2 class="text-3xl font-bold">Gestión de operaciones</h2>
      <p>Desde esta página puedes visualizar el listado de operaciones de CyberPulseLabs, permitiendo su total gestión 
      editándolas, eliminándolas o añadiendo nuevas operaciones de forma sencilla.
      </p>
    </div>

    <div class="rounded-box w-auto p-4 bg-base-300 flex flex-col items-center gap-4 shadow-md shadow-primary">
      <h2 class="text-3xl font-bold">Búsqueda de operaciones</h2>
      <FiltrarOperaciones></FiltrarOperaciones>
    </div>

    <div class="col-span-full rounded-box w-auto p-4 gap-4 bg-base-300 flex flex-col items-center shadow-md shadow-primary">
      <h2 class="text-3xl font-bold">Listado de operaciones</h2>
      <TablaOperaciones></TablaOperaciones>
    </div>
  </main>
</template>