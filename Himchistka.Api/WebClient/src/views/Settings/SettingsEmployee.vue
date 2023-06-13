<template>
    <section class="pa-5">
        <v-toolbar>
            <v-toolbar-title>
                <h4>Настройки сотрудников</h4>
            </v-toolbar-title>
            <v-spacer></v-spacer>
        </v-toolbar>
        <v-card-text>
            <v-layout>
                <v-flex lg12 hidden-sm-and-down>
                    <v-data-table
                    :headers="headers"
                    :items="Employers"
                    :loading="loading"
                    loading-text="Загрузка данных"
                    :items-per-page="5"
                    class="elevation-1"
                    >
                    <template v-slot:top>
                            <v-toolbar flat>
                                <v-btn
                                    color="primary"
                                    dark
                                    class="mb-2"
                                    @click="ItemDialog = true, addForm = true"
                                    >
                                    Добавить
                                    </v-btn>
                            </v-toolbar>
                        </template>
                        <template v-slot:item.actions="{ item }">
                            <v-icon
                                small
                                class="mr-2"
                                @click="editItem(item), addForm = false"
                            >
                                mdi-pencil
                            </v-icon>
                            <v-icon
                                small
                                @click="deleteItem(item)"
                            >
                                mdi-delete
                            </v-icon>
                            </template>
                    </v-data-table>
                </v-flex>
            </v-layout>
        </v-card-text>
        <v-dialog
            v-model="ItemDialog"
            :fullscreen="$vuetify.breakpoint.smAndDown"
            max-width="800"
            :persistent="true">
            <v-card class="pb-4">
                <v-toolbar>
                    <v-toolbar-title>
                        <h4>{{addForm ? 'Добавить' : 'Редактировать'}}</h4>
                        <v-spacer></v-spacer>
                    </v-toolbar-title>
                </v-toolbar>
                <v-card-text>
                    <v-container tag="section" py-3 fluid grid-list-md>
                        <v-form :lazy-validation="true">
                                <v-text-field
                                        v-model="form.fullName"
                                        label="ФИО"
                                    ></v-text-field>   
                                    <v-text-field
                                        v-model="form.email"
                                        label="Email"
                                    ></v-text-field>
                                    <v-text-field
                                        v-model="form.phoneNumber"
                                        label="Номер телефона"
                                        type="tel"
                                        prefix="+"
                                        mask="7##########"
                                    ></v-text-field> 
                                    <v-text-field v-if="addForm"
                                        v-model="form.username"
                                        label="Логин"
                                    ></v-text-field>      
                                    <v-text-field v-if="addForm"
                                        v-model="form.password"
                                        label="Пароль"
                                    ></v-text-field>  
                                    <v-select
                                        v-model="form.places"
                                        :items="PlaceItems"
                                        label="Места работы"
                                        multiple
                                        item-value="id"
                                        item-text="name"
                                        >
                                        
                                    </v-select>
                        </v-form>
                        
                    </v-container>
                </v-card-text>
                <v-card-actions>
                    <v-btn rounded @click="ItemDialog = false" color="secondary">Отмена</v-btn>
                    <v-btn rounded color="primary" @click="addItem">Добавить</v-btn>
                </v-card-actions>
  
            </v-card>
        </v-dialog>
        <v-dialog v-model="dialogDelete" max-width="700px">
          <v-card>
            <v-card-title class="text-h6">Вы уверены, что хотите удалить данного сотрудника?</v-card-title>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue darken-1" text @click="this.dialogDelete = false, this.deleteItemId = null">Отмена</v-btn>
              <v-btn color="blue darken-1" text @click="deleteItemConfirm">Подтвердить</v-btn>
              <v-spacer></v-spacer>
            </v-card-actions>
          </v-card>
        </v-dialog>
    </section>
  </template>
  
  <script>
  import axios from 'axios'
  
  let config = {
  headers: {
    "Authorization": "Bearer " + JSON.parse(localStorage.getItem('user')).access_token,
  }
  }
  
  export default {
  data(){
    return{
        PlaceItems: [],
        Employers: [],
        ItemDialog: false,
        dialogDelete: false,
        loading: true,
        deleteItemId: null,
        addForm: true,
        form: {
            id: null,
            fullName: '',
            email:'',
            phoneNumber: '',
            userName: '',
            password: '',
            places: []
        }
    }
  },
  computed: {
      headers () {
        return [
          {
            text: 'ФИО',
            align: 'start',
            sortable: false,
            value: 'fullName',
          },
          {
            text: 'Email',
            sortable: false,
            value: 'email',
          },
          {
            text: 'Номер телефона',
            sortable: false,
            value: 'phoneNumber',
          },
          {
            text: 'Логин',
            sortable: false,
            value: 'normalizedUserName',
          },
       { text: 'Действия', value: 'actions', sortable: false  },
        ]
      },
    },
  methods: {
    addItem(){
        axios.post('http://localhost:8000/api/Account/CreateEmployee',this.form)
        .then(res => {
            this.ItemDialog = false,
            this.form.fullName = '',
            this.form.email = ''
            this.form.phoneNumber = ''
            this.form.userName = ''
            this.form.password = ''
            this.form.id = null
            console.log(res.body)
            this.getItems();
        })
    },
    editItem(item){
        this.form.fullName = item.fullName
        this.form.email = item.email
        this.form.phoneNumber= item.phoneNumber
        this.form.userName= item.userName
        this.form.password= item.password
        this.form.places= item.places.map(e => e.id)
        this.form.id = item.id
        this.ItemDialog = true
    },
    deleteItem(item){
        this.deleteItemId = item.id
        this.dialogDelete = true
    },
    deleteItemConfirm(){
        axios.delete(`http://localhost:8000/api/Account?Id=${this.deleteItemId}`)
        .then(res => {
            console.log(res)
            this.deleteItemId = null
            this.dialogDelete = false
            this.getItems()
        })
    },
    getItems(){
      this.loading = true
      axios.get(`http://localhost:8000/api/Account/GetEmployee`)
      .then(res => {
        console.log(res)
        this.Employers = res.data
        this.loading = false
      })
    },
    getPlaces(){
        axios.get(`http://localhost:8000/api/Places`)
        .then(res => {
        this.PlaceItems = res.data
      })
    }
  },
  created() {
    this.getItems();
    this.getPlaces();
  }
  }
  
  </script>