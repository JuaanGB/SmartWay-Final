<script setup>
import TarjetaEquipo from '@/components/TarjetaEquipo.vue';
import TarjetaEquipoNuevo from '@/components/TarjetaEquipoNuevo.vue';
import { useEquipos } from '@/stores/Equipos';
import { useOperaciones } from '@/stores/Operaciones';
import { onMounted } from 'vue';

const equipoStore = useEquipos()
const opStore = useOperaciones()


onMounted( () => {
	equipoStore.getAllEquipos()
})

</script>

<template>

    <main class="flex flex-col items-center gap-4 m-4">
      	<h2 class="text-3xl font-bold">Gestión de equipos</h2>
		<div class="grid grid-cols-1 lg:grid-cols-3 w-full gap-4">
			<TarjetaEquipoNuevo class="mx-auto col-span-full"></TarjetaEquipoNuevo>
			<TarjetaEquipo class="mx-auto" v-for="eq in equipoStore.equipos" :key="eq.id"
				:id="eq.id" :nombre="eq.nombre" :especialidad="eq.especialidad" :operacionid="eq.operacionId">
			</TarjetaEquipo>
		</div>
  	</main>

	<dialog id="modalOp" class="modal">
        <div class="modal-box">
            <form method="dialog">
            <button class="btn btn-sm btn-circle btn-ghost absolute right-2 top-2">✕</button>
            </form>
            <h3 class="text-lg font-bold">{{ opStore.operacionAct.nombre }}</h3>
            <div class="badge badge-secondary">{{ opStore.operacionAct.estado }}</div>
            <p class="py-4">{{ opStore.operacionAct.fechaInicio }}</p>
        </div>
    </dialog>

</template>
