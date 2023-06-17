<template>
  <section class="pa-5">
      <v-toolbar>
          <v-toolbar-title>
              <h4>Настройки услуг</h4>
          </v-toolbar-title>
          <v-spacer></v-spacer>
      </v-toolbar>
      <v-card-text>
          <v-layout>
              <v-flex lg12 hidden-sm-and-down>
                  <v-data-table
                  :headers="headers"
                  :items="Services"
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
                                      v-model="form.name"
                                      label="Название услуги"
                                  ></v-text-field>   
                                  <v-textarea
                                      v-model="form.description"
                                      label="Описание"
                                  ></v-textarea>
                                  <v-text-field
                                      v-model="form.price"
                                      label="Стоимость"
                                      type="number"

                                  ></v-text-field>

                                  <p>Цвет услуги</p>
                                  <v-color-picker
                                    v-model="form.color"
                                    dot-size="11"
                                    hide-canvas
                                    hide-inputs
                                    hide-mode-switch
                                    hide-sliders
                                    mode="rgba"
                                    show-swatches
                                    swatches-max-height="100"
                                  ></v-color-picker>

                                  <v-select
                                      v-model="form.places"
                                      :items="PlaceItems"
                                      label="Доступные прачечные"
                                      multiple
                                      item-value="id"
                                      item-text="name"
                                      >
                                      
                                  </v-select>
                                  <section >
                                    <v-btn
                                    color="primary"
                                    @click = "addSpend"
                                    >
                                      Добавить расходы
                                    </v-btn>
                                    <v-card class="pb-4" v-for="spend in form.spendings" :key="spend.name">
                                       <v-select
                                       @change = changeCost
                                       v-model="spend.id"
                                       :items="spendings"
                                       label="Расходы"
                                       item-value="id"
                                       :item-text= spend.name
                                       >
                                       <template slot="item" slot-scope="data">
                                        {{ data.item.name }} ({{ data.item.unitName }})
                                      </template>
                                      <template slot="selection" slot-scope="data">
                                        {{ data.item.name }} ({{ data.item.unitName }})
                                      </template>
                                       </v-select>
                                       <v-text-field
                                        @change = changeCost
                                          v-model="spend.count"
                                          label="Количество"
                                          type="number"

                                      ></v-text-field>
                                    </v-card>
                                  </section>
                                  <div class="body-1">
                                        Себестоимость: {{ form.cost }} рублей
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
          <v-card-title class="text-h6">Вы уверены, что хотите удалить данную услугу?</v-card-title>
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
      spendings: [],
      Services: [],
      ItemDialog: false,
      dialogDelete: false,
      loading: true,
      deleteItemId: null,
      addForm: true,
      form: {
          id: null,
          name: '',
          description: '',
          price: '',
          places: [],
          spendingCount: 0,
          spendings: [],
          color: '',
          cost: 0
      }
  }
},
computed: {
    headers () {
      return [
        {
          text: 'Название',
          align: 'start',
          sortable: false,
          value: 'name',
        },
        {
          text: 'Описание',
          sortable: false,
          value: 'description',
        },
        {
          text: 'Стоимость',
          sortable: false,
          value: 'price',
        },
     { text: 'Действия', value: 'actions', sortable: false  },
      ]
    },
  },
methods: {
  addItem(){
    console.log(this.form)
    debugger
      axios.post('http://localhost:8000/api/Services/CreateServices',this.form)
      .then(res => {
          this.ItemDialog = false,
          this.form.name = '',
          this.form.description = ''
          this.form.price = ''
          this.form.places = ''
          this.form.spendings = ''
          this.form.id = null
          this.form.color = ''
          this.form.cost = 0
          console.log(res.body)
          this.getItems();
      })
  },
  addSpend(){
    let spend = {
      name: "имя",
      cost: 15
    }
    this.form.spendings.push(spend)
  },
  editItem(item){
      debugger
      this.form.name = item.name
      this.form.description = item.description
      this.form.phoneNumber= item.phoneNumber
      this.form.price= item.price
      this.form.places= item.places
      this.form.id = item.id
      this.form.color = item.color
      this.changeCost()
      this.ItemDialog = true
  },
  deleteItem(item){
      this.deleteItemId = item.id
      this.dialogDelete = true
  },
  deleteItemConfirm(){
      axios.delete(`http://localhost:8000/api/Services?Id=${this.deleteItemId}`)
      .then(res => {
          console.log(res)
          this.deleteItemId = null
          this.dialogDelete = false
          this.getItems()
      })
  },
  getItems(){
    this.loading = true
    axios.get(`http://localhost:8000/api/Services`)
    .then(res => {
      console.log(res)
      this.Services = res.data
      this.loading = false
    })
  },
  getPlaces(){
      axios.get(`http://localhost:8000/api/Places`)
      .then(res => {
      this.PlaceItems = res.data
    })
  },
  getSpendings(){
      axios.get(`http://localhost:8000/api/Spending`)
      .then(res => {
      this.spendings = res.data
    })
  },
  changeCost(){
    this.form.cost = 0
    debugger
    this.form.spendings.forEach(element => {
      let spend = this.spendings.find(s => s.id == element.id)
      this.form.cost = this.form.cost + (spend.price * element.count)
     });

    if(isNaN(this.form.cost))
        this.form.cost = 0
  }
},
created() {
  this.getItems();
  this.getPlaces();
  this.getSpendings();
}
}

</script>