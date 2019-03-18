import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-charts',
  templateUrl: './charts.component.html',
  styleUrls: ['./charts.component.css']
})
export class ChartsComponent implements OnInit {

  chart1 = {
      title: 'FirstSample',
    type: 'LineChart',
    data: [
      ['01/01/2019', 12.94, 15],
      ['02/01/2019', 10.49, 13],
      ['03/01/2019', 19.30, 13],
      ['04/01/2019', 21.45, 4],
    ],
    columnNames: ['Date', 'Linux', 'Windows'],
    options: {
      colors: ['#0000ff', '#ff0000'],
      animation: {
        duration: 250,
        easing: 'ease-in-out',
        startup: true
      }
    }
  };

  chart2 = {
    title: 'SecondSample',
    type: 'LineChart',    
    data: [
      ['01/01/2019', 12.94, 25, 10],
      ['02/01/2019', 10.49, 13, 2],
      ['03/01/2019', 19.30, 13, 30],
      ['04/01/2019', 21.45, 4, 12.1],
    ],
    columnNames: ['Date', 'Linux', 'Windows', 'X'],

    options: {
      colors: ['#0000ff', '#ff0000', '#cc66ff'],
      lineWidth: 3,      
      legend: { position: 'bottom' },
      curveType: 'function',
      displayRangeSelector: true,
      series: {
        0: { type: 'line' },
        1: { type: 'line' },
        2: { type: 'line', lineWidth: 1 }
      },
      animation: {
        duration: 250,
        easing: 'ease-in-out',
        startup: true
      },
      hAxis: {
        title: 'Date',
      },
      vAxis: {
        title: 'Time',
        },
        pointSize: 5,
    }
  };

    chart3 = {
        type: 'Table'
    };
      ngOnInit() {
      }

}
