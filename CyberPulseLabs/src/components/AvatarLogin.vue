<script setup>
import { onMounted, ref } from 'vue'
import { jwtDecode} from 'jwt-decode'


    const inicial = ref('?')

    onMounted( () => {
        console.log("montado")
        const token = localStorage.getItem("token")
        if (token) {
            try {
                const decoded = jwtDecode(token)
                console.log(decoded)
                const now = Date.now() / 1000

                if (decoded.exp && decoded.exp > now) {
                    const nombre = decoded.unique_name
                    console.log(decoded.unique_name)

                    if (nombre && nombre.length > 0) {
                        inicial.value = nombre.charAt(0).toUpperCase()
                    }
                } else {
                    localStorage.removeItem('token')
                }
            } catch (e) {
                console.warn('Token inválido:', e)
                localStorage.removeItem('token')
            }
        }
    })
    

</script>


<template>

    <RouterLink class="avatar avatar-placeholder" to="/login">
        <div class="bg-neutral text-neutral-content w-10 rounded-full">
            <span class="text-xl font-bold">{{ inicial }}</span>
        </div>
    </RouterLink>

</template>