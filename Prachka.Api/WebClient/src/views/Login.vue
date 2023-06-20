<template>
    <v-content>
       <v-container fluid fill-height>
          <v-layout align-center justify-center>
             <v-flex xs12 sm8 md4 >
                <v-card class="elevation-12 " >
                   <v-toolbar dark color="primary" class="pa-2 align-self-end">
                      <v-toolbar-title>Авторизация в системе</v-toolbar-title>
                   </v-toolbar>
                   <v-card-text>
                      <v-form>
                         <v-text-field
                            v-model="username"
                            name="login"
                            label="Логин"
                            type="text"
                         ></v-text-field>
                         <v-text-field
                            v-model="password"
                            id="password"
                            name="password"
                            label="Пароль"
                            type="password"
                         ></v-text-field>
                      </v-form>
                   </v-card-text>
                   <v-card-actions>
                      <v-spacer></v-spacer>
                      <v-btn color="primary" to="/orders" @click="submit">Войти</v-btn>
                   </v-card-actions>
                </v-card>
             </v-flex>
          </v-layout>
       </v-container>
    </v-content>
</template>

<script>
import axios from 'axios'

export default {
    data () {
        return {
            username: '',
            password: '',
            submitted: false
        }
    },
    created () {
    },
    methods: {
      submit() {
        let body = {
          userName: this.username,
          password: this.password
        }
        axios.post(`http://localhost:8000/api/Account/token`, body)
        .then(response => {
            localStorage.setItem('user', JSON.stringify(response.data));
            this.$router.push({ name: 'orders' })
          })
        .catch(res => {console.log(res)})
    },
    },
    
};
</script>