import { ref } from "vue";


export function useEditMode() {
    const editModeActive = ref(false)
    function toggleEditMode() {
        editModeActive.value = !editModeActive.value
    }
    return {editModeActive, toggleEditMode}
}