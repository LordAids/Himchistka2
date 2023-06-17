<template>
    <section class="pa-5">
        <v-toolbar>
            <v-toolbar-title>
                <h4>Настройки прачечных</h4>
            </v-toolbar-title>
            <v-spacer></v-spacer>
        </v-toolbar>
        <v-card-text>
            <v-layout>
                <v-flex lg12 hidden-sm-and-down>
                    <v-data-table
                    :headers="headers"
                    :items="PlaceItems"
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
                                    Добавить предприятие
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
                        <h4>{{addForm ? 'Добавить предприятие' : 'Редактировать предприятие'}}</h4>
                        <v-spacer></v-spacer>
                    </v-toolbar-title>
                </v-toolbar>
                <v-card-text>
                    <v-container tag="section" py-3 fluid grid-list-md>
                        <v-form :lazy-validation="true">
                                <v-text-field
                                        v-model="form.name"
                                        label="Название предприятия"
                                    ></v-text-field>   
                                    <v-text-field
                                        v-model="form.adress"
                                        label="Адрес"
                                    ></v-text-field>   
                                    <v-text-field
                                        v-model="form.mounthPrice"
                                        label="Точка окупаемости"
                                        type="number"
                                    ></v-text-field>   
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
            <v-card-title class="text-h6">Вы уверены, что хотите удалить данное предприятие?</v-card-title>
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
        ItemDialog: false,
        dialogDelete: false,
        loading: true,
        deleteItemId: null,
        addForm: true,
        form: {
            id: null,
            name: '',
            mounthPrice: null,
            adress:''
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
            value: 'name',
          },
          {
            text: 'Адрес',
            value: 'adress',
            filter: value => {
              if (!this.calories) return true

              return value < parseInt(this.calories)
            },
          },
       { text: 'Действия', value: 'actions', sortable: false  },
        ]
      },
    },
  methods: {
    addItem(){
        axios.post('http://localhost:8000/api/Places/CreatePlace',this.form,)
        .then(res => {
            this.ItemDialog = false,
            this.form.name = '',
            this.form.adress = ''
            this.form.id = null
            this.form.mounthPrice = 0;
            console.log(res.body)
            this.getItems();
        })
    },
    editItem(item){
        console.log(item)
        this.form.name = item.name
        this.form.adress = item.adress
        this.form.id = item.id
        this.ItemDialog = true
    },
    deleteItem(item){
        this.deleteItemId = item.id
        this.dialogDelete = true
    },
    deleteItemConfirm(){
        axios.delete(`http://localhost:8000/api/Places?Id=${this.deleteItemId}`)
        .then(res => {
            console.log(res)
            this.deleteItemId = null
            this.dialogDelete = false
            this.getItems()
        })
    },
    getItems(){
      this.loading = true
      axios.get(`http://localhost:8000/api/Places`)
      .then(res => {
        this.PlaceItems = res.data
        console.log(this.PlaceItems)
        this.loading = false
      })
    }
  },
  created() {
    this.getItems();
  }
}

</script>