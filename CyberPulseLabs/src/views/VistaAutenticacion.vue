<script setup>

function mostrarContraseña() {
    const x = document.getElementById("contraseña")
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}


</script>

<template>

    <main class="rounded-box bg-base-200 p-5 w-fit shadow-md shadow-primary mx-auto m-10 flex flex-col gap-2">
        
        <h2 class="font-bold text-center text-xl">¡Bienvenido!</h2>
        <label class="floating-label">
            <span>Nombre</span>
            <input type="text" placeholder="Nombre de usuario" class="input input-md" />
        </label>

        <div class="flex items-center gap-1">
            <input id="contraseña" class="input" type="password" placeholder="Contraseña">
            <button class="btn btn-round btn-primary btn-sm w-10 h-10" @click="mostrarContraseña">
                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24"><!-- Icon from HeroIcons by Refactoring UI Inc - https://github.com/tailwindlabs/heroicons/blob/master/LICENSE --><g fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="1.5"><path d="M2.036 12.322a1 1 0 0 1 0-.639C3.423 7.51 7.36 4.5 12 4.5c4.638 0 8.573 3.007 9.963 7.178c.07.207.07.431 0 .639C20.577 16.49 16.64 19.5 12 19.5c-4.638 0-8.573-3.007-9.963-7.178"/><path d="M15 12a3 3 0 1 1-6 0a3 3 0 0 1 6 0"/></g></svg>
            
            </button>
        </div>
        
        
        

    </main>

</template>