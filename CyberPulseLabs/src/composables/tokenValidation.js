import { jwtDecode } from "jwt-decode"


export function useTokenValidation() {

    const token = null
    let decoded = null

    function hasToken() {
        return token != null
    }

    function isValidToken() {
        let res = false
        if (!hasToken()) {
            console.log("if")
            res = false
        }
        else {
            try {
                console.log(token)
                decoded = jwtDecode(token)
                console.log(decoded)
                const now = Date.now() / 1000

                if (decoded.exp && decoded.exp > now) {
                    res = true
                } else {
                    localStorage.removeItem("token")
                    res = false
                }
            } catch (e) {
                localStorage.removeItem("token")
                res = false
            }
        }
        console.log(res)
        return res
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

    return {token, decoded, getUserId, hasToken, isValidToken, getUserFirstLetter}
}