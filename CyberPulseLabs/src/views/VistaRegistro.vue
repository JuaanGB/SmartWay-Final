<script setup>
import { ref } from 'vue'
import { RouterLink } from 'vue-router';
import * as api from '@/stores/Auth'
import { useNotificaciones } from '@/stores/Notificaciones'

const notiStore = useNotificaciones()

const mostrar = ref(false)
const nombre = ref('')
const email = ref('')
const contraseña = ref('')
const contraseña2 = ref('')

const terminos = ref(false)

function toggleContraseña() {
  mostrar.value = !mostrar.value
}

async function registrar() {
	if (!nombre.value || !email.value || !contraseña.value || !contraseña2.value) {
		notiStore.addNotificacion("error", 3000, "Debes rellenar todos los campos.")
		return
	}

	if (contraseña.value != contraseña2.value) {
		notiStore.addNotificacion("error", 3000, "Los dos campos de contraseñas no coinciden.")
		return
	}

	if (!terminos.value) {
		notiStore.addNotificacion("error", 3000, "Debes aceptar los términos y condiciones.")
		return
	}

	const exito = await api.register(nombre.value, email.value, contraseña.value)
    if (exito) 
        notiStore.addNotificacion("success", 3000, "Bienvenido.")
    else
        notiStore.addNotificacion("error", 3000, "Error al hacer el registro.")
}

</script>

<template>

	<div class="min-h-screen flex items-center justify-center">
		<main class="m-4 w-7/8 md:w-2xl rounded-box bg-base-200 p-8 shadow-xl flex flex-col gap-4 grid grid-cols-1 lg:grid-cols-2">
			<h2 class="font-bold col-span-full text-center text-2xl text-primary">Crear cuenta</h2>

			<!-- Nombre del agente -->
			<fieldset class="fieldset">
				<legend class="fieldset-legend">Nombre completo</legend>
				<input v-model="nombre" type="text" class="input w-full" placeholder="Tu nombre completo" />
			</fieldset>

			<!-- Email -->
			<fieldset class="fieldset">
				<legend class="fieldset-legend">Correo electrónico</legend>
				<input v-model="email" type="email" class="input w-full" placeholder="ejemplo@cyberpulselabs.com" />
			</fieldset>

			<!-- Contraseña -->
			<fieldset class="fieldset">
				<legend class="fieldset-legend">Contraseña</legend>
				<div class="relative">
					<input v-model="contraseña" :type="mostrar ? 'text' : 'password'" class="input w-full pr-12" placeholder="Contraseña">
					<label class="swap swap-flip text-primary absolute right-3 top-2">
						<input type="checkbox" @click="toggleContraseña" />
						<svg class="swap-off" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24"><!-- Icon from Material Design Icons by Pictogrammers - https://github.com/Templarian/MaterialDesign/blob/master/LICENSE --><path fill="currentColor" d="M12 9a3 3 0 0 1 3 3a3 3 0 0 1-3 3a3 3 0 0 1-3-3a3 3 0 0 1 3-3m0-4.5c5 0 9.27 3.11 11 7.5c-1.73 4.39-6 7.5-11 7.5S2.73 16.39 1 12c1.73-4.39 6-7.5 11-7.5M3.18 12a9.821 9.821 0 0 0 17.64 0a9.821 9.821 0 0 0-17.64 0"/></svg>
                    	<svg class="swap-on" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24"><!-- Icon from Material Design Icons by Pictogrammers - https://github.com/Templarian/MaterialDesign/blob/master/LICENSE --><path fill="currentColor" d="M2 5.27L3.28 4L20 20.72L18.73 22l-3.08-3.08c-1.15.38-2.37.58-3.65.58c-5 0-9.27-3.11-11-7.5c.69-1.76 1.79-3.31 3.19-4.54zM12 9a3 3 0 0 1 3 3a3 3 0 0 1-.17 1L11 9.17A3 3 0 0 1 12 9m0-4.5c5 0 9.27 3.11 11 7.5a11.8 11.8 0 0 1-4 5.19l-1.42-1.43A9.86 9.86 0 0 0 20.82 12A9.82 9.82 0 0 0 12 6.5c-1.09 0-2.16.18-3.16.5L7.3 5.47c1.44-.62 3.03-.97 4.7-.97M3.18 12A9.82 9.82 0 0 0 12 17.5c.69 0 1.37-.07 2-.21L11.72 15A3.064 3.064 0 0 1 9 12.28L5.6 8.87c-.99.85-1.82 1.91-2.42 3.13"/></svg>
                	</label>
				</div>
			</fieldset>

			<!-- Confirmar contraseña -->
			<fieldset class="fieldset">
				<legend class="fieldset-legend">Confirmar contraseña</legend>
				<input v-model="contraseña2" type="password" class="input w-full" placeholder="Repite la contraseña" />
			</fieldset>

			<!-- Términos y condiciiones -->
			<label class="flex items-center gap-2 col-span-full">
				<input v-model="terminos" type="checkbox" class="checkbox checkbox-primary" />
				<span class="text-sm">Acepto los términos y condiciones</span>
			</label>


			<button @click="registrar" class="btn btn-primary w-full col-span-full">Crear una cuenta</button>

			<div class="col-span-full text-gray-500 flex flex-col items-center text-sm mt-2">
				<span>¿Ya tienes una cuenta?</span>
				<span>Inicia sesión <RouterLink to="login" class="hover:underline cursor-pointer">aquí</RouterLink></span>
			</div>
		</main>
	</div>
</template>
