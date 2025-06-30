<script setup>
import TarjetaEquipo from '@/components/TarjetaEquipo.vue';
import TarjetaEquipoNuevo from '@/components/TarjetaEquipoNuevo.vue';

</script>

<template>

    <main class="flex flex-col items-center gap-4 m-4">
      	<h2 class="text-3xl font-bold">Gestión de equipos</h2>
		<div class="grid grid-cols-2 w-full gap-4">
			<TarjetaEquipoNuevo class="mx-auto"></TarjetaEquipoNuevo>
			<TarjetaEquipo class="mx-auto"></TarjetaEquipo>
			<TarjetaEquipo class="mx-auto"></TarjetaEquipo>
		</div>

  	</main>

</template>
