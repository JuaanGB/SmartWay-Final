<script setup>

</script>

<template>

    <main class="flex flex-col items-center gap-3 m-0">
        <div class="hero bg-base-300">
            <div class="hero-content flex-col">
                <h2 class="text-5xl font-bold">Cyberpulse Labs</h2>
                <h3 class="text-2xl">Herramienta de gestión</h3>
                <p>
                    Gestiona fácilmente la base de datos de la organización a través de una página web sencilla
                    que te permitirá añadir, eliminar, actualizar y consultar sus equipos, operaciones y agentes.
                </p>
                <p>
                    ¡Empieza ya a gestionar la organización!
                </p>
                <div class="flex lg:flex-row flex-col my-4 gap-4">
                    <button class="btn btn-primary" @click="$router.push('/operaciones')">Operaciones</button>
                    <button class="btn btn-primary" @click="$router.push('/agentes')">Agentes</button>
                    <button class="btn btn-primary" @click="$router.push('/equipos')">Equipos</button>
                </div>
            </div>
        </div>
    </main>

</template>

<style scoped>

.btn {
    padding-left: 20px;
    padding-right: 20px;
    width: 160px;
}

.p {
    text-align: center;
}

</style>