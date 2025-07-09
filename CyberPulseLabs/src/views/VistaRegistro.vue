<script setup>
import { onMounted, ref } from 'vue'
import { RouterLink } from 'vue-router';
import * as api from '@/stores/Auth'
import { useNotificaciones } from '@/stores/Notificaciones'
import InputContraseña from '@/components/InputContraseña.vue'
import { useTokenValidation } from '@/composables/tokenValidation';
import router from '@/router';

const notiStore = useNotificaciones()

const mostrar = ref(false)
const nombre = ref('')
const email = ref('')
const contraseña = ref('')
const contraseña2 = ref('')

const terminos = ref(false)
const tokenValidation = useTokenValidation()

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
    if (exito) {
        notiStore.addNotificacion("success", 3000, "Bienvenido.")
		router.push('/perfil')
	}
    else
        notiStore.addNotificacion("error", 3000, "Error al hacer el registro.")
}

onMounted( () => {
    if (tokenValidation.isValidToken()) {
        router.push('/perfil')
    }
})

</script>

<template>

	<div class="flex items-center justify-center mt-10">
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
        	<InputContraseña v-model="contraseña" titulo="Contraseña" placeholder="Contraseña"></InputContraseña>

			<!-- Confirmar contraseña -->
			<InputContraseña v-model="contraseña2" titulo="Repetir contraseña" placeholder="Contraseña"></InputContraseña>

			<!-- Términos y condiciiones -->
			<label class="flex items-center gap-2 col-span-full">
				<input v-model="terminos" type="checkbox" class="checkbox checkbox-primary" />
				<span class="text-sm">Acepto los términos y condiciones</span>
			</label>


			<button @click="registrar" class="btn btn-primary w-full col-span-full">Crear una cuenta</button>

			<div class="col-span-full text-gray-500 flex flex-col items-center text-sm mt-2">
				<span>¿Ya tienes una cuenta?</span>
				<span>Inicia sesión <RouterLink to="login" class="hover:underline cursor-pointer text-primary">aquí</RouterLink></span>
			</div>
		</main>
	</div>
</template>
