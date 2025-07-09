<script setup>
import AgentePerfil from '@/components/AgentePerfil.vue';
import InputContraseña from '@/components/InputContraseña.vue';
import { useTokenValidation } from '@/composables/tokenValidation';
import router from '@/router';
import { useAgentes } from '@/stores/Agentes';
import { useNotificaciones } from '@/stores/Notificaciones';
import { onBeforeMount, onMounted, ref } from 'vue';

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
        const id = tokenValidation.getUserId()
        agStore.setAgenteAct(id)
    }
})

</script>

<template>

    <main class="flex flex-row gap-5 m-10 items-center justify-center">
        <AgentePerfil
            :id="agStore.agenteAct?.id || null"
            :nombre="agStore.agenteAct?.nombre || 'Nombre'" 
            :activo="agStore.agenteAct?.activo || false"
            :equipo-id="agStore.agenteAct?.equipoId || null"
            :rango="agStore.agenteAct?.rango || null"
            :editable="false">
        </AgentePerfil>

        <div class="rounded-box bg-base-200 shadow-md shadow-primary w-1/3 p-4">
            <!-- Titulo -->
            <h2 class="font-bold text-center text-2xl text-primary mb-2">Cambio de contraseña</h2>
            <InputContraseña titulo="Contraseña antigua" placeholder="Contraseña" v-model="oldPassword"></InputContraseña>
            <InputContraseña titulo="Contraseña nueva" placeholder="Contraseña" v-model="newPassword"></InputContraseña>
            <button class="btn btn-primary w-full mt-4">Guardar cambios</button>
        </div>

    </main>
    

</template>