<template>
    <section>
        <v-toolbar>
            <v-toolbar-title>
                <h2>Настройки услуг</h2>
            </v-toolbar-title>
            <v-spacer></v-spacer>
        </v-toolbar>
        <v-card-text>
            <v-layout>
                <v-flex lg12 hidden-sm-and-down>
                    <v-data-table
                    :headers="headers"
                    :items="ServicesItems"
                    loading
                    loading-text="Загрузка данных"
                    >
                        <template v-slot:top>
                            <v-toolbar flat>
                                <v-btn
                                    color="primary"
                                    dark
                                    class="mb-2"
                                    >
                                    Добавить услугу
                                    </v-btn>
                            </v-toolbar>
                        </template>
                    </v-data-table>
                </v-flex>
            </v-layout>
        </v-card-text>
    </section>
    
</template>

<script>

export default {
  data(){
    return{
        ServicesItems: [],
    }
  },
  computed: {
      headers () {
        return [
          {
            text: 'Dessert (100g serving)',
            align: 'start',
            sortable: false,
            value: 'name',
          },
          {
            text: 'Calories',
            value: 'calories',
            filter: value => {
              if (!this.calories) return true

              return value < parseInt(this.calories)
            },
          },
          { text: 'Fat (g)', value: 'fat' },
          { text: 'Carbs (g)', value: 'carbs' },
          { text: 'Protein (g)', value: 'protein' },
          { text: 'Iron (%)', value: 'iron' },
        ]
      },
    },
  methods: {
    
  },
  created() {
  }
}

</script>