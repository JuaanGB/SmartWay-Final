<script setup>
    
    import { ref } from 'vue';

    const mobileMenuVisible = ref(false)

    function toggleMenuMobile() {
        mobileMenuVisible.value = !mobileMenuVisible.value
    }

</script>

<template>

    <div class="block md:hidden">
        <label class="btn btn-primary p-1 rounded-md btn-ghost swap swap-rotate">
            <input id="menu" type="checkbox" @click="toggleMenuMobile" :value="mobileMenuVisible"/>
            <svg class="swap-on" xmlns="http://www.w3.org/2000/svg" width="32" height="32" viewBox="0 0 24 24"><!-- Icon from Material Design Icons by Pictogrammers - https://github.com/Templarian/MaterialDesign/blob/master/LICENSE --><path fill="currentColor" d="M19 6.41L17.59 5L12 10.59L6.41 5L5 6.41L10.59 12L5 17.59L6.41 19L12 13.41L17.59 19L19 17.59L13.41 12z"/></svg>
            <svg class="swap-off" xmlns="http://www.w3.org/2000/svg" width="32" height="32" viewBox="0 0 24 24"><!-- Icon from Material Design Icons by Pictogrammers - https://github.com/Templarian/MaterialDesign/blob/master/LICENSE --><path fill="currentColor" d="M3 6h18v2H3zm0 5h18v2H3zm0 5h18v2H3z"/></svg>
        </label>
        <ul v-show="mobileMenuVisible" class="menu dropdown-content bg-primary text-white rounded-box z-[1000] w-52 p-2 shadow-sm absolute top-18">
            <li><slot></slot></li>
        </ul>
    </div>

</template>