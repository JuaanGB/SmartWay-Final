const uri = 'http://192.168.0.52:5152/api/Auth'

export async function _login(email, contraseña) {
    const item = { email, contraseña }

    let res = await fetch(`${uri}/login`, {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })

    if (res.ok) {
        const data = await res.json()
        localStorage.setItem('token', data.token)
    }
    return res.ok
}

export async function _register(nombre, correo, contraseña) {
    const item = { nombre, email: correo, contraseña }

    let res = await fetch(`${uri}/register`, {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })

    if (res.ok) {
        const data = await res.json()
        localStorage.setItem('token', data.token)
    }
    return res.ok
}

export async function _changePassword(id, old, _new) {
    const item = {agenteId: id, contraseñaAntigua: old, contraseñaNueva: _new}

    let res = await fetch(`${uri}/password/${id}`, {
        method: "PATCH",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
    return res.ok
}