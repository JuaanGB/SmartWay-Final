<script setup>
import AgentePerfil from '@/components/AgentePerfil.vue';
import InputContraseña from '@/components/InputContraseña.vue';
import { useTokenValidation } from '@/composables/tokenValidation';
import router from '@/router';
import { useAgentes } from '@/stores/Agentes';
import { useNotificaciones } from '@/stores/Notificaciones';
import { onBeforeMount, onMounted, ref } from 'vue';
import { changePassword } from '@/stores/Auth';

let id = null
const oldPassword = ref('')
const newPassword = ref('')
const tokenValidation = useTokenValidation()
const notiStore = useNotificaciones()
const agStore = useAgentes()


onMounted( () => {
    if (!tokenValidation.isValidToken()) {
        notiStore.addNotificacion("error", 3000, "La sesión iniciada ha expirado.")
        router.push('/login')
    }
})


onBeforeMount( () => {
    if (tokenValidation.isValidToken()) {
        id = tokenValidation.getUserId()
        agStore.setAgenteAct(id)
    }
})

async function cambiarContraseña() {
    if (!oldPassword.value || !newPassword.value) {
        notiStore.addNotificacion("error", 3000, "Debes rellenar ambos campos.")
        return
    } else if (oldPassword.value == newPassword.value) {
        notiStore.addNotificacion("error", 3000, "Indica una contraseña diferente a la anterior.")
        return
    }

    let exito = await changePassword(id, oldPassword.value, newPassword.value)
    if (exito) {
        notiStore.addNotificacion("success", 3000, "Contraseña cambiada con éxito.")
        flushInputs()
    } else {
        notiStore.addNotificacion("¿Estás seguro de que esa es la contraseña original?")
    }
}

function flushInputs() {
    oldPassword.value = ''
    newPassword.value = ''
}

async function cerrarSesion() {
    localStorage.removeItem("token")
    router.push('login')
    notiStore.addNotificacion("success", 3000, "Sesión cerrada correctamente.")
}

</script>

<template>

    <main class="grid grid-cols-1 md:grid-cols-[200px_500px] gap-2 m-10 justify-center content-center">
        <AgentePerfil class="mx-auto"
            :id="agStore.agenteAct?.id || null"
            :nombre="agStore.agenteAct?.nombre || 'Usuario no identificado'" 
            :activo="agStore.agenteAct?.activo || false"
            :equipo-id="agStore.agenteAct?.equipoId || null"
            :rango="agStore.agenteAct?.rango || null"
            :editable="false">
        </AgentePerfil>

        <div class="rounded-box bg-base-200 shadow-md shadow-primary p-4">
            <!-- Titulo -->
            <h2 class="font-bold text-center text-2xl text-primary mb-2">Cambio de contraseña</h2>
            <InputContraseña titulo="Contraseña antigua" placeholder="Contraseña" v-model="oldPassword"></InputContraseña>
            <InputContraseña titulo="Contraseña nueva" placeholder="Contraseña" v-model="newPassword"></InputContraseña>
            <button @click="cambiarContraseña" class="btn btn-primary w-full mt-4">Confirmar</button>
        </div>

        <button @click="cerrarSesion" class="row-1 md:row-3 btn btn-secondary col-span-full border-2 border-neutral shadow-md">Cerrar sesión</button>

    </main>
    

</template>