import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-charts',
  templateUrl: './charts.component.html',
  styleUrls: ['./charts.component.css']
})
export class ChartsComponent implements OnInit {

  public measurements: Measurement[];
  public chartType: string;
  private selectedIds: number[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute) {
    this.route.params.subscribe(params => {
      this.selectedIds = params['selectedIds'].split(',').map(Number);

      if (this.selectedIds && this.selectedIds.length > 0) {
        http.get<Measurement[]>(baseUrl + 'api/Benchmarks/' + this.selectedIds[0]).subscribe(result => {
          this.measurements = result;
          this.chart1.data = this.measurements.map(m => [new Date(m.date), m.value]);
        }, error => console.error(error)); }
      });
    }

    chart1 = {
        title: 'FirstSample',
        type: 'LineChart',
        data: [],
        cols: [
            { type: 'date', id: 'date' },
            { type: 'number', id: 'value' },
        ],
        options: {
            colors: ['#0000ff' ],
            animation: {
                duration: 250,
                easing: 'ease-in-out',
                startup: true
            },
            pointSize: 3,
        },
        height: 1000,
    };

    ngOnInit() {
    }

    changeType(newType) {
      this.chart1.type = newType;
    }
}

interface Measurement {
  benchmarkID: number;
  date: Date;
  value: number;
  metricName: string;
  unit: string;
}