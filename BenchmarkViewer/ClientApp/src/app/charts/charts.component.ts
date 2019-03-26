import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { NgbDate, NgbCalendar } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-charts',
  templateUrl: './charts.component.html',
  styleUrls: ['./charts.component.css']
})

export class ChartsComponent implements OnInit {

  private chartType: string;
  private selectedIds: number[];
  private benchmarkData: BenchmarkData;
  private fromTime: NgbDate;
  private toTime: NgbDate;

  constructor(calendar: NgbCalendar, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private route: ActivatedRoute) {
    this.fromTime = calendar.getNext(calendar.getToday(), 'd', -30);
    this.toTime = calendar.getToday();

    this.route.params.subscribe(params => {
      this.selectedIds = params['selectedIds'].split(',').map(Number);

      this.refreshData();
    });
  }

  chart = {
    title: 'Benchmark',
    type: 'ScatterChart',
    data: [],
    cols: [
      { type: 'date', id: 'date' },
      { type: 'number', id: 'value' },
    ],
    columnNames: ['Date', 'Value'],
    options: {
      legend: 'none',
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
        title: 'Value'
      },
    }
  };

  ngOnInit() {
  }

  changeType(newType) {
    this.chart.type = newType;
  }

  onDateSelection(event) {
    this.refreshData();
  }

  onWindowResize(event) {
    // the chart control does not support dynamic re-sizing
    // so when the window size changes, we reassign the data property
    // which triggers a redraw
    // idea from https://github.com/FERNman/angular-google-charts/issues/39#issuecomment-468502116
    this.chart.data = Object.assign([], this.chart.data);
  }

  refreshData() {
    if (this.selectedIds && this.selectedIds.length > 0) {
      // https://stackoverflow.com/questions/2552483/why-does-the-month-argument-range-from-0-to-11-in-javascripts-date-constructor
      const dateFrom = new Date(this.fromTime.year, this.fromTime.month - 1, this.fromTime.day);
      const dateTo = new Date(this.toTime.year, this.toTime.month - 1, this.toTime.day);
      const url = this.baseUrl + 'api/Benchmarks/' + this.selectedIds[0] + '/' + dateFrom.toUTCString() + '/' + dateTo.toUTCString();

      this.http.get<BenchmarkData>(url)
        .subscribe(result => {
          this.benchmarkData = result;
          this.chart.data = result.measurements.map(m => [new Date(m.date), m.value]);
          this.chart.title = result.benchmarkName;
          this.chart.columnNames[1] = result.benchmarkName;
        }, error => console.error(error));
    }
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
