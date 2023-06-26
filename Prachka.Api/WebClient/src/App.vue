<template>
  <v-app>
      <v-app-bar
        app
        color="primary"
        dark
      >
      
        <v-app-bar-nav-icon @click="drawer = true" v-if="!$route.path.includes('login')" ></v-app-bar-nav-icon>

        <v-toolbar-title>Прачечная система</v-toolbar-title>
    </v-app-bar>
    <v-navigation-drawer
      v-model="drawer"
      absolute
      temporary
      v-if="!$route.path.includes('login')"
    >
      <v-list
        nav
        dense
      >
        <v-list-item-group
          v-model="group"
          active-class=" text--accent-4"
        >
          <v-list-item to="/orders">
            <v-list-item-icon>
              <v-icon>mdi-note-multiple</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Заказы</v-list-item-title>
          </v-list-item>

          <v-list-item to="/clients" v-if="isAdmin">
            <v-list-item-icon>
              <v-icon>mdi-account</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Клиенты</v-list-item-title>
          </v-list-item>
          <v-list-item to="/analytics" v-if="isAdmin">
            <v-list-item-icon>
              <v-icon>mdi-chart-arc</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Аналитика</v-list-item-title>
          </v-list-item>

          <v-list-item to="/settings" v-if="isAdmin">
            <v-list-item-icon>
              <v-icon>mdi-cog-outline</v-icon>
            </v-list-item-icon>
            <v-list-item-title >Настройка</v-list-item-title>
          </v-list-item>
          <v-list-item to="/login" @click="logout">
            <v-list-item-icon>
              <v-icon>mdi-exit-to-app</v-icon>
            </v-list-item-icon>
            <v-list-item-title >Выход</v-list-item-title>
          </v-list-item>
        </v-list-item-group>
      </v-list>
    </v-navigation-drawer>
    
    
    <v-main>
        <router-view/>
      </v-main>
    
  </v-app>
</template>

<script>
export default {
    data: () => ({
      drawer: false,
      group: null,
      isAdmin: false
    }),
    methods: {
      logout(){
        localStorage.removeItem('user');
      }
    },
    created(){
      let roles = JSON.parse(localStorage.getItem('user')).roles
      if(roles.includes('Admin')){
        this.isAdmin = true
      }
    }
  }
</script>
