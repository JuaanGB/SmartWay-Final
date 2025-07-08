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

    <main class="grid grid-cols-1 md:grid-cols-2 items-center gap-4 m-4 w-7/8 mx-auto">
		<div class="rounded-box p-4 w-auto p-4 bg-base-300 flex flex-col items-center gap-4 shadow-md shadow-primary">
			<h2 class="text-3xl font-bold">Gestión de equipos</h2>
			<p class="text-center">Desde esta página puedes visualizar el listado de equipos de CyberPulseLabs, permitiendo su total gestión 
			editándolos, eliminándolos o añadiendo nuevos agentes de forma sencilla.
			</p>
		</div>

		<div class="rounded-box w-auto p-4 bg-base-300 flex flex-col items-center gap-4 shadow-md shadow-primary">
			<h2 class="text-3xl font-bold text-center">Añadir un nuevo equipo</h2>
			<TarjetaEquipoNuevo></TarjetaEquipoNuevo>	
		</div>

		<div class="col-span-full w-full roundex-box bg-base-300 flex flex-col items-center gap-4 p-4 shadow-md shadow-primary">
			<h2 class="text-3xl font-bold mt-8 col-span-full text-center">Listado de equipos</h2>
			<div class="overflow-y-auto col-span-full w-full">
				<div class="grid grid-cols-1 xl:grid-cols-2 rounded-box p-4 gap-6 w-full mx-auto p-4 h-70 xl:h-50">
					<TarjetaEquipo class="mx-auto" v-for="eq in equipoStore.equipos" :key="eq.id"
						:id="eq.id" :nombre="eq.nombre" :especialidad="eq.especialidad" :operacionid="eq.operacionId" :agentes="eq.agentes">
					</TarjetaEquipo>
				</div>	
			</div>
		</div>
		
  	</main>

	<dialog id="modalOp" class="modal">
        <div class="modal-box w-auto pt-10">
            <form method="dialog">
            	<button class="btn btn-sm btn-circle btn-ghost absolute right-2 top-2">✕</button>
            </form>
			<div class="flex gap-2 items-center">
				<h3 class="text-lg font-bold">{{ opStore.operacionAct.nombre }}</h3>
            	<MedallaEstado :estado="opStore.operacionAct.estado"></MedallaEstado>
			</div>
            
            <p>{{ opStore.operacionAct.fechaInicio }} a {{ opStore.operacionAct.fechaFin }}</p>
        </div>
    </dialog>

</template>
