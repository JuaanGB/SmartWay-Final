import { jwtDecode } from "jwt-decode"


export function useTokenValidation() {

    const token = localStorage.getItem("token")
    let decoded = null

    function hasToken() {
        return token != null
    }

    function isValidToken() {
        if (!hasToken()) return false

        try {
            decoded = jwtDecode(token)
            const now = Date.now() / 1000

            if (decoded.exp && decoded.exp > now) {
                return true
            } else {
                localStorage.removeItem("token")
                return false
            }
        } catch (e) {
            localStorage.removeItem("token")
            return false
        }
    }

    function getUserFirstLetter() {
        if (!isValidToken()) return '?'
        
        const nombre = decoded.unique_name
        return nombre.charAt(0)
    }

    function getUserId() {
        if (!decoded) return null
        return decoded.nameid
    }

    return {decoded, getUserId, hasToken, isValidToken, getUserFirstLetter}
}