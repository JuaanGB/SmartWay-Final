import { ref, watch } from "vue";


export function useEditMode(watch_callback) {
    const editModeActive = ref(false)

    function toggleEditMode() {
        editModeActive.value = !editModeActive.value
    }

    watch(editModeActive, watch_callback)

    return {editModeActive, toggleEditMode}
}