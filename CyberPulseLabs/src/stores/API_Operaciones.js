const uri = 'http://localhost:5152/api/Operaciones'

export async function _getAll() {
    try {
        let res = await fetch(uri)
        return res.json()
    } catch (error) {
        return false
    }
}

export async function _get(id) {
    let res = await fetch(`${uri}/${id}`, {
        method: "GET",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    })
    return res
}

export async function _getCount() {
    let res = await fetch(`${uri}/count`, {
        method: "GET",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    })
    return await res.json()
}

export async function _create(id, nombre, estado, inicio, fin) {
    const item = {
        id: id,
        nombre: nombre,
        estado: estado,
        fechaInicio: inicio,
        fechaFin: fin
    }

    let res = await fetch(`${uri}`, {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
    return res
}

export async function _update(id, nombre, estado, inicio, fin) {
    const item = {
        id: id,
        nombre: nombre,
        estado: estado,
        fechaInicio: inicio,
        fechaFin: fin
    }

    let res = await fetch(`${uri}/${id}`, {
        method: "PUT",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
    return res
}

export async function _delete(id) {
    let res = await fetch(`${uri}/${id}`, {
        method: "DELETE"
    })
    return res
}