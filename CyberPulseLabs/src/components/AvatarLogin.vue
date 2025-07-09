<script setup>
    import { onMounted, ref } from 'vue'
    import { jwtDecode } from 'jwt-decode'
    import router from '@/router'
import { useTokenValidation } from '@/composables/tokenValidation'

    const inicial = ref('?')
    const tokenValidation = useTokenValidation()
    onMounted(() => {
        inicial.value = tokenValidation.getUserFirstLetter()
    })

    function goToPage() {
        if (tokenValidation.isValidToken()) {
            router.push('/perfil')
            inicial.value = tokenValidation.getUserFirstLetter()
        }
            
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