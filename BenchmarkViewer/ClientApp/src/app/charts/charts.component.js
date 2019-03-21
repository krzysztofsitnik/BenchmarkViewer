"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
exports.__esModule = true;
var core_1 = require("@angular/core");
var ChartsComponent = /** @class */ (function () {
    function ChartsComponent(http, baseUrl) {
        var _this = this;
        this.chart1 = {
            title: 'FirstSample',
            type: 'LineChart',
            data1: [],
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
        http.get(baseUrl + 'api/Benchmarks/Get').subscribe(function (result) {
            _this.measurements = result;
        }, function (error) { return console.error(error); });
    }
    console.log("a");
    ChartsComponent.prototype.ngOnInit = function () {
    };
    ChartsComponent = __decorate([
        core_1.Component({
            selector: 'app-charts',
            templateUrl: './charts.component.html',
            styleUrls: ['./charts.component.css']
        }),
        __param(1, core_1.Inject('BASE_URL'))
    ], ChartsComponent);
    return ChartsComponent;
}());
exports.ChartsComponent = ChartsComponent;
