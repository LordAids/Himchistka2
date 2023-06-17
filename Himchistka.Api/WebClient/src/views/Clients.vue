<template>
    <section class="pa-5">
        <v-toolbar>
            <v-toolbar-title>
                <h4>Клиенты</h4>
            </v-toolbar-title>
            <v-spacer></v-spacer>
        </v-toolbar>
        <v-card-text>
            <v-layout>
                <v-flex lg12 hidden-sm-and-down>
                    <v-data-table
                    :headers="headers"
                    :items="Clients"
                    :loading="loading"
                    loading-text="Загрузка данных"
                    :items-per-page="15"
                    class="elevation-1"
                    @click:row="openClientCards"
                    >
                    <template v-slot:top>
                            <v-toolbar flat max-width="600px">
                                <v-flex  class="d-flex pa-2" >
                                    <v-btn xs3
                                        color="primary"
                                        dark
                                        class="mb-2  ma-2"
                                        @click="newClientForm = true, addForm = true"
                                        >
                                        Добавить
                                    </v-btn>
                                </v-flex>
                            </v-toolbar>
                        </template>
                        <template v-slot:item.status="{item}" max-width="200">
                            <v-select
                                v-model="item.status"
                                :items="statuses"
                                label="Статус заказа"
                                item-value="value"
                                item-text="text"
                                @change="statusChange(item)"
                            >
                            </v-select>
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
                                locale="ru-ru"
                                :first-day-of-week="0"
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
        <v-dialog v-model="clientDialog" max-width="700px" :persistent="true">
            <v-toolbar dark color="primary">
                    <h4>
                        <span> {{ dialogForm.firstName + " " + dialogForm.lastName }}</span>
                    </h4>
                    <v-spacer></v-spacer>
                    <v-btn icon dark @click="clientDialog = false">
                        <v-icon>mdi-close</v-icon>
                    </v-btn>
                </v-toolbar>
            <v-card>
                <v-card-text>
                    <v-container grid-list-md fluid>
                        <v-layout class="align-center">
                            <v-flex xs4 subheading>
                                <div class="text-bold">
                                    Имя:
                                </div>
                            </v-flex>
                            <v-flex xs8 text-xs-right>
                                <div class="text--secondary">
                                    {{ dialogForm.firstName }}
                                </div>
                            </v-flex>
                        </v-layout>
                        <v-layout class="align-center">
                            <v-flex xs4 subheading>
                                <div class="text-bold">
                                    Фамилия:
                                </div>
                            </v-flex>
                            <v-flex xs8 text-xs-right>
                                <div class="text--secondary">
                                    {{ dialogForm.lastName }}
                                </div>
                            </v-flex>
                        </v-layout>
                        <v-layout class="align-center">
                            <v-flex xs4 subheading>
                                <div class="text-bold">
                                    Почта:
                                </div>
                            </v-flex>
                            <v-flex xs8 text-xs-right>
                                <div class="text--secondary">
                                    {{ dialogForm.email }}
                                </div>
                            </v-flex>
                        </v-layout>
                        <v-layout class="align-center">
                            <v-flex xs4 subheading>
                                <div class="text-bold">
                                    Номер телефона:
                                </div>
                            </v-flex>
                            <v-flex xs8 text-xs-right>
                                <div class="text--secondary">
                                    {{ dialogForm.phoneNumber }}
                                </div>
                            </v-flex>
                        </v-layout>
                        <h4>Заказы:</h4>
                        <v-layout v-for="(order,i) in dialogForm.orders">
                            <v-flex xs4 subheading>
                                <div class="text-bold">
                                    №{{ order.number }}
                                </div>
                            </v-flex>
                            <v-flex xs4 text-xs-right>
                                <div class="text--secondary">
                                    {{ order.cost }} рублей
                                </div>
                            </v-flex>
                            <v-flex xs4 text-xs-right>
                                <div class="text--secondary">
                                    {{ getStatus(order.status) }}
                                </div>
                            </v-flex>
                        </v-layout>
                    </v-container>
                </v-card-text>
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
        clientDialog: false,
        loading: false,
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
        },
        dialogForm: {
            firstName : '',
            lastName: '',
            email: null,
            phoneNumber: null,
            number: null,
            status: '',
            orders: []
        }
    }
  },
  computed: {
      headers () {
        return [
          {
            text: 'Имя',
            align: 'start',
            sortable: false,
            value: 'firstName',
          },
          {
            text: 'Фамилия',
            sortable: false,
            value: 'lastName',
          },
          {
            text: 'Электронная почта',
            sortable: false,
            value: 'email',
          },
          {
            text: 'Номер телефона',
            sortable: false,
            value: 'phoneNumber',
          },
       { text: 'Действия', value: 'actions', sortable: false  },
        ]
      },
      statuses (){
        return[
            {
                text: 'Новый заказ',
                value: 1
            },
            {
                text: 'В работе',
                value: 2
            },
            {
                text: 'Ожидание заказчика',
                value: 3
            },
            {
                text: 'Выполнено',
                value: 4
            }
        ]
      }
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
      let body = {
        placeId: this.selectedPlace
      }
      axios.post(`http://localhost:8000/api/Orders?placeId=${this.selectedPlace}`, body)
      .then(res => {
        this.Orders = res.data.result
        this.loading = false
      })
    },
    getPlaces(){
        axios.get(`http://localhost:8000/api/Places`)
        .then(res => {
            this.Places = res.data
            if(this.selectedPlace == null){
                this.selectedPlace = this.Places[0].id
                this.getItems()
            }
        })
    },
    getServices(){
        axios.get(`http://localhost:8000/api/Services`)
        .then(res => {
        this.Services = res.data
        })
    },
    getClientOrder(id){
        axios.get(`http://localhost:8000/api/Orders/ClientOrder?clientId=${id}`)
        .then(res => {
        this.dialogForm.orders = res.data
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
    },
    statusChange(item){
        debugger
        axios.post(`http://localhost:8000/api/Orders/ChangeStatus?orderId=${item.id}&statusId=${item.status}`)
        .then(res => {
            this.getItems();
        })
    },
    openClientCards(item){
        this.dialogForm.id = item.id
        this.dialogForm.firstName = item.firstName
        this.dialogForm.lastName = item.lastName
        this.dialogForm.email = item.email
        this.dialogForm.phoneNumber = item.phoneNumber
        this.dialogForm.number = item.number
        this.getClientOrder(this.dialogForm.id)
        this.clientDialog = true
    },
    getStatus(id){
        debugger
        let status = this.statuses.filter(s => s.value == id)
        return status[0].text
    }
    
  },
  created() {
    this.getPlaces();
    this.getServices();
    this.getClients();
    this.getItems();
    this.getClientOrder();
  }
  }
  
  </script>