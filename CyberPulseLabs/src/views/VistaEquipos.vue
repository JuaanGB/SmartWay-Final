<script setup>
import MedallaEstado from '@/components/MedallaEstado.vue';
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
		<p>Desde esta página puedes visualizar el listado de equipos de CyberPulseLabs, permitiendo su total gestión 
		editándolos, eliminándolos o añadiendo nuevos agentes de forma sencilla.
		</p>
		<h2 class="text-3xl font-bold mt-8">Añadir un nuevo equipo</h2>
		<div class="grid grid-cols-1 lg:grid-cols-2 w-full gap-4">
			<TarjetaEquipoNuevo class="mx-auto col-span-full"></TarjetaEquipoNuevo>
			<h2 class="text-3xl font-bold mt-8 col-span-full text-center">Listado de equipos</h2>
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
            <h3 class="text-lg font-bold">Operación: {{ opStore.operacionAct.nombre }}</h3>
            <MedallaEstado :estado="opStore.operacionAct.estado"></MedallaEstado>
            <p>{{ opStore.operacionAct.fechaInicio }} - {{ opStore.operacionAct.fechaFin }}</p>
        </div>
    </dialog>

</template>
