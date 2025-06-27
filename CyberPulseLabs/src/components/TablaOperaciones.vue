<script setup>
import FilaTablaOperaciones from '@/components/FilaTablaOperaciones.vue'


</script>

<template>

    <table class="table table-zebra max-w-3/4 border rounded-box border-base-content/5 overflow-x-auto">
      <thead class="bg-base-300">
        <tr>
          <th></th> <!-- Vacío para botones -->
          <th>ID</th>
          <th>Nombre</th>
          <th>Estado</th>
          <th>Inicio</th>
          <th>Fin</th>
        </tr>
      </thead>
      <tbody>
        <FilaTablaOperaciones></FilaTablaOperaciones>
      </tbody>
    </table>

</template>