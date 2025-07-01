const uri = 'http://localhost:5152/api/Equipos'

export async function _getAll() {
    let res = await fetch(uri)
    return res.json()
}

export async function _create(id, nombre, especialidad, operacionID) {
    const item = {
        id: id,
        nombre: nombre,
        especialidad: especialidad,
        operacionID: operacionID
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

export async function _update(id, nombre, especialidad, operacionID) {
    const item = {
        id: id,
        nombre: nombre,
        especialidad: especialidad,
        operacionID: operacionID
    }
    console.log(item)

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