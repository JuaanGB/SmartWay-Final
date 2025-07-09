export class API {

    uri

    constructor(uri) {
        this.uri = uri
    }

    async _getAll() {
        try {
            let res = await fetch(this.uri, {
                method: 'GET',
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem('token')
                }
            })
            return res.json()
        } catch (error) {
            return false
        }
    }

    async _get(id) {
        let res = await fetch(`${this.uri}/${id}`, {
            method: "GET",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem('token')
            },
        })
        return res.json()
    }

    async _getCount() {
        try {
        let res = await fetch(`${this.uri}/count`, {
            method: "GET",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem('token')
            },
        })
        return await res.json()
        } catch (error) {
            return false
        }
        
    }

    async _create(item) {   
        let res = await fetch(`${this.uri}`, {
            method: "POST",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem('token')
            },
            body: JSON.stringify(item)
        })
        return res
    }

    async _update(item) {    
        let res = await fetch(`${this.uri}/${item.id}`, {
            method: "PATCH",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem('token')
            },
            body: JSON.stringify(item)
        })
        return res
    }

    async _delete(id) {
        let res = await fetch(`${this.uri}/${id}`, {
            method: "DELETE",
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('token')
            }
        })
        return res
    }
}