<script setup>

    const props = defineProps(['nombre','estado','inicio','fin'])

</script>

<template>

    <dialog class="modal">
        <div class="modal-box">
            <form method="dialog">
            <button class="btn btn-sm btn-circle btn-ghost absolute right-2 top-2">✕</button>
            </form>
            <h3 class="text-lg font-bold">{{ props.nombre }}</h3>
            <div class="badge badge-secondary">{{ props.estado }}</div>
            <p class="py-4">{{ props.inicio + " - " + props.fin }}</p>
        </div>
    </dialog>

</template>