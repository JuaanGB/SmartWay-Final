<script setup>
import { onMounted, ref } from 'vue'
import * as api from '@/stores/Auth'
import { useNotificaciones } from '@/stores/Notificaciones'
import router from '@/router'
import InputContraseña from '@/components/InputContraseña.vue'
import { useTokenValidation } from '@/composables/tokenValidation'

const notiStore = useNotificaciones()

const tokenValidation = useTokenValidation()
const email = ref('')
const contraseña = ref('')

async function iniciarSesion() {
    if (!email.value || !contraseña.value) {
        notiStore.addNotificacion("error", 3000, "Debes rellenar ambos campos.")
        return
    }

    const exito = await api.login(email.value, contraseña.value)
    if (exito) {
        notiStore.addNotificacion("success", 3000, "Bienvenido.")
        router.push('/perfil')
    } else
        notiStore.addNotificacion("error", 3000, "Credenciales incorrectas.")
}

function completarCorreo() {
    if (email.value && !email.value.includes('@'))
        email.value += "@cyberpulselabs.com"
}

onMounted( () => {
    if (tokenValidation.isValidToken()) {
        router.push('/perfil')
    }
})
</script>

<template>
    <div class="min-h-screen flex items-center justify-center">
        <main class="w-7/8 max-w-md rounded-box bg-base-200 p-8 shadow-xl flex flex-col gap-4">
        
        <!-- Titulo -->
        <h2 class="font-bold text-center text-2xl text-primary">¡Bienvenido!</h2>

        <!-- Email -->
        <fieldset class="fieldset">
            <legend class="fieldset-legend">Correo electrónico</legend>
            <input v-model="email" :onblur="completarCorreo" type="email" class="input w-full" placeholder="agente@cybperpulselabs.com" />
        </fieldset>

        <!-- Contraseña -->
        <InputContraseña v-model="contraseña" titulo="Contraseña" placeholder="Contraseña"></InputContraseña>

        <!-- Boton de validar -->
        <button @click="iniciarSesion" class="btn btn-primary w-full">Iniciar sesión</button>

        <!-- Acceder a registro -->
        <div class="text-gray-500 flex flex-col items-center text-sm mt-2">
            <span>¿No tienes una cuenta?</span>
            <span>Crea una <RouterLink class="hover:underline cursor-pointer" to="/register">aquí</RouterLink></span>
        </div>

        </main>
    </div>
</template>
