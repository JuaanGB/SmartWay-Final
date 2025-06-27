# SmartWay-Final

## Pinia

```javascript
defineStore('id', () => {
  // Variables y referencias (estado)
  // Variables computed (getters)
  // Métodos/Funciones (acciones)
})
```

- Para acceder a otros métodos/getters dentro del mismo `defineStore`, acceder a la instancia completa con `this.algo`.

- Podemos usar un `store` dentro de otro `store`.