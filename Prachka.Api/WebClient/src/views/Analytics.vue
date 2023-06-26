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
                <v-card class="order-1 pa-2 outlined align-start align-self-start" align-center width="600px">
                <p class="body-1">Фильтрация</p>
                        <!-- <v-select
                            v-model="chartType"
                            :items="chartsType"
                            label="Тип графика"
                            item-value="value"
                            item-text="name">
                        </v-select> -->

                        <v-select
                            v-model="chosenPlace"
                            :items="Places"
                            label="Предприятие"
                            item-value="id"
                            multiple
                            item-text="name">
                        </v-select>

                        <v-text-field
                          v-model="dateRangeText"
                          label="Период"
                          prepend-icon="mdi-calendar"
                          readonly
                        ></v-text-field>
                        <v-date-picker
                          v-model="dates"
                          range
                          locale="ru-ru"
                          no-title
                          :first-day-of-week="1"
                        ></v-date-picker>
       
                        <v-select 
                            v-model="chosenServices"
                            :items="Services"
                            multiple
                            label="Услуги"
                            item-value="id"
                            item-text="name"
                            >
                        </v-select>
                        <v-select 
                            v-model="chosenSpendings"
                            :items="Spendings"
                            label="Расходы"
                            multiple
                            item-value="id"
                            item-text="name">
                        </v-select> 
                        <v-btn
                        color="primary"
                        @click="changeChart"
                        >
                        Сформировать аналитику
                        </v-btn> 
                </v-card>
                <v-card class="order-2 pa-2 outlined tile" width="600px">
                        <v-layout>
                            <v-flex lg12 hidden-sm-and-down>
                              <div>
                                  Суммарное количество заказов: {{ totalOrders }}
                                </div>
                                <div>
                                  Суммарная прибыль: {{ totalProfit.toFixed(2) }} рублей
                                </div>
                                <div>
                                  Суммарные затраты: {{  totalSpending.toFixed(2) }} рублей
                                </div>
                              <Bar 
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
                                <Pie
                                    :chart-options="chartOptions"
                                    :chart-data="pieData"
                                    :chart-id="chartId"
                                    :dataset-id-key="datasetIdKey"
                                    :plugins="plugins"
                                    :css-classes="cssClasses"
                                    :styles="styles"
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
import { Bar } from 'vue-chartjs/legacy'
import { Line as LineChartGenerator } from 'vue-chartjs/legacy'
import axios from 'axios'

import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  BarElement,
  ArcElement,
  LineElement,
  LinearScale,
  CategoryScale,
  PointElement
} from 'chart.js'

ChartJS.register(Title, Tooltip, Legend, ArcElement, BarElement, CategoryScale, LineElement, LinearScale, PointElement)

export default {
  name: 'PieChart',
  components: {
    Pie,
    Bar,
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
    dateRangeText () {
      if(this.dates != null){

        return this.dates.join(' ~ ')
      }
      },
  },
  data() {
    return {
        chartData: {
            labels: [],
            datasets: []
        },
      chartOptions: {
        responsive: true,
        maintainAspectRatio: false
      },
      pieData: {
        labels: [
          'Понедельник',
          'Вторник',
          'Среда',
          'Четверг',
          'Пятница',
          'Суббота',
          'Воскресенье'
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
    Places: [],
    chosenPlace: [],
    Services: [],
    chosenServices: [],
    Spendings: [],
    chosenSpendings: [],
    Orders: [],
    chartType: 1,
    dates: null,
    totalOrders: 0,
    totalProfit: 0,
    totalSpending: 0


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
    getOrders(){
        let body = {
        placeId: this.chosenPlace
      }
        axios.get(`http://localhost:8000/api/Orders`)
        .then(res => {
            this.Orders = res.data.result
            this.loading = false
        })
    },
    changeChart(){
        let body = {
          Services: this.chosenServices,
          Places: this.chosenPlace,
          Spendings: this.chosenSpendings,
          Dates: this.dates
        }
          axios.post(`http://localhost:8000/api/Analityc/Chart`, body)
            .then(res => {
              console.log(res)
              this.chartData.labels = res.data.labels,
              this.chartData.datasets = []
              let spendings = {
                label: 'Расходы',
                        backgroundColor: '#F44336',
                        data: res.data.spendings
              }
              let profits = {
                label: 'Доходы',
                        backgroundColor: '#4CAF50',
                        data: res.data.profits
              }
              this.chartData.datasets.push(spendings)
              this.chartData.datasets.push(profits)

              this.totalProfit = res.data.totalProfit
              this.totalOrders = res.data.totalOrders
              this.totalSpending = res.data.totalSpend
            })
          axios.post(`http://localhost:8000/api/Analityc/Pie`, body)
            .then(res => {
              console.log(res)
              this.pieData.labels = res.data.labels,
              this.pieData.datasets = []
              let dataset = {
                        backgroundColor: res.data.colors,
                        data: res.data.value }
              this.pieData.datasets.push(dataset)
            })
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
