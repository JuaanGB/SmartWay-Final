<script setup>
    import router from '@/router'
    import { useSesion } from '@/stores/Sesion'

    const sesion = useSesion()

    function goToPage() {
        if (sesion.isValidToken) {
            router.push('/perfil')
        } 
        else {
            router.push('/login')
        }
            
    }
</script>

<template>

    <div class="avatar avatar-placeholder cursor-pointer" @click="goToPage">
        <div class="bg-neutral text-neutral-content w-10 rounded-full">
            <span class="text-xl font-bold">{{ sesion.userFirstLetter }}</span>
        </div>
    </div>

</template>