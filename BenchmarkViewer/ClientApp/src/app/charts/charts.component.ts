import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-charts',
  templateUrl: './charts.component.html',
  styleUrls: ['./charts.component.css']
})

export class ChartsComponent implements OnInit {

  public chartType: string;
  private selectedIds: number[];
  public benchmarkData: BenchmarkData;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute) {
    this.route.params.subscribe(params => {
      this.selectedIds = params['selectedIds'].split(',').map(Number);

      if (this.selectedIds && this.selectedIds.length > 0) {
        http.get<BenchmarkData>(baseUrl + 'api/Benchmarks/' + this.selectedIds[0]).subscribe(result => {
          this.benchmarkData = result;
          this.chart.data = result.measurements.map(m => [new Date(m.date), m.value]);
          this.chart.title = result.benchmarkName;
          this.chart.columnNames[1] = result.benchmarkName;
        }, error => console.error(error));
      }
    });
  }

  chart = {
    title: 'FirstSample',
    type: 'ScatterChart',
    data: [],
    cols: [
      { type: 'date', id: 'date' },
      { type: 'number', id: 'value' },
    ],
    columnNames: ['a', 'b'],
    options: {
      legend:
      {
        position: 'right',
      },
      displayAnnotations: true,
      colors: ['#0000ff'],
      animation: {
        duration: 150,
        easing: 'ease-in-out',
        startup: true
      },
      pointSize: 3,
      pointShape: 'circle',
      hAxis: {
        title: 'Date',
      },
      vAxis: {
        title: 'Time'
      },
    }
  };

  ngOnInit() {
  }

  changeType(newType) {
    this.chart.type = newType;
  }
}

interface BenchmarkData {
  benchmarkName: string;
  benchmarkId: number;
  measurements: Measurement[];
}

interface Measurement {
  benchmarkID: number;
  date: Date;
  value: number;
  metricName: string;
  unit: string;
}
