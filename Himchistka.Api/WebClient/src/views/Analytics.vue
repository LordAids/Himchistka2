<template  text-xs-center>
     <section  >
        <v-toolbar>
            <v-toolbar-title outlined>
                <h4>Аналитика</h4>
            </v-toolbar-title>
            <v-spacer></v-spacer>
        </v-toolbar>
            <v-card class="d-flex flex-wrap-reverse pa-5" flat tile xs12>
            <v-layout class="d-flex justify-center" align-center text-xs-center>
                <v-card class="order-1 pa-2 outlined tile" width="600px">
                <p class="body-1">Фильтрация</p>
                        <v-select
                            v-model="chartType"
                            :items="chartsType"
                            label="Тип графика"
                            item-value="value"
                            item-text="name">
                        </v-select>

                        <v-select
                            v-model="chosenPlace"
                            @change="changeChart"
                            :items="Places"
                            label="Предприятие"
                            item-value="id"
                            item-text="name">
                        </v-select>
                        <v-select
                            v-model="chosenServices"
                            @change="changeChart"
                            :items="Services"
                            multiple
                            label="Услуги"
                            item-value="id"
                            item-text="name">
                        </v-select>
                        <v-select
                            v-model="chosenSpendings"
                            @change="changeChart"
                            :items="Spendings"
                            label="Расходы"
                            multiple
                            item-value="id"
                            item-text="name">
                        </v-select>  
                </v-card>
                <v-card class="order-2 pa-2 outlined tile" width="600px">
                        <v-layout>
                            <v-flex lg12 hidden-sm-and-down>
                                <Pie v-if="chartType == 2"
                                    :chart-options="chartOptions"
                                    :chart-data="chartData"
                                    :chart-id="chartId"
                                    :dataset-id-key="datasetIdKey"
                                    :plugins="plugins"
                                    :css-classes="cssClasses"
                                    :styles="styles"
                                    :width="width"
                                    :height="height"
                                />
                                <LineChartGenerator v-if="chartType == 1"
                                    :chart-options="chartOptions"
                                    :chart-data="chartData"
                                    :chart-id="chartId"
                                    :dataset-id-key="datasetIdKey"
                                    :plugins="plugins"
                                    :css-classes="cssClasses"
                                    :styles="styles"
                                    :width="width"
                                    :height="height"
                                />
                            </v-flex>
                        </v-layout>
                </v-card>
            </v-layout>
            
        </v-card>
        
    </section>
</template>

<script>
import { Pie } from 'vue-chartjs/legacy'
import { Line as LineChartGenerator } from 'vue-chartjs/legacy'
import axios from 'axios'

import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  ArcElement,
  LineElement,
  LinearScale,
  CategoryScale,
  PointElement
} from 'chart.js'

ChartJS.register(Title, Tooltip, Legend, ArcElement, CategoryScale, LineElement, LinearScale, PointElement)

export default {
  name: 'PieChart',
  components: {
    Pie,
    LineChartGenerator
  },
  props: {
    chartId: {
      type: String,
      default: 'line-chart'
    },
    datasetIdKey: {
      type: String,
      default: 'label'
    },
    width: {
      type: Number,
      default: 400
    },
    height: {
      type: Number,
      default: 400
    },
    cssClasses: {
      default: '',
      type: String
    },
    styles: {
      type: Object,
      default: () => {}
    },
    plugins: {
      type: Array,
      default: () => []
    }
  },
  computed: {
    chartsType () {
      return [
        {
          name: 'График',
          value: 1,
        },
        {
          name: 'Круговая диаграмма',
          value: 2,
        },
      ]
    },
  },
  data() {
    return {
        chartData: {
        labels: [
          'January',
          'February',
          'March',
          'April',
          'May',
          'June',
          'July'
        ],
        datasets: [
            {
                label: 'Расходы',
                backgroundColor: '#F44336',
                data: [40, 39, 10, 40, 39, 80, 40]
            },
            {
                label: 'Доходы',
                backgroundColor: '#4CAF50',
                data: [50, 49, 20, 50, 49, 90, 50]
            }
        ]
      },
      chartOptions: {
        responsive: true,
        maintainAspectRatio: false
      },
      pieData: {
        labels: [
          'January',
          'February',
          'March',
          'April',
          'May',
          'June',
          'July'
        ],
        datasets: [
            {
                label: 'Расходы',
                backgroundColor: '#f87979',
                data: [40, 39, 10, 40, 39, 80, 40]
            },
            {
                label: 'Доходы',
                backgroundColor: '#f87979',
                data: [50, 49, 20, 50, 49, 90, 50]
            }
        ]
      },
      chartOptions: {
        responsive: true,
        maintainAspectRatio: false
      },
    Places: [],
    chosenPlace: 1,
    Services: [],
    chosenServices: [],
    Spendings: [],
    chosenSpendings: [],
    Orders: [],
    chartType: 1,

    }
    
  },
  methods: {
    getItems(){
        axios.get(`http://localhost:8000/api/Spending`)
        .then(res => {
            this.Spendings = res.data
        })
    },
    getPlaces(){
        let firstPlace = {
            name: "Все",
            id: 1
        }
        axios.get(`http://localhost:8000/api/Places`)
        .then(res => {

            res.data.push(firstPlace)
            this.Places = res.data
        })
    },
    getServices(){
        axios.get(`http://localhost:8000/api/Services`)
        .then(res => {
        this.Services = res.data
        })
    },
    getOrders(){
        axios.get(`http://localhost:8000/api/Orders`)
        .then(res => {
            this.Orders = res.data.result
            this.loading = false
        })
    },
    changeChart(){
        debugger
    }
  },
  created(){
    this.getItems();
    this.getPlaces();
    this.getServices();
    this.getOrders();
  }
}
</script>
