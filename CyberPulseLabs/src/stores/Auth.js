const uri = 'http://localhost:5152/api/Auth'

async function login(email, contraseña) {
    const item = {
        email: email,
        contraseña: contraseña
    }

    let res = await fetch(`${uri}/login`, {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
    return res
}

async function register(nombre, correo, contraseña) {
    const item = {
        nombre: nombre,
        email: correo,
        contraseña: contraseña
    }
}