import { createRouter, createWebHistory } from 'vue-router'
import VistaPrincipal from '../views/VistaPrincipal.vue'
import VistaAgentes from '@/views/VistaAgentes.vue'
import VistaEquipos from '@/views/VistaEquipos.vue'
import VistaOperaciones from '@/views/VistaOperaciones.vue'
import VistaLogin from '@/views/VistaLogin.vue'
import VistaRegistro from '@/views/VistaRegistro.vue'
import VistaPerfil from '@/views/VistaPerfil.vue'

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
      path: '/login',
      name: 'login',
      component: VistaLogin,
    },
    {
      path: '/register',
      name: 'register',
      component: VistaRegistro,
    },
    {
      path: '/perfil',
      name: 'perfil',
      component: VistaPerfil,
    },
  ],
})

export default router
