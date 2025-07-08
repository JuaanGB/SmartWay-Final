<script setup>
    import { onMounted, ref } from 'vue'
    import { jwtDecode } from 'jwt-decode'
    import router from '@/router'

    const inicial = ref('?')

    function getDecodedTokenIfValid(token) {
        if (!token) return null

        try {
            const decoded = jwtDecode(token)
            const now = Date.now() / 1000

            if (decoded.exp && decoded.exp > now) {
                return decoded
            } else {
                console.warn('Token expirado.')
                localStorage.removeItem('token')
            }
        } catch (e) {
            console.warn('Token inválido:', e)
            localStorage.removeItem('token')
        }

        return null
    }

    onMounted(() => {
        const token = localStorage.getItem("token")
        const decoded = getDecodedTokenIfValid(token)

        if (decoded) {
            const nombre = decoded.name || decoded.unique_name || decoded.Nombre || decoded.nombre
            if (nombre && nombre.length > 0) {
                inicial.value = nombre.charAt(0).toUpperCase()
            }
        }
    })

    function goToPage() {
        const token = localStorage.getItem("token")
        const decoded = getDecodedTokenIfValid(token)

        if (decoded)
            router.push('/perfil')
        else {
            router.push('/login')
            inicial.value = '?'
        }
            
    }
</script>




<template>

    <div class="avatar avatar-placeholder cursor-pointer" @click="goToPage">
        <div class="bg-neutral text-neutral-content w-10 rounded-full">
            <span class="text-xl font-bold">{{ inicial }}</span>
        </div>
    </div>

</template>