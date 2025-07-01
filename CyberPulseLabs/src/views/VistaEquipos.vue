<script setup>
import ModalOperacion from '@/components/ModalOperacion.vue';
import TarjetaEquipo from '@/components/TarjetaEquipo.vue';
import TarjetaEquipoNuevo from '@/components/TarjetaEquipoNuevo.vue';
import { useEquipos } from '@/stores/Equipos';
import { onMounted } from 'vue';

const equipoStore = useEquipos()

onMounted( () => {
	equipoStore.getAllEquipos()
})

function mostrarModal(id) {
	// Llamada get id a la API para tener el elemento que se quiere mostrar en el modal
	modal.showModal()
}

</script>

<template>

    <main class="flex flex-col items-center gap-4 m-4">
      	<h2 class="text-3xl font-bold">Gestión de equipos</h2>
		<div class="grid grid-cols-1 lg:grid-cols-2 w-full gap-4">
			<TarjetaEquipoNuevo class="mx-auto col-span-full"></TarjetaEquipoNuevo>
			<TarjetaEquipo class="mx-auto" v-for="eq in equipoStore.equipos" :key="eq.id"
				:id="eq.id" :nombre="eq.nombre" :especialidad="eq.especialidad" :operacionid="eq.operacionId">
			</TarjetaEquipo>
			
		</div>

		<ModalOperacion id="modal" @modal="(id) => mostrarModal(id)"></ModalOperacion>

  	</main>

</template>
