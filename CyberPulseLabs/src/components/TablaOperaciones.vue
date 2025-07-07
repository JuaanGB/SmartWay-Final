<script setup>
import FilaTablaOperaciones from '@/components/FilaTablaOperaciones.vue'
import { useOperaciones } from '@/stores/Operaciones';
import { useNotificaciones } from '@/stores/Notificaciones';
import { onMounted, ref } from 'vue';
import FilaTablaNuevaOperacion from './FilaTablaNuevaOperacion.vue';

const opStore = useOperaciones()
const notiStore = useNotificaciones()

onMounted(() => {
  if (!opStore.getAllOperaciones())
      notiStore.addNotificacion("error", 3000, "No se pudo obtener las operaciones.")
})

function refreshOperaciones() {
  if (opStore.getAllOperaciones())
    notiStore.addNotificacion("success", 3000, "Lista de operaciones actualizada correctamente.")
  else
    notiStore.addNotificacion("error", 3000, "No se pudo obtener las operaciones.")
}

</script>

<template>

    <div class="w-full m-1 overflow-x-auto overflow-y-auto">
      <table class="min-w-[600px] mx-auto table max-w-3/4 border rounded-box border-base-content/5">
        <thead class="bg-base-200">
          <tr>
            <th class="min-w-28">
              <!-- Recargar lista de operaciones -->
              <button class="btn btn-circle btn-sm btn-outline btn-neutral" @click="refreshOperaciones">
                  <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24"><!-- Icon from Material Design Icons by Pictogrammers - https://github.com/Templarian/MaterialDesign/blob/master/LICENSE --><path fill="currentColor" d="M2 12a9 9 0 0 0 9 9c2.39 0 4.68-.94 6.4-2.6l-1.5-1.5A6.7 6.7 0 0 1 11 19c-6.24 0-9.36-7.54-4.95-11.95S18 5.77 18 12h-3l4 4h.1l3.9-4h-3a9 9 0 0 0-18 0"/></svg>
              </button>
            </th> <!-- Vacío para botones -->
            <th>ID</th>
            <th class="min-w-50">Nombre</th>
            <th class="min-w-40">Estado</th>
            <th>Inicio</th>
            <th>Fin</th>
          </tr>
        </thead>
        <tbody>
          <FilaTablaNuevaOperacion></FilaTablaNuevaOperacion>
          <FilaTablaOperaciones v-for="op in opStore.operaciones" :key="op.id"
            :id="op.id" :nombre="op.nombre" :estado="op.estado" :inicio="op.fechaInicio" :fin="op.fechaFin">
          </FilaTablaOperaciones>
        </tbody>
      </table>
    </div>
    
</template>