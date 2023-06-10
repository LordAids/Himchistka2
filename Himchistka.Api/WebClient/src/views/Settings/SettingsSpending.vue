<template>
    <section>
        
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
                    loading
                    loading-text="Загрузка данных"
                    >
                    <template v-slot:top>
                            <v-toolbar flat>
                                <v-btn
                                    color="primary"
                                    dark
                                    class="mb-2"
                                    @click="ItemDialog = true"
                                    >
                                    Добавить предприятие
                                    </v-btn>
                            </v-toolbar>
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
                    
                </v-toolbar>
            </v-card>
        </v-dialog>
    </section>
</template>

<script>

export default {
  data(){
    return{
        PlaceItems: [],
        ItemDialog: false,
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