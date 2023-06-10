<template>
    <v-card>
        <v-tabs  centered v-model="selectedTab">
            <v-tab :key="1">Предприятия</v-tab>
            <v-tab :key="2">Сотрудники</v-tab>
            <v-tab :key="3">Услуги</v-tab>
            <v-tab :key="4">Расходы</v-tab>
        </v-tabs>

        <v-tabs-items v-model="selectedTab">
            <v-tab-item :key="1" ><SettingsPlace/></v-tab-item>
            <v-tab-item :key="2" ><SettingsEmployee/></v-tab-item>
            <v-tab-item :key="3" ><SettingsServices/></v-tab-item>
            <v-tab-item :key="4" ><SettingsServices/></v-tab-item>
        </v-tabs-items>
        
    </v-card>

</template>

<script>
import SettingsEmployee from '@/views/Settings/SettingsEmployee.vue'
import SettingsPlace from '@/views/Settings/SettingsPlace.vue'
import SettingsServices from '@/views/Settings/SettingsServices.vue'
import SettingsSpending from '@/views/Settings/SettingsSpending.vue'

export default {
  components: {
    SettingsEmployee,
    SettingsPlace,
    SettingsServices,
    SettingsSpending
  },
  
  data(){
    return{
      selectedTab: null,

    }
  },
  methods: {
    
  },
  created() {
  }
}

</script>