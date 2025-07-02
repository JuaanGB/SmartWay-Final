<script setup>
import FiltrarOperaciones from '@/components/FiltrarOperaciones.vue';
import TablaOperaciones from '@/components/TablaOperaciones.vue'

</script>

<template>
  <main class="flex flex-col items-center gap-4 m-4">
    <h2 class="text-3xl font-bold">Gestión de operaciones</h2>

    <p>Desde esta página puedes visualizar el listado de operaciones de CyberPulseLabs, permitiendo su total gestión 
      editándolas, eliminándolas o añadiendo nuevas operaciones de forma sencilla.
    </p>

    <h2 class="text-3xl font-bold mt-8">Búsqueda de operaciones</h2>
    <FiltrarOperaciones></FiltrarOperaciones>

    <h2 class="text-3xl font-bold mt-8">Listado de operaciones</h2>
    <TablaOperaciones></TablaOperaciones>
  </main>
</template>