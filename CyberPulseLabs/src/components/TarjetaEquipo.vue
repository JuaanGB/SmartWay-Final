<script setup>
import { ref } from 'vue';


    const visibleMembers = ref(false)

    function toggleMembers() {
        visibleMembers.value = !visibleMembers.value
    }

    function addMember() {
        // Obtener texto del input con sus comprobaciones (no es necesario v-model)
        // Llamada a pinia para que gestione todo (búsqueda del miembro, adición y actualización)
    }


</script>

<template>

    <div class="join">
        <div class="card card-secondary shadow-sm bg-base-300 flex-row join-item">
            <div class="card-body w-80">
                <!-- Información siempre visible -->
                <div class="flex flex-row">
                    <h3 class="card-title">Nombre equipo (#ID)</h3>
                    <button class="btn btn-circle btn-secondary btn-sm ml-auto" @click="toggleMembers">
                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 16 16"><!-- Icon from HeroIcons by Refactoring UI Inc - https://github.com/tailwindlabs/heroicons/blob/master/LICENSE --><path fill="currentColor" d="M8 8a2.5 2.5 0 1 0 0-5a2.5 2.5 0 0 0 0 5m-4.844 3.763c.16-.629.44-1.21.813-1.72a2.5 2.5 0 0 0-2.725 1.377c-.136.287.102.58.418.58h1.449q.015-.116.045-.237m9.691 0q.03.12.046.237h1.446c.316 0 .554-.293.417-.579a2.5 2.5 0 0 0-2.722-1.378c.374.51.653 1.09.813 1.72M14 7.5a1.5 1.5 0 1 1-3 0a1.5 1.5 0 0 1 3 0M3.5 9a1.5 1.5 0 1 0 0-3a1.5 1.5 0 0 0 0 3M5 13c-.552 0-1.013-.455-.876-.99a4.002 4.002 0 0 1 7.753 0c.136.535-.324.99-.877.99z"/></svg>
                    </button>
                </div>
                <div class="badge badge-primary">Monitoreo</div>
                <p>Se encarga de: <a>Operacion</a></p>
            </div>    
        </div>
        <!-- Información visible si clica botón de miembros -->
        <ul v-show="visibleMembers" class="list bg-base-200 rounded-box shadow-sm w-40 join-item">
            <li class="list-row">Miembro 1</li>
            <li class="list-row">Miembro 2</li>
            <li class="join">
                <input class="input join-item" type="text" placeholder="nuevo miembro">
                <button class="btn btn-primary join-item" @click="addMember">Ok</button>
            </li>
        </ul>
    </div>

</template>