import { createRouter, createWebHistory } from 'vue-router'
import VistaPrincipal from '../views/VistaPrincipal.vue'
import VistaAgentes from '@/views/VistaAgentes.vue'
import VistaEquipos from '@/views/VistaEquipos.vue'
import VistaOperaciones from '@/views/VistaOperaciones.vue'
import VistaAutenticacion from '@/views/VistaAutenticacion.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: VistaPrincipal,
    },
    {
     path: '/agentes',
     name: 'agentes',
     component: VistaAgentes,
    },
    {
     path: '/equipos',
     name: 'equipos',
     component: VistaEquipos,
    },
    {
     path: '/operaciones',
     name: 'operaciones',
     component: VistaOperaciones,
    },
    {
      path: '/auth',
      name: 'auth',
      component: VistaAutenticacion,
    }
  ],
})

export default router
