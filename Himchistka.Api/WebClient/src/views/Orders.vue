<template>
    <section class="pa-5">
        <v-toolbar>
            <v-toolbar-title>
                <h4>Заказы</h4>
            </v-toolbar-title>
            <v-spacer></v-spacer>
        </v-toolbar>
        <v-card-text>
            <v-layout>
                <v-flex lg12 hidden-sm-and-down>
                    <v-data-table
                    :headers="headers"
                    :items="Orders"
                    :loading="loading"
                    loading-text="Загрузка данных"
                    :items-per-page="15"
                    class="elevation-1"
                    >
                    <template v-slot:top>
                            <v-toolbar flat max-width="600px">
                                <v-flex  class="d-flex pa-2" >
                                    <v-btn xs3
                                        color="primary"
                                        dark
                                        class="mb-2  ma-2"
                                        @click="ItemDialog = true, addForm = true"
                                        >
                                        Добавить
                                    </v-btn>
                                    <v-select xs3
                                        v-model="selectedPlace"
                                        :items="Places"
                                        label="Предприятие"
                                        item-value="id"
                                        item-text="name"
                                        @change="getItems"
                                        >

                                    </v-select>
                                    
                                </v-flex>
                                
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
                                    <v-btn
                                    @click="newClientForm = true"
                                    >
                                    Новый клиент?
                                    </v-btn>
                                    <v-autocomplete
                                       v-model="form.clientId"
                                       :items="Clients"
                                       label="Клиент"
                                       item-value="id"
                                       item-text="selectName"
                                       >
                                    </v-autocomplete>
                                    <v-select
                                       v-model="form.services"
                                       multiple
                                       :items="Services"
                                       label="Услуги"
                                       item-value="id"
                                       item-text="name"
                                       @change="changeCoast"
                                       >
                                    </v-select> 
                                    <v-text-field
                                        v-model="form.comment"
                                        label="Комментарий"
                                    ></v-text-field> 
                                    <div class="body-1">
                                        Итогвая цена: {{ form.Cost }} рублей
                                    </div>
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
            <v-card-title class="text-h6">Вы уверены, что хотите удалить данный заказ?</v-card-title>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue darken-1" text @click="this.dialogDelete = false, this.deleteItemId = null">Отмена</v-btn>
              <v-btn color="blue darken-1" text @click="deleteItemConfirm">Подтвердить</v-btn>
              <v-spacer></v-spacer>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <v-dialog v-model="newClientForm" max-width="500px" :persistent="true">
            <v-card class="pb-4">
                <v-toolbar>
                    <v-toolbar-title>
                        <h4>Добавить клиента</h4>
                        <v-spacer></v-spacer>
                    </v-toolbar-title>
                </v-toolbar>
                <v-card-text>
                    <v-container tag="section" py-3 fluid grid-list-md>
                        <v-form :lazy-validation="true">
                            <v-text-field
                                v-model="clientForm.firstName"
                                label="Имя"
                            ></v-text-field>
                            <v-text-field
                                v-model="clientForm.lastName"
                                label="Фамилия"
                            ></v-text-field>
                            <v-text-field
                                v-model="clientForm.email"
                                label="Email"
                            ></v-text-field>
                            <v-text-field
                                v-model="clientForm.phoneNumber"
                                label="Номер телефона"
                                type="tel"
                                prefix="+"
                                mask="7##########"
                            ></v-text-field>
                            <v-banner
                            color="primary"
                            outlined
                            >
                            Дата рождения
                            </v-banner>
                            <v-date-picker
                                v-model="clientForm.birthday"
                                color="primary"
                            ></v-date-picker>
                        </v-form>
                    </v-container>
                </v-card-text>
                <v-card-actions>
                    <v-btn rounded @click="newClientForm = false" color="secondary">Отмена</v-btn>
                    <v-btn rounded color="primary" @click="addClient">Добавить</v-btn>
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
        Places: [],
        Orders: [],
        Services: [],
        Clients: [],
        selectedPlace: [],
        ItemDialog: false,
        dialogDelete: false,
        newClientForm: false,
        loading: true,
        deleteItemId: null,
        addForm: true,
        form: {
            id: null,
            clientId: null,
            services: [],
            Cost: 0,
            placeId: null,
        },
        clientForm: {
            firstName : '',
            lastName : '',
            email : '',
            phoneNumber : '',
            birthday: null
        }
    }
  },
  computed: {
      headers () {
        return [
          {
            text: 'Клиент',
            align: 'start',
            sortable: false,
            value: 'clientName',
          },
          {
            text: 'Комментарий',
            sortable: false,
            value: 'comment',
          },
          {
            text: 'Стоимость (руб)',
            sortable: false,
            value: 'cost',
          },
          {
            text: 'Статус',
            sortable: false,
            value: 'status',
          },
       { text: 'Действия', value: 'actions', sortable: false  },
        ]
      },
    },
  methods: {
    addItem(){
        debugger
        this.form.placeId = this.selectedPlace
        axios.post('http://localhost:8000/api/Orders/CreateOrder',this.form)
        .then(res => {
            this.form.id = null,
            this.form.clientId = null,
            this.form.services = [],
            this.form.Cost = 0,
            this.form.placeId = 0;
            this.ItemDialog = false,
            console.log(res.body)
            this.getItems();
        })
    },
    addClient(){
        axios.post('http://localhost:8000/api/Client/CreateClient',this.clientForm)
        .then(res => {
            this.newClientForm = false,
            this.clientForm.firstName = '',
            this.clientForm.lastName = '',
            this.clientForm.email = '',
            this.clientForm.phoneNumber = '',
            this.clientForm.birthday = null
            console.log(res.body)
            this.getClients();
        })
    },
    editItem(item){
        console.log(item)
        this.form.name = item.name
        this.form.unitName = item.unitName
        this.form.price= item.price
        this.form.id = item.id
        this.ItemDialog = true
    },
    deleteItem(item){
        this.deleteItemId = item.id
        this.dialogDelete = true
    },
    deleteItemConfirm(){
        axios.delete(`http://localhost:8000/api/Spending?Id=${this.deleteItemId}`)
        .then(res => {
            console.log(res)
            this.deleteItemId = null
            this.dialogDelete = false
            this.getItems()
        })
    },
    getItems(){
      this.loading = true
      let body = {
        placeId: this.selectedPlace
      }
      axios.get(`http://localhost:8000/api/Orders?placeId=${this.selectedPlace}`, body)
      .then(res => {
        this.Orders = res.data.result
        this.loading = false
      })
    },
    getPlaces(){
        axios.get(`http://localhost:8000/api/Places`)
        .then(res => {
            this.Places = res.data
        })
    },
    getServices(){
        axios.get(`http://localhost:8000/api/Services`)
        .then(res => {
        this.Services = res.data
        })
    },
    getClients(){
        axios.get(`http://localhost:8000/api/Client`)
        .then(res => {
        this.Clients = res.data.result
        console.log(this.Clients)
        })
    },
    changeCoast(){
        console.log(this.form.services)
        this.form.Cost = 0
        let selectedServices = []
        this.form.services.forEach(element => {
            let serv = this.Services.find(s => s.id == element)
            selectedServices.push(serv)
            this.form.Cost = serv.price + this.form.Cost
        });
        
        debugger
    }
  },
  created() {
    this.getItems();
    this.getPlaces();
    this.getServices();
    this.getClients();
  }
  }
  
  </script>