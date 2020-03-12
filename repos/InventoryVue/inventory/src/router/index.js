import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Categoria from '../components/Categoria.vue'
import Articulo from '../components/Ariticulo.vue'
import Ingreso from '../components/Ingreso.vue'
import Venta from '../components/Salida.vue'
import Proveedor from '../components/Proveedor.vue'
import Cliente from '../components/Cliente.vue'
import Rol from '../components/Rol.vue'
import Usuario from '../components/Usuario.vue'
import Login from '../components/Login.vue'
import Almacen from '../components/Almacen.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'home',
    component: Home,
    meta :{
      administrador :true,
      almacenero: true,
      vendedor: true
    }
  },
  {
    path: '/categorias',
    name: 'categorias',
    component: Categoria,
    meta :{
      administrador :true,
      almacenero: true
    }
  },
  {
    path: '/almacenes',
    name: 'almacenes',
    component: Almacen,
    meta :{
      administrador :true,
      almacenero: true
    }
  },
  {
    path: '/articulos',
    name: 'articulos',
    component: Articulo,
    meta :{
      administrador :true,
      almacenero: true
    }
  },
  {
    path: '/ingresos',
    name: 'ingresos',
    component: Ingreso,
    meta :{
      administrador :true,
      almacenero: true
    }
  }
  ,
    {
      path: '/ventas',
      name: 'ventas',
      component: Venta,
      meta :{
        administrador :true,
        vendedor: true
      }
    },
    {
      path: '/proveedores',
      name: 'proveedores',
      component: Proveedor,
      meta :{
        administrador :true,
        almacenero: true
      }
    },
    {
      path: '/clientes',
      name: 'clientes',
      component: Cliente,
      meta :{
        administrador :true,
        vendedor: true
      }
    },
    {
      path: '/roles',
      name: 'roles',
      component: Rol,
      meta :{
        administrador :true
      }
    },
    {
      path: '/usuarios',
      name: 'usuarios',
      component: Usuario,
      meta :{
        administrador :true
      }
    },
    {
      path: '/login',
      name: 'login',
      component: Login,
      meta : {
        libre: true
      }
    }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
