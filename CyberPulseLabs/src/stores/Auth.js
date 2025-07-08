const uri = 'http://localhost:5152/api/Auth'

export async function login(email, contraseña) {
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

export async function register(nombre, correo, contraseña) {
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
